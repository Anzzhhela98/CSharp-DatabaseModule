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
    }
}
