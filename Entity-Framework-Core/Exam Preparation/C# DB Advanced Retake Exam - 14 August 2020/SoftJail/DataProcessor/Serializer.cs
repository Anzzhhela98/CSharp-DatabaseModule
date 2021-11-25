namespace SoftJail.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.Linq;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context
                .Prisoners
                .ToList()
                .Where(p => ids.Contains(p.Id))
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers.Select(po => new
                    {
                        OfficerName = po.Officer.FullName,
                        Department = po.Officer.Department.Name
                    })
                        .OrderBy(o => o.OfficerName)
                        .ToList(),
                    TotalOfficerSalary = decimal.Parse( p.PrisonerOfficers
                         .Sum(po => po.Officer.Salary)
                         .ToString("F2"))
                })
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToList();



            var json = JsonConvert.SerializeObject(prisoners, Formatting.Indented);

            return json;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var names = prisonersNames.Split(",");

            var prisoners = context
                .Prisoners
                .Where(p => names.Contains(p.FullName))
                .Select(p=> new ExportPrisonerDto
                {
                    Id = p.Id,
                    Name= p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd"),
                    EncryptedMessages = p.Mails
                        .Select(d => new ExportMessageDto
                        {
                           Description =string.Join("", d.Description.Reverse())

                        })
                        .ToArray()
                })
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToList();

            var xml = XmlConverter.Serialize(prisoners, "Prisoners");

            return xml;
        }
        //Use the method provided in the project skeleton, which receives a string of comma-separated prisoner names.
        //Export the prisoners: for each prisoner, export its id, name, incarcerationDate in the format “yyyy-MM-dd” and their encrypted mails.
        //The encrypted algorithm you have to use is just to take each prisoner mail description and reverse it.
        //Sort the prisoners by their name (ascending), then by their id (ascending).

    }
}