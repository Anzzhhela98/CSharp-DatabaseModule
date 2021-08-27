SELECT COUNT(*) AS Count 
FROM Continents AS C
LEFT JOIN Countries AS CO ON CO.ContinentCode = C.ContinentCode
LEFT JOIN MountainsCountries AS MC ON MC.CountryCode = CO.CountryCode
LEFT JOIN Mountains AS M ON M.Id = MC.MountainId
WHERE M.Id IS NULL