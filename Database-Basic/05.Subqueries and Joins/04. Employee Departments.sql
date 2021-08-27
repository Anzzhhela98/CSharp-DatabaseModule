SELECT TOP(5)
		E.EmployeeID, E.FirstName, E.Salary, D.Name AS DepartmentName 
FROM Employees E
JOIN Departments D ON D.DepartmentID = E.DepartmentID 
WHERE  E.Salary >=15000
ORDER BY D.DepartmentID ASC