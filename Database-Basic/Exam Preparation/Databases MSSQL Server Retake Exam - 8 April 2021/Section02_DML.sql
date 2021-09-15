--02. Insert

INSERT INTO Employees 
VALUES 
('Marlo',  'O''Malley',1958-9-21,  NULL,1),
('Niki',   'Stanaghan',1969-11-26, NULL,4),
('Ayrton', 'Senna',    1960-03-21, NULL,9),
('Ronnie', 'Peterson', 1944-02-14, NULL,9),
('Giovanna','Amati',   1959-07-20, NULL,5)

INSERT INTO Reports 
VALUES 
(1, 1, 2017-04-13, NULL, 'Stuck Road on Str.133',     6,2),
(6, 3, 2015-09-05, 2015-12-06,'Charity trail running',3,5),
(14,2, 2015-09-07, NULL,'Falling bricks on Str.58',   5,2),
(4,3,  2017-07-03, 2017-07-06,'Cut off streetlight on Str.11', 1, 1)

--03. Update
UPDATE Reports
SET CloseDate = GETDATE()
WHERE CloseDate IS NULL

--04. Delete
DELETE 
FROM Reports WHERE [StatusId] = 4;