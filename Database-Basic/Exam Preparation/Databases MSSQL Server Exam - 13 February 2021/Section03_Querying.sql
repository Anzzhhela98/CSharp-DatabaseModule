--05. Commits

SELECT C.Id, C.Message, C.RepositoryId, C.ContributorId
FROM Commits AS C
ORDER BY 
	C.Id ASC, 
	C.Message ASC, 
	C.RepositoryId ASC, 
	C.ContributorId ASC;

--06. Front-end

SELECT F.Id, F.Name, F.Size 
FROM Files AS F
WHERE F.Size > 1000 AND F.Name LIKE '%html%'
ORDER BY
	F.Size DESC,
	F.Name ASC;

--07. Issue Assignment

SELECT I.Id, CONCAT(U.Username,' : ', I.Title) AS IssueAssignee
FROM Issues AS I
JOIN Users  AS U ON U.Id = I.AssigneeId
ORDER BY 
	I.Id DESC,
	I.AssigneeId ASC

--08. Single Files

SELECT FL.Id, FL.Name, CONCAT(FL.Size, 'KB') AS Size 
FROM Files       AS F
RIGHT JOIN Files AS FL ON F.ParentId = FL.Id
WHERE F.ParentId IS NULL
ORDER BY FL.Id, FL.Name, FL.Size DESC

--09. Commits in Repositories

SELECT R.Id,R.Name, COUNT(C.Id) AS Commits
FROM Repositories AS R
JOIN Commits AS C ON C.RepositoryId = R.Id
JOIN RepositoriesContributors AS RC ON RC.RepositoryId = R.Id
GROUP BY R.Id, R.Name
ORDER BY Commits DESC, R.Id , R.Name
SELECT * FROM RepositoriesContributors

--10. Average Size

SELECT U.Username, AVG(F.Size) AS Size
FROM Users AS U
JOIN Commits AS C ON C.ContributorId = U.Id
JOIN Files AS F ON F.CommitId = C.Id
GROUP BY U.Username
ORDER BY 
	Size DESC,
	U.Username ASC