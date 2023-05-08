using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MobiFiber.Models
{
    public partial class MobiFiberContext : DbContext
    {
        private  string connectString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public MobiFiberContext()
        {
        }

        public MobiFiberContext(DbContextOptions<MobiFiberContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<MobifiberAgent> MobifiberAgents { get; set; }
        public virtual DbSet<MobifiberConfig> MobifiberConfigs { get; set; }
        public virtual DbSet<MobifiberContract> MobifiberContracts { get; set; }
        public virtual DbSet<MobifiberDevelopmentUnit> MobifiberDevelopmentUnits { get; set; }
        public virtual DbSet<MobifiberDevice> MobifiberDevices { get; set; }
        public virtual DbSet<MobifiberHistory> MobifiberHistories { get; set; }
        public virtual DbSet<MobifiberPackage> MobifiberPackages { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleGroup> RoleGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=192.168.0.97,8433;Database=MobiFiber;User ID=develop;Password=abc@123");
                optionsBuilder.UseSqlServer(connectString);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Degree).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Facebook).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gmail).HasMaxLength(50);

                entity.Property(e => e.ImagePath).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Position).HasMaxLength(50);

                entity.Property(e => e.Skype).HasMaxLength(50);

                entity.Property(e => e.Twitter).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<MobifiberAgent>(entity =>
            {
                entity.HasKey(e => e.AmId)
                    .HasName("PK_Mobifiber_Agency");

                entity.ToTable("Mobifiber_Agents");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.AgentCode).HasMaxLength(50);

                entity.Property(e => e.AgentsName).HasMaxLength(250);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxCode).HasMaxLength(50);
            });

            modelBuilder.Entity<MobifiberConfig>(entity =>
            {
                entity.ToTable("Mobifiber_Config");

                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.Property(e => e.DateLastUpdate).HasColumnType("datetime");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(100);
            });

            modelBuilder.Entity<MobifiberContract>(entity =>
            {
                entity.HasKey(e => e.ContractId)
                    .HasName("PK_Contract_1");

                entity.ToTable("Mobifiber_Contract");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.AgentcodeAm)
                    .HasColumnName("AgentcodeAM")
                    .HasComment("Ma dai ly");

                entity.Property(e => e.BillDate)
                    .HasColumnType("datetime")
                    .HasComment("ngay hoa don");

                entity.Property(e => e.BillNumber)
                    .HasMaxLength(50)
                    .HasComment("so hoa don");

                entity.Property(e => e.BillPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("so tien tren hoa don");

                entity.Property(e => e.ContractNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("so hop dong");

                entity.Property(e => e.CustomerIdvm)
                    .HasMaxLength(100)
                    .HasColumnName("CustomerIDVM")
                    .HasComment("ma khach hang");

                entity.Property(e => e.CustomerName).HasMaxLength(255);

                entity.Property(e => e.DateCreate)
                    .HasColumnType("datetime")
                    .HasComment("ngay tao contract");

                entity.Property(e => e.DateLastUpdate)
                    .HasColumnType("datetime")
                    .HasComment("ngay update cuoi cung");

                entity.Property(e => e.DeveloperName).HasComment("don vi thiet ke");

                entity.Property(e => e.IdentityCard).HasMaxLength(50);

                entity.Property(e => e.InfrastructurePartners)
                    .HasMaxLength(255)
                    .HasComment("doi tac co so ha tang");

                entity.Property(e => e.RegisterDate).HasColumnType("datetime");

                entity.Property(e => e.SignDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasComment("0 : dang thuc hien  1 : het hieu luc  ");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasComment("Số điện thoại");

                entity.Property(e => e.TypeOfCooperation)
                    .HasMaxLength(255)
                    .HasComment("phuong thuc hop tac");

                entity.Property(e => e.UserCreate).HasComment("nguoi tao contract");

                entity.Property(e => e.UserLastUpdate).HasComment("nguoi update cuoi cung");
            });

            modelBuilder.Entity<MobifiberDevelopmentUnit>(entity =>
            {
                entity.HasKey(e => e.DevelopId)
                    .HasName("PK_DevelopmentUnit");

                entity.ToTable("Mobifiber_DevelopmentUnit");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.DevelopCode).HasMaxLength(50);

                entity.Property(e => e.DevelopName).HasMaxLength(250);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxCode).HasMaxLength(50);
            });

            modelBuilder.Entity<MobifiberDevice>(entity =>
            {
                entity.HasKey(e => e.DeviceId)
                    .HasName("PK_Device_1");

                entity.ToTable("Mobifiber_Device");

                entity.Property(e => e.AllocationTime).HasComment("thoi gian phan bo theo thang");

                entity.Property(e => e.DateActive)
                    .HasColumnType("datetime")
                    .HasComment("ngay kich hoat");

                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.Property(e => e.DateInputWarehouse)
                    .HasColumnType("datetime")
                    .HasComment("ngay nhap kho");

                entity.Property(e => e.DateLastUpdate).HasColumnType("datetime");

                entity.Property(e => e.DateReinputWarehouse)
                    .HasColumnType("datetime")
                    .HasComment("ngay nhap kho lai");

                entity.Property(e => e.DeviceCode).HasMaxLength(50);

                entity.Property(e => e.DeviceName).HasMaxLength(255);

                entity.Property(e => e.DevicePrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("gia thiet bi");

                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((7))")
                    .HasComment("New = 7, NotNew = 8, Guarantee = 9");

                entity.Property(e => e.Serial).HasMaxLength(100);

                entity.Property(e => e.Status).HasComment("0 trong kho,  1: dang thuc hien hop dong, 2 : Delete");
            });

            modelBuilder.Entity<MobifiberHistory>(entity =>
            {
                entity.ToTable("Mobifiber_History");

                entity.Property(e => e.Action).HasComment("Action Enum ActionTypeCustom");

                entity.Property(e => e.DateCreate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FieldChange).HasMaxLength(50);

                entity.Property(e => e.IdRefer).HasComment("Id refer, type = thiet bi => gia tri nay se la id thiet bi");

                entity.Property(e => e.NewValue).HasMaxLength(500);

                entity.Property(e => e.OldValue).HasMaxLength(500);

                entity.Property(e => e.Type).HasComment("Type theo module");
            });

            modelBuilder.Entity<MobifiberPackage>(entity =>
            {
                entity.HasKey(e => e.PackageId)
                    .HasName("PK_Data Package_1");

                entity.ToTable("Mobifiber_Package");

                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.Property(e => e.DateLastUpdate)
                    .HasColumnType("datetime")
                    .HasComment("ngay update cuoi cung");

                entity.Property(e => e.Decision).HasMaxLength(100);

                entity.Property(e => e.PackageName).HasMaxLength(100);

                entity.Property(e => e.PackageNumber).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PriceVat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PriceVAT");

                entity.Property(e => e.Status).HasComment("0 : hieu luc, 1: het hieu luc, 3 delete");

                entity.Property(e => e.UserCreate).HasComment("nguoi ta goi cuoc data");

                entity.Property(e => e.UserLastUpdate).HasComment("nguoi update cuoi cung");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("Module");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.NameModuleRole).HasMaxLength(500);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
            });

            modelBuilder.Entity<RoleGroup>(entity =>
            {
                entity.ToTable("RoleGroup");

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
