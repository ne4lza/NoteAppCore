namespace NoteApp.Service.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserLastName { get; set; }
        public string? UserUserName { get; set; }
        public string? UserPassword { get; set; }
        public DateTime UserCreatedDate { get; set; }
    }
}
