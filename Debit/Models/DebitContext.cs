using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Debit.Models
{
    public partial class DebitContext : DbContext
    {
        public DebitContext()
        {
        }

        public DebitContext(DbContextOptions<DebitContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Accountuser> Accountuser { get; set; }
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<Billtype> Billtype { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //读取连接字符串
                optionsBuilder.UseMySql(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build()
                    .GetConnectionString("default"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasColumnType("char(20)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("char(20)");

                entity.Property(e => e.RegisterTime).HasColumnType("bigint(20)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("char(20)");
            });

            modelBuilder.Entity<Accountuser>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.AccountId })
                    .HasName("PRIMARY");

                entity.ToTable("accountuser");

                entity.HasIndex(e => e.AccountId)
                    .HasName("FK_AccountUser_Account");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.Property(e => e.AccountId).HasColumnType("int(11)");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Accountuser)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountUser_Account");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Accountuser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountUser_User");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("bill");

                entity.HasIndex(e => e.AccountId)
                    .HasName("FK_Bill_Account");

                entity.HasIndex(e => e.Type)
                    .HasName("FK_Bill_BillType");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_Bill_User");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AccountId).HasColumnType("int(11)");

                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");

                entity.Property(e => e.Date).HasColumnType("bigint(20)");

                entity.Property(e => e.IsIncome).HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("char(60)");

                entity.Property(e => e.Remark).HasColumnType("char(60)");

                entity.Property(e => e.Type).HasColumnType("int(11)");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_Account");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.Type)
                    .HasConstraintName("FK_Bill_BillType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_User");
            });

            modelBuilder.Entity<Billtype>(entity =>
            {
                entity.ToTable("billtype");

                entity.HasIndex(e => e.AccountId)
                    .HasName("FK_BillType_Account");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AccountId).HasColumnType("int(11)");

                entity.Property(e => e.IsIncome).HasColumnType("bit(1)");

                entity.Property(e => e.Name).HasColumnType("char(20)");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Billtype)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillType_Account");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.AccountId)
                    .HasName("FK_User_Account");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AccountId).HasColumnType("int(11)");

                entity.Property(e => e.CreateDate).HasColumnType("bigint(20)");

                entity.Property(e => e.IsShare).HasColumnType("bit(1)");

                entity.Property(e => e.MoneyAmount).HasColumnType("decimal(18,2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("char(20)");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Account");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
