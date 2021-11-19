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
    [Migration("20211119013651_MedicineMigration2")]
    partial class MedicineMigration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("backend.Model.Feedback", b =>
                {
                    b.Property<string>("IdFeedback")
                        .HasColumnType("text");

                    b.Property<string>("ContentFeedback")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IdHospital")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdFeedback");

                    b.ToTable("Feedback");
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

                    b.Property<string>("Manufacturer")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<List<string>>("PossibleReactions")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("PotentialDangers")
                        .HasColumnType("text");

                    b.Property<List<string>>("SideEffects")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("WayOfConsumption")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("MedicineId");

                    b.HasIndex("MedicineId")
                        .IsUnique();

                    b.ToTable("Medicine");
                });

            modelBuilder.Entity("backend.Model.MedicineInventory", b =>
                {
                    b.Property<Guid>("MedicineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("MedicineId");

                    b.ToTable("MedicineInventory");
                });
#pragma warning restore 612, 618
        }
    }
}
