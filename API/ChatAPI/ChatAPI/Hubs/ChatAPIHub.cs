using Core.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs
{
    public class ChatAPIHub : Hub<IChatHub>
    {
    }

    public interface IChatHub
    {
        public Task UpdateMessages(IEnumerable<MessageDTO> messgaes, int groupId, int newMaxPage);
    }
}
