--16.Create SoftUni Database 
CREATE DATABASE SoftUni

CREATE TABLE [Towns](
[Id] INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR (20) NOT NULL,)

CREATE TABLE [Addresses](
[Id] INT PRIMARY KEY IDENTITY,
[AddressText] NVARCHAR (50) NOT NULL,
[TownId] INT FOREIGN KEY REFERENCES [Towns](Id),)

CREATE TABLE [Departments](
[Id] INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR (20) NOT NULL,)

CREATE TABLE [Employees](
[Id] INT PRIMARY KEY IDENTITY,
[FirstName] NVARCHAR (20) NOT NULL,
[MiddleName] NVARCHAR (20) NOT NULL,
[LastName] NVARCHAR (20) NOT NULL,
[JobTitle] NVARCHAR (20) NOT NULL,
[DepartmentId] INT FOREIGN KEY REFERENCES [Departments](Id),
[HireDate] DATE NOT NULL,
[Salary] DECIMAL(7,2) NOT NULL,
[AddressId] INT FOREIGN KEY REFERENCES [Addresses](Id))

--17.Backup Database 

--18.Basic Insert 

INSERT INTO [Towns] ([Name])
VALUES 
('Sofia'),
('Plovdiv'),
('Varna'),
('Burgas')

INSERT INTO [Departments] ([Name])
VALUES 
('Engineering'),
('Sales'),
('Marketing'),
('Software Development'),
('Quality Assurance')

INSERT INTO [Employees] ([FirstName],[MiddleName],[LastName],[JobTitle],
[DepartmentId],[HireDate],[Salary])
VALUES 
('Petar' , 'P' , 'Petrov', 'Senior Engineer',1,'2004/02/03',4000.00),
('Maria' , 'P' , 'Ivanova', 'Quality Assurance',5,'2016/08/08',525.00),
('Georgi' , 'T' , 'Ivanov', 'Sales',2,'2007/09/10',3000.00),
('Peter' , 'P' , 'Pan', '.Marketing',3,'2016/11/11',599.88)

--19.Basic Select all Fields

SELECT * FROM [Towns]
SELECT * FROM [Departments]
SELECT * FROM [Employees]

--20. Basic Select All Fields and Order Them

SELECT * FROM [Towns]
	ORDER BY [Name] ASC

SELECT * FROM [Departments]
	ORDER BY [Name] ASC

SELECT * FROM [Employees]
	ORDER BY [Salary] DESC

--21. Basic Select Some Fields

SELECT [Name] FROM [Towns]
	ORDER BY [Name] ASC

SELECT [Name] FROM [Departments]
	ORDER BY [Name] ASC

SELECT [FirstName],[LastName],[JobTitle],[Salary] FROM [Employees]
	ORDER BY [Salary] DESC

--22. Increase Employees Salary

	UPDATE [Employees]
SET 
    Salary = Salary*1.10

SELECT *FROM [Employees]

--23. Decrease Tax Rate

UPDATE [Payments]
SET
[TaxRate] = [TaxRate] * 0.97
SELECT [TaxRate] FROM [Payments]

--24.Delete all records 

TRUNCATE TABLE [Occupancies]
