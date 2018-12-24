﻿using System.Collections.Generic;

namespace P03_FootballBetting.Data.Models
{
    public class Color
    {
        public int ColorId { get; set; }
        public string Name { get; set; }

        public ICollection<Team> PrimaryKitTeams { get; set; } //1,2
        public ICollection<Team> SecondaryKitTeams { get; set; } //1,2
    }
}
