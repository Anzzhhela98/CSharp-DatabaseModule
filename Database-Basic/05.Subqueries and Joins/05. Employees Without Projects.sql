SELECT TOP(3)
Employees.EmployeeID,
Employees.FirstName
FROM Employees
LEFT JOIN EmployeesProjects ON EmployeesProjects.EmployeeID = Employees.EmployeeID
WHERE EmployeesProjects.ProjectID IS NULL
ORDER BY EmployeeID
