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
    /// <summary>
    /// Gets or sets the DbSet for the Question entities.
    /// </summary>
    public DbSet<Question> Questions { get; set; }
    public DbSet<BrandQuestionDefault> BrandQuestionDefaults { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for the QuestionOption entities.
    /// </summary>
    public DbSet<QuestionOption> QuestionOptions { get; set; }

    // --- ADD NEW DBSETS ---
    public DbSet<CampaignQuestionConfig> CampaignQuestionConfigs { get; set; }
    public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
    public DbSet<SurveyAnswerSelectedOption> SurveyAnswerSelectedOptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Brand>()
            .HasIndex(b => b.BrandName)
            .IsUnique();

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Sku)
            .IsUnique();


        modelBuilder.Entity<CampaignProduct>()
            .HasKey(cp => new { cp.CampaignId, cp.ProductId });

        modelBuilder.Entity<CampaignProduct>()
            .HasOne(cp => cp.Campaign)
            .WithMany(c => c.CampaignProducts)
            .HasForeignKey(cp => cp.CampaignId);

        modelBuilder.Entity<CampaignProduct>()
            .HasOne(cp => cp.Product)
            .WithMany(p => p.CampaignProducts)
            .HasForeignKey(cp => cp.ProductId);

        modelBuilder.Entity<Store>()
            .HasIndex(s => s.StoreCode)
            .IsUnique();

        modelBuilder.Entity<CampaignProduct>().HasKey(cp => new { cp.CampaignId, cp.ProductId });
        modelBuilder.Entity<CampaignProduct>().HasOne(cp => cp.Campaign).WithMany(c => c.CampaignProducts).HasForeignKey(cp => cp.CampaignId);
        modelBuilder.Entity<CampaignProduct>().HasOne(cp => cp.Product).WithMany(p => p.CampaignProducts).HasForeignKey(cp => cp.ProductId);

        // --- New Configuration for BrandQuestionDefault ---
        // Define a composite key to ensure a question is only added once per brand.
        modelBuilder.Entity<BrandQuestionDefault>()
            .HasKey(bqd => new { bqd.BrandId, bqd.QuestionId });

        // Configure the relationship from Brand to BrandQuestionDefault
        modelBuilder.Entity<BrandQuestionDefault>()
            .HasOne(bqd => bqd.Brand)
            .WithMany(b => b.DefaultQuestions)
            .HasForeignKey(bqd => bqd.BrandId);

        // Configure the relationship from Question to BrandQuestionDefault
        modelBuilder.Entity<BrandQuestionDefault>()
            .HasOne(bqd => bqd.Question)
            .WithMany(q => q.BrandDefaults)
            .HasForeignKey(bqd => bqd.QuestionId);

        // --- New Configurations ---
        modelBuilder.Entity<CampaignQuestionConfig>().HasKey(cqc => new { cqc.CampaignId, cqc.QuestionId });
        modelBuilder.Entity<CampaignQuestionConfig>().HasOne(cqc => cqc.Campaign).WithMany(c => c.QuestionConfigurations).HasForeignKey(cqc => cqc.CampaignId);
        modelBuilder.Entity<CampaignQuestionConfig>().HasOne(cqc => cqc.Question).WithMany(q => q.CampaignConfigurations).HasForeignKey(cqc => cqc.QuestionId);

        // Define relationship between SurveyAnswer and PromoterActivity
        modelBuilder.Entity<SurveyAnswer>()
            .HasOne(sa => sa.PromoterActivity)
            .WithMany(pa => pa.SurveyAnswers)
            .HasForeignKey(sa => sa.PromoterActivityId)
            .OnDelete(DeleteBehavior.Cascade); // It's okay to delete answers if the activity is deleted.

        // Define relationship between SurveyAnswer and Question but restrict delete
        modelBuilder.Entity<SurveyAnswer>()
            .HasOne(sa => sa.Question)
            .WithMany() // A question doesn't need a direct list of all answers ever given to it.
            .HasForeignKey(sa => sa.QuestionId)
            .OnDelete(DeleteBehavior.Restrict); // PREVENTS the cascade delete cycle.

        // Define relationship between SurveyAnswerSelectedOption and SurveyAnswer
        modelBuilder.Entity<SurveyAnswerSelectedOption>()
            .HasOne(saso => saso.SurveyAnswer)
            .WithMany(sa => sa.SelectedOptions)
            .HasForeignKey(saso => saso.SurveyAnswerId)
            .OnDelete(DeleteBehavior.Cascade); // It's okay to delete selected options if the answer is deleted.

        // Define relationship between SurveyAnswerSelectedOption and QuestionOption but restrict delete
        modelBuilder.Entity<SurveyAnswerSelectedOption>()
            .HasOne(saso => saso.QuestionOption)
            .WithMany() // An option doesn't need a list of all answers that ever selected it.
            .HasForeignKey(saso => saso.QuestionOptionId)
            .OnDelete(DeleteBehavior.Restrict); // PREVENTS the cascade delete cycle.

        // Hash the password for the default admin user
        var adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!");

        // Seed the default admin user
        modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@promotrack.com",
                PasswordHash = adminPasswordHash,
                Role = "Admin",
                IsActive = true,
                CreatedDate = null,
            });
    }
}
