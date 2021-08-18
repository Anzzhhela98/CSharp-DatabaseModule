CREATE DATABASE Minions
USE Minions
 
CREATE TABLE Minions(
Id INT PRIMARY KEY NOT NULL,
[Name] NVARCHAR(50)NOT NULL,
Age INT,)
 
CREATE TABLE Towns(
Id INT PRIMARY KEY NOT NULL,
[Name] NVARCHAR(50) NOT NULL)
 
ALTER TABLE Minions
ADD TownId INT FOREIGN KEY REFERENCES Towns(Id)
 
INSERT INTO Towns(Id,[Name])
     VALUES
             (1,'Sofia'),
             (2,'Plovdiv'),
             (3,'Varna')
 
INSERT INTO Minions(Id,[Name],Age,TownId)
    VALUES 
            (1,'Kevin',22,1),
            (2,'Bob',15,3),
            (3,'Steward',NULL,2)
 
UPDATE Towns 
SET 
    [Name] = 'Sofia'
WHERE
    Id = 1;
 
 
SELECT * FROM Minions
SELECT * FROM Towns
SELECT Id,[Name] FROM Towns

TRUNCATE TABLE Minions
DROP TABLE Minions 
DROP TABLE Towns