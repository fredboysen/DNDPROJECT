using BookTradingHub.WebAPI.Models;
using System;

namespace BookTradingHub.Database.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDB context)
        {
            context.Books.RemoveRange(context.Books);
            context.Users.RemoveRange(context.Users);
            context.Ratings.RemoveRange(context.Ratings);

            var books = new[]
            {
                new Book
                {
                    title = "Soul",
                    author = "Olivia Wilson",
                    genre = "Fiction",
                    averageRating = 4.5,
                    ImageUrl = "book1.jpg"
                },
                new Book
                {
                    title = "The Book of Art (Get Your Mind to the Abstract Art)",
                    author = "Regina Phalange",
                    genre = "Art",
                    averageRating = 4.2,
                    ImageUrl = "book2.jpg"
                },
                new Book
                {
                    title = "Educated - A Memoir",
                    author = "Tara Westover",
                    genre = "Biography",
                    averageRating = 2.7,
                    ImageUrl = "book3.jpg"
                },
                new Book
                {
                    title = "Harry Potter and the Cursed Child: Parts One and Two",
                    author = "J.K. Rowling, John Tiffany, Jack Thorne",
                    genre = "Fantasy",
                    averageRating = 4.8,
                    ImageUrl = "book4.jpg"
                },
                new Book
                {
                    title = "The Suicide Prevention Pocketbook",
                    author = "Joy Hibbins",
                    genre = "Mental Health",
                    averageRating = 3.1,
                    ImageUrl = "book5.jpg"
                },
                new Book
                {
                    title = "C# For Dummies",
                    author = "Stephen Randy Davis",
                    genre = "Programming",
                    averageRating = 1.2,
                    ImageUrl = "book6.jpg"
                }
            };

            context.Books.AddRange(books);
            context.SaveChanges();

        
            var ratings = books.Select((book, index) => new Rating
            {
                book_Id = book.book_Id,
                title = book.title,
                stars = 3 + (index % 3),
                review = $"This is an amazing book by {book.author}."
            }).ToArray();



            context.Ratings.AddRange(ratings);
            context.SaveChanges();


            var users = new[]
            {
                new User
                {
                    username = "testuser",
                    email = "testuser@example.com",
                    role = "user",
                    password = "test123"
                },
                new User
                {
                    username = "testadmin",
                    email = "testadmin@example.com",
                    role = "admin",
                    password = "test123"
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
