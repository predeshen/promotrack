using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Polly;
using PromoTrack.Application.Interfaces;
using PromoTrack.Infrastructure.Data;
using PromoTrack.Infrastructure.Repositories;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

const string MyAllowSpecificOrigins = "AllowBlazorApp";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          // Ensure this matches the PWA's address
                          policy.WithOrigins("http://localhost:8080")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// 2. Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IPromoterActivityRepository, PromoterActivityRepository>();
builder.Services.AddScoped<IShelfImageRepository, ShelfImageRepository>();
builder.Services.AddScoped<IExtractedProductDataRepository, ExtractedProductDataRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

// 4. Configure Controllers and JSON Serializer
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- END: Services Configuration ---


var app = builder.Build();

// --- THIS IS THE NEW, MORE ROBUST AUTOMATION LOGIC ---
// It will wait and retry if the database isn't ready yet.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var context = services.GetRequiredService<ApplicationDbContext>();

    try
    {
        logger.LogInformation("Attempting to apply database migrations...");

        var retryPolicy = Policy
            .Handle<SqlException>()
            .WaitAndRetry(new[]
            {
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(15)
            }, (exception, timeSpan, retryCount, ctx) =>
            {
                logger.LogWarning(exception, "Error connecting to DB. Retrying in {timeSpan}s... (Attempt {retryCount})", timeSpan.Seconds, retryCount);
            });

        retryPolicy.Execute(() =>
        {
            context.Database.Migrate();
            logger.LogInformation("Database migration successful.");
        });
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred during database migration after all retries.");
    }
}

app.UseSwagger();
app.UseSwaggerUI();
// --- START: HTTP Request Pipeline Configuration ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply the CORS policy here, right before Authorization. This is the crucial part.
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

// This single line discovers and maps all your controller endpoints.
app.MapControllers();

app.Run();