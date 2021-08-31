SELECT E.DepartmentID,
MIN(E.Salary) AS MinimumSalary 
FROM Employees AS E
WHERE (E.DepartmentID = 2 OR 
	  E.DepartmentID = 5 OR
	  E.DepartmentID = 7) AND E.HireDate > '01/01/2000'
GROUP BY DepartmentID