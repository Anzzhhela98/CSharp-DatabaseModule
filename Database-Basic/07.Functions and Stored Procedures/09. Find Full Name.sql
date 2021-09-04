CREATE PROCEDURE usp_GetHoldersFullName
AS
BEGIN
SELECT CONCAT(A.FirstName, ' ', A.LastName)
FROM AccountHolders AS A
END

EXECUTE usp_GetHoldersFullName
