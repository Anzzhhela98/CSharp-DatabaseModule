SELECT ContinentCode,CurrencyCode, Total AS CurrencyUsage 
FROM (SELECT ContinentCode,CurrencyCode,
COUNT(*) AS Total,
DENSE_RANK() OVER (PARTITION BY ContinentCode Order by COUNT(*) DESC ) AS Ranked
FROM Countries 
GROUP BY ContinentCode, CurrencyCode) AS K
WHERE Ranked = 1 AND Total > 1
ORDER BY ContinentCode,CurrencyCode