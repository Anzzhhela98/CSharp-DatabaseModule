SELECT E.EmployeeID,E.FirstName,
CASE WHEN YEAR(P.StartDate)>=2005 THEN NULL
ELSE P.Name
END AS ProjectName
FROM Employees E
JOIN EmployeesProjects EP ON EP.EmployeeID = E.EmployeeID
JOIN Projects P ON P.ProjectID = EP.ProjectID
WHERE E.EmployeeID=24