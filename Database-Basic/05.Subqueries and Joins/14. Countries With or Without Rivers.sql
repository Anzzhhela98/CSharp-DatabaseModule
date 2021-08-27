SELECT TOP(5) C.CountryName, R.RiverName
FROM Rivers AS R
RIGHT JOIN CountriesRivers AS CR ON CR.RiverId = R.Id
RIGHT JOIN Countries AS C ON C.CountryCode = CR.CountryCode
WHERE C.ContinentCode = 'AF'
ORDER BY C.CountryName ASC