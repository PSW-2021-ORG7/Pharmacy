// <auto-generated />
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

            modelBuilder.Entity("IngredientMedicine", b =>
                {
                    b.Property<int>("IngredientsId")
                        .HasColumnType("integer");

                    b.Property<int>("MedicinesId")
                        .HasColumnType("integer");

                    b.HasKey("IngredientsId", "MedicinesId");

                    b.HasIndex("MedicinesId");

                    b.ToTable("IngredientMedicine");
                });

            modelBuilder.Entity("backend.Events.EventInventoryCheck.InventoryCheck", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DosageInMg")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("InventoryCheck", "Events");
                });

            modelBuilder.Entity("backend.Model.Ad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("PromotionEndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Ad");
                });

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

            modelBuilder.Entity("backend.Model.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("backend.Model.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

                    b.HasKey("Id");

                    b.ToTable("Medicine");
                });

            modelBuilder.Entity("backend.Model.MedicineCombination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FirstMedicineId")
                        .HasColumnType("integer");

                    b.Property<int>("SecondMedicineId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FirstMedicineId");

                    b.HasIndex("SecondMedicineId");

                    b.ToTable("MedicineCombination");
                });

            modelBuilder.Entity("backend.Model.MedicineInventory", b =>
                {
                    b.Property<int>("MedicineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("MedicineId");

                    b.ToTable("MedicineInventory");
                });

            modelBuilder.Entity("backend.Model.Order", b =>
                {
                    b.Property<int>("Order_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<bool>("deliveryReqired")
                        .HasColumnType("boolean");

                    b.HasKey("Order_Id");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("backend.Model.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AdId")
                        .HasColumnType("integer");

                    b.Property<int?>("MedicineId")
                        .HasColumnType("integer");

                    b.Property<int?>("Order_Id")
                        .HasColumnType("integer");

                    b.Property<double>("PriceForSingleEntity")
                        .HasColumnType("double precision");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int?>("ShoppingCart_Id")
                        .HasColumnType("integer");

                    b.HasKey("OrderItemId");

                    b.HasIndex("AdId");

                    b.HasIndex("MedicineId");

                    b.HasIndex("Order_Id");

                    b.HasIndex("ShoppingCart_Id");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("backend.Model.ShoppingCart", b =>
                {
                    b.Property<int>("ShoppingCart_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("ShoppingCart_Id");

                    b.HasIndex("UserId");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("backend.Model.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsLogicalDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("IngredientMedicine", b =>
                {
                    b.HasOne("backend.Model.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Model.Medicine", null)
                        .WithMany()
                        .HasForeignKey("MedicinesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("backend.Model.MedicineCombination", b =>
                {
                    b.HasOne("backend.Model.Medicine", "FirstMedicine")
                        .WithMany()
                        .HasForeignKey("FirstMedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Model.Medicine", "SecondMedicine")
                        .WithMany()
                        .HasForeignKey("SecondMedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FirstMedicine");

                    b.Navigation("SecondMedicine");
                });

            modelBuilder.Entity("backend.Model.Order", b =>
                {
                    b.HasOne("backend.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend.Model.OrderItem", b =>
                {
                    b.HasOne("backend.Model.Ad", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("AdId");

                    b.HasOne("backend.Model.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineId");

                    b.HasOne("backend.Model.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("Order_Id");

                    b.HasOne("backend.Model.ShoppingCart", null)
                        .WithMany("ShoppingCartItem")
                        .HasForeignKey("ShoppingCart_Id");

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("backend.Model.ShoppingCart", b =>
                {
                    b.HasOne("backend.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend.Model.Ad", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("backend.Model.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("backend.Model.ShoppingCart", b =>
                {
                    b.Navigation("ShoppingCartItem");
                });
#pragma warning restore 612, 618
        }
    }
}
