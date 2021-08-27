SELECT TOP(5) E.EmployeeID,E.FirstName, P.Name AS ProjectName 
FROM Employees E
JOIN EmployeesProjects EP ON EP.EmployeeID = E.EmployeeID
JOIN Projects P ON P.ProjectID = EP.ProjectID
WHERE P.StartDate > '2002-08-13' AND P.EndDate IS NULL
ORDER BY E.EmployeeID ASC;
