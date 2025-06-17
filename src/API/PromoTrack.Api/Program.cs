using Microsoft.EntityFrameworkCore;
using PromoTrack.Application.Interfaces; 
using PromoTrack.Infrastructure.Data;
using PromoTrack.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//Dependency Injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IPromoterActivityRepository, PromoterActivityRepository>();
builder.Services.AddScoped<IShelfImageRepository, ShelfImageRepository>();
builder.Services.AddScoped<IExtractedProductDataRepository, ExtractedProductDataRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

// --- ADD CORS POLICY ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        builder => builder.WithOrigins("https://localhost:7276") // Use your PWA's HTTPS URL
                           .AllowAnyHeader()
                           .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- USE THE CORS POLICY ---
app.UseCors("AllowBlazorApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
