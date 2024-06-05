using Microsoft.EntityFrameworkCore;
using BrewingBuddies_DataService.Data;
using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_DataService.Repositories;
using BrewingBuddies_BLL.Services;
using BrewingBuddies_BLL.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ILeagueUserService, LeagueUserService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();


builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigins",
            builder =>
            {
                //builder.WithOrigins("http://localhost:5173")
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
    }); 

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Apply CORS middleware
    app.UseCors("AllowSpecificOrigins");

//kijk hier na
    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.MapControllers();
    app.Run();