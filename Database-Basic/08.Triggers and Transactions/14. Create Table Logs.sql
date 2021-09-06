CREATE TABLE Logs
(
LogId INT PRIMARY KEY IDENTITY, 
   AccountId INT FOREIGN KEY REFERENCES Accounts(Id) , 
   OldSum DECIMAL(15,2) NOT NULL,
   NewSum DECIMAL(15,2) NOT NULL
)

CREATE TRIGGER tr_EntersNewEntry ON Accounts
FOR UPDATE
AS 
DECLARE @accountId INT  = (SELECT Id FROM inserted)
DECLARE @newSum DECIMAL(15,2)  = (SELECT Balance FROM inserted)
DECLARE @oldSum DECIMAL(15,2)  = (SELECT Balance FROM deleted)

INSERT INTO Logs(AccountID,NewSum,OldSum) VALUES
(@accountId, @newSum, @oldSum)
GO

UPDATE Accounts 
SET Balance +=100
WHERE Id = 4

SELECT * FROM Accounts
SELECT * FROM Logs