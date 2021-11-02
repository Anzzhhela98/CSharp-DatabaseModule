using System;
using System.Globalization;
using System.Linq;
using System.Text;
using BookShop.Models.Enums;

namespace BookShop
{
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            // DbInitializer.ResetDatabase(db);


            //Console.WriteLine(GetBooksByAgeRestriction(db, "miNor"));
            //Console.WriteLine(GetGoldenBooks( db));
            //Console.WriteLine(GetBooksByPrice(db));
            //Console.WriteLine(GetBooksNotReleasedIn(db,2000));
            //Console.WriteLine(GetBooksByCategory(db, "horror mystery drama"));
            //Console.WriteLine(GetBooksReleasedBefore(db, "12-04-1992"));
            //Console.WriteLine(GetAuthorNamesEndingIn(db, "e"));
            //Console.WriteLine(GetBookTitlesContaining(db, "sK"));
            //Console.WriteLine(GetBookTitlesContaining(db, "R"));
            //Console.WriteLine(CountBooks(db, 12));
            //Console.WriteLine(CountCopiesByAuthor(db));
            //Console.WriteLine(GetTotalProfitByCategory(db));
            //Console.WriteLine(GetMostRecentBooks(db));
            //IncreasePrices(db);
            Console.WriteLine(RemoveBooks(db));
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var result = new StringBuilder();

            var restriction = Enum.Parse<AgeRestriction>(command, true);

            var books = context.Books
                .Where(a => a.AgeRestriction == restriction)
                .Select(t => new
                {
                    Title = t.Title
                })
                .OrderBy(t=>t.Title)
                .ToList();


            foreach (var book in books)
            {
                result.AppendLine(book.Title);
            }

            return result.ToString().TrimEnd();
        }
        public static string GetGoldenBooks(BookShopContext context)
        {
            var result = new StringBuilder();

            var books = context.Books
                .Where(x => x.EditionType == EditionType.Gold && x.Copies < 5000)
                .Select(x => new
                {
                    Title = x.Title,
                    Id = x.BookId,
                })
                .OrderBy(x => x.Id)
                .ToList();

            foreach (var book in books)
            {
                result.AppendLine(book.Title);
            }

            return result.ToString().TrimEnd();
        }
        public static string GetBooksByPrice(BookShopContext context)
        {
            var result = new StringBuilder();

            var books = context.Books
                .Where(x => x.Price > 40)
                .Select(x => new
                {
                    Price = x.Price,
                    Title = x.Title
                })
                .OrderByDescending(x => x.Price)
                .ToList();

            foreach (var book in books)
            {
                result.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return result.ToString().TrimEnd();
        }
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var result = new StringBuilder();

            var books = context.Books
                    .Where(x => x.ReleaseDate.Value.Year != year)
                    .OrderBy(x=>x.BookId)
                    .Select(x =>x.Title
                    )
                    .ToList();

            foreach (var book in books)
            {
                result.AppendLine(book);
            }

            return result.ToString().TrimEnd();
        }
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var result = new StringBuilder();
            var categories = input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToList();

            var books = context.Books
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .Select(x => new
                {
                    Title = x.Title
                })
                .OrderBy(t => t.Title)
                .ToList();

            foreach (var book in books)
            {
                result.AppendLine(book.Title);
            }

            return result.ToString().TrimEnd();
        }
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var result = new StringBuilder();

            var parsedDate = DateTime.ParseExact(date, "dd-M-yyyy", CultureInfo.InvariantCulture);
            var books = context.Books
                .Where(b => b.ReleaseDate < parsedDate)
                .OrderByDescending(d => d.ReleaseDate)
                .Select(b => new
                {
                    Title = b.Title,
                    Price = b.Price,
                    EditionType = b.EditionType,
                })
                .ToList();

            foreach (var book in books)
            {
                result.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }

            return result.ToString().TrimEnd();
        }
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var result = new StringBuilder();

            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + ' ' + a.LastName
                })
                .OrderBy(a => a.FullName)
                .ToList();

            foreach (var author in authors)
            {
                result.AppendLine(author.FullName);
            }

            return result.ToString().TrimEnd();
        }
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var result = new StringBuilder();

            var titles = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(t => new
                {
                    Title = t.Title
                })
                .OrderBy(t => t.Title)
                .ToList();


            foreach (var book in titles)
            {
                result.AppendLine(book.Title);
            }

            return result.ToString().TrimEnd();
        }
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var result = new StringBuilder();

            var books = context.Books
                .OrderBy(a => a.BookId)
                .Where(a => a.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .Select(a => new
                {
                    Title = a.Title,
                    AuthorName = a.Author.FirstName + ' ' + a.Author.LastName
                })
                .ToList();


            foreach (var book in books)
            {
                result.AppendLine($"{book.Title} ({book.AuthorName})");
            }

            return result.ToString().TrimEnd();
        }
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {

            var books = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .ToList();

            return books.Count;
        }
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var result = new StringBuilder();

            //var books = context.Books
            //    .Select(b => new
            //    {
            //        FullName = b.Author.FirstName + ' ' + b.Author.LastName,
            //        Copies = b.Copies
            //    })
            //    .OrderByDescending(b=> b.Copies)
            //    .ToList();

            var authors = context.Authors
                .Select(a => new
                {
                    FullName = a.FirstName + ' ' + a.LastName,
                    BooksCount = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(b=> b.BooksCount)
                .ToList();

            foreach (var author in authors)
            {
                result.AppendLine($"{author.FullName} - {author.BooksCount}");
            }

            return result.ToString().TrimEnd();
        }
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    Profit = c.CategoryBooks.Sum(b=>b.Book.Price * b.Book.Copies)
                })
                .OrderByDescending(p=>p.Profit)
                .ThenBy(c=>c.CategoryName)
                .ToList();

            var result = new StringBuilder();


            foreach (var category in categories)
            {
                result.AppendLine($"{category.CategoryName} ${category.Profit:F2}");
            }

            return result.ToString().TrimEnd();
        }
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var result = new StringBuilder();

            var mostRecentBooks = context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    CategoryName = c.Name,
                    BooksName = c.CategoryBooks
                        .OrderByDescending(b => b.Book.ReleaseDate)
                        .Select(b =>new
                        {
                            ReleaseDate = b.Book.ReleaseDate,
                            Title = b.Book.Title
                        })
                        .Take(3)
                        .ToList()
                }).ToList();

            foreach (var category in mostRecentBooks)
            {
                result.AppendLine($"--{category.CategoryName}");
                foreach (var book in category.BooksName)
                {
                    result.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }

            return result.ToString().TrimEnd();
        }
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            var count = books.Count();

            var booksCategories = context.BooksCategories
                .Where(b => b.Book.Copies < 4200)
                .ToList();

            context.BooksCategories.RemoveRange(booksCategories);

            context.Books.RemoveRange(books);
            context.SaveChanges();

            return count;
        }
    }
}
