using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Linq;
using System.Text;

namespace P01_HospitalDatabase
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            using (var context = new HospitalContext())
            {
                Restart(context);

                var patients = context.Patients
                    .Select(p => new
                    {
                        p.FirstName,
                        p.LastName,
                        p.Address,
                        p.Email,
                        p.HasInsurance,
                        Diagnoses = p.Diagnoses.Select(c => new
                        {
                            c.Name,
                            c.Comments
                        }),
                        Visitations = p.Visitations.Select(v => new
                        {
                            v.Comments,
                            v.Doctor
                        }),
                        Prescriptions = p.Prescriptions.Select(pm => new
                        {
                            pm.Medicament
                        }).ToArray()
                    });

                foreach (var x in patients)
                {
                    Console.WriteLine($"Name: {x.FirstName} {x.LastName}");
                    Console.WriteLine($"Address: {x.Address}");
                    Console.WriteLine($"Email: {x.Email}");
                    Console.WriteLine($"IsInsurance: {x.HasInsurance}");
                    Console.WriteLine($"Diagnoses: ");

                    foreach (var y in x.Diagnoses)
                    {
                        Console.WriteLine($"--{y.Name} -> {y.Comments}");
                    }

                    Console.WriteLine($"Doctor: ");
                    foreach (var z in x.Visitations)
                    {
                        Console.WriteLine($"----{z.Doctor.Name} -> {z.Comments}");
                    }

                    Console.WriteLine($"Medicament: ");
                    foreach (var r in x.Prescriptions)
                    {
                        Console.WriteLine($"------{r.Medicament.Name}");
                    }

                    Console.WriteLine("===================================================================");
                }
            }
        }

        private static void Restart(HospitalContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Seed(context);
        }

        private static void Seed(HospitalContext context)
        {
            var patients = new[]
            {
               new Patient
               {
                   FirstName = "Paul",
                   LastName = "Jones",
                   Address = "New York",
                   Email = "paulj",
                   HasInsurance = true
               },
               new Patient
               {
                   FirstName = "Jan",
                   LastName = "Clark",
                   Address = "Seatle",
                   Email = "Jonny",
                   HasInsurance = true
               },
               new Patient
               {
                   FirstName = "Pit",
                   LastName = "Holms",
                   Address = "LA",
                   Email = "pity",
                   HasInsurance = false
               }
           };
            context.Patients.AddRange(patients);

            var doctors = new []
            {
                new Doctor { Name = "Pesho", Specialty = "brain surgeon"},
                new Doctor { Name = "Gosho", Specialty = "gynecologist"},
                new Doctor { Name = "Ivancho", Specialty = "traumatologist"}
            };
            context.Doctors.AddRange(doctors);

            var medicaments = new[]
            {
                new Medicament{Name = "Aspirin"},
                new Medicament{Name = "Viagra"},
                new Medicament{Name = "Antibiotic"},
            };
            context.Medicaments.AddRange(medicaments);           

            var patientmedicament = new[]
            {
                new PatientMedicament{ Patient = patients[0], Medicament = medicaments[0]},
                new PatientMedicament{ Patient = patients[1], Medicament = medicaments[1]},
                new PatientMedicament{ Patient = patients[2], Medicament = medicaments[2]},
            };
            context.Prescriptions.AddRange(patientmedicament);
           
            var visitations = new[]
            {
                new Visitation{ Patient = patients[0], Doctor = doctors[0], Comments = "njama da go bade"},
                new Visitation{ Patient = patients[1], Doctor = doctors[1], Comments = "bez lekarstva ne stava"},
                new Visitation{ Patient = patients[2], Doctor = doctors[2], Comments = "dano ne se muchi mnogo"},
            };
            context.Visitations.AddRange(visitations);
            
            var diagnoses = new[]
            {
                new Diagnose { Name = "schizophrenia", Comments = "Lud za vrazvane", Patient = patients[0]},
                new Diagnose { Name = "not potent", Comments = "Ne moje da go vdigne", Patient = patients[1]},
                new Diagnose { Name = "broken neck", Comments = "няма да ходи повече", Patient = patients[2]},
            };
            context.Diagnoses.AddRange(diagnoses);

            context.SaveChanges();
        }
    }
}
