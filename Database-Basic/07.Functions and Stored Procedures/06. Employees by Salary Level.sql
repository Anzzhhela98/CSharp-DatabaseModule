CREATE PROCEDURE usp_EmployeesBySalaryLevel(@Type NVARCHAR(10))
AS BEGIN
SELECT Result.FirstName AS [First Name], Result.LastName AS [Last Name] 
FROM (
	  SELECT E.FirstName , E.LastName,
	  dbo.ufn_GetSalaryLevel(E.Salary) AS [Level]
	  FROM Employees AS E) AS Result
WHERE Result.Level = @Type
END 


EXECUTE usp_EmployeesBySalaryLevel @Type = 'High'