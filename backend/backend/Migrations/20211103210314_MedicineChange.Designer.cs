﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using backend.DAL;

namespace backend.Migrations
{
    [DbContext(typeof(DrugStoreContext))]
    [Migration("20211103210314_MedicineChange")]
    partial class MedicineChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("backend.Model.Allergen", b =>
                {
                    b.Property<Guid>("AllergenId")
                        .HasColumnType("uuid");

                    b.Property<List<string>>("IngredientNames")
                        .HasColumnType("text[]");

                    b.Property<List<string>>("MedicineNames")
                        .HasColumnType("text[]");

                    b.Property<string>("Other")
                        .HasColumnType("text");

                    b.HasKey("AllergenId");

                    b.ToTable("Allergen");
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
                        .HasForeignKey("AllergenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("backend.Model.Medicine", b =>
                {
                    b.Navigation("Allergens");
                });
#pragma warning restore 612, 618
        }
    }
}
