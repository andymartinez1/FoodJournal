using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodJournal.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyMeals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Meals",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "LastDayEaten",
                table: "Meals",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "TimesEaten",
                table: "Meals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "LastDayEaten",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "TimesEaten",
                table: "Meals");
        }
    }
}
