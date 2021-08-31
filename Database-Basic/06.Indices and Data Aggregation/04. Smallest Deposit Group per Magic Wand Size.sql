SELECT TOP(2) W.DepositGroup	
FROM WizzardDeposits AS W
GROUP BY W.DepositGroup
ORDER BY AVG(W.MagicWandSize)