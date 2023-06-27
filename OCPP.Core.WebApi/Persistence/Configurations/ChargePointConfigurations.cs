using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCPP.Core.Domain.Entities;

namespace OCPP.Core.WebApi.Persistence.Configurations;

public class ChargePointConfigurations : IEntityTypeConfiguration<ChargePoint>
{
  public void Configure(EntityTypeBuilder<ChargePoint> builder)
  {
    builder.ToTable("ChargePoint");

    builder.HasIndex(e => e.ChargePointId, "ChargePoint_Identifier")
              .IsUnique();

    builder.Property(e => e.ChargePointId).HasMaxLength(100);

    builder.Property(e => e.Comment).HasMaxLength(200);

    builder.Property(e => e.Name).HasMaxLength(100);

    builder.Property(e => e.Username).HasMaxLength(50);

    builder.Property(e => e.Password).HasMaxLength(50);

    builder.Property(e => e.ClientCertThumb).HasMaxLength(100);
  }
}