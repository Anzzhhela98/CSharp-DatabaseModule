--05. Products By Price

SELECT P.Name, P.Price, P.Description
FROM   Products AS P
ORDER BY
	P.Price DESC,
	P.Name  ASC

--06. Negative Feedback

SELECT F.ProductId, F.Rate, F.Description, F.CustomerId, C.Age, C.Gender
FROM Feedbacks AS F
JOIN Customers AS C ON C.Id = F.CustomerId
WHERE F.Rate < 5.0
ORDER BY	
	F.ProductId DESC,
	F.Rate ASC

--07. Customers without Feedback
SELECT CONCAT(C.FirstName,' ', C.LastName) AS CustomerName, C.PhoneNumber, C.Gender
FROM Customers AS C
 LEFT JOIN Feedbacks AS F ON F.CustomerId = C.Id
WHERE F.CustomerId IS NULL
ORDER BY 
	C.Id ASC

--08. Customers by Criteria

SELECT C.FirstName, C.Age, C.PhoneNumber
FROM Customers AS C
JOIN Countries  AS CN ON CN.Id = C.CountryId
WHERE (C.Age >= 21 AND C.FirstName LIKE '%an%') OR 
	  (C.PhoneNumber LIKE '%38' AND CN.Name != 'Greece')
ORDER BY	
	  C.FirstName ASC,
	  C.Age		  DESC

--09. Middle Range Distributors

SELECT DistributorName, IngredientName, ProductName, AverageRate
FROM
(SELECT D.Name AS DistributorName,
       I.Name AS IngredientName,
       P.Name AS ProductName,
  AVG(F.Rate) AS AverageRate
FROM Distributors AS D
JOIN Ingredients  AS I ON I.DistributorId = D.Id
JOIN ProductsIngredients PI ON I.Id = PI.IngredientId
JOIN Products			  P ON P.Id = PI.ProductId
JOIN Feedbacks			  F ON P.Id =  F.ProductId
GROUP BY D.Name, I.Name, P.Name) AS Result
WHERE AverageRate BETWEEN 5.0 AND 8.0 
ORDER BY 
	DistributorName ASC,	
	IngredientName  ASC,
	ProductName     ASC

--10. Country Representative


