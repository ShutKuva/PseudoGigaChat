using Core.BaseEntities;

namespace Core
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public List<GroupUser>? GroupUsers { get; set; }

        public List<Message>? Messages { get; set; }
    }
}
