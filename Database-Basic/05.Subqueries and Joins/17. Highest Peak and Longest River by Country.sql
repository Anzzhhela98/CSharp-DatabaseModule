SELECT TOP(5) 
C.CountryName,
MAX(P.Elevation) AS HighestPeakElevation, 
MAX(R.Length) AS LongestRiverLength 
FROM Countries AS C
LEFT JOIN CountriesRivers AS CR ON CR.CountryCode = C.CountryCode
LEFT JOIN Rivers AS R ON R.Id = CR.RiverId
LEFT JOIN MountainsCountries AS MC ON MC.CountryCode = C.CountryCode
LEFT JOIN Mountains AS M ON M.Id = MC.MountainId
LEFT JOIN Peaks AS P ON P.MountainId = MC.MountainId
GROUP BY C.CountryName
ORDER BY HighestPeakElevation DESC, LongestRiverLength DESC, C.CountryName ASC


