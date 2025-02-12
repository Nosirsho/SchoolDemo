using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Persistence.Entities;


namespace School.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<StudentEntity>
{
    public void Configure(EntityTypeBuilder<StudentEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasMany(s => s.Parents)
            .WithOne(s => s.Student)
            .HasForeignKey(s => s.StudentId);

    }
}