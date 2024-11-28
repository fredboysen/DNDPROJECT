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

            // Define relationships
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.book)
                .WithMany()
                .HasForeignKey(r => r.book_id);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.user)
                .WithMany()
                .HasForeignKey(r => r.user_Id);
        }
    }
}
