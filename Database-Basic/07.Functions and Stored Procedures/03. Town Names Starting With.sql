CREATE PROCEDURE  usp_GetTownsStartingWith(@Symbol NVARCHAR(50))
AS

BEGIN
DECLARE @CountOfSymbol INT = LEN(@Symbol)
SELECT T.Name as Town
FROM Towns AS T
WHERE SUBSTRING(LOwER(T.Name), 1, @CountOfSymbol) = LOWER(@Symbol)
END

EXECUTE usp_GetTownsStartingWith @Symbol = 'bel'