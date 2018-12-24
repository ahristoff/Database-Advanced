USE MinionsDB

SELECT t.Name FROM Towns as t
join Countries as c on c.Id = t.CountryId
WHERE c.Name = @Country