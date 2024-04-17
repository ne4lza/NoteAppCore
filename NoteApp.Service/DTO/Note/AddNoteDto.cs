namespace NoteApp.Service.DTO.Note
{
    public class AddNoteDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int UserId { get; set; }
    }
}
