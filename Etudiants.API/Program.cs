using Etudiants.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    });

// Add services to the container.

builder.Services.AddDbContext<EtudiantsContext>(options =>
  options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    //Modifie le comportement de ASP.NET Core Web API pour supprimer la validation du modelstate afin qu'elle puisse se faire 
    //dans le controleur
    .ConfigureApiBehaviorOptions(options =>
                 {
                     options.SuppressModelStateInvalidFilter = true;
                 });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
