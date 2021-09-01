CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS NVARCHAR(10)
AS 
BEGIN
DECLARE @result NVARCHAR(10)
	IF(@salary < 30000)
	 SET @result = 'Low'
	ELSE IF(@salary >= 30000 AND @salary <= 50000)
	 SET @result = 'Average'
	ELSE
	 SET @result = 'High'
	 RETURN @result
END;

SELECT E.Salary, dbo.ufn_GetSalaryLevel(e.Salary)
FROM Employees AS E

