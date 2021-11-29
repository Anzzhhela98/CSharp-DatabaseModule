namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var project = context
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

            var xmlResult = XmlConverter.Serialize(project, "Projects");
            ;
            return xmlResult;
        }

        //Export all projects that have at least one task.
        //For each project, export its name, tasks count, and if it has end(due) date which is represented like "Yes" and "No".
        //For each task, export its name and label type.Order the tasks by name (ascending).
        //Order the projects by tasks count (descending), then by name (ascending).
        //NOTE: You may need to call.ToArray() function before the selection in order to detach entities from the database and avoid runtime errors (EF Core bug). 


        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context
                .Employees
                .Where(e => e.EmployeesTasks.Any(t => DateTime.Compare(t.Task.OpenDate,date) >= 0))
                .OrderByDescending(e => e.EmployeesTasks.Count)
                .ThenBy(e => e.Username)
                .Take(10)
                .ToList()
                .Select(e => new
                {
                    Username =  e.Username,
                    Tasks = e.EmployeesTasks
                        .Where(t => DateTime.Compare(t.Task.OpenDate,date) >= 0)
                        .OrderByDescending(t => t.Task.DueDate)
                        .ThenBy(t => t.Task.Name)
                        .Select(t=> new
                        {
                            TaskName = t.Task.Name,
                            OpenDate = t.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                            DueDate = t.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                            LabelType = Enum.GetName(typeof(LabelType), t.Task.LabelType),
                            ExecutionType = Enum.GetName(typeof(ExecutionType), t.Task.ExecutionType),
                        }).ToArray()
                });

            var serializer = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return serializer;
        }
    }
}