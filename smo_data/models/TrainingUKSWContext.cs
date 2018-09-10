using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace smo_data.models
{
    public partial class TrainingUKSWContext : DbContext
    {
        private IConfiguration Configuration;
        public TrainingUKSWContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public TrainingUKSWContext(DbContextOptions<TrainingUKSWContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TrRole> TrRole { get; set; }
        public virtual DbSet<TrUser> TrUser { get; set; }
        public virtual DbSet<TtUserRole> TtUserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                //optionsBuilder.UseNpgsql("Host=localhost;Database=TrainingUKSW;username=postgres;password=postgres");
                optionsBuilder.UseNpgsql(Configuration.GetConnectionString("postgresconnstring"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("TR_ROLE");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasColumnName("role_name")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TrUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("TR_USER");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(300);

                entity.Property(e => e.FullName)
                    .HasColumnName("full_name")
                    .HasMaxLength(400);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(150);

                entity.Property(e => e.UserName)
                    .HasColumnName("user_name")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<TtUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("TT_USER_ROLE");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TtUserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_role_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TtUserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_id");
            });
        }
    }
}
