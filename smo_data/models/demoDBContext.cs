using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace smo_data.models
{
    public partial class demoDBContext : DbContext
    {
        public demoDBContext()
        {
        }

        public demoDBContext(DbContextOptions<demoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TrUser> TrUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=demoDB;username=postgres;password=rahasia");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrUser>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("TR_USER");

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthdate)
                    .HasColumnName("BIRTHDATE")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasColumnType("character varying(200)");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasColumnName("FULLNAME")
                    .HasColumnType("character varying(350)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("USERNAME")
                    .HasColumnType("character varying(150)");
            });
        }
    }
}
