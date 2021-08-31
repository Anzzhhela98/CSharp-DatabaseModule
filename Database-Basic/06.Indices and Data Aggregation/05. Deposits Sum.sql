SELECT W.DepositGroup,
SUM(W.DepositAmount) AS TotalSum
FROM WizzardDeposits AS W
GROUP BY W.DepositGroup