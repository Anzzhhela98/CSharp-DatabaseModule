CREATE OR ALTER PROCEDURE usp_GetHoldersWithBalanceHigherThan(@Number DECIMAL(18,4))
AS 
BEGIN
SELECT  AH.FirstName AS [First Name],
        AH.LastName  AS [Last Name]
FROM Accounts AS A
JOIN AccountHolders AS AH ON A.AccountHolderId = AH.Id
GROUP BY AH.FirstName, AH.LastName
HAVING SUM( A.Balance) > @Number
 ORDER BY FirstName, LastName
END

EXECUTE usp_GetHoldersWithBalanceHigherThan @Number = 540