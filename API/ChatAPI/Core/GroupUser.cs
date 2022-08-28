using Core.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class GroupUser : BaseEntity
    {
        [ForeignKey("Group")]
        public int? GroupId { get; set; }
        public Group? Group { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
