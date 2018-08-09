using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Models
{
    public partial class cap01devContext : DbContext
    {
        static NLog.Logger Logger
        {
            get
            {
                return NLog.LogManager.GetCurrentClassLogger();
            }
        }

        public cap01devContext()
        {
        }

        public cap01devContext(DbContextOptions<cap01devContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Admins> Admins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Logger.Trace("Context.OnConfiguring IN");
            try
            {
                if (!optionsBuilder.IsConfigured)
                {
                    Logger.Debug("IsConfigured: {0}", optionsBuilder.IsConfigured);
                    if (Settings.Instance!=null)
                        optionsBuilder.UseNpgsql(Settings.Instance.DbConnectionString);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Connection to DB error");
            }
            finally
            {
                Logger.Trace("Context.OnConfiguring IN");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Admins>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("Admins_email_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("character varying(256)");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creationDate")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("character varying(32)");

                entity.Property(e => e.FullName)
                    .HasColumnName("fullName")
                    .HasColumnType("character varying(32)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.PasswordHash)
                    .HasColumnName("passwordHash")
                    .HasColumnType("character varying(60)");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("updateDate")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");
            });
        }
    }
}
