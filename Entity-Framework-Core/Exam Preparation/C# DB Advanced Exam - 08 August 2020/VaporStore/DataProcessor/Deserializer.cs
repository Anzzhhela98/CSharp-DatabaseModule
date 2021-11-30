namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.Dto.Import;

    public static class Deserializer
    {
        private const string ErrorMessage = "Invalid Data";
        private const string SuccessfullyAddedGame = "Added {0} ({1}) with {2} tags";

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var serialize =  JsonConvert.DeserializeObject<List<ImportGameDto>>(jsonString);
            var sb = new StringBuilder();

            List<Game> allNewgames = new List<Game>();

            List<Developer> developers = new List<Developer>();
            List<Genre> genres = new List<Genre>();
            List<Tag> tags = new List<Tag>();

            foreach (var currGame in serialize)
            {
                if (!IsValid(currGame))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime releaseDate;
                bool isDateValid = DateTime.TryParseExact(currGame.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out releaseDate);

                if (!isDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var Newgame = new Game()
                {
                    Name= currGame.Name,
                    Price= currGame.Price,
                    ReleaseDate = releaseDate
                };

                var dev = developers.FirstOrDefault(d => d.Name == currGame.Developer);

                if (dev == null)
                {
                    dev = new Developer()
                    {
                        Name = currGame.Developer
                    };

                    developers.Add(dev);
                }

                Newgame.Developer = dev;

                var gen = genres.FirstOrDefault(d => d.Name == currGame.Genre);

                if (gen == null)
                {
                    gen = new Genre()
                    {
                        Name = currGame.Genre
                    };

                    genres.Add(gen);
                }

                Newgame.Genre = gen;

                foreach (var currTag in currGame.Tags)
                {
                    if (string.IsNullOrEmpty(currTag))
                    {
                        continue;
                    }

                    var NewTag = tags.FirstOrDefault(d => d.Name == currTag);

                    if (NewTag == null)
                    {
                        NewTag = new Tag()
                        {
                            Name = currTag
                        };

                        tags.Add(NewTag);
                    }

                    Newgame.GameTags.Add(new GameTag()
                    {
                        Game = Newgame,
                        Tag = NewTag,
                    });

                }

                if (Newgame.GameTags.Count == 0)
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                allNewgames.Add(Newgame);
                sb.AppendLine(string.Format(SuccessfullyAddedGame, Newgame.Name, Newgame.Genre.Name, Newgame.GameTags.Count));
            }

            context.Games.AddRange(allNewgames);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        //•	If any validation errors occur(such as if a Price is negative, a Name/ReleaseDate/Developer/Genre is missing, Tags are missing or empty), do not import any part of the entity and append an error message to the method output.
        //•	Dates are always in the format “yyyy-MM-dd”. Do not forget to use CultureInfo.InvariantCulture!
        //•	If a developer/genre/tag with that name doesn’t exist, create it. 

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}