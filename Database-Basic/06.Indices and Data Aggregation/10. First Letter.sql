SELECT FirstLetter 
	FROM(SELECT SUBSTRING(W.FirstName,1,1) AS FirstLetter
FROM WizzardDeposits AS W
WHERE W.DepositGroup = 'Troll Chest') AS AllFirstLetters
GROUP BY FirstLetter