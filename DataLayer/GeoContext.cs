using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class GeoContext : DbContext
    {
        public GeoContext(DbContextOptions<GeoContext> options) : base(options) { }
        public DbSet<City> Cities { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<River> Rivers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<City>().ToTable("Cities");
            builder.Entity<City>().Property(i => i.ID).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<City>()
                .HasOne(c => c.BelongsTo)
                .WithMany(c => c.Cities)
                .HasForeignKey(c => c.Country_ID)
                .OnDelete(DeleteBehavior.Restrict);

            /*
            builder.Entity<River>()
                .HasMany(r => r.BelongsTo)
                .WithMany(co => co.Rivers)
                .Map(rcs =>
                {
                    rcs.MapLeftKey("RiverRefID");
                    rcs.MapRightKey("CountryRefID");
                    rcs.ToTable("RiverCountry");
                });*/
            /*
            builder.Entity<River>().ToTable("Rivers");
            builder.Entity<River>().Property(i => i.ID).IsRequired().ValueGeneratedOnAdd();*/

            builder.Entity<Country>().ToTable("Countries");
            builder.Entity<Country>().Property(i => i.ID).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Country>()
                .HasOne(c => c.BelongsTo)
                .WithMany(c => c.Countries)
                .HasForeignKey(c => c.Continent_ID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Continent>().ToTable("Continents");
            builder.Entity<Continent>().Property(i => i.ID).IsRequired().ValueGeneratedOnAdd();
        }
    }
}
