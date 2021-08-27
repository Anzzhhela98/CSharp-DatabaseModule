SELECT E.EmployeeID, E.FirstName, E.LastName, D.Name AS DepartmentName 
FROM Employees E
JOIN Departments D ON D.DepartmentID = E.DepartmentID 
WHERE  D.Name = 'Sales'
ORDER BY E.EmployeeID ASC