--SELECT Name as name, COUNT(mv.MinionId)as count FROM Minions as m
--join MinionsVillains as mv on mv.MinionId = m.Id

--group by Name

SELECT Name as name From Minions;
SELECT COUNT(*) as count FROM Minions