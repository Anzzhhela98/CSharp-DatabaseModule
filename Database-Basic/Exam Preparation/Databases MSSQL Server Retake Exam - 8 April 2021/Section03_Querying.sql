--05.	Unassigned Reports

SELECT R.[Description], CONVERT(varchar, R.OpenDate,105)
FROM Reports AS R
WHERE R.EmployeeId IS NULL
ORDER BY R.OpenDate , R.[Description]

--06. Reports & Categories

SELECT R.Description, C.Name 
FROM Reports    AS R
JOIN Categories AS C ON C.Id = R.CategoryId
ORDER BY R.Description, C.Name

--07. Most Reported Category

SELECT TOP(5) C.Name AS CategoryName, 
COUNT(*) AS ReportsNumber
FROM Reports AS R
JOIN Categories AS C ON C.Id = R.CategoryId
GROUP BY R.CategoryId, C.Name
ORDER BY ReportsNumber DESC, C.Name

--08. Birthday Report

SELECT  U.Username, C.Name
FROM Reports AS R 
JOIN Categories AS C ON C.Id = R.CategoryId
JOIN Users AS U ON U.Id = R.UserId
WHERE 
	DATEPART(MONTH,U.Birthdate) = DATEPART(MONTH,R.OpenDate) AND
	DATEPART(DAY,U.Birthdate) = DATEPART(DAY,R.OpenDate)
ORDER BY U.Username ASC, C.Name ASC


--09. User per Employee
SELECT 
	 FirstName + ' ' + LastName AS FullName, 
	 COUNT(DISTINCT R.UserId) AS UsersCount
FROM Reports   AS R
RIGHT JOIN Employees AS E ON E.Id = R.EmployeeId
GROUP BY EmployeeId, FirstName, LastName
ORDER BY UsersCount DESC, FullName ASC

--10. Full Info

SELECT  	 
	ISNULL(E.FirstName + ' ' + E.LastName,'None') AS Employee,
	ISNULL(D.Name , 'None') AS Department,
	ISNULL(C.Name,'None' )  AS Category,
	R.Description,
	FORMAT(R.OpenDate,'dd.MM.yyyy') AS OpenDate,
	S.Label AS [Status],
	U.Name AS [User]
FROM Reports		 AS R
LEFT JOIN Employees  AS E ON E.Id = R.EmployeeId
LEFT JOIN Categories AS C ON C.Id = R.CategoryId 
LEFT JOIN [Status]	 AS S ON S.Id = R.StatusId 
LEFT JOIN Users		 AS U ON U.Id = R.UserId
LEFT JOIN Departments AS D ON D.Id = E.DepartmentId

ORDER BY 
	E.FirstName  DESC, 
	E.LastName   DESC,
	D.Name       ASC,
	C.Name       ASC,
	R.Description ASC,
	R.OpenDate	 ASC,
	S.Label	     ASC,
	U.Username   ASC