use MinionsDB

SELECT Name, Age FROM Minions as m
join MinionsVillains as mv on mv.MinionId = m.Id
where mv.VillainId = @villainId
