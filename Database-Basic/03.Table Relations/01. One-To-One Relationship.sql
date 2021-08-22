CREATE DATABASE EXERCISES
USE [EXERCISES]

CREATE TABLE [Passports](
[PassportID]     INT PRIMARY KEY IDENTITY,
[PassportNumber] CHAR(10),)

CREATE TABLE [Persons](
[PersonID]   INT PRIMARY KEY IDENTITY,
[FirstName]  NVARCHAR(30),
[Salary]     DECIMAL(10,2),
[PassportID] INT FOREIGN KEY REFERENCES [Passports]([PassportID] ))

INSERT INTO [Passports]([PassportNumber])
VALUES
('6526BG'),
('7226BEU')

INSERT INTO [Persons] ([FirstName],[Salary],[PassportID])
VALUES
('ANZHELA',20000,1),
('IVAYLO',50000,2)

-- trying to use join, it doesn't make much sense
SELECT [FirstName]+' '+Passports.PassportNumber AS NAMEANDNUMBER, [Salary]
FROM [Persons]
JOIN [Passports] ON Persons.PassportID=Passports.PassportID