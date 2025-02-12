using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Persistence.Entities;

namespace School.Persistence.Configurations;

public class ParentConfiguration : IEntityTypeConfiguration<ParentEntity>
{
    public void Configure(EntityTypeBuilder<ParentEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(s => s.Student)
            .WithMany(s => s.Parents)
            .HasForeignKey(s => s.StudentId);

    }
}