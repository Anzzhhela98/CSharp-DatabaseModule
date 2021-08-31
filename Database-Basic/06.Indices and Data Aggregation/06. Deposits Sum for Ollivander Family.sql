SELECT W.DepositGroup,
SUM(W.DepositAmount) AS TotalSum
FROM WizzardDeposits  AS W
WHERE W.MagicWandCreator = 'Ollivander family'
GROUP BY W.DepositGroup
