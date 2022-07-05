using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WpfCinema.Models
{
    public partial class wpfCinemaContext : DbContext
    {
        public wpfCinemaContext()
        {
        }

        public wpfCinemaContext(DbContextOptions<wpfCinemaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Screening> Screenings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;User ID=mrpbjo;Password=debil;Database=wpfCinema;Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Hall>(entity =>
            {
                entity.HasKey(e => e.Number);

                entity.ToTable("Hall");

                entity.Property(e => e.Number)
                    .ValueGeneratedNever()
                    .HasColumnName("number");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("FK_Movie_Category");
            });

            modelBuilder.Entity<Screening>(entity =>
            {
                entity.ToTable("Screening");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("endedAt");

                entity.Property(e => e.Hall).HasColumnName("hall");

                entity.Property(e => e.Movie).HasColumnName("movie");

                entity.Property(e => e.StartedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("startedAt");

                entity.HasOne(d => d.HallNavigation)
                    .WithMany(p => p.Screenings)
                    .HasForeignKey(d => d.Hall)
                    .HasConstraintName("FK_Screening_Hall");

                entity.HasOne(d => d.MovieNavigation)
                    .WithMany(p => p.Screenings)
                    .HasForeignKey(d => d.Movie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Screening_Movie");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
