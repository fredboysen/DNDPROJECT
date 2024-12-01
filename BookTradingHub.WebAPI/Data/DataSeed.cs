using BookTradingHub.WebAPI.Models; 
using System;

namespace BookTradingHub.Database.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDB context)
        {
            // Remove existing data
            context.Books.RemoveRange(context.Books);
            context.Users.RemoveRange(context.Users); // Remove all users (for seeding)
            context.Ratings.RemoveRange(context.Ratings); // Remove all ratings (for seeding)

            // Seed new books
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

            // Add books and save
            context.Books.AddRange(books);
            context.SaveChanges(); // Ensure books are inserted and book_ids are generated

            // Seed new ratings
            var ratings = new[]
            {
                new Rating
                {
                    book_Id = books[0].book_Id, // Book 1
                    title = "Amazing Read!",
                    stars = 5,
                    review = "I absolutely loved this book. It's so inspiring!"
                },
                new Rating
                {
                    book_Id = books[1].book_Id, // Book 2
                    title = "Very informative",
                    stars = 4,
                    review = "The book provides insightful information, but could be more engaging."
                },
                new Rating
                {
                    book_Id = books[2].book_Id, // Book 3
                    title = "Great memoir",
                    stars = 4,
                    review = "A very emotional and thought-provoking memoir."
                },
                new Rating
                {
                    book_Id = books[3].book_Id, // Book 4
                    title = "Loved the fantasy elements",
                    stars = 5,
                    review = "The fantasy story was captivating, I couldn't put it down!"
                },
                new Rating
                {
                    book_Id = books[4].book_Id, // Book 5
                    title = "Helpful",
                    stars = 3,
                    review = "An okay book, helpful but not as detailed as I expected."
                },
                new Rating
                {
                    book_Id = books[5].book_Id, // Book 6
                    title = "Not for beginners",
                    stars = 2,
                    review = "I found this book hard to follow and not beginner-friendly."
                }
            };

            // Add ratings and save
            context.Ratings.AddRange(ratings);
            context.SaveChanges(); // Ensure ratings are inserted after books are inserted

            // Seed new users
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

            // Add users and save
            context.Users.AddRange(users);
            context.SaveChanges(); // Save users data
        }
    }
}
