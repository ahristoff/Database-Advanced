SELECT v.Name as Name, Count(mv.MinionId)as Number FROM 
Villains as v join MinionsVillains as mv on mv.VillainId = v.Id 
group by v.Name
Having Count(mv.MinionId) > 3 
order by Number DESC