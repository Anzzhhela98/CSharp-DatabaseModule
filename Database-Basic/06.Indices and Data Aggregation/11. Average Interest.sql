SELECT W.DepositGroup,
	   W.IsDepositExpired,
AVG(DepositInterest) AS AverageInterest
FROM WizzardDeposits AS W
WHERE W.DepositStartDate > '1/1/1985'
GROUP BY DepositGroup, IsDepositExpired
ORDER BY DepositGroup DESC,IsDepositExpired ASC