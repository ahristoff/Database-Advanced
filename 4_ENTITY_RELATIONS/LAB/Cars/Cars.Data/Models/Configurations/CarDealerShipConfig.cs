using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cars.Data.Models.Configurations
{
    public class CarDealerShipConfig : IEntityTypeConfiguration<CarDealerShip>
    {
        public void Configure(EntityTypeBuilder<CarDealerShip> builder)
        {
            builder                //.Entity<CarDealerShip>                      // many -> many
               .HasKey(cd => new { cd.CarId, cd.DealerShipId });

            builder                  //.Entity<CarDealerShip>   
                .HasOne(cd => cd.Car)
                .WithMany(c => c.CarDealerShips)
                .HasForeignKey(c => c.CarId);

            builder                  //.Entity<CarDealerShip>   
                 .HasOne(cd => cd.DealerShip)
                 .WithMany(c => c.CarDealerShips)
                 .HasForeignKey(c => c.DealerShipId);
        }
    }
}
