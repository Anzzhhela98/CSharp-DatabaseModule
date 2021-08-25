SELECT [FirstName]
FROM Employees
WHERE ([DepartmentID] = 3 OR [DepartmentID]=10) 
  AND (YEAR([HireDate])>=1995 AND YEAR([HireDate])<=2005)