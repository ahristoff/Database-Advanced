
namespace Cars.Data.Models
{
    public class CarDealerShip
    {
        public int CarId { get; set; }

        public Car Car { get; set; }


        public int DealerShipId { get; set; }

        public DealerShip DealerShip { get; set; }
    }
}
