-- 08.Create Table Users
CREATE TABLE [Users] (
    [Id] INT IDENTITY PRIMARY KEY,
    [Username] VARCHAR  (30) UNIQUE NOT NULL,
    [Password] VARCHAR(30) NOT NULL,
    [ProfilePicture] VARBINARY(MAX)
	CHECK (DATALENGTH(ProfilePicture) <= 900*1024), 
    [LastLoginTime] DATETIME,
    [IsDeleted ] BIT NOT NULL,
)
 
INSERT INTO [Users] ( [Username],[Password], [ProfilePicture],[LastLoginTime],[IsDeleted ])
VALUES
	   ('Vanq1','ASDa23a',NULL,'1998/08/05',0),
	   ('Vanq2','SSF23a',NULL,'1998/08/05',1),
	   ('Vanq3','ASa23a',NULL,'1998/08/05',0),
	   ('Vanq4','ASDa23a1',NULL,'1998/08/05',1),
	   ('Vanq5','ASDa23aa',NULL,'1998/08/05',0)
 
--09.Change Primary Key 
ALTER TABLE [Users]
DROP CONSTRAINT PK__Users__3214EC0714F5301E; 
 
ALTER TABLE [Users]
ADD CONSTRAINT PK__Users__CompositeIdUsername
PRIMARY KEY([Id],[Username])
 
--10. Add Check Constraint 
ALTER TABLE [Users]
ADD CONSTRAINT CK_UserS_PaswordLength
CHECK (LEN([Password])>=5)
 
--11. Set Default Value of a Field 
ALTER TABLE [Users]
ADD CONSTRAINT DF_Users_LastLoginTime
DEFAULT GETDATE() FOR [LastLoginTime]
 
--CHECK
INSERT INTO [Users] ([Username],[Password],[IsDeleted ])
VALUES
	  ('Vanq1GF','ASDa23a',0)
 
--12 Set Unique Field 
ALTER TABLE [Users]
DROP CONSTRAINT PK__Users__CompositeIdUsername
 
ALTER TABLE [Users]
ADD CONSTRAINT PK__Users__IdPrimaryKey
PRIMARY KEY([Id])
 
ALTER TABLE [Users]
ADD CONSTRAINT CK_Username_Length
CHECK (LEN([Username])>=3)
 
SELECT * FROM [Users]