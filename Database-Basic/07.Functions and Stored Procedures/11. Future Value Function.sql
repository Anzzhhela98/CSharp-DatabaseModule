CREATE FUNCTION ufn_CalculateFutureValue( @InitalSum          DECIMAL(18,4),
										  @YearlyInterestRate FLOAT,
										  @NumberOfYears      DECIMAL(10,2))
RETURNS DECIMAL (18,4)
BEGIN 
	DECLARE @Resul DECIMAL(18,4)
	SELECT @Resul = @InitalSum * (POWER((1 + @YearlyInterestRate),@NumberOfYears))
	RETURN @Resul
END
											
SELECT dbo.ufn_CalculateFutureValue(1000,0.1,5)