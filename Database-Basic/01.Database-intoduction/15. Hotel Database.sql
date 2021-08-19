CREATE TABLE Employees(
	[Id] INT PRIMARY KEY IDENTITY,
	[FirstName] NVARCHAR (20) NOT NULL,
	[LastName] NVARCHAR (20) NOT NULL,
	[Title] NVARCHAR (20) NOT NULL,
	[Notes] NVARCHAR (100)
)

CREATE TABLE Customers(
	[AccountNumber] INT PRIMARY KEY IDENTITY,
	[FirstName] NVARCHAR (20) NOT NULL,
	[LastName] NVARCHAR (20) NOT NULL,
	[PhoneNumber] NVARCHAR (10) NOT NULL,
	[EmergencyName] NVARCHAR (20) NOT NULL,
	[EmergencyNumber] NVARCHAR (10) NOT NULL,
	[Notes] NVARCHAR (100)
)

CREATE TABLE RoomStatus(
	[RoomStatus] NVARCHAR(10) PRIMARY KEY,
	[Notes] NVARCHAR(100)
)

CREATE TABLE RoomTypes (
	[RoomType] NVARCHAR(10) PRIMARY KEY,
	[Notes] NVARCHAR(100)
)

CREATE TABLE BedTypes(
	[BedType] NVARCHAR(10) PRIMARY KEY,
	[Notes] NVARCHAR(100)
)

CREATE TABLE Rooms(
	[RoomNumber] INT PRIMARY KEY IDENTITY,
	[RoomType] NVARCHAR (10) FOREIGN KEY REFERENCES RoomTypes(RoomType),
	[BedType] NVARCHAR (10) FOREIGN KEY REFERENCES BedTypes(BedType),
	[Rate] DECIMAL (5,1) NOT NULL,
	[RoomStatus] NVARCHAR (20) NOT NULL,
	[Notes] NVARCHAR(100)
)

CREATE TABLE Payments(
	[Id] INT PRIMARY KEY IDENTITY,
	[EmployeeId] INT FOREIGN KEY REFERENCES Employees(Id),
	[PaymentDate] DATETIME2 NOT NULL,
	[AccountNumber] INT FOREIGN KEY REFERENCES Customers(AccountNumber),
	[FirstDateOccupied] DATETIME2 NOT NULL,
	[LastDateOccupied] DATETIME2 NOT NULL,
	[TotalDays] AS DATEDIFF(DAY,FirstDateOccupied,LastDateOccupied),
	[AmountCharged] DECIMAL (6,2) NOT NULL,
	[TaxRate] DECIMAL (5,2) NOT NULL,
	[TaxAmount] AS AmountCharged*TaxRate,
	[PaymentTotal] DECIMAL (6,2) NOT NULL,
	[NOTES] NVARCHAR(100))

CREATE TABLE Occupancies(
	[Id] INT PRIMARY KEY IDENTITY,
	[EmployeeId] INT FOREIGN KEY REFERENCES Employees(Id),
	[DateOccupied] DATETIME2 NOT NULL,
	[AccountNumber] INT FOREIGN KEY REFERENCES Customers(AccountNumber),
	[RoomNumber] INT FOREIGN KEY REFERENCES Rooms(RoomNumber),
	[RateApplied] DECIMAL(5,2), 
	[PhoneCharge] DECIMAL(5,2),
	[Notes] NVARCHAR(100))

INSERT INTO Employees(FirstName, LastName, Title)
VALUES
	('UAN', 'DINKO', 'Physicist'),
	('FINO', 'DESN', 'Biologist'),
	('DINO', 'DINEV', 'Programmer')

INSERT INTO [Customers] ([FirstName], [LastName], [PhoneNumber], [EmergencyName], [EmergencyNumber])
	VALUES
		('DINO','LIK','0232366598','Burns','123'),
		('AS','AS','8851264','Burns','123'),
		('ASQ','DINO','8851264','Burns','123')

INSERT INTO [RoomStatus] ([RoomStatus])
	VALUES
		('FREE'),
		('OCCUPIED'),
		('FREE CLEAN')

INSERT INTO RoomTypes(RoomType)
VALUES
	('Single'),
	('Double'),
	('Appartment')

INSERT INTO BedTypes(BedType) 
VALUES
	('Single'),
	('Double'),
	('Couch')

INSERT INTO Rooms( RoomType, BedType, Rate, RoomStatus) 
VALUES
	( 'Single', 'Single', 30.0, 'CLOSE'),
	( 'Double', 'Double', 45.0, 'CLOSE'),
	( 'Appartment', 'Double', 90.0, 'CLOSE')

INSERT INTO Payments ( PaymentDate, AccountNumber,
FirstDateOccupied, LastDateOccupied,
AmountCharged, TaxRate,
PaymentTotal)
	VALUES
		('2019-04-20','1','2020-05-19','2020-05-25',120,10,260),
		('2019-04-20','2','2020-05-18','2020-05-25',820,10,260),
		('2019-04-20','3','2020-05-17','2020-05-25',720,10,260) 

INSERT INTO Occupancies ( DateOccupied,
AccountNumber, RoomNumber, RateApplied, PhoneCharge)
	VALUES
		('2019-04-10',1,1,25.5, 10 ),
		('2019-04-10',2,2,25.5, 10),
		('2019-04-10',3,3,25.5, 10)