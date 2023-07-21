using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCPP.Core.Domain.Entities;

namespace OCPP.Core.WebApi.Persistence.Configurations;

public class ChargePointConfigurations : IEntityTypeConfiguration<ChargeStation>
{
  public void Configure(EntityTypeBuilder<ChargeStation> builder)
  {
    builder.HasIndex(e => e.StationId, "ChargeStation_Identifier")
              .IsUnique();

    builder.Property(e => e.StationId).HasMaxLength(100);
    builder.Property(e => e.StationName).HasMaxLength(100);

    builder.Property(e => e.Username).HasMaxLength(50);
    builder.Property(e => e.Password).HasMaxLength(50);
  }
}