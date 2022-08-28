using AutoMapper;
using BLL;
using BLL.Abstractions;
using ChatAPI.Hubs;
using Core;
using Core.DTOs;
using Core.Options;
using Microsoft.AspNet.SignalR;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SourceController : ControllerBase
    {
        private readonly ICRUDService<User> _userService;
        private readonly ICRUDService<Group> _groupCRUDService;
        private readonly ICRUDService<Message> _messageCRUDService;
        private readonly IMessageService _messageService;
        private readonly IGroupService _groupService;
        private readonly IHubContext<ChatAPIHub, IChatHub> _hub;
        private readonly IMapper _mapper;

        public SourceController(
            ICRUDService<User> userService,
            ICRUDService<Group> groupCRUDService,
            ICRUDService<Message> messageCRUDService,
            IMessageService messageService,
            IGroupService groupService,
            IHubContext<ChatAPIHub, IChatHub> hub,
            IMapper mapper
        )
        {
            _userService = userService;
            _groupCRUDService = groupCRUDService;
            _messageCRUDService = messageCRUDService;
            _messageService = messageService;
            _groupService = groupService;
            _hub = hub;
            _mapper = mapper;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            User me = await _userService.Get(int.Parse(User.FindFirst("Id").Value));

            UserDTOToUI result = _mapper.Map<UserDTOToUI>(me);

            return Ok(result);
        }

        [HttpGet("messages/{id}/{page}")]
        public async Task<IActionResult> GetMessages(int id, int page)
        {
            List<Message> messagesFromDb = await _messageService.GetMessagesOnThePage(id, page);

            List<MessageDTO> messages = _mapper.Map<List<MessageDTO>>(messagesFromDb);

            return Ok(messages);
        }

        [HttpGet("message/pages/{id}")]
        public async Task<IActionResult> GetNumberOfPages(int id)
        {
            int number = await _messageService.CountPages(id);

            return Ok(number);
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetGroups()
        {
            User user;
            GroupToGroupDTOConverter converter;

            int userId = int.Parse(User.FindFirst("Id").Value);

            converter = new GroupToGroupDTOConverter(userId);

            user = await _userService.Get(userId);

            List<GroupDTO> result = new List<GroupDTO>();

            foreach (GroupUser groupUser in user.GroupUsers)
            {
                result.Add(converter.Convert(groupUser.Group));
            }

            return Ok(result);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            List<User> users = await _userService.GetAll();

            return Ok(users);
        }

        [HttpPost("send/{id}")]
        public async Task<OkResult> SendMessage(MessageDTO message, int id)
        {
            await _messageCRUDService.Create(new MessageDTOToMessageConverter().Convert(message));

            return await UpdateMessagesForClients(id);
        }

        [HttpPost("edit/{id}")]
        public async Task<OkResult> EditMessage(MiniMessageDTO message, int id)
        {
            Message dbMessage = await _messageCRUDService.Get(message.Id);

            dbMessage.Text = message.Text;

            await _messageCRUDService.Edit(dbMessage);

            return await UpdateMessagesForClients(id);
        }

        [HttpGet("delete/{groupId}/{messageId}")]
        public async Task<OkResult> DeleteMessage(int messageId, int groupId)
        {
            await _messageCRUDService.Delete(messageId);

            return await UpdateMessagesForClients(groupId);
        }

        [HttpGet("private/{userId}")]
        public async Task<OkObjectResult> GetPrivateGroupWith(int userId)
        {
            int id = int.Parse(User.FindFirst("Id").Value);

            int groupId = await _groupService.GetPrivateGroup(id, userId);

            return Ok(groupId);
        }

        private async Task<OkResult> UpdateMessagesForClients(int groupId)
        {
            int pages = await _messageService.CountPages(groupId);

            List<Message> messagesFromDb = await _messageService.GetMessagesOnThePage(groupId, pages);

            List<MessageDTO> messages = _mapper.Map<List<MessageDTO>>(messagesFromDb);

            await _hub.Clients.All.UpdateMessages(messages, groupId, pages);

            return Ok();
        }
    }
}
