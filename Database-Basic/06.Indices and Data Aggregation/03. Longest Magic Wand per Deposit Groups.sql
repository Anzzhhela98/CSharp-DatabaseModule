SELECT W.DepositGroup, 
	MAX(W.MagicWandSize) AS LongestMagicWand 
FROM WizzardDeposits AS W
GROUP BY W.DepositGroup