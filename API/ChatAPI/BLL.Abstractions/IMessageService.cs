using Core;

namespace BLL.Abstractions
{
    public interface IMessageService
    {
        public Task<List<Message>> GetMessagesOnThePage(int groupId, int page);
        public Task<int> CountPages(int groupId);
    }
}
