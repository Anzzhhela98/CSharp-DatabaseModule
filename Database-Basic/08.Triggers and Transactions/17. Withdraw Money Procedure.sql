CREATE  PROCEDURE usp_WithdrawMoney (@AccountId INT, @MoneyAmount DECIMAL(18,4))
AS
BEGIN TRANSACTION 
	DECLARE @AccountExist INT = (SELECT COUNT(*) FROM Accounts WHERE Id = @AccountId)
	DECLARE @CurrentBalance DECIMAL(18,4) = (SELECT Balance FROM Accounts WHERE Id = @AccountId)
IF(@AccountExist = 0)
	BEGIN 
	THROW 50001, 'Account with thath Id doesn`t exist!', 1
	END
IF(@MoneyAmount < 0)
	BEGIN 
	THROW 50002, 'Money should be positive number', 1
	END
IF(@CurrentBalance - @MoneyAmount < 0)
    BEGIN 
	THROW 50003, 'You Don`t have enough money!', 1
	END

UPDATE Accounts
SET Balance -= @MoneyAmount
WHERE Id = @AccountId
COMMIT

SELECT * FROM Accounts 
WHERE Id = 1

EXEC  usp_WithdrawMoney @AccountID = 1, @MoneyAmount = 10