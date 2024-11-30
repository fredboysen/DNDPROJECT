using BookTradingHub.WebAPI.Models; 

namespace BookTradingHub.Database.Data

{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDB context)
        {
            // Remove existing data
            context.Books.RemoveRange(context.Books);
            context.Users.RemoveRange(context.Users); // Remove all users (for seeding)

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
                    averageRating = 4.7,
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
                }
            };

            context.Books.AddRange(books);

            // Seed new users
            var users = new[]
            {
                new User
                {
                    username = "testuser",
                    email = "testuser@example.com",
                    role = "user",
                    password = "test123"  // Make sure password is stored as plain text or use a hash
                },
                new User
                {
                    username = "testadmin",
                    email = "testadmin@example.com",
                    role = "admin",
                    password = "test123"  // Same note as above for password handling
                }
            };

            context.Users.AddRange(users);

            // Save changes to database
            context.SaveChanges();
        }
    }
}

