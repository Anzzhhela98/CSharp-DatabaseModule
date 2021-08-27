SELECT E.EmployeeID,
	   E.FirstName,
	   E.LastName,
	   E.ManagerID,
	   M.FirstName AS ManagerName
FROM Employees AS E
JOIN Employees AS M ON M.EmployeeID = E.ManagerID 
WHERE E.ManagerID = 3 OR E.ManagerID = 7
ORDER BY E.EmployeeID
