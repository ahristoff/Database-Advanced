SELECT m.Name as Mname, m.Age as age, t.Name as Tname FROM Minions AS m
join Towns as t on t.Id = m.TownId
where m.Name = @mname and m.Age =@age and t.Name = @tname