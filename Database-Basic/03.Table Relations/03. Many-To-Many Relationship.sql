CREATE TABLE [Students] (
 [StudentID] INT IDENTITY PRIMARY KEY,
 [Name] NVARCHAR(30)NOT NULL)

CREATE TABLE [Exams](
 [ExamID] INT IDENTITY(101,1) PRIMARY KEY,
 [Name] NVARCHAR(30)NOT NULL)
 
CREATE TABLE [StudentsExams](
 [StudentID]  INT REFERENCES [Students]([StudentID]), 
 [ExamID] INT REFERENCES [Exams]([ExamID]),
 PRIMARY KEY (StudentID, ExamID))

INSERT INTO [Students]
VALUES 
('Mila'),
('Tonitoo'),
('Ron')

INSERT INTO [Exams]
VALUES 
('SpringMVC'),
('Neo4jaaa'),
('Oracle 11gaaaa')

INSERT INTO [StudentsExams]
VALUES 
(1,101),
(1,102),
(2,101),
(3,103),
(2,102),
(2,103)

--SELECT * FROM [Students]
--SELECT * FROM [Exams]
--SELECT * FROM [StudentsExams]
