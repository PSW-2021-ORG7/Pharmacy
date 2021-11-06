﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using backend.DAL;

namespace backend.Migrations
{
    [DbContext(typeof(DrugStoreContext))]
    partial class DrugStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("backend.Model.Allergen", b =>
                {
                    b.Property<Guid>("AllergenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<List<string>>("IngredientNames")
                        .HasColumnType("text[]");

                    b.Property<Guid?>("MedicineId")
                        .HasColumnType("uuid");

                    b.Property<List<string>>("MedicineNames")
                        .HasColumnType("text[]");

                    b.Property<string>("Other")
                        .HasColumnType("text");

                    b.HasKey("AllergenId");

                    b.HasIndex("MedicineId");

                    b.ToTable("Allergen");
                });

            modelBuilder.Entity("backend.Model.Hospital", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApiKey")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SiteLink")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("ApiKey")
                        .IsUnique();

                    b.ToTable("Hospital");
                });

            modelBuilder.Entity("backend.Model.Medicine", b =>
                {
                    b.Property<Guid>("MedicineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("DosageInMilligrams")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<List<string>>("SideEffect")
                        .HasColumnType("text[]");

                    b.Property<string>("WayOfConsumption")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("MedicineId");

                    b.ToTable("Medicine");
                });

            modelBuilder.Entity("backend.Model.Allergen", b =>
                {
                    b.HasOne("backend.Model.Medicine", null)
                        .WithMany("Allergens")
                        .HasForeignKey("MedicineId");
                });

            modelBuilder.Entity("backend.Model.Medicine", b =>
                {
                    b.Navigation("Allergens");
                });
#pragma warning restore 612, 618
        }
    }
}
