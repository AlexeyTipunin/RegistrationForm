﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistrationForm.DAL.src.Context;

namespace RegistrationForm.DAL.Migrations
{
    [DbContext(typeof(RegistrationDbContext))]
    [Migration("20190208074345_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity("RegistrationForm.DAL.src.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AgreeToWorkForFood");

                    b.Property<string>("Login")
                        .IsRequired();

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<int>("ProvinceId");

                    b.HasKey("AccountId");

                    b.HasAlternateKey("Login");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("RegistrationForm.DAL.src.Entities.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("RegistrationForm.DAL.src.Entities.Province", b =>
                {
                    b.Property<int>("ProvinceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ProvinceId");

                    b.HasIndex("CountryId");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("RegistrationForm.DAL.src.Entities.Account", b =>
                {
                    b.HasOne("RegistrationForm.DAL.src.Entities.Province", "Province")
                        .WithMany("Accounts")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RegistrationForm.DAL.src.Entities.Province", b =>
                {
                    b.HasOne("RegistrationForm.DAL.src.Entities.Country", "Country")
                        .WithMany("Provinces")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}