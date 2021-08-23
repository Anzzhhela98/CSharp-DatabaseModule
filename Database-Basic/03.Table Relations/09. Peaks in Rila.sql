SELECT Mountainrange, Peaks.PeakName,Peaks.Elevation 
FROM Mountains
JOIN Peaks ON Peaks.MountainId=Mountains.ID 
		   AND Mountains.MountainRange='Rila'
ORDER BY Peaks.Elevation DESC;