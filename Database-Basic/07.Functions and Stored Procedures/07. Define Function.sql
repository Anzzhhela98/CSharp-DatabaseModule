CREATE FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(20), @word NVARCHAR(20)) 
RETURNS BIT
AS 
BEGIN
DECLARE @CountOfLetters INT = LEN(@word);
DECLARE @Index INT = 1;

	WHILE @Index <= @CountOfLetters
		BEGIN 
		IF(CHARINDEX(SUBSTRING(@word, @Index, 1), @setOfLetters) = 0)
		    BEGIN
			RETURN 0
			END
			SET @Index+=1
		END 
		RETURN 1
END

CREATE TABLE Demo
	(SetOfLetters NVARCHAR(20) NOT NULL ,
	 Word  NVARCHAR(20) NOT NULL)

INSERT INTO Demo
VALUES
('oistmiahf' ,'Sofia'),
('oistmiahf ' ,'halves '),
('bobr' ,'Rob')

SELECT D.SetOfLetters, D.Word, dbo.ufn_IsWordComprised(D.SetOfLetters,d.Word)
FROM DEMO AS D
