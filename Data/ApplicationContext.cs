using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace restore.Data
{
    public class ApplicationContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<OS> OS { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Storage> Storage { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .HasIndex(p =>p.Slug)
                .IsUnique();

            builder.Entity<ProductFeature>()
                .HasKey(x => new { x.ProductId, x.FeatureId});

            builder.Entity<ProductVariant>()
                .HasKey(x => new { x.ProductId, x.ColorId, x.StorageId });
        }
    }
}
