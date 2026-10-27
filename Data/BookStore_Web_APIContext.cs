using BookStore_Web_API.Models;
using Microsoft.EntityFrameworkCore;


public class BookStore_Web_APIContext : DbContext
{
    public BookStore_Web_APIContext(DbContextOptions<BookStore_Web_APIContext> options) 
        : base(options)
    {    
    }

    public DbSet<BookModel> BookModel { get; set; }
}


//public class BookStore_Web_APIContext(DbContextOptions<BookStore_Web_APIContext> options) : DbContext(options)
//{
//    public DbSet<BookStore_Web_API.Models.BookModel> BookModel { get; set; } = default!;
//}
