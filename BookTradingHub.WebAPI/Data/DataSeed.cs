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
                    isbn = "1234567890",
                    genre = "Fiction",
                    publisher = "Penguin Random House",
                    publish_Date = new DateTime(2020, 6, 1),
                    condition = "New",
                    averageRating = 4.5,
                    ImageUrl = "book1.jpg"
                },
                new Book
                {
                    title = "The Book of Art (Get Your Mind to the Abstract Art)",
                    author = "Regina Phalange",
                    isbn = "0987654321",
                    genre = "Art",
                    publisher = "Art Press",
                    publish_Date = new DateTime(2018, 11, 20),
                    condition = "Used",
                    averageRating = 4.2,
                    ImageUrl = "book2.jpg"
                },
                new Book
                {
                    title = "Educated - A Memoir",
                    author = "Tara Westover",
                    isbn = "1122334455",
                    genre = "Biography",
                    publisher = "Random House",
                    publish_Date = new DateTime(2018, 2, 20),
                    condition = "New",
                    averageRating = 2.7,
                    ImageUrl = "book3.jpg"
                },
                new Book
                {
                    title = "Harry Potter and the Cursed Child: Parts One and Two",
                    author = "J.K. Rowling, John Tiffany, Jack Thorne",
                    isbn = "6677889900",
                    genre = "Fantasy",
                    publisher = "Scholastic",
                    publish_Date = new DateTime(2016, 7, 31),
                    condition = "New",
                    averageRating = 4.8,
                    ImageUrl = "book4.jpg"
                },
                new Book
                {
                    title = "The Suicide Prevention Pocketbook",
                    author = "Joy Hibbins",
                    isbn = "1234567890",
                    genre = "Mental Health",
                    publisher = "Wellbeing Books",
                    publish_Date = new DateTime(2019, 5, 15),
                    condition = "New",
                    averageRating = 3.1,
                    ImageUrl = "book5.jpg"
                },
                new Book
                {
                    title = "C# For Dummies",
                    author = "Stephen Randy Davis",
                    isbn = "1122334455",
                    genre = "Programming",
                    publisher = "For Dummies",
                    publish_Date = new DateTime(2021, 8, 10),
                    condition = "New",
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
