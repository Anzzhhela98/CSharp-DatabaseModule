SELECT[TownID],[Name]
FROM  [Towns]
WHERE [Name] NOT LIKE 'R%' AND 
	  [Name] NOT LIKE 'D%' AND
	  [Name] NOT LIKE 'B%'
	  ORDER BY [Name] ASC 