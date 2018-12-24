use MinionsDB



CREATE TABLE EvilnessFactors(
Id INT PRIMARY KEY IDENTITY,
Name NVARCHAR(10) CHECK(Name IN ('super good', 'good', 'bad', 'evil', 'super evil')) NOT NULL
)

CREATE TABLE Villains(
Id INT PRIMARY KEY IDENTITY,
Name NVARCHAR(50) NOT NULL,
EvilnessFactorsiD INT FOREIGN KEY REFERENCES EvilnessFactors(Id)
)

CREATE TABLE Countries(
Id INT PRIMARY KEY IDENTITY,
Name NVARCHAR(50) NOT NULL
)

CREATE TABLE Towns(
Id INT PRIMARY KEY IDENTITY,
Name NVARCHAR(50) NOT NULL,
CountryId INT FOREIGN KEY REFERENCES Countries(Id)
)

CREATE TABLE Minions(
Id INT PRIMARY KEY IDENTITY,
Name NVARCHAR(50) NOT NULL,
Age INT NOT NULL,
TownId INT FOREIGN KEY REFERENCES Towns(Id)
)

CREATE TABLE MinionsVillains(
MinionId INT FOREIGN KEY REFERENCES Minions(Id),
VillainId INT FOREIGN KEY REFERENCES Villains(Id),
CONSTRAINT pk_MinionsVillains
PRIMARY KEY(MinionId, VillainId)
)


use MinionsDB 
insert into Countries(Name) values
('Bulgaria'),
('Germany'),
('Spain'),
('Italy'),
('Austria')
insert into Towns(Name, CountryId) Values
('Sofia', 1),
('Berlin', 2),
('Madrid', 3),
('Rome', 4),
('Wien', 5)

insert into Minions(Name, Age, TownId) Values
('Tom', 11, 2),
('Bob', 12, 3),
('Stew', 8, 4),
('Alex', 9, 5),
('Hary',15 , 1)
insert into EvilnessFactors(Name) VALUES
('super evil'),
('evil'),
('bad'),
('good'),
('super good')
insert into Villains(Name,EvilnessFactorsiD) VALUES
('Gosho', 1),
('Pesho', 2),
('Tosho', 3),
('Simo', 4),
('Krasi',5)



insert into MinionsVillains(VillainId, MinionId) values
(1, 5),
(1, 4),
(1, 3),
(1, 2),
(1, 1),
(2, 1),
(3, 2),
(3, 3),
(3, 4),
(3, 5),
(3, 1),
(4, 2),
(5, 3)






