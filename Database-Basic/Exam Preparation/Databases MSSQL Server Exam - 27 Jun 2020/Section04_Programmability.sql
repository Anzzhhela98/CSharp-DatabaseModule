	--12. Cost of Order

CREATE  FUNCTION udf_GetCost(@JobId INT)
RETURNS DECIMAL(18,2)
AS
BEGIN
	DECLARE @Result DECIMAL(18,2) =
	        (
			 SELECT SUM(Price)
		     FROM  Parts      AS P
			 JOIN  OrderParts AS OP ON OP.PartId = P.PartId
			 JOIN  Orders     AS O  ON O.OrderId = OP.OrderId
			 JOIN  Jobs       AS J  ON O.JobId   = J.JobId
			 WHERE J.JobId = @JobId
			 )
	RETURN ISNULL(@Result,0)
END

SELECT dbo.udf_GetCost(1)

CREATE OR ALTER PROCEDURE usp_PlaceOrder
(@JobId INT, @PartSerialNumber VARCHAR(50), @Quantity INT)
AS
BEGIN
    BEGIN TRANSACTION
	IF(@Quantity <= 0)
		THROW 50012, 'Part quantity must be more than zero!', 1 

	IF(@JobId IN ((SELECT JobId FROM Jobs WHERE Status LIKE 'Finished')))
		THROW 50011, 'This job is not active!',1

	IF (@JobId NOT IN (SELECT JobId FROM Jobs))
        THROW 50013, 'Job not found!',1;

	IF(@PartSerialNumber NOT IN (SELECT SerialNumber FROM Parts))
		THROW 50014, 'Part not found!',1

	IF (@JobId IN (SELECT JobId FROM Orders) AND (SELECT IssueDate FROM Orders WHERE JobId = @JobId) IS NULL)
		BEGIN
		DECLARE @OrderId INT  = (SELECT OrderId FROM Orders WHERE JobId = @JobId AND IssueDate IS NULL)
		DECLARE @PartId  INT  = (SELECT PartId  FROM Parts  WHERE SerialNumber = @PartSerialNumber)

	  IF (@OrderId IN (SELECT OrderId FROM OrderParts) AND @PartId IN (SELECT PartId FROM OrderParts))
		BEGIN
              UPDATE OrderParts
              SET Quantity += @Quantity
              WHERE OrderId = @OrderId AND PartId = @PartId
		END

	  ELSE
	   BEGIN
			  INSERT INTO OrderParts(OrderId, PartId, Quantity)
              VALUES (@OrderId, @PartId, @Quantity)
	   END

     END
COMMIT
END

DECLARE @err_msg AS NVARCHAR(MAX);
BEGIN TRY
    EXEC usp_PlaceOrder 1, 'MINUSQUANTITY', -1
END TRY
BEGIN CATCH
    SET @err_msg = ERROR_MESSAGE();
    SELECT @err_msg
END CATCH

