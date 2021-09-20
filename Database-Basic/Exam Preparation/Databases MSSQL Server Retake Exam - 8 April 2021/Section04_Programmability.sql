--11.	Hours to Complete
CREATE FUNCTION udf_HoursToComplete(@StartDate DATETIME, @EndDate DATETIME)
RETURNS INT 
AS 
BEGIN
	DECLARE @Result INT
		IF @StartDate IS NULL OR @EndDate IS NULL
	BEGIN 
		SET @Result = 0;
	END
		ELSE 
	BEGIN
		SET @Result = DATEDIFF(HOUR, @StartDate, @EndDate)
	END

	RETURN @Result;
END

--SELECT dbo.udf_HoursToComplete(OpenDate, CloseDate) AS TotalHours
--FROM Reports

--12.	Assign Employee

CREATE PROCEDURE usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT)
AS
	DECLARE @EmployeeDep INT = (SELECT E.DepartmentId
								FROM Employees AS E
								WHERE E.Id = @EmployeeId)

	DECLARE @Report INT = (SELECT C.DepartmentId
						  FROM Reports AS R
						   JOIN Categories AS C ON C.Id = R.CategoryId
						  WHERE R.Id = @ReportId )

	IF @EmployeeDep != @Report
	THROW 50005,'Employee doesn''t belong to the appropriate department!', 1;
	
UPDATE Reports 
SET EmployeeId = @EmployeeId
	WHERE Id = @ReportId
GO

--EXEC usp_AssignEmployeeToReport 17, 2