using BLL.Abstractions;
using Core;
using DAL.Abstractions;

namespace BLL
{
    public class GroupService : IGroupService
    {
        private readonly ICRUDService<Group> _groupService;

        public GroupService(ICRUDService<Group> groupService)
        {
            _groupService = groupService;
        }

        public async Task<int> GetPrivateGroup(int firstId, int secondId)
        {
            List<Group> groups = await _groupService.GetByCondition(g => g.ItsPersonal);

            Group group = groups.FirstOrDefault(g => (g.GroupUsers?.Exists(gu => gu.UserId == firstId) ?? false) && (g.GroupUsers?.Exists(gu => gu.UserId == secondId) ?? false));

            if (group != null)
            {
                return group.Id;
            }

            await _groupService.Create(new Group
            {
                Name = "",
                ItsPersonal = true
            });

            IEnumerable<Group> emptyGroups = await _groupService.GetByCondition(group => group.Name == "" && group.ItsPersonal && group.GroupUsers.Count == 0);

            Group firstGroup = emptyGroups.First();

            firstGroup.GroupUsers = new List<GroupUser>() { 
                new GroupUser{ GroupId = firstGroup.Id, UserId = firstId },
                new GroupUser{ GroupId = firstGroup.Id, UserId = secondId }
            };

            await _groupService.Edit(firstGroup);

            return firstGroup.Id;
        }
    }
}
