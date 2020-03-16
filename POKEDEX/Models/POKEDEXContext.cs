using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace POKEDEX.Models
{
    public partial class POKEDEXContext : DbContext
    {
        public POKEDEXContext()
        {
        }

        public POKEDEXContext(DbContextOptions<POKEDEXContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pokemon> Pokemon { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<TipoAtaque> TipoAtaque { get; set; }
        public virtual DbSet<TipoPokemon> TipoPokemon { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-AQ9ABPJ;Database=POKEDEX;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Pokemon>(entity =>
            {
                entity.HasKey(e => e.IdPokemon)
                    .HasName("PK__POKEMON__E651EA4464CAACFF");

                entity.ToTable("POKEMON");

                entity.Property(e => e.IdPokemon).HasColumnName("ID_POKEMON");

                entity.Property(e => e.IdRegion).HasColumnName("ID_REGION");

                entity.Property(e => e.IdTipoAtaque).HasColumnName("ID_TIPO_ATAQUE");

                entity.Property(e => e.IdTipoPokemon).HasColumnName("ID_TIPO_POKEMON");

                entity.Property(e => e.NombrePokemon)
                    .IsRequired()
                    .HasColumnName("NOMBRE_POKEMON")
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRegionNavigation)
                    .WithMany(p => p.Pokemon)
                    .HasForeignKey(d => d.IdRegion)
                    .HasConstraintName("FK__POKEMON__ID_REGI__3B75D760");

                entity.HasOne(d => d.IdTipoAtaqueNavigation)
                    .WithMany(p => p.Pokemon)
                    .HasForeignKey(d => d.IdTipoAtaque)
                    .HasConstraintName("FK__POKEMON__ID_TIPO__412EB0B6");

                entity.HasOne(d => d.IdTipoPokemonNavigation)
                    .WithMany(p => p.Pokemon)
                    .HasForeignKey(d => d.IdTipoPokemon)
                    .HasConstraintName("FK__POKEMON__ID_TIPO__3C69FB99");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.IdRegion)
                    .HasName("PK__REGION__D8BB64B0E2011251");

                entity.ToTable("REGION");

                entity.Property(e => e.IdRegion).HasColumnName("ID_REGION");

                entity.Property(e => e.Color)
                    .HasColumnName("COLOR")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.NombreRegion)
                    .IsRequired()
                    .HasColumnName("NOMBRE_REGION")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoAtaque>(entity =>
            {
                entity.HasKey(e => e.IdTipoAtaque)
                    .HasName("PK__TIPO_ATA__37296F320A18677F");

                entity.ToTable("TIPO_ATAQUE");

                entity.Property(e => e.IdTipoAtaque).HasColumnName("ID_TIPO_ATAQUE");

                entity.Property(e => e.NombreTipoAtaque)
                    .HasColumnName("NOMBRE_TIPO_ATAQUE")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoPokemon>(entity =>
            {
                entity.HasKey(e => e.IdTipoPokemon)
                    .HasName("PK__TIPO_POK__EC8D7B69305EC343");

                entity.ToTable("TIPO_POKEMON");

                entity.Property(e => e.IdTipoPokemon).HasColumnName("ID_TIPO_POKEMON");

                entity.Property(e => e.NombreTipoPokemon)
                    .HasColumnName("NOMBRE_TIPO_POKEMON")
                    .IsUnicode(false);
            });
        }
    }
}
