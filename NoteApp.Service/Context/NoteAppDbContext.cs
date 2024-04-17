using Microsoft.EntityFrameworkCore;
using NoteApp.Service.Models;

namespace NoteApp.Service.Context
{
    public class NoteAppDbContext : DbContext
    {
        public NoteAppDbContext(DbContextOptions<NoteAppDbContext> options) : base(options)
        {
        }
        public DbSet<User> TBL_Users { get; set; }
        public DbSet<Note> TBL_Notes { get; set; }
    }
}
