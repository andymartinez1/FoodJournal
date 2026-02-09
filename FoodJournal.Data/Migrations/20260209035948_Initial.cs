using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodJournal.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    Calories = table.Column<int>(type: "INTEGER", nullable: true),
                    Protein = table.Column<double>(type: "REAL", nullable: true),
                    Fat = table.Column<double>(type: "REAL", nullable: true),
                    Carbs = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.FoodId);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    MealId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    MealType = table.Column<int>(type: "INTEGER", nullable: false),
                    Calories = table.Column<int>(type: "INTEGER", nullable: true),
                    Protein = table.Column<double>(type: "REAL", nullable: true),
                    Fat = table.Column<double>(type: "REAL", nullable: true),
                    Carbs = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.MealId);
                });

            migrationBuilder.CreateTable(
                name: "FoodMeal",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "INTEGER", nullable: false),
                    MealId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodMeal", x => new { x.FoodId, x.MealId });
                    table.ForeignKey(
                        name: "FK_FoodMeal_FoodItems_FoodId",
                        column: x => x.FoodId,
                        principalTable: "FoodItems",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodMeal_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodMeal_MealId",
                table: "FoodMeal",
                column: "MealId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodMeal");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "Meals");
        }
    }
}
