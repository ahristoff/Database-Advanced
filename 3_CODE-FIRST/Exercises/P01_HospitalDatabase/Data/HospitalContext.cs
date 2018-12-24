using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext :DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions options)
            :base(options)
        {           
        }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<PatientMedicament> Prescriptions { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

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
            builder.Entity<Patient>()
                .HasMany(d => d.Diagnoses)
                .WithOne(p => p.Patient)
                .HasForeignKey(c => c.PatientId);

            builder.Entity<Patient>()
                .HasMany(v => v.Visitations)
                .WithOne(p => p.Patient)
                .HasForeignKey(c => c.PatientId);

            builder.Entity<Patient>()
                .Property(c => c.FirstName)
                .HasMaxLength(50);
            builder.Entity<Patient>()
               .Property(c => c.LastName)
               .HasMaxLength(50);
            builder.Entity<Patient>()
               .Property(c => c.Address)
               .HasMaxLength(250);
            builder.Entity<Patient>()
               .Property(c => c.FirstName)
               .HasMaxLength(50);
            builder.Entity<Patient>()
               .Property(c => c.Email)
               .HasMaxLength(50)
               //.HasColumnType("char")
               .IsUnicode(false);   


            builder.Entity<PatientMedicament>()
                .HasKey(pm => new { pm.PatientId, pm.MedicamentId });

            builder.Entity<PatientMedicament>()
                .HasOne(pm => pm.Patient)
                .WithMany(pm => pm.Prescriptions)
                .HasForeignKey(pm => pm.PatientId);

            builder.Entity<PatientMedicament>()
                .HasOne(pm => pm.Medicament)
                .WithMany(pm => pm.Prescriptions)
                .HasForeignKey(pm => pm.MedicamentId);


            builder.Entity<Doctor>()
                .HasMany(v => v.Visitations)
                .WithOne(d => d.Doctor)
                .HasForeignKey(c => c.DoctorId);
        }
    }
}
