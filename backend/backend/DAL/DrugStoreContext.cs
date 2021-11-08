﻿using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.DAL
{
    public class DrugStoreContext : DbContext
    {
        public DrugStoreContext(DbContextOptions<DrugStoreContext> options) : base(options) { }

        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<Allergen> Allergen { get; set; }
        public DbSet<Hospital> Hospital { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Hospital>()
                .HasIndex(u => u.ApiKey)
                .IsUnique();
        }
    }
}