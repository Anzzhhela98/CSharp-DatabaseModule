CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber(@Number DECIMAL(18,4))
AS

BEGIN 
SELECT E.FirstName AS [First Name],
	   E.LastName  AS [Last Name]
FROM Employees AS E
WHERE E.Salary >= @Number
END

EXECUTE dbo.usp_GetEmployeesSalaryAboveNumber @Number = 48100
