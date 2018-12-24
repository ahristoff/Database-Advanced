using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_StudentSystem.Data.Models.Configurations
{
    public class CourseConfig: IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder                                    //4
                .HasMany(s => s.Resources)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId);

            builder                                    //5
                .HasMany(s => s.HomeworkSubmissions)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId);

            builder
                .Property(s => s.Name)
                .HasMaxLength(80)
                .IsUnicode();

            builder
               .Property(s => s.Description)
               .IsUnicode()
               .IsRequired(false);
        }
    }
}
