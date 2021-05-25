using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SteamMarketApi.FeedHostedService
{
    public partial class SteamMarketApiContext : DbContext
    {
        public SteamMarketApiContext()
        {
        }

        public SteamMarketApiContext(DbContextOptions<SteamMarketApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SteamItems> SteamItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=SteamMarketApi;User Id=postgres;Password=123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SteamItems>(entity =>
            {
                entity.Property(e => e.LowestPrice).HasColumnType("numeric");

                entity.Property(e => e.MedianPrice).HasColumnType("numeric");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
