using Microsoft.EntityFrameworkCore;
using LibraryManagementModels;
namespace LibraryManagementAPI.Database
{
    public class LibraryContext : DbContext
    {

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) 
        {
            
        }

        public DbSet<BookModel> Books { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<TransactionModel> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed books
            modelBuilder.Entity<BookModel>().HasData( new BookModel 
            {
                BookId = 1,
                ISBN = "9780132350884",
                Title = "Clean Code",
                Genre = "Programming",
                Description = "A Handbook of Agile Software Craftsmanship.",
                Author = "Robert C. Martin"
            }, 
            new BookModel 
            {
                BookId = 2,
                ISBN = "9780321127426",
                Title = "Domain-Driven Design",
                Genre = "Software Development",
                Description = "Tackling Complexity in the Heart of Software.",
                Author = "Eric Evans"
            },
            new BookModel
            {
                BookId = 3,
                ISBN = "9780596007126",
                Title = "Head First Design Patterns",
                Genre = "Design Patterns",
                Description = "A brain-friendly guide to design patterns.",
                Author = "Eric Freeman & Elisabeth Robson"
            },
            new BookModel
            {
                BookId = 4,
                ISBN = "9780134494166",
                Title = "The Pragmatic Programmer",
                Genre = "Programming",
                Description = "Your Journey to Mastery, 20th Anniversary Edition.",
                Author = "Andrew Hunt & David Thomas"
            });

            modelBuilder.Entity<UserModel>().HasData( new UserModel
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe12345678",
                PasswordHash = "5f4dcc3b5aa765d61d8327deb882cf99", // Example MD5 hash for "password"
                Email = "johndoe@example.com",
                PhoneNumber = "09122222222"
            },
            new UserModel
            {
                FirstName = "Jane",
                LastName = "Smith",
                UserName = "janesmith87654321",
                PasswordHash = "e99a18c428cb38d5f260853678922e03", // Example MD5 hash for "abc123"
                Email = "janesmith@example.com",
                PhoneNumber = "09123444444"
            },
            new UserModel
            {
                FirstName = "Michael",
                LastName = "Johnson",
                UserName = "michaeljohnson1212",
                PasswordHash = "d8578edf8458ce06fbc5bb76a58c5ca4", // Example MD5 hash for "qwerty"
                Email = "michaeljohnson@example.com",
                PhoneNumber = "09912312322"
            },
            new UserModel
            {
                FirstName = "Emily",
                LastName = "Davis",
                UserName = "emilydavis24681357",
                PasswordHash = "25f9e794323b453885f5181f1b624d0b", // Example MD5 hash for "12345678"
                Email = "emilydavis@example.com",
                PhoneNumber = "09123543211"
            });
        }
    }
}
