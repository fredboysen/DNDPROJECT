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

        
           modelBuilder.Entity<Rating>()
                .HasOne(r => r.Book)  
                .WithMany()           
                .HasForeignKey(r => r.book_Id) 
                .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .Property(u => u.username)
            .HasMaxLength(100);
        }
    }
}
