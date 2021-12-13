namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.Data.Models.Enums;
    using Theatre.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";

        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            var serialize =  XmlConverter.Deserializer<ImportPlayDto>(xmlString, "Plays");

            var sb = new StringBuilder();

            var plays =  new List<Play>();

            foreach (var currPlay in serialize)
            {
                if (!IsValid(currPlay))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                };

                TimeSpan duration;
                bool isTimeSpanValid =
                    TimeSpan.TryParseExact(currPlay.Duration, "c", CultureInfo.InvariantCulture, TimeSpanStyles.None, out duration);

                if (!isTimeSpanValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                };

                if (duration.TotalHours < 1)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                };


                Genre  genre;
                var isValidGenre = Enum.TryParse<Genre>(currPlay.Genre, out genre);

                if (!isValidGenre)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var play = new Play()
                {
                    Title = currPlay.Title,
                    Duration = duration,
                    Rating = currPlay.Rating,
                    Genre = genre,
                    Description = currPlay.Description,
                    Screenwriter = currPlay.Screenwriter,
                };

                plays.Add(play);
                sb.AppendLine(string.Format(SuccessfulImportPlay, play.Title, genre, play.Rating));
            }

            context.Plays.AddRange(plays);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
        //        •	If any validation errors occur such as:
        //•	Invalid: title/genre/rating/description/screenwriter
        //•	Duration of the play is less than 1(one) hour
        //Do not import any part of the entity and append an error message "Invalid data!" to the method output.
        //•	Durations will always be in the format "c". Do not forget to use CultureInfo.InvariantCulture!



        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            var serialize =  XmlConverter.Deserializer<importCastDto>(xmlString, "Casts");

            var casts =  new List<Cast>();
            var sb = new StringBuilder();

            foreach (var currCast in serialize)
            {
                if (!IsValid(currCast))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var isMain = currCast.IsMainCharacter ? "main" : "lesser";

                var cast =  new Cast()
                {
                    FullName = currCast.FullName,
                    IsMainCharacter = currCast.IsMainCharacter,
                    PhoneNumber = currCast.PhoneNumber,
                    PlayId = currCast.PlayId,
                };

                casts.Add(cast);
                sb.AppendLine(string.Format(SuccessfulImportActor, cast.FullName, isMain));
            }

            context.Casts.AddRange(casts);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
        //•	If any validation errors occur such as:
        //•	Invalid: full name/phone number
        //Do not import any part of the entity and append an error message "Invalid data!" to the method output.PlayId will be always valid.



        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serialize = JsonConvert.DeserializeObject<List<ImportTheatreDto>>(jsonString);

            var theaters =  new List<Theatre>();

            foreach (var currTheater in serialize)
            {
                //var validPlays = context.Plays.Select(x => x.Id).ToList();
                //var hasInvalidPlay = false;

                if (!IsValid(currTheater))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                };

                var theater = new Theatre()
                {
                    Name = currTheater.Name,
                    NumberOfHalls = currTheater.NumberOfHalls,
                    Director = currTheater.Director,
                };

                foreach (var currTicket in currTheater.Tickets)
                {
                    if (!IsValid(currTicket))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    //if (!validPlays.Contains(currTicket.PlayId))
                    //{
                    //    hasInvalidPlay = true;
                    //    break;
                    //}

                    var ticket =  new Ticket()
                    {
                        Price = currTicket.Price,
                        RowNumber = currTicket.RowNumber,
                        PlayId = currTicket.PlayId,
                    };

                    theater.Tickets.Add(ticket);
                }

                //if (hasInvalidPlay)
                //{
                //    continue;
                //}

                theaters.Add(theater);
                sb.AppendLine(string.Format(SuccessfulImportTheatre, theater.Name, theater.Tickets.Count));
            }

            Console.WriteLine(context.Theatres.Count());
            context.Theatres.AddRange(theaters);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        //•	If there are any validation errors(such as invalid play),
        //do not import any part of the entity and append an error message to the method output.
        //•	If there are any Ticket validation errors, do not import the invalid ticket, print "Invalid data!", and continue to the next ticket.



        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
