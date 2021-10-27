using System;
using Microsoft.EntityFrameworkCore;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enumerators;

namespace MusicHub.Data
{
    public class MusicHubDbContext : DbContext
    {
        public MusicHubDbContext()
        {
        }

        public MusicHubDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongPerformer> SongPerformers { get; set; }
        public DbSet<Writer> Writers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<SongPerformer>(songPerformer =>
                {
                    songPerformer
                        .HasKey(sp => new
                        {
                            sp.PerformerId,
                            sp.SongId
                        });
                });

            //Enums
            modelBuilder
                .Entity<Song>()
                .Property(e => e.Genre)
                .HasConversion(
                    v => v.ToString(),
                    v => (Genre) Enum.Parse(typeof(Genre), v));
        }
    }
}