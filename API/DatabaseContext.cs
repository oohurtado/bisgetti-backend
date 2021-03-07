using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>().ToTable("Addresses");
            modelBuilder.Entity<Person>().ToTable("People");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Settings>().ToTable("Settings");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.City).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Id).HasColumnName("Address_Id");
                entity.Property(e => e.InteriorNumber).HasMaxLength(10);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.OutdoorNumber).IsRequired().HasMaxLength(10);
                entity.Property(e => e.PersonId).IsRequired().HasColumnName("Person_Id");
                entity.Property(e => e.PostalCode).IsRequired().HasMaxLength(10);
                entity.Property(e => e.State).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Street).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Suburb).IsRequired().HasMaxLength(50);

                entity.HasOne(p => p.Person).WithMany(p => p.Addresses);

                entity.HasIndex(c => new { c.PersonId, c.Name }).IsUnique().HasFilter(null);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Birthdate).HasColumnType("datetime");
                entity.Property(e => e.CreationTime).IsRequired().HasColumnType("datetime");
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Id).HasColumnName("Person_Id");
                entity.Property(e => e.PersonType).IsRequired().HasColumnName("Type");
                entity.Property(e => e.IsRegistered).IsRequired();
                entity.Property(e => e.IsVerified).IsRequired();

                entity.HasMany(p => p.Addresses).WithOne(p => p.Person).OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(c => new { c.Email }).IsUnique().HasFilter(null);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.IsAvailable).IsRequired();
                entity.Property(e => e.IsHidden).IsRequired();
                entity.Property(e => e.Id).HasColumnName("Product_Id");
                entity.Property(e => e.Ingredients).HasMaxLength(100);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.IsNew).IsRequired();
                entity.Property(e => e.Price).HasColumnType("money");
                entity.Property(e => e.ProductAvailability).HasColumnName("Availability");
                entity.Property(e => e.ProductType).HasColumnName("Type");

                entity.HasIndex(c => new { c.Name }).IsUnique().HasFilter(null);
            });

            modelBuilder.Entity<Settings>(entity =>
            {
                entity.Property(e => e.HasHomeDelivery);
                entity.Property(e => e.Id).HasColumnName("Settings_Id");
                entity.Property(e => e.MenuProductsJson);
                entity.Property(e => e.MenuMessagesJson);
                entity.Property(e => e.IsOnlineActive);
                entity.Property(e => e.PlaceInformationJson);
                entity.Property(e => e.ShippingCost).HasColumnType("money");
            });
        }
    }
}
