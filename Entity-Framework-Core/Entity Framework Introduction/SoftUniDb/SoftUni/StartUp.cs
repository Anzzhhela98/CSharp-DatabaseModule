using System;
using System.Globalization;
using System.Linq;
using System.Text;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();
            using (context)
            {
                //Console.WriteLine(GetEmployeesFullInformation(context));
                //Console.WriteLine(GetEmployeesWithSalaryOver50000(context));
                //Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));
                //Console.WriteLine(AddNewAddressToEmployee(context));
                //Console.WriteLine(GetEmployeesInPeriod(context));
                //Console.WriteLine(GetAddressesByTown(context));
                //Console.WriteLine(GetEmployee147(context));
                //Console.WriteLine(GetDepartmentsWithMoreThan5Employees(context));
                //Console.WriteLine(GetLatestProjects(context));
                //Console.WriteLine(IncreaseSalaries(context));
                //Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(context));
                //Console.WriteLine(DeleteProjectById(context));
                //Console.WriteLine(RemoveTown(context));
            }
        }

        //03.	Employees Full Information
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                })
                .OrderBy(e => e.EmployeeId)
                .ToList();

            StringBuilder result = new StringBuilder();

            foreach (var employee in employees)
            {
                result.AppendLine(
                    $"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }

            return result.ToString().Trim();
        }

        //04.	Employees with Salary Over 50 000
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .Where(e => e.Salary > 50000)
                .OrderBy(e=>e.FirstName);

            var result = new StringBuilder();

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }

            return result.ToString().TrimEnd();
        }

        //05. Employees from Research and Development
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {

            var result = new StringBuilder();
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Department,
                    e.Salary
                })
                .Where(e=>e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e=>e.FirstName)
                .ToList();

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.Department.Name} - ${employee.Salary:f2}");
            }

            return result.ToString().TrimEnd();
        }

        //06. Adding a New Address and Updating Employee
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var newAddress = new Address();
            newAddress.TownId = 4;
            newAddress.AddressText = "Vitoshka 15";

            context.Addresses.Add(newAddress);
            context.SaveChanges();

            var lastEmployee = context.Employees
                    .Where(e => e.LastName == "Nakov")
                    .First();

            lastEmployee.Address = newAddress;
            context.SaveChanges();

            var employees = context
                .Employees
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .Select(e => new
                {
                    e.AddressId,
                    e.Address.AddressText
                })
                .ToList();

            StringBuilder result = new StringBuilder();

            foreach (var item in employees)
            {
                result.AppendLine($"{item.AddressText}");
            }

            return result.ToString().TrimEnd();
        }

        //07. Employees and Projects

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var first10Employees = context.Employees
                .Where(e => e.EmployeesProjects.Any(ep =>
                    ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003))
                .Take(10)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects
                        .Select(ep => new
                        {
                            ProjectName = ep.Project.Name,
                            StartDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt",
                                CultureInfo.InvariantCulture),
                            EndDate = ep.Project.EndDate.HasValue
                                ? ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                                : "not finished"
                        })
                        .ToList()
                }).ToList();

            StringBuilder result = new StringBuilder();

            foreach (var employee in first10Employees)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");
                foreach (var p in employee.Projects)
                {
                    result.AppendLine($"--{p.ProjectName} - {p.StartDate} - {p.EndDate}");
                }
            }

            return result.ToString().TrimEnd();
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addressesInfo = context.Addresses
                .OrderByDescending(e => e.Employees.Count)
                .ThenBy(e => e.Town.Name)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .Select(a => new
                {
                    EmployeesCount = a.Employees.Count,
                    TownName = a.Town.Name,
                    AddressText = a.AddressText
                })
                .ToList();

            var result = new StringBuilder();

            foreach (var address in addressesInfo)
            {
                result.AppendLine($"{address.AddressText}, {address.TownName} - {address.AddressText} employees");
            }

            return result.ToString().Trim();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var employeeWhithId147 = context.Employees
                .Select(e=>new
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    JobTitle = e.JobTitle,
                    Projects = e.EmployeesProjects
                    .Select(p=> new
                    {
                        project = p.Project.Name 
                    })
                    .OrderBy(p=>p.project)
                    .ToList()
                })
                 .Where(e=>e.EmployeeId == 147)
                .ToList();
            var result = new StringBuilder();

            foreach (var employee in employeeWhithId147)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

                foreach (var p in employee.Projects)
                {
                    result.AppendLine(p.project);
                }
            }

            return result.ToString().Trim();
        }

        //10. Departments with More Than 5 Employees

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments
                .Where(e=>e.Employees.Count > 5)
                .OrderBy(e=>e.Employees.Count)
                .ThenBy(d=>d.Name)
                .Select(d => new
                {
                    departmentName = d.Name,
                    ManagerFirstName  = d.Manager.FirstName,
                    ManagerLastName = d.Manager.LastName,
                    allEmployees = d.Employees
                        .Select(e => new
                        {
                            FirstName = e.FirstName ,
                            LastName = e.LastName,
                            JobTitle = e.JobTitle
                        })
                        .OrderBy(e=>e.FirstName)
                        .ThenBy(e=>e.LastName)
                        .ToList()
                })
                .ToList();

            var result = new StringBuilder();

            foreach (var department in departments)
            {
                result.AppendLine($"{department.departmentName} - {department.ManagerFirstName}  {department.ManagerLastName}");

                foreach (var employee in department.allEmployees)
                {
                      result.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }

            return result.ToString().Trim();
        }

        //11. Find Latest 10 Projects

        public static string GetLatestProjects(SoftUniContext context)
        {
            var latestProjects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .Select(p => new
                {
                    name = p.Name,
                    description = p.Description,
                    startDate = p.StartDate
                })
                .ToList();
            var result = new StringBuilder();

            foreach (var project in latestProjects)
            {
                result.AppendLine($"{project.name}");
                result.AppendLine($"{project.description}");
                result.AppendLine($"{project.startDate}");
            }

            return result.ToString().Trim();
        }

        //12. Increase Salaries

        public static string IncreaseSalaries(SoftUniContext context)
        {
            var departments = new[]
            {
                "Engineering",
                "Tool Design",
                "Marketing",
                "Information Services"
            };

            var employeesFromDepartment = context.Employees
               .Where(e => departments.Contains(e.Department.Name))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();


            foreach (var employee in employeesFromDepartment)
            {
                employee.Salary *= 1.12m;
            }
            
            context.SaveChanges();

            var result = new StringBuilder();

            foreach (var employee in employeesFromDepartment)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
            }


            return result.ToString().Trim();
        }

        //13. Find Employees by First Name Starting With Sa

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees
                .Where( e=>e.FirstName.StartsWith("Sa"))
                .Select(e => new
                {
                    EmployeeFirstName = e.FirstName,
                    EmployeeLastName = e.LastName,
                    EmployeeJobTitle = e.JobTitle,
                    EmployeeSalary= e.Salary
                })
                .OrderBy(e=>e.EmployeeFirstName)
                .ThenBy(e=>e.EmployeeLastName)
                .ToList();

            var result = new StringBuilder();

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.EmployeeFirstName} {employee.EmployeeLastName}" +
                                  $" - {employee.EmployeeJobTitle} - (${employee.EmployeeSalary:F2})");
            }

            return result.ToString().Trim();
        }

        //14. Delete Project by Id

        public static string DeleteProjectById(SoftUniContext context)
        {
            var projectToDelete = context.Projects
                .FirstOrDefault(p => p.ProjectId == 2);

            var employeeProject = context.EmployeesProjects
                .Where(p => p.EmployeeId == 2)
                .ToList();


            foreach (var project in employeeProject)
            {
                context.EmployeesProjects.Remove(project);
            }

            context.Projects.Remove(projectToDelete);

            context.SaveChanges();

            var result = new StringBuilder();

            var projects = context.Projects
                .Select(p => p.Name)
                .Take(10)
                .ToList();

            foreach (var project in projects)
            {
                result.AppendLine($"{project}");
            }

            return result.ToString().Trim();
        }

        //15. Remove Town

        public static string RemoveTown(SoftUniContext context)
        {
            var townToRemove = context.Towns
                .FirstOrDefault(t => t.Name == "Seattle");

            var AddressesToDelete = context.Addresses
                .Where(a => a.TownId == townToRemove.TownId);

            var employeesToDelete = context.Employees
                .Where(e => AddressesToDelete.Any(a => a.AddressId == e.AddressId));

            var countOfAddressRemoved = AddressesToDelete.Count();

            foreach (var employee in employeesToDelete)
            {
                employee.AddressId = null;
            }

            foreach (var address in AddressesToDelete)
            {
                context.Addresses.Remove(address);
            }

            context.Towns.Remove(townToRemove);

            context.SaveChanges();

            return $"{countOfAddressRemoved} addresses in Seattle were deleted";
        }
     }
}