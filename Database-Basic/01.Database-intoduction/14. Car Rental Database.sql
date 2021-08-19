CREATE TABLE [Categories](
	[Id] INT IDENTITY PRIMARY KEY,
	[CategoryName] NVARCHAR(100) NOT NULL,
	[DailyRate] DECIMAL(6,2) NOT NULL,
	[WeeklyRate] DECIMAL(8,2) NOT NULL,
	[MonthlyRate] DECIMAL(10,2) NOT NULL,
	[WeekendRate] DECIMAL(6,2) NOT NULL)

CREATE TABLE [Cars](
	[Id] INT PRIMARY KEY IDENTITY,
	[PlateNumber] NVARCHAR(10) NOT NULL,
	[Manufacturer] NVARCHAR (30) NOT NULL,
	[Model] NVARCHAR (30) NOT NULL,
	[CarYear] INT NOT NULL,
	[CategoryId] INT FOREIGN KEY REFERENCES Categories(Id),
	[Doors] INT NOT NULL,
	[Picture] VARBINARY (MAX),
	[Condition] NVARCHAR (30),
	[Avilable] BIT NOT NULL,
)

CREATE TABLE [Employees](
	[Id] INT PRIMARY KEY IDENTITY,
	[FirstName] NVARCHAR (20) NOT NULL,
	[LastName] NVARCHAR (20) NOT NULL,
	[Title] NVARCHAR (30) NOT NULL,
	[Notes] NVARCHAR(MAX))

CREATE TABLE [Customers](
	[Id] INT PRIMARY KEY IDENTITY,
	[DriveLicenceNumber] NVARCHAR (10) NOT NULL,
	[FullName] NVARCHAR (30) NOT NULL,
	[Address] NVARCHAR (30),
	[City] NVARCHAR (30) NOT NULL,
	[ZIPCode] NVARCHAR (30) NOT NULL,
	[Notes] NVARCHAR(100))

CREATE TABLE [RentalOrders](
	[Id] INT PRIMARY KEY IDENTITY,
	[EmployeeId] INT FOREIGN KEY REFERENCES Employees(Id),
	[CustomerId] INT FOREIGN KEY REFERENCES Customers(Id),
	[CarId] INT FOREIGN KEY REFERENCES Cars(Id),
	[TankLevel] INT NOT NULL,
	[KilometrageStart] INT NOT NULL,
	[KilometrageEnd] INT NOT NULL,
	[TotalKilometrage] AS [KilometrageStart] - [KilometrageEnd],
	[StartDate] DATETIME NOT NULL,
	[EndDate] DATETIME NOT NULL,
	[TotalDate] AS DATEDIFF(Day,[StartDate],[EndDate]),
	[RateApplied] DECIMAL (5,2) NOT NULL,
	[TaxRate] DECIMAL (7,2) NOT NULL,
	[OrderStatus] NVARCHAR (30) NOT NULL,
	[Notes] NVARCHAR(100)
)

INSERT INTO [Categories]([CategoryName],[DailyRate],[WeeklyRate],[MonthlyRate],[WeekendRate])
VALUES 
	('COUPE',100.25,1000,10000,56.25),
	('HATCHBACK',100.25,1000,10000,56.25),
	('MINIVAN',100.25,1000,10000,56.25)

INSERT INTO [Cars]([PlateNumber],[Manufacturer],[Model],[CarYear]
,CategoryId,[Doors],[Picture],[Condition],[Avilable])
VALUES 
	('6565HGGH3','BMW','BMWX3',2006,1,4,NULL,'YES',1),
	('D565HGGH3','BMW','BMWX3',2006,2,4,NULL,'YES',0),
	('5465HGGH3','BMW','BMWX3',2006,3,4,NULL,'YES',1)

INSERT INTO [Employees]([FirstName],[LastName],[Title])
VALUES 
	('DINO','BEENS','SENIOR'),
	('IVO','FRIKO','MANAGER'),
	('BINO','DIKO','DIRECTOR')

INSERT INTO [Customers]([DriveLicenceNumber],[FullName],[Address],[City],[ZIPCode])
VALUES 
	('10203035','JOHN DEEP LAYER','ST.AVENU 5','NEW YORK', '2030'),
	('20586544','IJI DEEP HGHG','ST.AVENU 5','NEW YORK', '2030'),
	('10203035','DINI DEEP DIKO','ST.AVENU 5','NEW YORK', '2030')

INSERT INTO [RentalOrders]([EmployeeId],[CustomerId],[CarId],[TankLevel]
,[KilometrageStart],[KilometrageEnd],[StartDate],[EndDate],[RateApplied],[TaxRate],
[OrderStatus])
VALUES 
(1,1,1,7,600,900,'2020-07-02','2020-07-04',69.00,20.00,'TRUE'),
(2,2,2,7,600,900,'2020-07-02','2020-07-04',89.00,20.00,'FALSE'),
(3,3,3,7,600,900,'2020-07-02','2020-07-04',99.00,20.00,'TRUE')