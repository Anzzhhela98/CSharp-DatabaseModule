--05. Mechanic Assignments

SELECT CONCAT(M.FirstName,' ', M.LastName) AS Mechanic, 
	   J.Status, 
	   J.IssueDate 
FROM Mechanics AS M
	JOIN Jobs  AS J ON J.MechanicId = M.MechanicId
	ORDER BY M.MechanicId, J.IssueDate, J.JobId

--06. Current Clients

SELECT CONCAT(C.FirstName,' ',C.LastName), DATEDIFF(DAY, J.IssueDate, '2017-04-24') AS 'Days going', J.Status
FROM Jobs    AS J
JOIN Clients AS C ON C.ClientId = J.ClientId
WHERE J.Status != 'Finished'
ORDER BY DATEDIFF(DAY, J.IssueDate, '2017-04-24') DESC,
		 C.ClientId ASC

--07. Mechanic Performance

SELECT * FROM Mechanics
SELECT * FROM Jobs

SELECT  M.FirstName +' '+ M.LastName                  AS Mechanic,
		AVG(DATEDIFF(DAY, J.IssueDate, J.FinishDate)) AS [Average Days]
FROM Mechanics AS M
	JOIN Jobs  AS J ON J.MechanicId = M.MechanicId
	GROUP BY M.MechanicId, (M.FirstName +' '+ M.LastName)
	ORDER BY M.MechanicId

--08. Available Mechanics

	SELECT M.FirstName +' '+M.LastName
FROM Mechanics AS M
LEFT JOIN Jobs J on M.MechanicId = J.MechanicId
WHERE J.Status ='Finished' OR J.JobId IS NULL
ORDER BY M.MechanicId

	
--09. Past Expenses

SELECT J.JobId, ISNULL(SUM(P.Price * OP.Quantity),0) AS Total
FROM Jobs AS J 
	LEFT JOIN Orders     AS O  ON  O.JobId   = J.JobId
	LEFT JOIN OrderParts AS OP ON OP.OrderId = O.OrderId
	LEFT JOIN Parts      AS P  ON OP.PartId  = P.PartId
	WHERE J.Status = 'Finished'
	GROUP BY J.JobId
	ORDER BY SUM(P.Price) DESC, J.JobId ASC


--10. Missing Parts

SELECT 
	   P.PartId,
       P.Description,
       PN.Quantity as Required,
       P.StockQty,
	   IIF(O.Delivered = 0, OP.Quantity, 0)
FROM Parts AS P
	LEFT JOIN PartsNeeded AS PN ON P.PartId  = PN.PartId
	LEFT JOIN OrderParts  AS OP ON P.PartId  = OP.PartId
	LEFT JOIN Orders      AS O  ON O.OrderId = OP.OrderId
	LEFT JOIN Jobs        AS J  ON J.JobId   = PN.JobId
	WHERE J.Status !='Finished'  AND 
	(P.StockQty +  IIF(O.Delivered = 0, OP.Quantity, 0))< PN.Quantity
	ORDER BY PartId






