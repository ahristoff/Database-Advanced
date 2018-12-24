using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cars.Data.Models.Configurations
{
    public class CarConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder                //.Entity<Car>                   //one -> many
                .HasOne(c => c.Make)            
                .WithMany(e => e.Cars)
                .HasForeignKey(e => e.MakeId);

            builder              //.Entity<Car>                      //one -> one
               .HasOne(p => p.LicensePlate)
               .WithOne(c => c.Car)
               .HasForeignKey<Car>(f => f.LicensePlateId);

            //builder              //.Entity<Car>                     
            //   .HasOne(p => p.LicensePlate)
            //   .WithOne(c => c.Car)
            //   .HasForeignKey<LicensePlate>(f => f.CarId);  
            //if in LicensePlate - public int? CarId { get; set; }
        }
    }
}
