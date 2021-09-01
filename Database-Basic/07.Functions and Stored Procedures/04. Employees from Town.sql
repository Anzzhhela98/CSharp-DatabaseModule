CREATE PROCEDURE usp_GetEmployeesFromTown(@TownName NVARCHAR(30))
AS
BEGIN
SELECT E.FirstName AS [First Name], E.LastName AS [Last Name]
FROM Employees AS E
JOIN Addresses AS A ON A.AddressID = E.AddressID
JOIN Towns AS T ON T.TownID = A.TownID
WHERE T.Name = @TownName

END

EXECUTE usp_GetEmployeesFromTown @TownName = 'Sofia'