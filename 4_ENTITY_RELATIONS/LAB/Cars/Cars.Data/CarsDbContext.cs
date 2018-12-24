using Cars.Data.Models;
using Cars.Data.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Cars.Data
{
    public class CarsDbContext :DbContext
    {
        public CarsDbContext()
        {
        }
        public CarsDbContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<DealerShip> DealerShips { get; set; }

        public DbSet<Engine> Engines { get; set; }

        public DbSet<LicensePlate> LicensePlates { get; set; }

        public DbSet<Make> Makes { get; set; }

        public DbSet<CarDealerShip> CarDealerShips { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
             builder.ApplyConfiguration(new CarDealerShipConfig());

             builder.ApplyConfiguration(new CarConfig());

             builder.ApplyConfiguration(new EngineConfig());                     
        }
    }
}
