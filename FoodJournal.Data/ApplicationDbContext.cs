using FoodJournal.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Food> FoodItems { get; set; }
    public DbSet<Meal> Meals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Food>()
            .HasMany(f => f.Meals)
            .WithMany(m => m.Ingredients)
            .UsingEntity<FoodMeal>();
    }
}