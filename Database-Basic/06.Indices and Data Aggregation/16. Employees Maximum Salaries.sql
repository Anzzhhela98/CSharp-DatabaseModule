SELECT E.DepartmentID, 
MAX(E.Salary) AS MaxSalary
FROM Employees AS E
GROUP BY E.DepartmentID
HAVING MAX(Salary) < 30000 OR MAX(Salary) >  70000