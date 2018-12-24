using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_StudentSystem.Data.Models.Configurations
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder          //2
                .HasMany(s => s.HomeworkSubmissions)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId);

            builder
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            builder
               .Property(s => s.PhoneNumber)
               .HasColumnType("char(10)")
               .IsUnicode(false);

            builder
                .Property(s => s.Birthday)
                .IsRequired(false);
        }
    }
}
