using OCPP.Core.WebApi;
using OCPP.Core.WebApi.Middlewares;
using OCPP.Core.WebApi.Persistence;

var allowAllOrigins = "_allowAllOrigins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
  options.AddPolicy(
    name: allowAllOrigins,
    builder => builder.WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddDbContext<OCPPCoreDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Accept WebSocket
app.UseWebSockets();
// Integrate custom OCPP Middleware
app.UseOCPP();
app.UseCors(allowAllOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
