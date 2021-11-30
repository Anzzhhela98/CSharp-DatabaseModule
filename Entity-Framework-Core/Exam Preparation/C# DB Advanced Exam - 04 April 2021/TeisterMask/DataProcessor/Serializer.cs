namespace TeisterMask.DataProcessor
{
    using Data;
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using System.Globalization;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context
                .Projects
                .Where(p => p.Tasks.Any())
                .ToList()
                .Select(p =>  new  ExportProjectDto()
                {
                    TasksCount = p.Tasks.Count,
                    ProjectName = p.Name,
                    HasEndDate = p.DueDate == null ? "No" : "Yes",
                    Tasks =  p.Tasks
                                .OrderBy(t => t.Name)
                                .Select(t =>  new ExportTaskDto()
                                {
                                    Name =  t.Name,
                                    Label = t.LabelType.ToString()
                                })
                                .ToList()
                })
                .OrderByDescending(p => p.Tasks.Count)
                .ThenBy(p => p.ProjectName)
                .ToList();

            var xmlResult = XmlConverter.Serialize(projects, "Projects");

            return xmlResult;
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context
                .Employees
                .ToList() //?
                .Where(e => e.EmployeesTasks.Any(t => DateTime.Compare(t.Task.OpenDate,date) >= 0))
                .Select(e => new
                {
                    Username =  e.Username,
                    Tasks = e.EmployeesTasks
                        .Where(t => t.Task.OpenDate >= date)
                        .OrderByDescending(t => t.Task.DueDate)
                        .ThenBy(t => t.Task.Name)
                        .Select(t=> new
                        {
                            TaskName = t.Task.Name,
                            OpenDate = t.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                            DueDate = t.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                            LabelType = Enum.GetName(typeof(LabelType), t.Task.LabelType),
                            ExecutionType = Enum.GetName(typeof(ExecutionType), t.Task.ExecutionType),
                        }).ToList()
                })
                .OrderByDescending(e => e.Tasks.Count)
                .ThenBy(e => e.Username)
                .Take(10)
                .ToList();

            var serializer = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return serializer;
        }
    }
}