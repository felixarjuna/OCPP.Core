using Microsoft.EntityFrameworkCore;
using OCPP.Core.Domain.Entities;

#nullable disable
namespace OCPP.Core.WebApi.Persistence;

public class OCPPCoreDbContext : DbContext
{
  public OCPPCoreDbContext(DbContextOptions<OCPPCoreDbContext> options)
      : base(options)
  {
  }

  public virtual DbSet<ChargeStation> ChargeStations { get; set; }
  public virtual DbSet<Connector> ConnectorStatuses { get; set; }
  public virtual DbSet<Transaction> Transactions { get; set; }
  public virtual DbSet<ChargeTag> ChargeTags { get; set; }
  public virtual DbSet<MessageLog> MessageLogs { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    IConfiguration _configuration = new ConfigurationBuilder()
      .SetBasePath(AppContext.BaseDirectory)
      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
      .Build();

    if (!optionsBuilder.IsConfigured)
    {
      string sqlConnString = _configuration.GetConnectionString("SqlServer");
      string liteConnString = _configuration.GetConnectionString("SQLite");
      if (!string.IsNullOrWhiteSpace(sqlConnString))
      {
        optionsBuilder.UseSqlServer(sqlConnString);
      }
      else if (!string.IsNullOrWhiteSpace(liteConnString))
      {
        optionsBuilder.UseSqlite(liteConnString);
      }
    }
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(OCPPCoreDbContext).Assembly);

    modelBuilder.Entity<ChargeTag>(entity =>
    {
      entity.HasKey(e => e.TagId)
                .HasName("PK_ChargeKeys");
      entity.Property(e => e.TagId).HasMaxLength(50);
      entity.Property(e => e.ParentTagId).HasMaxLength(50);
      entity.Property(e => e.TagName).HasMaxLength(200);
    });

    modelBuilder.Entity<MessageLog>(entity =>
    {
      entity.ToTable("MessageLog");
      entity.HasKey(e => e.LogId);
      entity.HasIndex(e => e.LogTime, "IX_MessageLog_ChargePointId");

      entity.Property(e => e.ChargePointId)
                .IsRequired()
                .HasMaxLength(100);

      entity.Property(e => e.ErrorCode).HasMaxLength(100);
      entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(100);
    });

    modelBuilder.Entity<Transaction>(entity =>
    {
      entity.Property(e => e.ConnectorId)
                .IsRequired()
                .HasMaxLength(100);

      entity.Property(e => e.Uid).HasMaxLength(50);
      entity.Property(e => e.StartResult).HasMaxLength(100);
      entity.Property(e => e.StartTagId).HasMaxLength(50);
      entity.Property(e => e.StopReason).HasMaxLength(100);
      entity.Property(e => e.StopTagId).HasMaxLength(50);
    });
  }
}
