CREATE PROCEDURE usp_DepositMoney(@AccountId INT, @MoneyAmount DECIMAL(18,4))
AS
BEGIN TRANSACTION 
	DECLARE @accountExist INT = (SELECT COUNT(*) FROM Accounts AS A WHERE A.Id = @AccountId)
	IF(@accountExist = 0)
	BEGIN 
	THROW 50001, 'Account with thath Id doesn`t exist!', 1
	END
	IF(@MoneyAmount < 0)
	BEGIN 
	THROW 50002, 'Money should be positive number', 1
	END

UPDATE  Accounts 
SET Balance += @MoneyAmount
WHERE Id = @AccountId
COMMIT

SELECT * FROM Accounts 
WHERE Id = 1

EXEC  usp_DepositMoney @AccountID = 1, @MoneyAmount = 100