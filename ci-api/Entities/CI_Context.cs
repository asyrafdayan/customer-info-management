using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CIManagement.Entities
{
    public partial class CI_Context : DbContext
    {
        public CI_Context()
        {
        }

        public CI_Context(DbContextOptions<CI_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCustomer> TblCustomers { get; set; } = null!;
        public virtual DbSet<TblStaff> TblStaffs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.ToTable("tblCustomer");

                entity.Property(e => e.Id);

                entity.Property(e => e.CustomerCity).HasMaxLength(100).HasColumnType("varchar");

                entity.Property(e => e.CustomerCountry).HasMaxLength(100);

                entity.Property(e => e.CustomerName).HasMaxLength(250);

                entity.Property(e => e.CustomerPosCode).HasMaxLength(50);

                entity.Property(e => e.CustomerState).HasMaxLength(100);

                entity.Property(e => e.CustomerStreet).HasMaxLength(100);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblStaff>(entity =>
            {
                entity.ToTable("tblStaff");

                entity.Property(e => e.Id);

                entity.Property(e => e.Department).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.StaffName).HasMaxLength(250);

                entity.Property(e => e.Username).HasMaxLength(100);

                entity.Property(e => e.CreateAccess);

                entity.Property(e => e.EditAccess);

                entity.Property(e => e.ReadAccess);

                entity.Property(e => e.DeleteAccess);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
