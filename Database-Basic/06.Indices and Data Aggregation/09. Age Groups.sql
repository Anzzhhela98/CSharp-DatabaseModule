SELECT AllAge AS AgeGroup, 
 COUNT(*) AS WizardCount
 FROM (SELECT 
	CASE
	    WHEN W.Age >= 0  AND W.Age <= 10 THEN '[0-10]'
	    WHEN W.Age >= 11 AND W.Age <= 20 THEN '[11-20]' 
	    WHEN W.Age >= 21 AND W.Age <= 30 THEN '[21-30]'
	    WHEN W.Age >= 31 AND W.Age <= 40 THEN '[31-40]'
	    WHEN W.Age >= 41 AND W.Age <= 50 THEN '[41-50]'
	    WHEN W.Age >= 51 AND W.Age <= 60 THEN '[51-60]'
	    ELSE '[61+]'
	END AS AllAge 
	FROM WizzardDeposits AS W) AS DataAge
GROUP BY AllAge