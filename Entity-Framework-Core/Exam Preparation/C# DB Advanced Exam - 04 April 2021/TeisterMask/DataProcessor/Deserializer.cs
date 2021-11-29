namespace TeisterMask.DataProcessor
{
    using Data;
    using System;
    using System.Text;
    using System.Globalization;
    using TeisterMask.Data.Models;
    using System.Collections.Generic;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ImportDto;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using System.Linq;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var serialize  =  XmlConverter.Deserializer<ImportProjectDto>(xmlString, "Projects");
            ;
            var sb = new StringBuilder();
            var projects =  new List<Project>();

            foreach (var currProject in serialize)
            {
                if (!IsValid(currProject))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime ProjectOpenDate;

                var parsedOpenDate = DateTime.TryParseExact(currProject.OpenDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out ProjectOpenDate);

                DateTime ProjectDueDate;

                var parsedDueDate = DateTime.TryParseExact(currProject.DueDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out ProjectDueDate);

                var project =  new Project
                {
                    Name = currProject.Name,
                    OpenDate =ProjectOpenDate,
                    DueDate = parsedDueDate ? (DateTime?) ProjectDueDate : null
                };


                foreach (var currTask in currProject.Tasks)
                {
                    if (!IsValid(currTask))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime TaskOpenDate;

                    var parsedTaskOpenDate = DateTime.TryParseExact(currTask.OpenDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out TaskOpenDate);

                    if (!parsedTaskOpenDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (TaskOpenDate < ProjectOpenDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime TaskDueDate;

                    var parsedTaskDueDate = DateTime.TryParseExact(currTask.DueDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out TaskDueDate);

                    if (!parsedTaskOpenDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (project.DueDate.HasValue)
                    {
                        if (TaskDueDate > ProjectDueDate)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }
                    }


                    //Enum executionType;
                    //Enum labelType;

                    //var isExecutionTypeValid = Enum.TryParse(currTask.ExecutionType, out executionType);
                    //var isLabelTypeValid = Enum.TryParse(currTask.LabelType, out labelType);

                    var task =  new Task
                    {
                        Name = currTask.Name,
                        OpenDate = TaskOpenDate,
                        DueDate = TaskDueDate,
                        LabelType = (LabelType)currTask.LabelType,
                        ExecutionType = (ExecutionType)currTask.ExecutionType
                    };

                    project.Tasks.Add(task);
                }

                sb.AppendLine(string.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
                projects.Add(project);
            }

            context.Projects.AddRange(projects);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var serialize = JsonConvert.DeserializeObject<List<ImportEmployeeDto>>(jsonString);
            var sb = new StringBuilder();
            var employees = new List<Employee>();

            foreach (var currEmployee in serialize)
            {
                if (!IsValid(serialize))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (currEmployee.Username.Any(ch => !char.IsLetterOrDigit(ch)))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                HashSet<int> tasksId = currEmployee.Tasks.Select(x=>x).ToHashSet();

                var employee = new Employee
                {
                    Username = currEmployee.Username,
                    Phone    = currEmployee.Phone,
                    Email = currEmployee.Email,
                };

                foreach (var currtask in tasksId)
                {
                    var task = context.Tasks.FirstOrDefault(x=>x.Id == currtask);

                    if (task == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var employeeTask = new EmployeeTask
                    {
                        Employee = employee,
                        Task = task
                    };

                    employee.EmployeesTasks.Add(employeeTask);
                }

                employees.Add(employee);
                sb.AppendLine(string.Format(SuccessfullyImportedEmployee, employee.Username, employee.EmployeesTasks.Count));
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}