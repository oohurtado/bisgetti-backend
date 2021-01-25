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
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().ToTable("People");
            modelBuilder.Entity<Settings>().ToTable("Settings");

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Birthdate).HasColumnType("datetime");
                entity.Property(e => e.CreationTime).IsRequired().HasColumnType("datetime");
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Id).HasColumnName("Person_Id");
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PersonType).IsRequired().HasColumnName("Type");
                entity.Property(e => e.Registered);
                entity.Property(e => e.Verified);

                entity.HasIndex(c => new { c.Email }).IsUnique().HasFilter(null);
            });

            modelBuilder.Entity<Settings>(entity =>
            {
                entity.Property(e => e._PlaceEmails);
                entity.Property(e => e._PlaceName).HasMaxLength(50);
                entity.Property(e => e._PlaceDescription).HasMaxLength(50);
                entity.Property(e => e._PlaceKey).HasMaxLength(10);
                entity.Property(e => e.HomeDelivery);
                entity.Property(e => e.Id).HasColumnName("Settings_Id");
                entity.Property(e => e.MenuJson);
                entity.Property(e => e.MenuMsgDescription).HasMaxLength(50);
                entity.Property(e => e.MenuMsgExtra).HasMaxLength(50);
                entity.Property(e => e.MenuMsgTitle).HasMaxLength(50);
                entity.Property(e => e.MenuVersion).HasMaxLength(50);
                entity.Property(e => e.OnlineActive);
                entity.Property(e => e.PlaceInformationJson);
                entity.Property(e => e.ShippingCost).HasColumnType("money");

                entity.HasIndex(c => new { c._PlaceKey }).IsUnique().HasFilter(null);
            });
        }
    }
}
