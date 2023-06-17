using ChatAppSoftUni.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagerConsole.Infrastructure.Data
{
    public class ChatAppContext : DbContext
    {
        public ChatAppContext()
        {
        }
        public ChatAppContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Message> Messages { get; set; }
        //public DbSet<Book> Books { get; set; }
        //public DbSet<Author> Authors { get; set; }
        //public DbSet<Genre> Genres { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=ChatApp;User Id=sa;Password=None124578;MultipleActiveResultSets=true;TrustServerCertificate=True;");
            }
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
