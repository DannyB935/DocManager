using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace api_docmanager.Entities;

public partial class DocManagerContext : DbContext
{
    public DocManagerContext()
    {
    }

    public DocManagerContext(DbContextOptions<DocManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssignmentLog> AssignmentLogs { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssignmentLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ASSIGNME__3213E83F59528323");

            entity.ToTable("ASSIGNMENT_LOGS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAssign)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("date_assign");
            entity.Property(e => e.DocId).HasColumnName("doc_id");
            entity.Property(e => e.UsrAssign).HasColumnName("usr_assign");

            entity.HasOne(d => d.Doc).WithMany(p => p.AssignmentLogs)
                .HasForeignKey(d => d.DocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ASSIGNMEN__doc_i__5CD6CB2B");

            entity.HasOne(d => d.UsrAssignNavigation).WithMany(p => p.AssignmentLogs)
                .HasForeignKey(d => d.UsrAssign)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ASSIGNMEN__usr_a__5DCAEF64");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DOCUMENT__3213E83FA6CD7A8B");

            entity.ToTable("DOCUMENTS", tb => tb.HasTrigger("tgr_generate_doc_num"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Anonym)
                .HasDefaultValue(false)
                .HasColumnName("anonym");
            entity.Property(e => e.Body)
                .HasMaxLength(1042)
                .HasColumnName("body");
            entity.Property(e => e.DateCreate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("date_create");
            entity.Property(e => e.DateDone)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("date_done");
            entity.Property(e => e.Deleted)
                .HasDefaultValue(false)
                .HasColumnName("deleted");
            entity.Property(e => e.DeptName)
                .HasMaxLength(256)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("dept_name");
            entity.Property(e => e.DocNum)
                .HasMaxLength(64)
                .HasColumnName("doc_num");
            entity.Property(e => e.DocType)
                .HasDefaultValue(0)
                .HasColumnName("doc_type");
            entity.Property(e => e.GenByUsr)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("gen_by_usr");
            entity.Property(e => e.LnameRecip)
                .HasMaxLength(128)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("lname_recip");
            entity.Property(e => e.LnameSender)
                .HasMaxLength(128)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("lname_sender");
            entity.Property(e => e.NameRecip)
                .HasMaxLength(64)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("name_recip");
            entity.Property(e => e.NameSender)
                .HasMaxLength(64)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("name_sender");
            entity.Property(e => e.PositionRecip)
                .HasMaxLength(128)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("position_recip");
            entity.Property(e => e.PositionSender)
                .HasMaxLength(128)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("position_sender");
            entity.Property(e => e.Subject)
                .HasMaxLength(128)
                .HasColumnName("subject");
            entity.Property(e => e.TitleRecip)
                .HasMaxLength(64)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("title_recip");
            entity.Property(e => e.TitleSender)
                .HasMaxLength(64)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("title_sender");
            entity.Property(e => e.UnitBelong).HasColumnName("unit_belong");
            entity.Property(e => e.UsrAssign)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("usr_assign");
            entity.Property(e => e.UsrRecip)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("usr_recip");
            entity.Property(e => e.UsrSender)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("usr_sender");

            entity.HasOne(d => d.GenByUsrNavigation).WithMany(p => p.DocumentGenByUsrNavigations)
                .HasForeignKey(d => d.GenByUsr)
                .HasConstraintName("FK__DOCUMENTS__gen_b__59FA5E80");

            entity.HasOne(d => d.UnitBelongNavigation).WithMany(p => p.Documents)
                .HasForeignKey(d => d.UnitBelong)
                .HasConstraintName("FK__DOCUMENTS__unit___5812160E");

            entity.HasOne(d => d.UsrAssignNavigation).WithMany(p => p.DocumentUsrAssignNavigations)
                .HasForeignKey(d => d.UsrAssign)
                .HasConstraintName("FK__DOCUMENTS__usr_a__59063A47");

            entity.HasOne(d => d.UsrRecipNavigation).WithMany(p => p.DocumentUsrRecipNavigations)
                .HasForeignKey(d => d.UsrRecip)
                .HasConstraintName("FK__DOCUMENTS__usr_r__571DF1D5");

            entity.HasOne(d => d.UsrSenderNavigation).WithMany(p => p.DocumentUsrSenderNavigations)
                .HasForeignKey(d => d.UsrSender)
                .HasConstraintName("FK__DOCUMENTS__usr_s__5629CD9C");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UNITS__3213E83F18E02277");

            entity.ToTable("UNITS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deleted)
                .HasDefaultValue(false)
                .HasColumnName("deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.Prefix)
                .HasMaxLength(32)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("prefix");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__USER_ACC__357D4CF867E5778B");

            entity.ToTable("USER_ACCOUNTS");

            entity.Property(e => e.Code)
                .ValueGeneratedNever()
                .HasColumnName("code");
            entity.Property(e => e.Deleted)
                .HasDefaultValue(false)
                .HasColumnName("deleted");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .HasColumnName("email");
            entity.Property(e => e.Lname)
                .HasMaxLength(128)
                .HasColumnName("lname");
            entity.Property(e => e.NameUsr)
                .HasMaxLength(64)
                .HasColumnName("name_usr");
            entity.Property(e => e.UnitBelong)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("unit_belong");
            entity.Property(e => e.UsrRole).HasColumnName("usr_role");

            entity.HasOne(d => d.UnitBelongNavigation).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.UnitBelong)
                .HasConstraintName("FK__USER_ACCO__unit___5AEE82B9");

            entity.HasOne(d => d.UsrRoleNavigation).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.UsrRole)
                .HasConstraintName("FK__USER_ACCO__usr_r__5BE2A6F2");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER_ROL__3213E83F21CA29A8");

            entity.ToTable("USER_ROLES");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deleted)
                .HasDefaultValue(false)
                .HasColumnName("deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
