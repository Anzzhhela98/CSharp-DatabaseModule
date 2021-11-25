namespace SoftJail.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var serializer = JsonConvert.DeserializeObject<IEnumerable<ImportDepartmentCellDto>>(jsonString);

            var sb = new StringBuilder();
            var departments = new List<Department>();

            foreach (var departmentCell in serializer)
            {
                if (!IsValid(departmentCell) ||
                    !departmentCell.Cells.All(IsValid) ||
                    !departmentCell.Cells.Any())
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var deparment  =  new Department
                {

                    Name= departmentCell.Name,
                    Cells = departmentCell.Cells.Select(x=> new Cell
                    {
                        CellNumber=x.CellNumber,
                        HasWindow=x.HasWindow,
                  
                    }).ToList()
                };

                departments.Add(deparment);
                sb.AppendLine($"Imported {departmentCell.Name} with {departmentCell.Cells.Count()} cells");
            }

            context.Departments.AddRange(departments);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var serializer =  JsonConvert.DeserializeObject<IEnumerable<ImportPrissonerMailDto>>(jsonString);

            var sb = new StringBuilder();
            var prisoners = new List<Prisoner>();

            foreach (var prisonerMail in serializer)
            {
                if (!IsValid(prisonerMail) || !prisonerMail.Mails.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");

                    continue;
                }

                DateTime incarcerationDate;

                var parsedIncarcerationDate = DateTime.TryParseExact(prisonerMail.IncarcerationDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out incarcerationDate);

                DateTime releaseDate;

                var parsedReleaseDate = DateTime.TryParseExact(prisonerMail.ReleaseDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate);


                var prisoner = new Prisoner
                {
                    FullName = prisonerMail.FullName,
                    Nickname = prisonerMail.Nickname,
                    Age = prisonerMail.Age,
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = parsedReleaseDate ? (DateTime?) releaseDate : null,
                    Bail = prisonerMail.Bail,
                    CellId = prisonerMail.CellId,
                    Mails = prisonerMail.Mails.Select(x=> new Mail
                    {
                        Description = x.Description,
                        Sender =x.Sender,
                        Address= x.Address

                    }).ToList()
                };

                prisoners.Add(prisoner);
                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var serializer  =  XmlConverter.Deserializer<ImportOfficerPrisonerDto>(xmlString, "Officers");
            var sb = new StringBuilder();
            var officers = new List<Officer>();

            foreach (var officerPrisoner in serializer)
            {
                if (!IsValid(officerPrisoner) || !officerPrisoner.Prisoners.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                Position position;

                var isPositionValid = Enum.TryParse(officerPrisoner.Position, out position);

                Weapon weapon;

                var isWeaponValid = Enum.TryParse(officerPrisoner.Weapon, out weapon);

                if (!isPositionValid || !isWeaponValid)
                {
                    sb.AppendLine("Invalid Data");

                    continue;
                }

                var officer = new Officer
                {
                    FullName  = officerPrisoner.Name,
                    Salary  = officerPrisoner.Money,
                    Position  = Enum.Parse<Position>(officerPrisoner.Position),
                    Weapon  =  Enum.Parse<Weapon>(officerPrisoner.Weapon),
                    DepartmentId = officerPrisoner.DepartmentId,
                    OfficerPrisoners = officerPrisoner.Prisoners.Select(x=> new OfficerPrisoner
                    {
                        PrisonerId = x.Id

                    }).ToArray()
                };

                officers.Add(officer);
                sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count()} prisoners)");
            }

            context.Officers.AddRange(officers);
            Console.WriteLine(officers.Count());
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}