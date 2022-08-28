namespace Core.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public UserDTOToUI User { get; set; }
        public MessageDTO? Replied { get; set; }

        public int GroupId { get; set; }

        public DateTime Created { get; set; }
    }
}
