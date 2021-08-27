SELECT MC.CountryCode ,
COUNT(*)
FROM Mountains AS M
JOIN MountainsCountries AS MC ON MC.MountainId = M.Id
WHERE MC.CountryCode ='BG' OR 
	  MC.CountryCode ='US' OR 
	  MC.CountryCode = 'RU'
GROUP BY MC.CountryCode