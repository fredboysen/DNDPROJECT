using Microsoft.EntityFrameworkCore;
using BookTradingHub.WebAPI.Models;

namespace BookTradingHub.Database.Data
{
    public class ApplicationDB : DbContext
    {
        public ApplicationDB(DbContextOptions<ApplicationDB> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        // Define the relationship using the foreign key 'book_id'
            modelBuilder.Entity<Rating>()
                .HasOne<Book>()  // We're referencing the Book entity
                .WithMany()  // A book can have many ratings
                .HasForeignKey(r => r.book_id)  // Foreign key on Rating
                .OnDelete(DeleteBehavior.Cascade);
    
        modelBuilder.Entity<User>()
            .Property(u => u.username)
            .HasMaxLength(100);
        }
    }
}
