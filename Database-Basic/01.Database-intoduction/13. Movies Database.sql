CREATE TABLE [Directors] (
    [Id] INT IDENTITY PRIMARY KEY,
    [DirectorName] NVARCHAR(100) NOT NULL,
    [Notes] NVARCHAR(200),
)

CREATE TABLE [Genres] (
    [Id] INT IDENTITY PRIMARY KEY,
    [GenreName] NVARCHAR(100) NOT NULL,
    [Notes] NVARCHAR(200),
)
CREATE TABLE [Categories] (
    [Id] INT IDENTITY PRIMARY KEY,
    [CategoryName] NVARCHAR(100) NOT NULL,
    [Notes] NVARCHAR(200),
)

CREATE TABLE [Movies] (
    [Id] INT IDENTITY PRIMARY KEY,
    [Title] NVARCHAR(100) NOT NULL,
    [DirectorId] INT FOREIGN KEY REFERENCES Directors(Id),
    [CopyrightYear] INT NOT NULL,
    [Length] TIME NOT NULL,
    [GenreId] INT FOREIGN KEY REFERENCES Genres(Id),
    [CategoryId]  INT FOREIGN KEY REFERENCES Categories(Id),
    [Rating] DECIMAL(3,1) NOT NULL,
  [Notes] NVARCHAR(200),
)

INSERT INTO [Directors] ([DirectorName],[Notes])
VALUES
('PESHO',NULL),
('MIRA',NULL),
('DIDA',NULL),
('QNA',NULL),
('IVAN',NULL)

INSERT INTO [Categories]([CategoryName],[Notes])
VALUES
('Feature films',NULL),
('Historical films',NULL),
('Documentary films',NULL),
('Biographical films',NULL),
('Silent films',NULL)

INSERT INTO [Genres] ([GenreName],[Notes])
VALUES
('Action',NULL),
('Comedy',NULL),
('Fantasy',NULL),
('Mystery',NULL),
('Romance',NULL)

INSERT INTO [Movies] ([Title],[DirectorId],[CopyrightYear],[Length],[GenreId],[CategoryId],
[Rating])
VALUES
       ('Blazing Saddles',1,1862,'1:45:00',5, 1, 9.2 ),
		('Phantasm',2,1234,'1:45:00',1,2,7.5),
		('Stir Crazy',3,1785,'1:45:00',3,3,7.9),
		('Jurassic Park',4,1899,'1:45:00',4,4,10.9),
		('West Side Story',5,1254,'1:45:00',2,5,7.9)