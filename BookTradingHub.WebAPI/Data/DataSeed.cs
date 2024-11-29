using BookTradingHub.WebAPI.Models;  // Adjust to your namespace
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookTradingHub.Database.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDB context)
        {
            // Remove all existing books
            context.Books.RemoveRange(context.Books);

            // Add new sample data
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

            // Add new books
            context.Books.AddRange(books);

            // Save changes to the database
            context.SaveChanges();
        }
    }
}
