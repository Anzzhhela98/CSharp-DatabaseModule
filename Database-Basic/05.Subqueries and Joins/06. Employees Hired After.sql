SELECT E.FirstName,E.LastName, E.HireDate, D.Name AS DeptName 
FROM Employees E
JOIN Departments D ON D.DepartmentID = E.DepartmentID
WHERE E.HireDate >'1999-01-01' AND (D.Name ='Sales' OR D.Name ='Finance')
ORDER BY E.HireDate ASC