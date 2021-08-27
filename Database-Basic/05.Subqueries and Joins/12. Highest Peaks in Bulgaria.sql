SELECT C.CountryCode, MountainRange, PeakName, P.Elevation
FROM Peaks AS P
JOIN Mountains AS M ON M.Id = P.MountainId  
JOIN MountainsCountries AS MC ON MC.MountainId = M.Id
JOIN Countries AS C ON C.CountryCode = MC.CountryCode
WHERE P.Elevation > 2835 AND C.CountryCode = 'BG'
ORDER BY P.Elevation DESC