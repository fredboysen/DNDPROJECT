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
                .HasOne(r => r.Book)   // Each Rating has one Book
                .WithMany()            // A Book can have many Ratings (if this relationship is correct)
                .HasForeignKey(r => r.book_Id) // The foreign key is `book_Id`
                .OnDelete(DeleteBehavior.Cascade); // Define the delete behavior (optional)
        
        modelBuilder.Entity<User>()
            .Property(u => u.username)
            .HasMaxLength(100);
        }
    }
}
