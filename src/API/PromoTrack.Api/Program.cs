using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore; // Add this using statement
using PromoTrack.Infrastructure.Data;  // Add this using statement

var builder = WebApplication.CreateBuilder(args);

// --- START: Add our services to the container ---

// 1. Get the database connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Register ApplicationDbContext with the Dependency Injection container
//    We also configure it to use SQL Server with the connection string we just retrieved.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- END: Add our services to the container ---


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
