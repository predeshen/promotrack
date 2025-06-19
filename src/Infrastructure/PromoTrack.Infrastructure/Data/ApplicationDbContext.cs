using Microsoft.EntityFrameworkCore;
using PromoTrack.Domain;

namespace PromoTrack.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<CampaignProduct> CampaignProducts { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<PromoterActivity> PromoterActivities { get; set; }
    public DbSet<ShelfImage> ShelfImages { get; set; }
    public DbSet<ExtractedProductData> ExtractedProductData { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionOption> QuestionOptions { get; set; }
    public DbSet<BrandQuestionDefault> BrandQuestionDefaults { get; set; }
    public DbSet<CampaignQuestionConfig> CampaignQuestionConfigs { get; set; }
    public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
    public DbSet<SurveyAnswerSelectedOption> SurveyAnswerSelectedOptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- Unique Indexes ---
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Brand>().HasIndex(b => b.BrandName).IsUnique();
        modelBuilder.Entity<Product>().HasIndex(p => p.Sku).IsUnique();
        modelBuilder.Entity<Store>().HasIndex(s => s.StoreCode).IsUnique();

        // --- Composite Keys for Junction Tables ---
        modelBuilder.Entity<CampaignProduct>().HasKey(cp => new { cp.CampaignId, cp.ProductId });
        modelBuilder.Entity<BrandQuestionDefault>().HasKey(bqd => new { bqd.BrandId, bqd.QuestionId });
        modelBuilder.Entity<CampaignQuestionConfig>().HasKey(cqc => new { cqc.CampaignId, cqc.QuestionId });

        // --- Configure Relationships ---
        modelBuilder.Entity<CampaignProduct>().HasOne(cp => cp.Campaign).WithMany(c => c.CampaignProducts).HasForeignKey(cp => cp.CampaignId);
        modelBuilder.Entity<CampaignProduct>().HasOne(cp => cp.Product).WithMany(p => p.CampaignProducts).HasForeignKey(cp => cp.ProductId);
        modelBuilder.Entity<BrandQuestionDefault>().HasOne(bqd => bqd.Brand).WithMany(b => b.DefaultQuestions).HasForeignKey(bqd => bqd.BrandId);
        modelBuilder.Entity<BrandQuestionDefault>().HasOne(bqd => bqd.Question).WithMany(q => q.BrandDefaults).HasForeignKey(bqd => bqd.QuestionId);
        modelBuilder.Entity<CampaignQuestionConfig>().HasOne(cqc => cqc.Campaign).WithMany(c => c.QuestionConfigurations).HasForeignKey(cqc => cqc.CampaignId);
        modelBuilder.Entity<CampaignQuestionConfig>().HasOne(cqc => cqc.Question).WithMany(q => q.CampaignConfigurations).HasForeignKey(cqc => cqc.QuestionId);

        // --- THIS IS THE CRITICAL FIX FOR THE CASCADE PATH ERROR ---
        modelBuilder.Entity<SurveyAnswer>().HasOne(sa => sa.PromoterActivity).WithMany(pa => pa.SurveyAnswers).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<SurveyAnswer>().HasOne(sa => sa.Question).WithMany().HasForeignKey(sa => sa.QuestionId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<SurveyAnswerSelectedOption>().HasOne(saso => saso.SurveyAnswer).WithMany(sa => sa.SelectedOptions).HasForeignKey(saso => saso.SurveyAnswerId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<SurveyAnswerSelectedOption>().HasOne(saso => saso.QuestionOption).WithMany().HasForeignKey(saso => saso.QuestionOptionId).OnDelete(DeleteBehavior.Restrict);

        // --- Fix for Decimal Precision Warnings ---
        modelBuilder.Entity<Product>().Property(p => p.DefaultPrice).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<CampaignProduct>().Property(p => p.CampaignSpecificPrice).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<ExtractedProductData>().Property(p => p.Price).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Store>().Property(s => s.Latitude).HasColumnType("decimal(18, 9)");
        modelBuilder.Entity<Store>().Property(s => s.Longitude).HasColumnType("decimal(18, 9)");
        modelBuilder.Entity<PromoterActivity>().Property(pa => pa.CheckInLatitude).HasColumnType("decimal(18, 9)");
        modelBuilder.Entity<PromoterActivity>().Property(pa => pa.CheckInLongitude).HasColumnType("decimal(18, 9)");
        modelBuilder.Entity<PromoterActivity>().Property(pa => pa.CheckOutLatitude).HasColumnType("decimal(18, 9)");
        modelBuilder.Entity<PromoterActivity>().Property(pa => pa.CheckOutLongitude).HasColumnType("decimal(18, 9)");

        // --- THIS IS THE FINAL FIX ---
        // Seeding Logic with a pre-generated, static hash for "Password123!"
        modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@promotrack.com",
                // This is a static, pre-generated hash. It will never change.
                PasswordHash = "$2a$11$LT4ufsLIZCKkZnDFuMhgmekvdDqO9Zg6BIVCNvTXd/qCTgYyb0DaG",//Password123!
                Role = "Admin",
                IsActive = true,
                CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}