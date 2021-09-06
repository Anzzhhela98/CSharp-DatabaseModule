CREATE PROCEDURE usp_TransferMoney(@SenderId INT, @ReceiverId INT, @Amount DECIMAL(18,4))
AS
BEGIN TRANSACTION 
EXECUTE usp_DepositMoney @ReceiverId , @Amount
EXECUTE usp_WithdrawMoney @SenderId, @Amount 
COMMIT

SELECT * FROM Accounts WHERE Id = 1
SELECT * FROM Accounts WHERE Id = 2
EXECUTE usp_TransferMoney @SenderId = 2 , @ReceiverId = 1, @Amount = 1000