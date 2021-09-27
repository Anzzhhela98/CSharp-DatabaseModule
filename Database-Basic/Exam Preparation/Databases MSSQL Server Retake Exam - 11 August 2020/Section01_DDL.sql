CREATE DATABASE Bakery

CREATE TABLE Countries
(
Id     INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Customers
(
Id          INT PRIMARY KEY IDENTITY,
FirstName   VARCHAR(25) NOT NULL,
LastName    VARCHAR(25) NOT NULL,
Gender          CHAR(1) NOT NULL 
				CHECK(Gender ='M' OR Gender ='F'),
Age		    INT         NOT NULL,
PhoneNumber VARCHAR(10) NOT NULL,
CountryId   INT NOT NULL REFERENCES Countries(Id)
);

CREATE TABLE Products
(
Id            INT PRIMARY KEY IDENTITY,
[Name]        VARCHAR(25)   NOT NULL UNIQUE,
[Description] VARCHAR(250)  NOT NULL, 
Recipe        VARCHAR(max)	NOT NULL,				
Price		  MONEY         NOT NULL		
					CHECK(Price >= 0)
);

CREATE TABLE Feedbacks
(
Id            INT PRIMARY KEY IDENTITY,
[Description] VARCHAR(255), 
Rate          DECIMAL(18,2)  NOT NULL
					CHECK(Rate >= 0 AND Rate <= 10),
ProductId	  INT NOT NULL REFERENCES Products(Id),
CustomerId	  INT NOT NULL REFERENCES Customers(Id)
);

CREATE TABLE Distributors
(
Id            INT PRIMARY KEY IDENTITY,
[Name]        VARCHAR(25)   NOT NULL UNIQUE, 
AddressText   VARCHAR(30)   NOT NULL,
Summary       VARCHAR(200)  NOT NULL,
CountryId	  INT NOT NULL REFERENCES Countries(Id)
);

CREATE TABLE Ingredients
(
Id              INT PRIMARY KEY IDENTITY,
[Name]          VARCHAR(30)    NOT NULL, 
[Description]   VARCHAR(200)   NOT NULL,
OriginCountryId INT NOT NULL REFERENCES Countries(Id),
DistributorId	INT NOT NULL REFERENCES Distributors(Id)
);

CREATE TABLE ProductsIngredients
(
ProductId       INT NOT NULL REFERENCES Products(Id),
IngredientId	INT NOT NULL REFERENCES Ingredients(Id)
			PRIMARY KEY (ProductId, IngredientId)
);
