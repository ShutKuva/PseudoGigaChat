using BLL.Abstractions;
using Core;
using Core.Options;
using Microsoft.Extensions.Options;

namespace BLL
{
    public class MessageService : IMessageService
    {
        private readonly ICRUDService<Group> _groupService;
        private readonly ICRUDService<Message> _messageService;
        private readonly GlobalSettings _globalSettings;

        public MessageService(
            ICRUDService<Group> groupService,
            ICRUDService<Message> messageService,
            IOptions<GlobalSettings> settings
        )
        {
            _groupService = groupService;
            _messageService = messageService;
            _globalSettings = settings.Value;
        }

        public async Task<List<Message>> GetMessagesOnThePage(int groupId, int page)
        {
            Group group = await _groupService.Get(groupId);

            if (page == 0)
            {
                return new List<Message>();
            }

            List<Message> messages = await _messageService.GetNumberOf(page * _globalSettings.NumberOfMessagesInOnePage, page * _globalSettings.NumberOfMessagesInOnePage - _globalSettings.NumberOfMessagesInOnePage, m => m.GroupId == groupId, (x, y) => x.Created.CompareTo(y.Created));

            return messages;
        }

        public async Task<int> CountPages(int groupId)
        {
            int number = await _messageService.Count(m => m.GroupId == groupId);

            return (int)Math.Ceiling((double)number / 20);
        }
    }
}
