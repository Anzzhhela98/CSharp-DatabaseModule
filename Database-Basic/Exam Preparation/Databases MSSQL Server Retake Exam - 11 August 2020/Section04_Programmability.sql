--11. Customers With Countries

CREATE VIEW v_UserWithCountries AS
 SELECT CONCAT(C.FirstName, ' ', C.LastName) AS CustomerName, 
		C.Age, 
		C.Gender, 
		CO.Name AS CountryName 
 FROM Customers AS C
 JOIN Countries AS CO ON CO.Id = C.CountryId

 --12. Delete Products

 CREATE TRIGGER tr_DeleteProducts
  ON Products
  INSTEAD OF DELETE AS 
  BEGIN 
	DECLARE @DeletedProducts INT = 
		(SELECT P.Id
			  FROM Products AS P 
					JOIN deleted AS D
						ON D.Id = P.Id)
	DELETE 
		FROM Feedbacks
		WHERE ProductId = @DeletedProducts

	DELETE
        FROM ProductsIngredients
        WHERE ProductId = @deletedProducts

    DELETE Products
        WHERE Id = @deletedProducts
END
  
  DELETE FROM Products WHERE Id = 7