CREATE PROCEDURE usp_GetEmployeesSalaryAbove35000 
AS
BEGIN
DECLARE @aboveSalary decimal = 35000
SELECT E.FirstName AS [First Name],
	   E.LastName  AS [Last Name]
FROM Employees AS E
WHERE E.Salary > @aboveSalary
END;

EXECUTE [dbo].[usp_GetEmployeesSalaryAbove35000]
