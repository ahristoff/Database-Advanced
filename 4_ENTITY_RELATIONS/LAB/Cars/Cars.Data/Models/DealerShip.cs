using System.Collections.Generic;

namespace Cars.Data.Models
{
    public class DealerShip
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CarDealerShip> CarDealerShips { get; set; } = new List<CarDealerShip>();
    }
}
