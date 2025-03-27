using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Persistence.Entities;

namespace School.Persistence.Configurations;

public class NotificationLogConfiguration : IEntityTypeConfiguration<NotificationLogEntity>
{
    public void Configure(EntityTypeBuilder<NotificationLogEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("NotificationLog");
    }
}