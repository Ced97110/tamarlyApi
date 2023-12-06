using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Entity;
using Microsoft.AspNetCore.Identity;


namespace API.Data
{
    public static class DbInitializer 
    {
        public static async Task Initialize(StoreContext context, UserManager<User> userManager)
        {

            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    UserName = "bob",
                    Email = "bob@test.com"
                };


                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");

                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };

                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRolesAsync(admin, new[] { "Member", "Admin" });
            }




            if (context.Books.Any()) return;


                var books = new List<Book>
                {
                    new Book
                    {
                        Title = "Asp.Net Core",
                        Author = "Andrew Lock",
                        Description = "ASP.NET Core in Action, Second Edition is a comprehensive guide to creating web applications with ASP.NET Core 5.0.",
                        Price = 34,
                        ImageUrl = "/images/books/andrew-lock.png",
                        Category = "Web Development",
                        Language = "french",
                        Rating = "4",
                        Publisher = "Manning Publications",
                        PageCount = 400,
                        Review = "A practical guide to ASP.NET Core.",
                        FileSize = 10,
                        FileFormat = "PDF",
                        PublicationDate = new DateTime(2014, 7, 29).ToUniversalTime()
                    },
                    new Book
                    {
                        Title = "C# in a Nutshell",
                        Author = "Joseph Albahari",
                        Description = "Organized around concepts and use cases, this comprehensive and complete reference provides intermediate and advanced programmers with a concise map of C# and .NET.",
                        Price = 44,
                        ImageUrl = "/images/books/csharp.jpg",
                        Category = "Programming",
                        Language = "french",
                        Rating = "5",
                        Publisher = "O'Reilly Media",
                        PageCount = 1016,
                        Review = "A must-have reference for C# developers.",
                        FileSize = 12,
                        FileFormat = "PDF",
                        PublicationDate = new DateTime(2020, 4, 13).ToUniversalTime()
                    },
                    new Book
                    {
                        Title = "Python Algorithms",
                        Author = "Magnus Lie Hetland",
                        Description = "Mastering basic algorithms in the Python language.",
                        Price = 46,
                        ImageUrl = "/images/books/python-algorithms.jpeg",
                        Category = "Programming",
                        Language = "english",
                        Rating = "2",
                        Publisher = "Apress",
                        PageCount = 400,
                        Review = "A comprehensive guide to Python algorithms.",
                        FileSize = 8,
                        FileFormat = "PDF",
                        PublicationDate = new DateTime(2023, 10, 23).ToUniversalTime()
                    },
                    new Book
                    {
                        Title = "Internet Infrastructure",
                        Author = "Richard Fox",
                        Description = "Networking, web services, and cloud computing.",
                        Price = 12,
                        ImageUrl = "/images/books/internet-infrastructure.jpg",
                        Category = "Networking",
                        Language = "english",
                        Rating = "3",
                        Publisher = "O'Reilly Media",
                        PageCount = 320,
                        Review = "An introduction to internet infrastructure.",
                        FileSize = 5,
                        FileFormat = "PDF",
                        PublicationDate = new DateTime(2018, 1, 23).ToUniversalTime()
                    },
                    new Book
                    {
                        Title = "Practical SQL",
                        Author = "Anthony Debaros",
                        Description = "A beginner's guide to storytelling with data.",
                        Price = 22,
                        ImageUrl = "/images/books/practical-sql.jpg",
                        Category = "Technology",
                        Language = "english",
                        Rating = "1",
                        Publisher = "No Starch Press",
                        PageCount = 256,
                        Review = "Great book for SQL beginners.",
                        FileSize = 3,
                        FileFormat = "PDF",
                        PublicationDate = new DateTime(2019, 1, 25).ToUniversalTime()
                    },
                    new Book
                    {
                        Title = "Learning Python",
                        Author = "Mark Lutz",
                        Description = "Powerful Object-Oriented Programming",
                        Price = 62,
                        ImageUrl = "/images/books/learn-python.jpg",
                        Category = "Religion",
                        Language = "english",
                        Rating = "4",
                        Publisher = "O'Reilly Media",
                        PageCount = 1600,
                        Review = "An in-depth guide to Python programming.",
                        FileSize = 15,
                        FileFormat = "PDF",
                        PublicationDate = new DateTime(2017, 5, 14).ToUniversalTime()
                    },
                    new Book
                    {
                        Title = "The Full Stack Developer",
                        Author = "Chris Northwood",
                        Description = "Your essential guide to the everyday skills expected of a modern full-stack web developer.",
                        Price = 120,
                        ImageUrl = "/images/books/full-stack.jpg",
                        Category = "Media",
                        Language = "french",
                        Rating = "4",
                        Publisher = "No Starch Press",
                        PageCount = 450,
                        Review = "A comprehensive guide to full-stack development.",
                        FileSize = 18,
                        FileFormat = "PDF",
                        PublicationDate = new DateTime(2016, 3, 13).ToUniversalTime()
                    },
                    new Book
                    {
                        Title = "JavaScript",
                        Author = "David Flanagan",
                        Description = "The definitive guide to JavaScript.",
                        Price = 53,
                        ImageUrl = "/images/books/javascript.jpg",
                        Category = "Programming",
                        Language = "english",
                        Rating = "4",
                        Publisher = "O'Reilly Media",
                        PageCount = 700,
                        Review = "An excellent reference for JavaScript.",
                        FileSize = 11,
                        FileFormat = "PDF",
                        PublicationDate = new DateTime(2010, 8, 3).ToUniversalTime()
                    },
                    new Book
                    {
                        Title = "Bootstrap",
                        Author = "Jake Spurlock",
                        Description = "The definitive guide to designing killer interfaces and responsive websites with the Bootstrap framework.",
                        Price = 123,
                        ImageUrl = "/images/books/bootstrap.jpg",
                        Category = "Web Design",
                        Language = "english",
                        Rating = "2",
                        Publisher = "Packt Publishing",
                        PageCount = 300,
                        Review = "A practical guide to Bootstrap.",
                        FileSize = 9,
                        FileFormat = "PDF",
                        PublicationDate = new DateTime(2013, 7, 12).ToUniversalTime()
                    },
                    new Book
                    {
                        Title = "The Road to Learn React",
                        Author = "Robin Wieruch",
                        Description = "The definitive guide to learn React library.",
                        Price = 14,
                        ImageUrl = "/images/books/road-to-learn-react.jpg",
                        Category = "Web Development",
                        Language = "spanish",
                        Rating = "4",
                        Publisher = "Leanpub",
                        PageCount = 220,
                        Review = "A beginner-friendly guide to React.",
                        FileSize = 6,
                        FileFormat = "PDF",
                        PublicationDate = new DateTime(2019, 6, 23).ToUniversalTime()
                    },
                    new Book
                    {
                        Title = "Dive into Algorithms",
                        Author = "Bradford Tuckfield",
                        Description = "A Pythonic adventure for the intrepid beginner.",
                        Price = 36,
                        ImageUrl = "/images/books/dive-into.jpg",
                        Category = "Programming",
                        Language = "spanish",
                        Rating = "3",
                        Publisher = "O'Reilly Media",
                        PageCount = 360,
                        Review = "An introduction to algorithms with Python.",
                        FileSize = 7,
                        FileFormat = "PDF",
                        PublicationDate = new DateTime(2022, 10, 29).ToUniversalTime()
                    },
                    new Book
                    {
                        Title = "Essential Computer Science",
                        Author = "Paul D. Crutcher",
                        Description = "A programmer's guide to foundational concepts.",
                        Price = 41,
                        ImageUrl = "/images/books/computer-science.jpg",
                        Category = "Computer Science",
                        Language = "spanish",
                        Rating = "4",
                        Publisher = "Apress",
                        PageCount = 280,
                        Review = "A comprehensive guide to computer science concepts.",
                        FileSize = 5,
                        FileFormat = "PDF",
                        PublicationDate = new DateTime(2014, 1, 10).ToUniversalTime()
                    },
                    new Book
                    {
                        Title = "System Design Interview",
                        Author = "Alex Xu",
                        Description = "A programmer's guide to system design.",
                        Price = 29,
                        ImageUrl = "/images/books/system-design-interview.jpg",
                        Category = "Software Engineering",
                        Language = "french",
                        Rating = "2",
                        Publisher = "Pramp",
                        PageCount = 240,
                        Review = "A helpful resource for system design interviews.",
                        FileSize = 4,
                        FileFormat = "eBook",
                        PublicationDate = new DateTime(2023, 4, 13).ToUniversalTime()
                    },
                    new Book
                    {
                        Title = "The Future of the Music Industry: How digital technology is changing the music industry",
                        Author = "Peters Point",
                        Description = "Are you ready to embark on an eye-opening journey into the future of the music industry?",
                        Price = 49,
                        ImageUrl = "/images/books/tamarly.jpg",
                        Category = "Music",
                        Language = "english",
                        Rating = "4",
                        Publisher = "Tamarly Publishing",
                        PageCount = 109,
                        Review = "A helpful resource for the future of the music industry.",
                        FileSize = 16,
                        FileFormat = "eBook",
                        PublicationDate = new DateTime(2023, 6, 15).ToUniversalTime()
                    },
                };




            foreach (var book in books)
                {
                 context.Books.Add(book);
                }

                 context.SaveChanges();
         
        }
    }
}




