using Core.BaseEntities;

namespace Core
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }

        public bool ItsPersonal { get; set; } = false;

        public List<GroupUser>? GroupUsers { get; set; }

        public List<Message>? Messages { get; set; }
    }
}
