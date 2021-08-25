CREATE TABLE [People](
[Id] INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(30),
[Birthdate] DATETIME2)
 
INSERT INTO People VALUES
     ('Ivanichka', '1998-07-02')
    ,('Dilqn', '1991-10-10')
    ,('Alina', '1996-06-20')
    ,('Bina', '2001-01-16')
 
SELECT [Name],
    DATEDIFF(YEAR,[Birthdate], GETDATE()) AS [Age in Years],
    DATEDIFF(MONTH,[Birthdate], GETDATE()) AS [Age in Months],
    DATEDIFF(DAY,[Birthdate], GETDATE()) AS [Age in Days],
    DATEDIFF(MINUTE,[Birthdate], GETDATE()) AS [Age in Minutes]
FROM People
