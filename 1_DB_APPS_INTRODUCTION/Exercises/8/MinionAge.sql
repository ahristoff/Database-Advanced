SELECT Name, Age, COUNT(*)AS Num FROM Minions
GROUP BY Name, Age
--where Age = @age