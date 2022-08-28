using Core.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class Message : BaseEntity
    {
        public string Text { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        [ForeignKey("Replied")]
        public int? RepliedId { get; set; }
        public Message? Replied { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        public int? GroupId { get; set; }
        public Group? Group { get; set; }
    }
}
