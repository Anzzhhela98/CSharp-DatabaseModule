SELECT TOP(50)
E.EmployeeID,
CONCAT(E.FirstName,' ',
E.LastName) AS EmployeeName,
CONCAT(M.FirstName,' ',M.LastName) AS ManagerName,
D.Name AS DepartmentName 
FROM Employees AS E
JOIN Employees AS M ON M.EmployeeID = E.ManagerID
JOIN Departments AS D ON D.DepartmentID = E.DepartmentID
ORDER BY E.EmployeeID