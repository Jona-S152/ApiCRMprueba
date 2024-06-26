using ApiCRMprueba.Models;
using ApiCRMprueba.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Bteflnnvxgfztjiyphq0Context>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DB_Connection")));

// Add services to the container.
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//builder.Services.ConfigureHttpJsonOptions(options =>
//{
//    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//    options.SerializerOptions.WriteIndented = true;
//    options.SerializerOptions.MaxDepth = 2;
//});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

//builder.Services.AddControllersWithViews()
//    .AddNewtonsoftJson(options =>
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//);

builder.Services.AddControllers();
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
