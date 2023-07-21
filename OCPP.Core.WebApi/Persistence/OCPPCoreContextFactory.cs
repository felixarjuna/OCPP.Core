using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OCPP.Core.WebApi.Persistence;

public class OCPPCoreContextFactory : IDesignTimeDbContextFactory<OCPPCoreDbContext>
{
  public OCPPCoreDbContext CreateDbContext(string[] args)
  {
    IConfiguration _configuration = new ConfigurationBuilder()
      .SetBasePath(AppContext.BaseDirectory)
      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
      .Build();
    string? connectionString = _configuration.GetConnectionString("SQLite");
    Console.WriteLine(connectionString);

    var builder = new DbContextOptionsBuilder<OCPPCoreDbContext>();
    builder.UseSqlite(connectionString);

    return new OCPPCoreDbContext(builder.Options);
  }
}