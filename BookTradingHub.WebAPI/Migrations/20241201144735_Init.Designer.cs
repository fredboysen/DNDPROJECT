﻿// <auto-generated />
using System;
using BookTradingHub.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookTradingHub.WebAPI.Migrations
{
    [DbContext(typeof(ApplicationDB))]
    [Migration("20241201144735_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("BookTradingHub.WebAPI.Models.Book", b =>
                {
                    b.Property<int>("book_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("averageRating")
                        .HasColumnType("REAL");

                    b.Property<string>("condition")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("genre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("isbn")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("publish_Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("publisher")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("book_Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookTradingHub.WebAPI.Models.Rating", b =>
                {
                    b.Property<int>("rating_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("book_Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("book_Id1")
                        .HasColumnType("INTEGER");

                    b.Property<string>("review")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("stars")
                        .HasColumnType("INTEGER");

                    b.Property<string>("title")
                        .HasColumnType("TEXT");

                    b.HasKey("rating_id");

                    b.HasIndex("book_Id");

                    b.HasIndex("book_Id1");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("BookTradingHub.WebAPI.Models.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("user_id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BookTradingHub.WebAPI.Models.Rating", b =>
                {
                    b.HasOne("BookTradingHub.WebAPI.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("book_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookTradingHub.WebAPI.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("book_Id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });
#pragma warning restore 612, 618
        }
    }
}
