using FoodJournal.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Food> FoodItems { get; set; }
    public DbSet<Meal> Meals { get; set; }
    public DbSet<FoodConsumptionEntry> FoodConsumptionEntries { get; set; }
    public DbSet<MealConsumptionEntry> MealConsumptionEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<FoodMeal>()
            .HasOne(fm => fm.Food)
            .WithMany()
            .HasForeignKey(fm => fm.FoodId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<FoodMeal>()
            .HasOne(fm => fm.Meal)
            .WithMany()
            .HasForeignKey(fm => fm.MealId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Food>()
            .HasMany(f => f.Meals)
            .WithMany(m => m.Ingredients)
            .UsingEntity<FoodMeal>();

        modelBuilder
            .Entity<FoodConsumptionEntry>()
            .HasOne(entry => entry.Food)
            .WithMany()
            .HasForeignKey(entry => entry.FoodId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<MealConsumptionEntry>()
            .HasOne(entry => entry.Meal)
            .WithMany()
            .HasForeignKey(entry => entry.MealId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
