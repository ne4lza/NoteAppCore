namespace NoteApp.Service.DTO.User
{
    public class AddUserDto
    {
        public string? UserName { get; set; }
        public string? UserLastName { get; set; }
        public string? UserUserName { get; set; }
        public string? UserPassword { get; set; }
        public DateTime UserCreatedDate { get; set; }
    }
}
