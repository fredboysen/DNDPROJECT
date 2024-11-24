using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookTradingHub.WebAPI.Models;

namespace BookTradingHub.Database.Data

{

public class ApplicationDB : DbContext
{
public ApplicationDB(DbContextOptions dbContextOptions)
: base(dbContextOptions)
{

}

public DbSet<Book> Books{get; set;}



}


}