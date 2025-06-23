using Common.Application;
using Shop.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//("Server=.;Database=EShop;TrustServerCertificate=True;MultipleActiveResultSets=true;Integrated Security=True;Encrypt=True");
//"Data Source=QSCCWSQL01.qscc.local;Initial Catalog=QSCC_QA;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true;Encrypt=True"
builder.Services.RegisterShopDependency(connectionString);
CommonBootstrapper.Init(builder.Services);


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
