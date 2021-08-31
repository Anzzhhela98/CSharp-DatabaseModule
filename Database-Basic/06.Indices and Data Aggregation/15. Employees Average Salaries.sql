SELECT * 
INTO EmployeeOver30000Salary
FROM Employees AS E
WHERE E.Salary > 30000

DELETE FROM EmployeeOver30000Salary 
WHERE  ManagerID = 42

UPDATE EmployeeOver30000Salary
SET Salary += 5000
WHERE DepartmentID = 1 

SELECT E.DepartmentID,
AVG(E.Salary)
FROM EmployeeOver30000Salary AS E 
GROUP BY DepartmentID
