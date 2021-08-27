SELECT TOP(50)
	   E.FirstName,
	   E.LastName, 
	   T.Name AS Town,
	   A.AddressText
FROM Employees E
JOIN Addresses A ON A.AddressID = E.AddressID 
JOIN Towns T ON T.TownID = A.TownID 
ORDER BY E.FirstName,E.LastName ASC