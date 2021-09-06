CREATE PROCEDURE usp_AssignProject(@emloyeeId INT , @projectID INT) 
AS
BEGIN TRANSACTION

DECLARE @maxProjectsAllowed INT = 3; 
DECLARE @countOfprojects INT = 
						(
						SELECT COUNT(*)
						FROM EmployeesProjects  AS E
						WHERE E.EmployeeID = @emloyeeId
					    )

IF(@countOfprojects >= @maxProjectsAllowed)
BEGIN
	ROLLBACK
	RAISERROR('The employee has too many projects!', 16, 1);
	RETURN
END;
INSERT INTO EmployeesProjects (EmployeeID,ProjectID)
	VALUES (@emloyeeId,@projectID)
COMMIT


EXEC usp_AssignProject 2,25