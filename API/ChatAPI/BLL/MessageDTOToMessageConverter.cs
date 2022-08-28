using Core;
using Core.DTOs;

namespace BLL
{
    public class MessageDTOToMessageConverter
    {
        public Message Convert(MessageDTO source)
        {
            return new Message
            {
                Id = source.Id,
                Text = source.Text,
                Created = source.Created,
                RepliedId = source.Replied?.Id,
                UserId = source.User.Id,
                GroupId = source.GroupId
            };
        }
    }
}
