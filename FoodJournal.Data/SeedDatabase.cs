using FoodJournal.Entities;
using FoodJournal.ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodJournal.Data;

public static class SeedDatabase
{
    public static void InitializeDatabase(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()
        );

        var chicken = new Food
        {
            Name = "Chicken Breast",
            Category = nameof(FoodCategory.Meat),
            Calories = 165,
            Protein = 31,
            Fat = 3.6,
            Carbs = 0
        };
        var broccoli = new Food
        {
            Name = "Broccoli",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 55,
            Protein = 3.7,
            Fat = 0.6,
            Carbs = 11.2
        };
        var brownRice = new Food
        {
            Name = "Brown Rice",
            Category = nameof(FoodCategory.Grain),
            Calories = 216,
            Protein = 5,
            Fat = 1.8,
            Carbs = 45
        };
        var salmon = new Food
        {
            Name = "Salmon",
            Category = nameof(FoodCategory.Seafood),
            Calories = 208,
            Protein = 20,
            Fat = 13,
            Carbs = 0
        };
        var almonds100g = new Food
        {
            Name = "Almonds (100g)",
            Category = nameof(FoodCategory.Nuts),
            Calories = 575,
            Protein = 21,
            Fat = 49,
            Carbs = 22
        };
        var greekYogurt = new Food
        {
            Name = "Greek Yogurt (plain, 170g)",
            Category = nameof(FoodCategory.Dairy),
            Calories = 100,
            Protein = 17,
            Fat = 0.7,
            Carbs = 6
        };
        var apple = new Food
        {
            Name = "Apple (medium)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 95,
            Protein = 0.5,
            Fat = 0.3,
            Carbs = 25
        };
        var avocado = new Food
        {
            Name = "Avocado (medium)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 234,
            Protein = 3,
            Fat = 21,
            Carbs = 12
        };
        var sweetPotato = new Food
        {
            Name = "Sweet Potato (medium)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 103,
            Protein = 2,
            Fat = 0.2,
            Carbs = 24
        };
        var oatmeal = new Food
        {
            Name = "Oatmeal (cooked, 1 cup)",
            Category = nameof(FoodCategory.Grain),
            Calories = 154,
            Protein = 6,
            Fat = 3,
            Carbs = 27
        };
        var egg = new Food
        {
            Name = "Egg (large)",
            Category = nameof(FoodCategory.Protein),
            Calories = 78,
            Protein = 6,
            Fat = 5,
            Carbs = 0.6
        };
        var spinach = new Food
        {
            Name = "Spinach (raw, 100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 23,
            Protein = 2.9,
            Fat = 0.4,
            Carbs = 3.6
        };
        var banana = new Food
        {
            Name = "Banana (medium)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 105,
            Protein = 1.3,
            Fat = 0.4,
            Carbs = 27
        };
        var tofu = new Food
        {
            Name = "Tofu (100g)",
            Category = nameof(FoodCategory.Protein),
            Calories = 76,
            Protein = 8,
            Fat = 4.8,
            Carbs = 1.9
        };
        var quinoa = new Food
        {
            Name = "Quinoa (cooked, 1 cup)",
            Category = nameof(FoodCategory.Grain),
            Calories = 222,
            Protein = 8,
            Fat = 3.6,
            Carbs = 39
        };
        var peanutButter = new Food
        {
            Name = "Peanut Butter (2 tbsp)",
            Category = nameof(FoodCategory.Nuts),
            Calories = 188,
            Protein = 8,
            Fat = 16,
            Carbs = 6
        };
        var cottageCheese = new Food
        {
            Name = "Cottage Cheese (100g)",
            Category = nameof(FoodCategory.Dairy),
            Calories = 98,
            Protein = 11,
            Fat = 4.3,
            Carbs = 3.4
        };
        var turkey = new Food
        {
            Name = "Turkey Breast",
            Category = nameof(FoodCategory.Meat),
            Calories = 135,
            Protein = 30,
            Fat = 0.7,
            Carbs = 0
        };
        var beef = new Food
        {
            Name = "Lean Ground Beef",
            Category = nameof(FoodCategory.Meat),
            Calories = 250,
            Protein = 26,
            Fat = 15,
            Carbs = 0
        };
        var pork = new Food
        {
            Name = "Pork Tenderloin",
            Category = nameof(FoodCategory.Meat),
            Calories = 143,
            Protein = 26,
            Fat = 3.5,
            Carbs = 0
        };
        var tuna = new Food
        {
            Name = "Tuna (canned in water, 100g)",
            Category = nameof(FoodCategory.Seafood),
            Calories = 116,
            Protein = 26,
            Fat = 0.8,
            Carbs = 0
        };
        var shrimp = new Food
        {
            Name = "Shrimp (100g)",
            Category = nameof(FoodCategory.Seafood),
            Calories = 99,
            Protein = 24,
            Fat = 0.3,
            Carbs = 0.2
        };
        var cod = new Food
        {
            Name = "Cod (100g)",
            Category = nameof(FoodCategory.Seafood),
            Calories = 82,
            Protein = 18,
            Fat = 0.7,
            Carbs = 0
        };
        var tilapia = new Food
        {
            Name = "Tilapia (100g)",
            Category = nameof(FoodCategory.Seafood),
            Calories = 96,
            Protein = 20,
            Fat = 1.7,
            Carbs = 0
        };
        var carrots = new Food
        {
            Name = "Carrots (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 41,
            Protein = 0.9,
            Fat = 0.2,
            Carbs = 10
        };
        var kale = new Food
        {
            Name = "Kale (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 35,
            Protein = 2.9,
            Fat = 0.9,
            Carbs = 4.4
        };
        var cauliflower = new Food
        {
            Name = "Cauliflower (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 25,
            Protein = 1.9,
            Fat = 0.3,
            Carbs = 5
        };
        var bellPepper = new Food
        {
            Name = "Bell Pepper (medium)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 37,
            Protein = 1.2,
            Fat = 0.3,
            Carbs = 9
        };
        var cucumber = new Food
        {
            Name = "Cucumber (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 16,
            Protein = 0.7,
            Fat = 0.1,
            Carbs = 3.6
        };
        var tomato = new Food
        {
            Name = "Tomato (medium)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 22,
            Protein = 1.1,
            Fat = 0.2,
            Carbs = 4.8
        };
        var zucchini = new Food
        {
            Name = "Zucchini (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 17,
            Protein = 1.2,
            Fat = 0.3,
            Carbs = 3.1
        };
        var mushrooms = new Food
        {
            Name = "Mushrooms (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 22,
            Protein = 3.1,
            Fat = 0.3,
            Carbs = 3.3
        };
        var asparagus = new Food
        {
            Name = "Asparagus (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 20,
            Protein = 2.2,
            Fat = 0.1,
            Carbs = 3.9
        };
        var greenBeans = new Food
        {
            Name = "Green Beans (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 31,
            Protein = 1.8,
            Fat = 0.2,
            Carbs = 7
        };
        var orange = new Food
        {
            Name = "Orange (medium)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 62,
            Protein = 1.2,
            Fat = 0.2,
            Carbs = 15
        };
        var strawberries = new Food
        {
            Name = "Strawberries (100g)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 32,
            Protein = 0.7,
            Fat = 0.3,
            Carbs = 7.7
        };
        var blueberries = new Food
        {
            Name = "Blueberries (100g)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 57,
            Protein = 0.7,
            Fat = 0.3,
            Carbs = 14
        };
        var grapes = new Food
        {
            Name = "Grapes (100g)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 69,
            Protein = 0.7,
            Fat = 0.2,
            Carbs = 18
        };
        var watermelon = new Food
        {
            Name = "Watermelon (100g)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 30,
            Protein = 0.6,
            Fat = 0.2,
            Carbs = 8
        };
        var pineapple = new Food
        {
            Name = "Pineapple (100g)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 50,
            Protein = 0.5,
            Fat = 0.1,
            Carbs = 13
        };
        var mango = new Food
        {
            Name = "Mango (100g)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 60,
            Protein = 0.8,
            Fat = 0.4,
            Carbs = 15
        };
        var peach = new Food
        {
            Name = "Peach (medium)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 59,
            Protein = 1.4,
            Fat = 0.4,
            Carbs = 14
        };
        var pear = new Food
        {
            Name = "Pear (medium)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 101,
            Protein = 0.6,
            Fat = 0.2,
            Carbs = 27
        };
        var cherries = new Food
        {
            Name = "Cherries (100g)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 63,
            Protein = 1.1,
            Fat = 0.2,
            Carbs = 16
        };
        var whiteRice = new Food
        {
            Name = "White Rice (cooked, 1 cup)",
            Category = nameof(FoodCategory.Grain),
            Calories = 205,
            Protein = 4.3,
            Fat = 0.4,
            Carbs = 45
        };
        var pasta = new Food
        {
            Name = "Pasta (cooked, 1 cup)",
            Category = nameof(FoodCategory.Grain),
            Calories = 221,
            Protein = 8,
            Fat = 1.3,
            Carbs = 43
        };
        var wholeWheatBread = new Food
        {
            Name = "Whole Wheat Bread (1 slice)",
            Category = nameof(FoodCategory.Grain),
            Calories = 81,
            Protein = 4,
            Fat = 1.1,
            Carbs = 14
        };
        var couscous = new Food
        {
            Name = "Couscous (cooked, 1 cup)",
            Category = nameof(FoodCategory.Grain),
            Calories = 176,
            Protein = 6,
            Fat = 0.3,
            Carbs = 36
        };
        var barley = new Food
        {
            Name = "Barley (cooked, 1 cup)",
            Category = nameof(FoodCategory.Grain),
            Calories = 193,
            Protein = 3.6,
            Fat = 0.7,
            Carbs = 44
        };
        var wildRice = new Food
        {
            Name = "Wild Rice (cooked, 1 cup)",
            Category = nameof(FoodCategory.Grain),
            Calories = 166,
            Protein = 6.5,
            Fat = 0.6,
            Carbs = 35
        };
        var cornTortilla = new Food
        {
            Name = "Corn Tortilla (1 medium)",
            Category = nameof(FoodCategory.Grain),
            Calories = 52,
            Protein = 1.4,
            Fat = 0.7,
            Carbs = 11
        };
        var walnuts = new Food
        {
            Name = "Walnuts (100g)",
            Category = nameof(FoodCategory.Nuts),
            Calories = 654,
            Protein = 15,
            Fat = 65,
            Carbs = 14
        };
        var cashews = new Food
        {
            Name = "Cashews (100g)",
            Category = nameof(FoodCategory.Nuts),
            Calories = 553,
            Protein = 18,
            Fat = 44,
            Carbs = 30
        };
        var pistachios = new Food
        {
            Name = "Pistachios (100g)",
            Category = nameof(FoodCategory.Nuts),
            Calories = 560,
            Protein = 20,
            Fat = 45,
            Carbs = 28
        };
        var pecans = new Food
        {
            Name = "Pecans (100g)",
            Category = nameof(FoodCategory.Nuts),
            Calories = 691,
            Protein = 9,
            Fat = 72,
            Carbs = 14
        };
        var sunflowerSeeds = new Food
        {
            Name = "Sunflower Seeds (100g)",
            Category = nameof(FoodCategory.Nuts),
            Calories = 584,
            Protein = 21,
            Fat = 51,
            Carbs = 20
        };
        var chiaSeeds = new Food
        {
            Name = "Chia Seeds (100g)",
            Category = nameof(FoodCategory.Nuts),
            Calories = 486,
            Protein = 17,
            Fat = 31,
            Carbs = 42
        };
        var pumpkinSeeds = new Food
        {
            Name = "Pumpkin Seeds (100g)",
            Category = nameof(FoodCategory.Nuts),
            Calories = 559,
            Protein = 30,
            Fat = 49,
            Carbs = 10
        };
        var milk = new Food
        {
            Name = "Milk (1 cup)",
            Category = nameof(FoodCategory.Dairy),
            Calories = 149,
            Protein = 8,
            Fat = 8,
            Carbs = 12
        };
        var cheddarCheese = new Food
        {
            Name = "Cheddar Cheese (1 oz)",
            Category = nameof(FoodCategory.Dairy),
            Calories = 114,
            Protein = 7,
            Fat = 9,
            Carbs = 0.4
        };
        var mozzarella = new Food
        {
            Name = "Mozzarella (1 oz)",
            Category = nameof(FoodCategory.Dairy),
            Calories = 85,
            Protein = 6,
            Fat = 6,
            Carbs = 1
        };
        var fetaCheese = new Food
        {
            Name = "Feta Cheese (1 oz)",
            Category = nameof(FoodCategory.Dairy),
            Calories = 75,
            Protein = 4,
            Fat = 6,
            Carbs = 1.2
        };
        var parmesan = new Food
        {
            Name = "Parmesan (1 oz)",
            Category = nameof(FoodCategory.Dairy),
            Calories = 122,
            Protein = 11,
            Fat = 8,
            Carbs = 1
        };
        var butter = new Food
        {
            Name = "Butter (1 tbsp)",
            Category = nameof(FoodCategory.Dairy),
            Calories = 102,
            Protein = 0.1,
            Fat = 12,
            Carbs = 0
        };
        var sourcream = new Food
        {
            Name = "Sour Cream (2 tbsp)",
            Category = nameof(FoodCategory.Dairy),
            Calories = 60,
            Protein = 0.8,
            Fat = 5,
            Carbs = 2
        };
        var chickpeas = new Food
        {
            Name = "Chickpeas (cooked, 1 cup)",
            Category = nameof(FoodCategory.Protein),
            Calories = 269,
            Protein = 15,
            Fat = 4,
            Carbs = 45
        };
        var lentils = new Food
        {
            Name = "Lentils (cooked, 1 cup)",
            Category = nameof(FoodCategory.Protein),
            Calories = 230,
            Protein = 18,
            Fat = 0.8,
            Carbs = 40
        };
        var blackBeans = new Food
        {
            Name = "Black Beans (cooked, 1 cup)",
            Category = nameof(FoodCategory.Protein),
            Calories = 227,
            Protein = 15,
            Fat = 0.9,
            Carbs = 41
        };
        var kidneyBeans = new Food
        {
            Name = "Kidney Beans (cooked, 1 cup)",
            Category = nameof(FoodCategory.Protein),
            Calories = 225,
            Protein = 15,
            Fat = 0.9,
            Carbs = 40
        };
        var edamame = new Food
        {
            Name = "Edamame (1 cup)",
            Category = nameof(FoodCategory.Protein),
            Calories = 189,
            Protein = 17,
            Fat = 8,
            Carbs = 16
        };
        var tempeh = new Food
        {
            Name = "Tempeh (100g)",
            Category = nameof(FoodCategory.Protein),
            Calories = 193,
            Protein = 19,
            Fat = 11,
            Carbs = 9
        };
        var seitan = new Food
        {
            Name = "Seitan (100g)",
            Category = nameof(FoodCategory.Protein),
            Calories = 370,
            Protein = 75,
            Fat = 1.9,
            Carbs = 14
        };
        var proteinPowder = new Food
        {
            Name = "Whey Protein Powder (1 scoop)",
            Category = nameof(FoodCategory.Protein),
            Calories = 120,
            Protein = 24,
            Fat = 1,
            Carbs = 3
        };
        var oliveoil = new Food
        {
            Name = "Olive Oil (1 tbsp)",
            Category = nameof(FoodCategory.Fat),
            Calories = 119,
            Protein = 0,
            Fat = 13.5,
            Carbs = 0
        };
        var coconutoil = new Food
        {
            Name = "Coconut Oil (1 tbsp)",
            Category = nameof(FoodCategory.Fat),
            Calories = 121,
            Protein = 0,
            Fat = 13.5,
            Carbs = 0
        };
        var flaxseeds = new Food
        {
            Name = "Flaxseeds (ground, 1 tbsp)",
            Category = nameof(FoodCategory.Nuts),
            Calories = 37,
            Protein = 1.3,
            Fat = 3,
            Carbs = 2
        };
        var hempseeds = new Food
        {
            Name = "Hemp Seeds (3 tbsp)",
            Category = nameof(FoodCategory.Nuts),
            Calories = 170,
            Protein = 10,
            Fat = 12,
            Carbs = 3
        };
        var honey = new Food
        {
            Name = "Honey (1 tbsp)",
            Category = nameof(FoodCategory.Sweetener),
            Calories = 64,
            Protein = 0.1,
            Fat = 0,
            Carbs = 17
        };
        var mapleSyrup = new Food
        {
            Name = "Maple Syrup (1 tbsp)",
            Category = nameof(FoodCategory.Sweetener),
            Calories = 52,
            Protein = 0,
            Fat = 0,
            Carbs = 13
        };
        var darkChocolate = new Food
        {
            Name = "Dark Chocolate (1 oz)",
            Category = nameof(FoodCategory.Snack),
            Calories = 155,
            Protein = 1.4,
            Fat = 9,
            Carbs = 17
        };
        var hummus = new Food
        {
            Name = "Hummus (2 tbsp)",
            Category = nameof(FoodCategory.Protein),
            Calories = 50,
            Protein = 1.5,
            Fat = 3,
            Carbs = 5
        };
        var guacamole = new Food
        {
            Name = "Guacamole (2 tbsp)",
            Category = nameof(FoodCategory.Fat),
            Calories = 50,
            Protein = 0.6,
            Fat = 4.5,
            Carbs = 3
        };
        var salsa = new Food
        {
            Name = "Salsa (2 tbsp)",
            Category = nameof(FoodCategory.Condiment),
            Calories = 10,
            Protein = 0.4,
            Fat = 0,
            Carbs = 2
        };
        var soysauce = new Food
        {
            Name = "Soy Sauce (1 tbsp)",
            Category = nameof(FoodCategory.Condiment),
            Calories = 9,
            Protein = 1,
            Fat = 0,
            Carbs = 1
        };
        var bbqsauce = new Food
        {
            Name = "BBQ Sauce (2 tbsp)",
            Category = nameof(FoodCategory.Condiment),
            Calories = 60,
            Protein = 0.3,
            Fat = 0.2,
            Carbs = 15
        };
        var ranch = new Food
        {
            Name = "Ranch Dressing (2 tbsp)",
            Category = nameof(FoodCategory.Condiment),
            Calories = 129,
            Protein = 0.5,
            Fat = 14,
            Carbs = 1.5
        };
        var balsamic = new Food
        {
            Name = "Balsamic Vinegar (1 tbsp)",
            Category = nameof(FoodCategory.Condiment),
            Calories = 14,
            Protein = 0,
            Fat = 0,
            Carbs = 3
        };
        var ketchup = new Food
        {
            Name = "Ketchup (1 tbsp)",
            Category = nameof(FoodCategory.Condiment),
            Calories = 17,
            Protein = 0.2,
            Fat = 0,
            Carbs = 4
        };
        var mustard = new Food
        {
            Name = "Mustard (1 tsp)",
            Category = nameof(FoodCategory.Condiment),
            Calories = 3,
            Protein = 0.2,
            Fat = 0.2,
            Carbs = 0.3
        };
        var mayo = new Food
        {
            Name = "Mayonnaise (1 tbsp)",
            Category = nameof(FoodCategory.Condiment),
            Calories = 94,
            Protein = 0.1,
            Fat = 10,
            Carbs = 0.1
        };
        var pepperoni = new Food
        {
            Name = "Pepperoni (10 slices)",
            Category = nameof(FoodCategory.Meat),
            Calories = 130,
            Protein = 6,
            Fat = 11,
            Carbs = 1
        };
        var bacon = new Food
        {
            Name = "Bacon (3 slices)",
            Category = nameof(FoodCategory.Meat),
            Calories = 161,
            Protein = 12,
            Fat = 12,
            Carbs = 0.6
        };
        var sausage = new Food
        {
            Name = "Sausage (1 link)",
            Category = nameof(FoodCategory.Meat),
            Calories = 92,
            Protein = 5,
            Fat = 7,
            Carbs = 1
        };
        var lamb = new Food
        {
            Name = "Lamb (100g)",
            Category = nameof(FoodCategory.Meat),
            Calories = 294,
            Protein = 25,
            Fat = 21,
            Carbs = 0
        };
        var duck = new Food
        {
            Name = "Duck (100g)",
            Category = nameof(FoodCategory.Meat),
            Calories = 337,
            Protein = 19,
            Fat = 28,
            Carbs = 0
        };
        var venison = new Food
        {
            Name = "Venison (100g)",
            Category = nameof(FoodCategory.Meat),
            Calories = 158,
            Protein = 30,
            Fat = 3,
            Carbs = 0
        };
        var crab = new Food
        {
            Name = "Crab (100g)",
            Category = nameof(FoodCategory.Seafood),
            Calories = 97,
            Protein = 19,
            Fat = 1.5,
            Carbs = 0
        };
        var lobster = new Food
        {
            Name = "Lobster (100g)",
            Category = nameof(FoodCategory.Seafood),
            Calories = 89,
            Protein = 19,
            Fat = 1,
            Carbs = 0
        };
        var scallops = new Food
        {
            Name = "Scallops (100g)",
            Category = nameof(FoodCategory.Seafood),
            Calories = 88,
            Protein = 17,
            Fat = 0.8,
            Carbs = 3
        };
        var mussels = new Food
        {
            Name = "Mussels (100g)",
            Category = nameof(FoodCategory.Seafood),
            Calories = 86,
            Protein = 12,
            Fat = 2.2,
            Carbs = 3.7
        };
        var oysters = new Food
        {
            Name = "Oysters (100g)",
            Category = nameof(FoodCategory.Seafood),
            Calories = 68,
            Protein = 7,
            Fat = 2.5,
            Carbs = 3.9
        };
        var halibut = new Food
        {
            Name = "Halibut (100g)",
            Category = nameof(FoodCategory.Seafood),
            Calories = 111,
            Protein = 23,
            Fat = 2,
            Carbs = 0
        };
        var trout = new Food
        {
            Name = "Trout (100g)",
            Category = nameof(FoodCategory.Seafood),
            Calories = 148,
            Protein = 20,
            Fat = 7,
            Carbs = 0
        };
        var sardines = new Food
        {
            Name = "Sardines (100g)",
            Category = nameof(FoodCategory.Seafood),
            Calories = 208,
            Protein = 25,
            Fat = 11,
            Carbs = 0
        };
        var anchovies = new Food
        {
            Name = "Anchovies (100g)",
            Category = nameof(FoodCategory.Seafood),
            Calories = 210,
            Protein = 29,
            Fat = 10,
            Carbs = 0
        };
        var mackerel = new Food
        {
            Name = "Mackerel (100g)",
            Category = nameof(FoodCategory.Seafood),
            Calories = 305,
            Protein = 19,
            Fat = 25,
            Carbs = 0
        };
        var eggplant = new Food
        {
            Name = "Eggplant (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 25,
            Protein = 1,
            Fat = 0.2,
            Carbs = 6
        };
        var lettuce = new Food
        {
            Name = "Lettuce (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 15,
            Protein = 1.4,
            Fat = 0.2,
            Carbs = 2.9
        };
        var celery = new Food
        {
            Name = "Celery (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 16,
            Protein = 0.7,
            Fat = 0.2,
            Carbs = 3
        };
        var radish = new Food
        {
            Name = "Radish (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 16,
            Protein = 0.7,
            Fat = 0.1,
            Carbs = 3.4
        };
        var onion = new Food
        {
            Name = "Onion (medium)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 44,
            Protein = 1.2,
            Fat = 0.1,
            Carbs = 10
        };
        var garlic = new Food
        {
            Name = "Garlic (1 clove)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 4,
            Protein = 0.2,
            Fat = 0,
            Carbs = 1
        };
        var ginger = new Food
        {
            Name = "Ginger (1 tsp)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 2,
            Protein = 0,
            Fat = 0,
            Carbs = 0.4
        };
        var beets = new Food
        {
            Name = "Beets (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 43,
            Protein = 1.6,
            Fat = 0.2,
            Carbs = 10
        };
        var brusselsSprouts = new Food
        {
            Name = "Brussels Sprouts (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 43,
            Protein = 3.4,
            Fat = 0.3,
            Carbs = 9
        };
        var cabbage = new Food
        {
            Name = "Cabbage (100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 25,
            Protein = 1.3,
            Fat = 0.1,
            Carbs = 6
        };
        var artichoke = new Food
        {
            Name = "Artichoke (1 medium)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 60,
            Protein = 4.2,
            Fat = 0.2,
            Carbs = 13
        };

        context.FoodItems.AddRange(
            chicken,
            broccoli,
            brownRice,
            salmon,
            almonds100g,
            greekYogurt,
            apple,
            avocado,
            sweetPotato,
            oatmeal,
            egg,
            spinach,
            banana,
            tofu,
            quinoa,
            peanutButter,
            cottageCheese,
            turkey,
            beef,
            pork,
            tuna,
            shrimp,
            cod,
            tilapia,
            carrots,
            kale,
            cauliflower,
            bellPepper,
            cucumber,
            tomato,
            zucchini,
            mushrooms,
            asparagus,
            greenBeans,
            orange,
            strawberries,
            blueberries,
            grapes,
            watermelon,
            pineapple,
            mango,
            peach,
            pear,
            cherries,
            whiteRice,
            pasta,
            wholeWheatBread,
            couscous,
            barley,
            wildRice,
            cornTortilla,
            walnuts,
            cashews,
            pistachios,
            pecans,
            sunflowerSeeds,
            chiaSeeds,
            pumpkinSeeds,
            milk,
            cheddarCheese,
            mozzarella,
            fetaCheese,
            parmesan,
            butter,
            sourcream,
            chickpeas,
            lentils,
            blackBeans,
            kidneyBeans,
            edamame,
            tempeh,
            seitan,
            proteinPowder,
            oliveoil,
            coconutoil,
            flaxseeds,
            hempseeds,
            honey,
            mapleSyrup,
            darkChocolate,
            hummus,
            guacamole,
            salsa,
            soysauce,
            bbqsauce,
            ranch,
            balsamic,
            ketchup,
            mustard,
            mayo,
            pepperoni,
            bacon,
            sausage,
            lamb,
            duck,
            venison,
            crab,
            lobster,
            scallops,
            mussels,
            oysters,
            halibut,
            trout,
            sardines,
            anchovies,
            mackerel,
            eggplant,
            lettuce,
            celery,
            radish,
            onion,
            garlic,
            ginger,
            beets,
            brusselsSprouts,
            cabbage,
            artichoke
        );

        var grilledChicken = new Meal
        {
            Name = "Grilled Chicken with Broccoli and Rice",
            Description =
                "A healthy meal with grilled chicken breast, steamed broccoli, and brown rice.",
            MealType = MealType.Lunch,
            Calories = 436,
            Protein = 39.7,
            Fat = 6,
            Carbs = 56.2,
            Ingredients = new List<Food> { chicken, broccoli, brownRice }
        };
        var chickenStirFry = new Meal
        {
            Name = "Chicken Stir Fry",
            Description = "A delicious chicken stir fry with vegetables and rice.",
            MealType = MealType.Dinner,
            Calories = 500,
            Protein = 40,
            Fat = 10,
            Carbs = 60,
            Ingredients = new List<Food> { chicken, broccoli, brownRice, peanutButter }
        };
        var salmonBowl = new Meal
        {
            Name = "Salmon Quinoa Bowl",
            Description = "Seared salmon on a bed of quinoa with avocado and spinach.",
            MealType = MealType.Dinner,
            Calories = 550,
            Protein = 35,
            Fat = 22,
            Carbs = 45,
            Ingredients = new List<Food> { salmon, quinoa, avocado, spinach }
        };
        var proteinBreakfast = new Meal
        {
            Name = "Protein Breakfast",
            Description = "Eggs, oatmeal, and Greek yogurt for a high-protein start.",
            MealType = MealType.Breakfast,
            Calories = 420,
            Protein = 43,
            Fat = 12,
            Carbs = 45,
            Ingredients = new List<Food> { egg, oatmeal, greekYogurt, banana }
        };
        var veganTofuBowl = new Meal
        {
            Name = "Vegan Tofu Bowl",
            Description =
                "Roasted sweet potato, quinoa, tofu, and spinach for a balanced vegan bowl.",
            MealType = MealType.Lunch,
            Calories = 520,
            Protein = 28,
            Fat = 14,
            Carbs = 70,
            Ingredients = new List<Food> { tofu, quinoa, sweetPotato, spinach }
        };
        var snackPlate = new Meal
        {
            Name = "Snack Plate",
            Description = "A simple snack plate with almonds, apple, and cottage cheese.",
            MealType = MealType.Snack,
            Calories = 318,
            Protein = 17.5,
            Fat = 26.6,
            Carbs = 30.4,
            Ingredients = new List<Food> { almonds100g, apple, cottageCheese }
        };
        var turkeyWrap = new Meal
        {
            Name = "Turkey Wrap",
            Description = "Whole wheat wrap with turkey, lettuce, tomato, and hummus.",
            MealType = MealType.Lunch,
            Calories = 350,
            Protein = 35,
            Fat = 8,
            Carbs = 35,
            Ingredients = new List<Food> { turkey, wholeWheatBread, lettuce, tomato, hummus }
        };
        var shrimpTacos = new Meal
        {
            Name = "Shrimp Tacos",
            Description = "Grilled shrimp tacos with corn tortillas and guacamole.",
            MealType = MealType.Dinner,
            Calories = 420,
            Protein = 30,
            Fat = 15,
            Carbs = 40,
            Ingredients = new List<Food> { shrimp, cornTortilla, guacamole, salsa, bellPepper }
        };
        var beefStirFry = new Meal
        {
            Name = "Beef Stir Fry",
            Description = "Lean beef with mixed vegetables and white rice.",
            MealType = MealType.Dinner,
            Calories = 580,
            Protein = 35,
            Fat = 18,
            Carbs = 65,
            Ingredients = new List<Food> { beef, whiteRice, broccoli, bellPepper, soysauce }
        };
        var tunaSalad = new Meal
        {
            Name = "Tuna Salad",
            Description = "Fresh tuna salad with mixed greens and balsamic dressing.",
            MealType = MealType.Lunch,
            Calories = 280,
            Protein = 32,
            Fat = 8,
            Carbs = 18,
            Ingredients = new List<Food> { tuna, lettuce, tomato, cucumber, balsamic }
        };
        var veggiePizza = new Meal
        {
            Name = "Veggie Pizza",
            Description = "Pizza with mozzarella, mushrooms, peppers, and tomato sauce.",
            MealType = MealType.Dinner,
            Calories = 550,
            Protein = 22,
            Fat = 20,
            Carbs = 65,
            Ingredients = new List<Food> { wholeWheatBread, mozzarella, mushrooms, bellPepper, tomato }
        };
        var greekSalad = new Meal
        {
            Name = "Greek Salad",
            Description = "Mediterranean salad with feta, olives, cucumber, and tomato.",
            MealType = MealType.Lunch,
            Calories = 320,
            Protein = 12,
            Fat = 22,
            Carbs = 20,
            Ingredients = new List<Food> { fetaCheese, cucumber, tomato, oliveoil, lettuce }
        };
        var pancakeBreakfast = new Meal
        {
            Name = "Pancake Breakfast",
            Description = "Whole wheat pancakes with maple syrup and strawberries.",
            MealType = MealType.Breakfast,
            Calories = 450,
            Protein = 12,
            Fat = 8,
            Carbs = 80,
            Ingredients = new List<Food> { wholeWheatBread, egg, milk, mapleSyrup, strawberries }
        };
        var burritoBowl = new Meal
        {
            Name = "Burrito Bowl",
            Description = "Brown rice bowl with black beans, chicken, and guacamole.",
            MealType = MealType.Lunch,
            Calories = 620,
            Protein = 45,
            Fat = 18,
            Carbs = 70,
            Ingredients = new List<Food> { chicken, brownRice, blackBeans, guacamole, salsa }
        };
        var lentilSoup = new Meal
        {
            Name = "Lentil Soup",
            Description = "Hearty lentil soup with carrots and kale.",
            MealType = MealType.Lunch,
            Calories = 340,
            Protein = 22,
            Fat = 3,
            Carbs = 58,
            Ingredients = new List<Food> { lentils, carrots, kale, onion, garlic }
        };
        var eggSandwich = new Meal
        {
            Name = "Egg Sandwich",
            Description = "Whole wheat toast with scrambled eggs and cheese.",
            MealType = MealType.Breakfast,
            Calories = 380,
            Protein = 24,
            Fat = 16,
            Carbs = 35,
            Ingredients = new List<Food> { egg, wholeWheatBread, cheddarCheese, butter }
        };
        var baconBreakfast = new Meal
        {
            Name = "Bacon and Eggs",
            Description = "Classic breakfast with bacon, eggs, and toast.",
            MealType = MealType.Breakfast,
            Calories = 480,
            Protein = 28,
            Fat = 26,
            Carbs = 32,
            Ingredients = new List<Food> { bacon, egg, wholeWheatBread, butter }
        };
        var proteinSmoothie = new Meal
        {
            Name = "Protein Smoothie",
            Description = "Smoothie with protein powder, banana, and almond milk.",
            MealType = MealType.Snack,
            Calories = 300,
            Protein = 32,
            Fat = 6,
            Carbs = 35,
            Ingredients = new List<Food> { proteinPowder, banana, greekYogurt }
        };
        var chickenCaesarSalad = new Meal
        {
            Name = "Chicken Caesar Salad",
            Description = "Grilled chicken over romaine lettuce with parmesan and dressing.",
            MealType = MealType.Lunch,
            Calories = 420,
            Protein = 38,
            Fat = 22,
            Carbs = 18,
            Ingredients = new List<Food> { chicken, lettuce, parmesan, oliveoil }
        };
        var pastaBolognese = new Meal
        {
            Name = "Pasta Bolognese",
            Description = "Pasta with lean ground beef and tomato sauce.",
            MealType = MealType.Dinner,
            Calories = 580,
            Protein = 36,
            Fat = 18,
            Carbs = 68,
            Ingredients = new List<Food> { pasta, beef, tomato, onion, garlic }
        };
        var fishAndChips = new Meal
        {
            Name = "Baked Cod and Sweet Potato",
            Description = "Baked cod with roasted sweet potato wedges.",
            MealType = MealType.Dinner,
            Calories = 420,
            Protein = 32,
            Fat = 8,
            Carbs = 50,
            Ingredients = new List<Food> { cod, sweetPotato, oliveoil }
        };
        var veggieBurger = new Meal
        {
            Name = "Veggie Burger",
            Description = "Black bean burger on whole wheat bun with avocado.",
            MealType = MealType.Lunch,
            Calories = 480,
            Protein = 22,
            Fat = 16,
            Carbs = 62,
            Ingredients = new List<Food> { blackBeans, wholeWheatBread, avocado, lettuce, tomato }
        };
        var teriyakiChicken = new Meal
        {
            Name = "Teriyaki Chicken Bowl",
            Description = "Chicken with teriyaki sauce over white rice with broccoli.",
            MealType = MealType.Dinner,
            Calories = 520,
            Protein = 42,
            Fat = 8,
            Carbs = 68,
            Ingredients = new List<Food> { chicken, whiteRice, broccoli, soysauce, honey }
        };
        var porkChops = new Meal
        {
            Name = "Pork Chops with Quinoa",
            Description = "Grilled pork chops with quinoa and asparagus.",
            MealType = MealType.Dinner,
            Calories = 520,
            Protein = 45,
            Fat = 15,
            Carbs = 48,
            Ingredients = new List<Food> { pork, quinoa, asparagus, oliveoil }
        };
        var fruitSalad = new Meal
        {
            Name = "Fruit Salad",
            Description = "Mixed fruit salad with strawberries, blueberries, and orange.",
            MealType = MealType.Snack,
            Calories = 180,
            Protein = 2.5,
            Fat = 0.8,
            Carbs = 42,
            Ingredients = new List<Food> { strawberries, blueberries, orange, banana }
        };
        var nutsAndSeeds = new Meal
        {
            Name = "Nuts and Seeds Mix",
            Description = "Trail mix with almonds, walnuts, and pumpkin seeds.",
            MealType = MealType.Snack,
            Calories = 420,
            Protein = 16,
            Fat = 36,
            Carbs = 18,
            Ingredients = new List<Food> { almonds100g, walnuts, pumpkinSeeds }
        };

        // link meals back to foods
        chicken.Meals = new List<Meal>
            { grilledChicken, chickenStirFry, burritoBowl, chickenCaesarSalad, teriyakiChicken };
        broccoli.Meals = new List<Meal> { grilledChicken, chickenStirFry, beefStirFry, teriyakiChicken };
        brownRice.Meals = new List<Meal> { grilledChicken, chickenStirFry, burritoBowl };
        salmon.Meals = new List<Meal> { salmonBowl };
        quinoa.Meals = new List<Meal> { salmonBowl, veganTofuBowl, porkChops };
        avocado.Meals = new List<Meal> { salmonBowl, veggieBurger };
        spinach.Meals = new List<Meal> { salmonBowl, veganTofuBowl };
        egg.Meals = new List<Meal> { proteinBreakfast, eggSandwich, baconBreakfast, pancakeBreakfast };
        oatmeal.Meals = new List<Meal> { proteinBreakfast };
        greekYogurt.Meals = new List<Meal> { proteinBreakfast, proteinSmoothie };
        banana.Meals = new List<Meal> { proteinBreakfast, proteinSmoothie, fruitSalad };
        tofu.Meals = new List<Meal> { veganTofuBowl };
        sweetPotato.Meals = new List<Meal> { veganTofuBowl, fishAndChips };
        almonds100g.Meals = new List<Meal> { snackPlate, nutsAndSeeds };
        apple.Meals = new List<Meal> { snackPlate };
        cottageCheese.Meals = new List<Meal> { snackPlate };
        peanutButter.Meals = new List<Meal> { chickenStirFry };
        turkey.Meals = new List<Meal> { turkeyWrap };
        shrimp.Meals = new List<Meal> { shrimpTacos };
        beef.Meals = new List<Meal> { beefStirFry, pastaBolognese };
        tuna.Meals = new List<Meal> { tunaSalad };
        whiteRice.Meals = new List<Meal> { beefStirFry, teriyakiChicken };
        mozzarella.Meals = new List<Meal> { veggiePizza };
        mushrooms.Meals = new List<Meal> { veggiePizza };
        bellPepper.Meals = new List<Meal> { beefStirFry, shrimpTacos, veggiePizza };
        fetaCheese.Meals = new List<Meal> { greekSalad };
        cucumber.Meals = new List<Meal> { greekSalad, tunaSalad };
        tomato.Meals = new List<Meal> { turkeyWrap, tunaSalad, veggiePizza, greekSalad, veggieBurger, pastaBolognese };
        wholeWheatBread.Meals = new List<Meal>
            { turkeyWrap, veggiePizza, pancakeBreakfast, eggSandwich, baconBreakfast, veggieBurger };
        lettuce.Meals = new List<Meal> { turkeyWrap, tunaSalad, greekSalad, chickenCaesarSalad, veggieBurger };
        hummus.Meals = new List<Meal> { turkeyWrap };
        cornTortilla.Meals = new List<Meal> { shrimpTacos };
        guacamole.Meals = new List<Meal> { shrimpTacos, burritoBowl };
        salsa.Meals = new List<Meal> { shrimpTacos, burritoBowl };
        soysauce.Meals = new List<Meal> { beefStirFry, teriyakiChicken };
        balsamic.Meals = new List<Meal> { tunaSalad };
        oliveoil.Meals = new List<Meal> { greekSalad, chickenCaesarSalad, fishAndChips, porkChops };
        mapleSyrup.Meals = new List<Meal> { pancakeBreakfast };
        strawberries.Meals = new List<Meal> { pancakeBreakfast, fruitSalad };
        milk.Meals = new List<Meal> { pancakeBreakfast };
        blackBeans.Meals = new List<Meal> { burritoBowl, veggieBurger };
        lentils.Meals = new List<Meal> { lentilSoup };
        carrots.Meals = new List<Meal> { lentilSoup };
        kale.Meals = new List<Meal> { lentilSoup };
        onion.Meals = new List<Meal> { lentilSoup, pastaBolognese };
        garlic.Meals = new List<Meal> { lentilSoup, pastaBolognese };
        cheddarCheese.Meals = new List<Meal> { eggSandwich };
        butter.Meals = new List<Meal> { eggSandwich, baconBreakfast };
        bacon.Meals = new List<Meal> { baconBreakfast };
        proteinPowder.Meals = new List<Meal> { proteinSmoothie };
        parmesan.Meals = new List<Meal> { chickenCaesarSalad };
        pasta.Meals = new List<Meal> { pastaBolognese };
        cod.Meals = new List<Meal> { fishAndChips };
        pork.Meals = new List<Meal> { porkChops };
        asparagus.Meals = new List<Meal> { porkChops };
        honey.Meals = new List<Meal> { teriyakiChicken };
        blueberries.Meals = new List<Meal> { fruitSalad };
        orange.Meals = new List<Meal> { fruitSalad };
        walnuts.Meals = new List<Meal> { nutsAndSeeds };
        pumpkinSeeds.Meals = new List<Meal> { nutsAndSeeds };

        context.Meals.AddRange(
            grilledChicken,
            chickenStirFry,
            salmonBowl,
            proteinBreakfast,
            veganTofuBowl,
            snackPlate,
            turkeyWrap,
            shrimpTacos,
            beefStirFry,
            tunaSalad,
            veggiePizza,
            greekSalad,
            pancakeBreakfast,
            burritoBowl,
            lentilSoup,
            eggSandwich,
            baconBreakfast,
            proteinSmoothie,
            chickenCaesarSalad,
            pastaBolognese,
            fishAndChips,
            veggieBurger,
            teriyakiChicken,
            porkChops,
            fruitSalad,
            nutsAndSeeds
        );

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SaveChanges();
    }
}