using Core;
using Core.DTOs;

namespace BLL
{
    public class GroupToGroupDTOConverter
    {
        private readonly int _userId;

        public GroupToGroupDTOConverter(int userId)
        {
            _userId = userId;
        }

        public GroupDTO Convert(Group group)
        {
            if (!group.ItsPersonal)
            {
                return new GroupDTO() { Id = group.Id, Name = group.Name };
            }

            return new GroupDTO() { Id = group.Id, Name = group.GroupUsers.First(gu => {
                return gu.UserId != _userId; 
            }).User.Name };
        }
    }
}
