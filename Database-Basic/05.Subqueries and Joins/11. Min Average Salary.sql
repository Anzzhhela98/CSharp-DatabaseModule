SELECT TOP(1) AVG(Salary) AS MinAverageSalary
FROM Employees AS E
GROUP BY E.DepartmentID
ORDER BY MinAverageSalary