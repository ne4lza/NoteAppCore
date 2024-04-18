using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Service.Context;
using NoteApp.Service.DTO.Note;
using NoteApp.Service.Models;

namespace NoteApp.Service.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteAppDbContext _context;

        public NoteController(NoteAppDbContext context)
        {
            _context = context;
        }

        [HttpGet("List/{notesId}")]
        public ActionResult<List<Note>> GetNotes(int notesId)
        {
            var query = _context.TBL_Notes.Where(x => x.UserId == notesId).ToList().OrderByDescending(x=>x.Id);
            return Ok(query);
        }
        [HttpGet("{noteId}")]
        public ActionResult GetNotesById(int noteId)
        {
            var query = _context.TBL_Notes.Where(x => x.Id == noteId).FirstOrDefault();
            return Ok(query);
        }
        [HttpPost("AddNote")]
        public ActionResult AddNote(AddNoteDto addNoteDto)
        {
            Note note = new Note()
            {
                Content = addNoteDto.Content,
                Title = addNoteDto.Title,
                UserId = addNoteDto.UserId,
                CreatedDate = DateTime.Now,

            };
            _context.TBL_Notes.Add(note);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("UpdateNote")]
        public ActionResult UpdateNote(UpdateNoteDTO updateNoteDTO)
        {
            Note note = new Note
            {
                Id = updateNoteDTO.Id,
                Content = updateNoteDTO.Content,
                Title = updateNoteDTO.Title,
                UserId = updateNoteDTO.UserId,
                CreatedDate = updateNoteDTO.CreatedDate

            };
            _context.TBL_Notes.Update(note);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("RemoveNote/{id}")]
        public ActionResult RemoveNote(int id)
        {
            var note = _context.TBL_Notes.Where(x => x.Id == id).FirstOrDefault();
             _context.TBL_Notes.Remove(note);
            _context.SaveChanges();
            return Ok();
        }
    }
}
