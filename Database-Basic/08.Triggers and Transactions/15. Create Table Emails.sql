CREATE TABLE NotificationEmails
(
Id INT PRIMARY KEY IDENTITY, 
Recipient INT FOREIGN KEY REFERENCES Accounts(Id), 
[Subject] NVARCHAR(50), 
Body NVARCHAR(MAX)
)

CREATE TRIGGER tr_NewRecordInsertedCreateEmail ON Logs
FOR INSERT
AS

DECLARE @Recipient INT  = (SELECT AccountId FROM inserted)
DECLARE @Subject  NVARCHAR(50) = (SELECT 'Balance change for account: ' + CAST(AccountID AS nvarchar) FROM inserted)
DECLARE @Body NVARCHAR(MAX) = (SELECT 'On ' + CAST(GETDATE() AS nvarchar) + ' your balance was changed from ' + CAST(OldSum AS nvarchar) + ' to ' + CAST(NewSum AS nvarchar) FROM inserted)

INSERT INTO NotificationEmails(Recipient, [Subject], Body  )
VALUES(@Recipient,@Subject,@Body)

GO

UPDATE Accounts
SET Balance +=100
WHERE Id = 1

SELECT * FROM Logs 
SELECT * FROM NotificationEmails
