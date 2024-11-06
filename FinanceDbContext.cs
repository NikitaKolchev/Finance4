using System;
using System.Collections.Generic;
using Finance4.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance4;

public partial class FinanceDbContext : DbContext
{
    public FinanceDbContext()
    {
    }

    public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BudgetPeriod> BudgetPeriods { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VwExpenseDetail> VwExpenseDetails { get; set; }

    public virtual DbSet<VwUserBudget> VwUserBudgets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server= DOM\\SQLEXPRESS; database=FinanceDB; Integrated Security=true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BudgetPeriod>(entity =>
        {
            entity.HasKey(e => e.PeriodId).HasName("PK__budgetPe__E521BB16B82DAA87");

            entity.ToTable("budgetPeriods");

            entity.Property(e => e.Budget).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NeededBalance).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.BudgetPeriods)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__budgetPer__UserI__4E88ABD4");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId).HasName("PK__expenses__1445CFD379E7C296");

            entity.ToTable("expenses");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Period).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.PeriodId)
                .HasConstraintName("FK__expenses__Period__52593CB8");

            entity.HasOne(d => d.Type).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__expenses__TypeId__5165187F");
        });

        modelBuilder.Entity<ExpenseType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__expenseT__516F03B58F3CF988");

            entity.ToTable("expenseTypes");

            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__1788CC4CEE8438BE");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__A9D10534967D21AC").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwExpenseDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ExpenseDetails");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ExpenseName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ExpenseType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserLogin)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwUserBudget>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_UserBudgets");

            entity.Property(e => e.Budget).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NeededBalance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserLogin)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
