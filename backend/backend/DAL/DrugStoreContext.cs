using backend.Model;
using backend.Model.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using static backend.Model.User;

namespace backend.DAL
{
    public class DrugStoreContext : DbContext
    {
        public DrugStoreContext(DbContextOptions<DrugStoreContext> options) : base(options) { }

        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<MedicineInventory> MedicineInventory { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<MedicineCombination> MedicineCombination { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Hospital>()
                .HasIndex(u => u.ApiKey)
                .IsUnique();

            builder.Entity<Ingredient>()
                .HasIndex(i => i.Name)
                .IsUnique();
            //builder.Entity<Medicine>().HasData(
            //    new
            //    {
            //        Id = 1,
            //        Name = "Andol",
            //        Description = "Lek za glavu",
            //        DosageInMilligrams = 300,
            //        Manufacturer = "Hemofarm",
            //        SideEffects = new List<string> { "Mucnina", "glavobolja" },
            //        PossibleReactions = new List<string> { "Alergija", "Osip" },
            //        WayOfConsumption = "Pre obroka",
            //        PotenitalDangers = "",
            //        Ingredients = new List<Ingredient>()
            //    }
            //    ); 

            //builder.Entity<User>().HasData(
            //    new
            //    {
            //        UserId = Guid.NewGuid(),
            //        FirstName = "Stefan",
            //        LastName = "Ljubovic",
            //        Username = "stefan",
            //        Password = "123456",
            //        Role = UserRole.Client,
            //        IsLogicalDeleted = false,
            //        IsBlocked = false
            //    }
            // );

           //builder.Entity<Order>().HasData(
           //     new { Order_Id = 1, Status = OrderStatus.Delivered, OrderDate = DateTime.Now });

           // builder.Entity<Order>().OwnsOne(o => o.OrderItems).HasData(
           //         new { Order_Id = 1 }
           //  );
        }
    }
}
