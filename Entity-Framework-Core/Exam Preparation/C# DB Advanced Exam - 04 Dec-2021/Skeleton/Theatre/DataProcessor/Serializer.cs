namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using Theatre.Data;
    using Theatre.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var theatres = context.Theatres
                .ToList()
                .Where(x => x.Tickets.Count > 20 &&  x.NumberOfHalls >= numbersOfHalls)
                .Select(x => new
                {
                    Name= x.Name,
                    Halls= x.NumberOfHalls,
                    TotalIncome = x.Tickets.Where(t => t.RowNumber >=1 && t. RowNumber <= 5).Sum(p => p.Price),
                    Tickets = x.Tickets.Where(t => t.RowNumber >= 1  && t. RowNumber <= 5 )
                .Select(t => new
                {

                    Price = t.Price,
                    RowNumber = t.RowNumber
                })
                .OrderByDescending(t => t.Price)
                .ToList()
                }).OrderByDescending(x=>x.Halls)
                .ThenBy(x=>x.Name)
                .ToArray();

            var serializer  = JsonConvert.SerializeObject(theatres, Formatting.Indented);

            return serializer;
        }

        // The given method in the project’s skeleton receives a number representing the number of halls.
        // Export all theaters where the hall's count is bigger or equal to the given and have 20 or more tickets available.
        //  For each theater, export its Name, Halls, TotalIncome of tickets which are between the first and fifth row inclusively, and Tickets.
        //      For each ticket (between first and fifth row inclusively), export its price, and the row number. Order the theaters by the number of halls descending, then by name (ascending).
        //      Order the tickets by price descending.
        //NOTE: If you receive correct output when running the query locally, but judge gives an error, materialize the query (.ToArray(), .ToList() etc.) before the.Where() statement.


        public static string ExportPlays(TheatreContext context, double rating)
        {
            var plays = context.Plays
                .ToArray()
                .Where(x => x.Rating <= rating)
                .Select(x=> new ExportPlayDto()
                {
                    Title = x.Title,
                    Duration = x.Duration.ToString("c"),
                    Rating = x.Rating == 0 ? "Premier" : x.Rating.ToString(),
                    Genre = x.Genre.ToString(),
                    Actors = x.Casts
                    .ToArray()
                    .Where(a => a.IsMainCharacter)
                    .Select( a =>  new ExportCastDto
                    {
                        FullName = a.FullName,
                        MainCharacter =  $"Plays main character in '{x.Title}'."

                    }).OrderByDescending(a=>a.FullName)
                    .ToArray()

                }).OrderBy(x=>x.Title).ThenByDescending(x=>x.Genre)
                .ToArray();

            var xmlResult = XmlConverter.Serialize(plays, "Plays");


            return xmlResult;
        }
    }
}
