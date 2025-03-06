using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Persistence.Entities;

namespace School.Persistence.Configurations;

public class ParentConfiguration : IEntityTypeConfiguration<ParentEntity>
{
    public void Configure(EntityTypeBuilder<ParentEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Parents");

        builder.HasMany(s => s.Students)
            .WithMany(p => p.Parents)
            .UsingEntity(j=>j.ToTable("StudentParents"));
    }
}