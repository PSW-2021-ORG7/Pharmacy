﻿using backend.Model;
using Microsoft.EntityFrameworkCore;
using System;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Hospital>()
                .HasIndex(u => u.ApiKey)
                .IsUnique();

            builder.Entity<Medicine>()
                .HasIndex(m => m.MedicineId)
                .IsUnique();
        }
    }
}
