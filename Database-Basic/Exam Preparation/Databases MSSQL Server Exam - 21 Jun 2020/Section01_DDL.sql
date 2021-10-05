CREATE DATABASE TripService
CREATE TABLE Cities
(
Id			INT  PRIMARY KEY IDENTITY,
[Name]		NVARCHAR(20)    NOT NULL,
CountryCode CHAR(2)        NOT NULL
)

CREATE TABLE Hotels
(
Id			  INT  PRIMARY KEY IDENTITY,
[Name]		  NVARCHAR(30)    NOT NULL,
CityId        INT REFERENCES Cities(Id) NOT NULL  ,
EmployeeCount INT           NOT NULL,
BaseRate      DECIMAL(15,2)  
)

CREATE TABLE Rooms
(
Id			 INT PRIMARY KEY IDENTITY,
Price        DECIMAL(15,2)   NOT NULL,
[Type]		 NVARCHAR(20)    NOT NULL,
Beds		 INT             NOT NULL,
HotelId      INT REFERENCES Hotels(Id) NOT NULL 
)

CREATE TABLE Trips
(
Id			 INT	  PRIMARY KEY IDENTITY,
RoomId       INT      REFERENCES Rooms(Id) NOT NULL,
BookDate	 DATETIME NOT NULL,
ArrivalDate	 DATETIME NOT NULL,
ReturnDate	 DATETIME NOT NULL, 
CancelDate	 DATETIME
)

CREATE TABLE Accounts
(
Id			 INT PRIMARY KEY IDENTITY,
FirstName	 NVARCHAR(50)    NOT NULL,
MiddleName	 NVARCHAR(20),
LastName	 NVARCHAR(50)    NOT NULL,
CityId       INT REFERENCES Cities(Id) NOT NULL,
BirthDate	 DATETIME        NOT NULL, 
Email	     NVARCHAR(100)   UNIQUE  NOT NULL 
)

CREATE TABLE AccountsTrips
(
AccountId    INT NOT NULL FOREIGN KEY REFERENCES Accounts(Id),
TripId       INT NOT NULL FOREIGN KEY REFERENCES Trips(Id),
Luggage	     INT CHECK(Luggage >= 0) NOT NULL,
		                  PRIMARY KEY (AccountId,TripId)
)