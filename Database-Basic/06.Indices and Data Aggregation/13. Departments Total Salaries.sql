SELECT E.DepartmentID,
	SUM(E.Salary) AS TotalSalary
FROM Employees AS E
GROUP BY DepartmentID
ORDER BY DepartmentID 