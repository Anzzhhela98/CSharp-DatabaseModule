SELECT TOP(5) 
	   E.EmployeeID AS EmployeeId, 
	   E.JobTitle, 
	   A.AddressID AS AddressId ,
	   A.AddressText
FROM Employees E
JOIN Addresses A ON A.AddressID = E.AddressID 
ORDER BY A.AddressID ASC