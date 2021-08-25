SELECT [PeakName],[RiverName],
 LOWER([PeakName] + SUBSTRING([RiverName],2,LEN([RiverName]))) 
 AS    [Mix]
FROM   [Peaks],[Rivers]
WHERE  LEFT(RiverName,1) = RIGHT(PeakName,1)
 ORDER BY [Mix]