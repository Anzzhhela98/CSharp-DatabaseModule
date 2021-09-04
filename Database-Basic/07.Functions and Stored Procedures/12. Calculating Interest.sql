CREATE PROCEDURE usp_CalculateFutureValueForAccount(@accountId INT, @interestRate FLOAT = 0.1)
AS
BEGIN 
	SELECT AH.FirstName AS [First Name], 
		   AH.LastName  AS [Last Name], 
		   A.Balance,
			dbo.ufn_CalculateFutureValue(A.Balance,@interestRate, 5)           
	FROM Accounts AS A
	JOIN AccountHolders AS AH ON AH.Id = A.AccountHolderId
	WHERE A.Id = @accountId
END

EXECUTE usp_CalculateFutureValueForAccount @accountId = 1, @interestRate = 0.1