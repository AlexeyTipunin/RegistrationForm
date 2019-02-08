using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RegistrationForm.DAL.src.Entities;

namespace RegistrationForm.DAL.src.Context
{
    public class RegistrationDbContext : DbContext
    {
        public RegistrationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(x => x.CountryId);
                entity.Property(x => x.Name).IsRequired();
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(x => x.ProvinceId);
                entity.Property(x => x.Name).IsRequired();

                entity.HasIndex(x => x.CountryId);
                entity.HasOne(x => x.Country)
                    .WithMany(x => x.Provinces)
                    .HasForeignKey(x => x.CountryId);
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(x => x.AccountId);
                entity.Property(x => x.Login).IsRequired();
                entity.HasAlternateKey(x => x.Login);
                entity.Property(x => x.PasswordHash).IsRequired();
                entity.Property(x => x.AgreeToWorkForFood).IsRequired();

                entity.HasIndex(x => x.ProvinceId);
                entity.HasOne(x => x.Province)
                    .WithMany(x => x.Accounts)
                    .HasForeignKey(x => x.ProvinceId);
            });

            modelBuilder.Entity<Country>().HasData(
                new[]
                {
                    new Country
                    {
                        CountryId = 1,
                        Name = "Country 1",
                        Provinces = new List<Province>()
                    },
                    new Country
                    {
                        CountryId = 2,
                        Name = "Country 2"
                    },
                    new Country
                    {
                        CountryId = 3,
                        Name = "Country 3"
                    },
                });

            modelBuilder.Entity<Province>().HasData(new Province[]
            {
                new Province
                {
                    CountryId = 1,
                    ProvinceId = 1,
                    Name = "Province 1.1"
                },
                new Province
                {
                    CountryId = 1,
                    ProvinceId = 2,
                    Name = "Province 1.2"
                },
                new Province
                {
                    CountryId = 1,
                    ProvinceId = 3,
                    Name = "Province 1.3"
                },
                new Province
                {
                    CountryId = 2,
                    ProvinceId = 4,
                    Name = "Province 2.1"
                },
                new Province
                {
                    CountryId = 2,
                    ProvinceId = 5,
                    Name = "Province 2.2"
                },
                new Province
                {
                    CountryId = 2,
                    ProvinceId = 6,
                    Name = "Province 2.3"
                },
                new Province
                {
                    CountryId = 3,
                    ProvinceId = 7,
                    Name = "Province 3.1"
                },
                new Province
                {
                    CountryId = 3,
                    ProvinceId = 8,
                    Name = "Province 3.2"
                },
                new Province
                {
                    CountryId = 3,
                    ProvinceId = 9,
                    Name = "Province 3.3"
                }
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
