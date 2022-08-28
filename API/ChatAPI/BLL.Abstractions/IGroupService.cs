using Core;

namespace BLL.Abstractions
{
    public interface IGroupService
    {
        public Task<int> GetPrivateGroup(int firstId, int secondId);
    }
}
