--11. All User Commits

CREATE FUNCTION udf_AllUserCommits(@username VARCHAR(30))
RETURNS INT 
AS
BEGIN
	DECLARE @Result INT 

	SET @Result = (SELECT COUNT(*)
					FROM Users AS U
					JOIN Commits AS C ON C.ContributorId = U.Id
					WHERE U.Username = @username)
	RETURN @Result
END;

SELECT dbo.udf_AllUserCommits('UnderSinduxrein')

--12. Search for Files

CREATE PROCEDURE usp_SearchForFiles(@fileExtension VARCHAR(10))
AS 
BEGIN
	SELECT F.Id , F.Name, CONCAT(F.Size,'KB') AS Size
	FROM   Files AS F
	WHERE  F.Name LIKE '%'+@fileExtension+'%'
	ORDER BY	
			F.Id,
			F.Name,
			F.Size DESC
END

EXEC usp_SearchForFiles 'txt'
