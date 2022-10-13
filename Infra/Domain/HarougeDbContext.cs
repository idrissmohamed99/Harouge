using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infra.Domain
{
    public partial class HarougeDbContext : DbContext
    {
        public HarougeDbContext()
        {
        }

        public HarougeDbContext(DbContextOptions<HarougeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BuildingTypes> BuildingTypes { get; set; }
        public virtual DbSet<Cases> Cases { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Modules> Modules { get; set; }
        public virtual DbSet<Permisstions> Permisstions { get; set; }
        public virtual DbSet<Printers> Printers { get; set; }
        public virtual DbSet<RolePermisstion> RolePermisstion { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Screens> Screens { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=HarougeDb;user id=sa;password=AS@n0601");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuildingTypes>(entity =>
            {
                entity.ToTable("buildingTypes");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BtName)
                    .IsRequired()
                    .HasColumnName("btName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Cases>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(128);

                entity.Property(e => e.AntiVsType)
                    .IsRequired()
                    .HasColumnName("antiVsType")
                    .HasMaxLength(255);

                entity.Property(e => e.CName)
                    .IsRequired()
                    .HasColumnName("cName")
                    .HasMaxLength(55);

                entity.Property(e => e.ComputerModel)
                    .IsRequired()
                    .HasColumnName("computerModel")
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.MsOfficeVs).HasColumnName("msOfficeVs");

                entity.Property(e => e.OperatingSystem)
                    .IsRequired()
                    .HasColumnName("operatingSystem")
                    .HasMaxLength(255);

                entity.Property(e => e.PrintId)
                    .IsRequired()
                    .HasColumnName("printId")
                    .HasMaxLength(128);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasColumnName("serialNumber")
                    .HasMaxLength(255);

                entity.Property(e => e.SizeHardDisk).HasColumnName("sizeHardDisk");

                entity.Property(e => e.TotalRam).HasColumnName("totalRam");

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("updateAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userId")
                    .HasMaxLength(128);

                entity.HasOne(d => d.Print)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.PrintId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cases_Printers");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cases_Users");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(128);

                entity.Property(e => e.Administration)
                    .HasColumnName("administration")
                    .HasMaxLength(50);

                entity.Property(e => e.BulidingId).HasColumnName("bulidingId");

                entity.Property(e => e.CaseId)
                    .HasColumnName("caseId")
                    .HasMaxLength(128);

                entity.Property(e => e.CreateAt)
                    .HasColumnName("createAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmployeName)
                    .IsRequired()
                    .HasColumnName("employeName")
                    .HasMaxLength(55);

                entity.Property(e => e.GeneralComment).HasColumnName("generalComment");

                entity.Property(e => e.InsideSite)
                    .HasColumnName("insideSite")
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");

                entity.Property(e => e.RoomNumber).HasColumnName("roomNumber");

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("updateAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userId")
                    .HasMaxLength(128);

                entity.HasOne(d => d.Buliding)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.BulidingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_buildingTypes");

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.CaseId)
                    .HasConstraintName("FK_Employee_Cases");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Users");
            });

            modelBuilder.Entity<Modules>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Permisstions>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ModuleId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Permisstions)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permisstions_Modules");
            });

            modelBuilder.Entity<Printers>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(128);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.PName)
                    .IsRequired()
                    .HasColumnName("pName")
                    .HasMaxLength(55);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasColumnName("serialNumber")
                    .HasMaxLength(255);

                entity.Property(e => e.TypeOfConnect)
                    .IsRequired()
                    .HasColumnName("typeOfConnect")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("updateAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userId")
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Printers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Printers_Users");
            });

            modelBuilder.Entity<RolePermisstion>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.PermisstionId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Permisstion)
                    .WithMany(p => p.RolePermisstion)
                    .HasForeignKey(d => d.PermisstionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermisstion_Permisstions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermisstion)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RolePermisstion_Roles");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.CreateUserId).HasMaxLength(128);

                entity.Property(e => e.ModifyAt).HasColumnType("datetime");

                entity.Property(e => e.ModuleId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Roles_Modules");
            });

            modelBuilder.Entity<Screens>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(128);

                entity.Property(e => e.CaseId)
                    .IsRequired()
                    .HasColumnName("caseId")
                    .HasMaxLength(128);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.SName)
                    .IsRequired()
                    .HasColumnName("sName")
                    .HasMaxLength(55);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasColumnName("serialNumber")
                    .HasMaxLength(255);

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("updateAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userId")
                    .HasMaxLength(128);

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.Screens)
                    .HasForeignKey(d => d.CaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Screens_Cases");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Screens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Screens_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.CreateUserId).HasMaxLength(128);

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.ModifyAt).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
