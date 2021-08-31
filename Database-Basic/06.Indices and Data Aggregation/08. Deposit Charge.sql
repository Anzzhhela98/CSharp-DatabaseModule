SELECT W.DepositGroup, W.MagicWandCreator,
  MIN(W.DepositCharge) AS MinDepositCharge
FROM  WizzardDeposits  AS W
GROUP BY  W.DepositGroup, W.MagicWandCreator
ORDER BY W.MagicWandCreator, W.DepositGroup ASC