using Microsoft.EntityFrameworkCore;
using PromoTrack.Application.Interfaces; // Add this using
using PromoTrack.Infrastructure.Data;
using PromoTrack.Infrastructure.Repositories; // Add this using

var builder = WebApplication.CreateBuilder(args);

// --- START: Add our services to the container ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register our repository.
// When a class asks for IUserRepository, the DI container will provide an instance of UserRepository.
// AddScoped means a new instance is created for each web request.
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddControllers();
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
