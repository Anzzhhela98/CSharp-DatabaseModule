--05. EEE-Mails

SELECT A.FirstName, 
	   A.LastName,
	   FORMAT(A.BirthDate, 'MM-dd-yyyy') AS BirthDate,
	   C.Name, 
	   A.Email
FROM Accounts AS A
JOIN Cities AS C ON C.Id = A.CityId
WHERE A.Email LIKE 'e%'
ORDER BY C.Name ASC

--06. City Statistics

SELECT C.Name,  COUNT(*) AS Hotels FROM Hotels AS H 
JOIN Cities AS C ON C.Id = H.CityId
GROUP BY H.CityId, C.Name
ORDER BY  COUNT(*) DESC, C.Name ASC

--07. Longest and Shortest Trips

SELECT A.Id AS AccountId, CONCAT(A.FirstName, ' ', A.LastName) AS FullName,
	   MAX(DATEDIFF(DAY, T.ArrivalDate, T.ReturnDate)) AS LongestTrip ,
	   MIN(DATEDIFF(DAY, T.ArrivalDate, T.ReturnDate)) AS ShortestTrip
FROM Accounts AS A
JOIN AccountsTrips	   AS [AC] ON [AC].AccountId = A.Id
JOIN Trips			   AS T    ON [AC].TripId = T.Id
WHERE A.MiddleName IS NULL AND T.CancelDate IS NULL
GROUP BY [AC].AccountId, A.Id, A.FirstName, A.LastName
ORDER BY MAX(DATEDIFF(DAY, T.ArrivalDate, T.ReturnDate)) DESC,
		 MIN(DATEDIFF(DAY, T.ArrivalDate, T.ReturnDate)) ASC 

--08. Metropolis

SELECT TOP(10) 
       C.Id,
	   C.Name        AS City,
	   C.CountryCode AS Country, 
	   COUNT(*)      AS Accounts
FROM Cities   AS C
JOIN Accounts AS A ON A.CityId = C.Id
GROUP BY C.Id, C.Name, C.CountryCode
ORDER BY COUNT(*) DESC

--09. Romantic Getaways

SELECT
	A.Id,
	A.Email,
	C.Name AS City,
	COUNT(*) AS Trips 
FROM Accounts A 
JOIN AccountsTrips AS [AT] ON [AT].AccountId = A.Id
JOIN Trips		   AS T ON T.Id = [AT].TripId
JOIN Rooms		   AS R ON R.Id = T.RoomId
JOIN Hotels		   AS H ON H.Id = R.HotelId
JOIN Cities		   AS C ON C.Id = H.CityId
WHERE A.CityId = C.Id
GROUP BY A.Id , A.Email, C.Name, A.Id 
ORDER BY Trips DESC, A.Id ASC

--10. GDPR Violation

SELECT 
    T.Id,
CASE
	WHEN a.MiddleName IS NULL THEN CONCAT(a.FirstName, ' ', a.LastName)
	ELSE CONCAT(a.FirstName, ' ' + a.MiddleName, ' ', a.LastName)
END AS [Full Name],
	c.[Name] AS [From],
	C2.Name AS [To],
CASE
	WHEN T.CancelDate IS NULL THEN CONCAT(DATEDIFF(DAY,T.ArrivalDate, T.ReturnDate), ' ', 'days')
	ELSE 'Canceled'
	END AS Duration
FROM AccountsTrips AS [AT]
JOIN Accounts      AS A    ON    A.Id  = [AT].AccountId
JOIN Cities        AS C    ON    C.Id  = A.CityId
JOIN Trips         AS T    ON    T.Id  = [AT].TripId
JOIN Rooms         AS R    ON    R.Id  = T.RoomId
JOIN Hotels        AS H    ON    H.Id  = R.HotelId
JOIN Cities        AS C2    ON   C2.Id = H.CityId
ORDER BY [Full Name], T.Id
