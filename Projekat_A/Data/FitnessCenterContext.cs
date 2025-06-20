using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using Projekat_A.Models;

namespace Projekat_A.Data;

public partial class FitnessCenterContext : DbContext
{
    public FitnessCenterContext()
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public FitnessCenterContext(DbContextOptions<FitnessCenterContext> options)
        : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupMembership> Groupmemberships { get; set; }

    public virtual DbSet<Hall> Halls { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<MembershipPayment> Membershippayments { get; set; }

    public virtual DbSet<MembershipType> Membershiptypes { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    public virtual DbSet<TrainingReservation> Trainingreservations { get; set; }

    public virtual DbSet<TrainingSession> Trainingsessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=Gim1nay*ija;database=fitness_center_hci",
                Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"),
                options => options.EnableRetryOnFailure());

            optionsBuilder.EnableServiceProviderCaching(false);
            optionsBuilder.EnableSensitiveDataLogging(false);
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.CoreEventId.NavigationBaseIncludeIgnored));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_bin")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("group");

            entity.HasIndex(e => e.TrainerUserId, "FK_Group_Trainer_idx");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("User_Id");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.TrainerUserId).HasColumnName("Trainer_User_Id");

            entity.HasOne(d => d.TrainerUser).WithMany(p => p.Groups)
                .HasForeignKey(d => d.TrainerUserId)
                .OnDelete(DeleteBehavior.Restrict) 
                .HasConstraintName("FK_Group_Trainer");

            entity.HasOne(d => d.User).WithOne(p => p.Group)
                .HasForeignKey<Group>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Group_User");
        });

        modelBuilder.Entity<GroupMembership>(entity =>
        {
            entity.HasKey(e => new { e.GroupUserId, e.MemberUserId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("groupmembership");

            entity.HasIndex(e => e.MemberUserId, "FK_Group_has_Member_Member_idx");

            entity.Property(e => e.GroupUserId).HasColumnName("Group_User_Id");
            entity.Property(e => e.MemberUserId).HasColumnName("Member_User_Id");

            entity.HasOne(d => d.GroupUser).WithMany(p => p.GroupMemberships)
                .HasForeignKey(d => d.GroupUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Group_has_Member_Group");

            entity.HasOne(d => d.MemberUser).WithMany(p => p.Groupmemberships)
                .HasForeignKey(d => d.MemberUserId)
                .OnDelete(DeleteBehavior.Restrict) 
                .HasConstraintName("FK_Group_has_Member_Member");
        });

        modelBuilder.Entity<Hall>(entity =>
        {
            entity.HasKey(e => e.IdHall).HasName("PRIMARY");

            entity.ToTable("hall");

            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("member");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("User_Id");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Surname).HasMaxLength(20);

            entity.HasOne(d => d.User).WithOne(p => p.Member)
                .HasForeignKey<Member>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict) 
                .HasConstraintName("FK_Member_User");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => e.IdMembership).HasName("PRIMARY");

            entity.ToTable("membership");

            entity.HasIndex(e => e.MemberUserId, "FK_Membership_Member_idx");

            entity.HasIndex(e => e.MembershipTypeIdType, "FK_Membership_MembershipType_idx");

            entity.Property(e => e.MemberUserId).HasColumnName("Member_User_Id");
            entity.Property(e => e.MembershipTypeIdType).HasColumnName("MembershipType_IdType");

            entity.HasOne(d => d.MemberUser).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.MemberUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Membership_Member");

            entity.HasOne(d => d.MembershipTypeIdTypeNavigation).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.MembershipTypeIdType)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Membership_MembershipType");
        });

        modelBuilder.Entity<MembershipPayment>(entity =>
        {
            entity.HasKey(e => e.IdPayment).HasName("PRIMARY");

            entity.ToTable("membershippayment");

            entity.HasIndex(e => e.MembershipIdMembership, "FK_MembershipPayment_Membership_idx");

            entity.Property(e => e.MembershipIdMembership).HasColumnName("Membership_IdMembership");
            entity.Property(e => e.PaymentMethod).HasMaxLength(45);
            entity.Property(e => e.Price).HasPrecision(10, 2);

            entity.HasOne(d => d.MembershipIdMembershipNavigation).WithMany(p => p.Membershippayments)
                .HasForeignKey(d => d.MembershipIdMembership)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MembershipPayment_Membership");
        });

        modelBuilder.Entity<MembershipType>(entity =>
        {
            entity.HasKey(e => e.IdType).HasName("PRIMARY");

            entity.ToTable("membershiptype");

            entity.Property(e => e.CurrentPrice).HasPrecision(10, 2);
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.IdPromotion).HasName("PRIMARY");

            entity.ToTable("promotion");

            entity.HasIndex(e => e.MemberUserId, "FK_Promotion_Member_idx");

            entity.HasIndex(e => e.MembershipIdMembership, "FK_Promotion_Membership_idx");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Discount).HasPrecision(10, 2);
            entity.Property(e => e.MemberUserId).HasColumnName("Member_User_Id");
            entity.Property(e => e.MembershipIdMembership).HasColumnName("Membership_IdMembership");
            entity.Property(e => e.ValidFrom).HasColumnType("datetime");
            entity.Property(e => e.ValidUntil).HasColumnType("datetime");

            entity.HasOne(d => d.MemberUser).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.MemberUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Promotion_Member");

            entity.HasOne(d => d.MembershipIdMembershipNavigation).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.MembershipIdMembership)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Promotion_Membership");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("trainer");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("User_Id");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Specialization).HasMaxLength(45);
            entity.Property(e => e.Surname).HasMaxLength(20);
            entity.Property(e => e.WorkingTime).HasMaxLength(20);

            entity.HasOne(d => d.User).WithOne(p => p.Trainer)
                .HasForeignKey<Trainer>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Trainer_User");
        });

        modelBuilder.Entity<TrainingReservation>(entity =>
        {
            entity.HasKey(e => e.IdReservation).HasName("PRIMARY");

            entity.ToTable("trainingreservation");

            entity.HasIndex(e => e.TrainingSessionIdSession, "FK_TrainingReservation_TrainingSession_idx");

            entity.HasIndex(e => e.MemberUserId, "FK_TrainingSession_Member_idx");

            entity.Property(e => e.MemberUserId).HasColumnName("Member_User_Id");
            entity.Property(e => e.TrainingSessionIdSession).HasColumnName("TrainingSession_IdSession");

            entity.HasOne(d => d.MemberUser).WithMany(p => p.Trainingreservations)
                .HasForeignKey(d => d.MemberUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TrainingReservation_Member");

            entity.HasOne(d => d.TrainingSessionIdSessionNavigation).WithMany(p => p.Trainingreservations)
                .HasForeignKey(d => d.TrainingSessionIdSession)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TrainingReservation_TrainingSession");
        });

        modelBuilder.Entity<TrainingSession>(entity =>
        {
            entity.HasKey(e => e.IdSession).HasName("PRIMARY");

            entity.ToTable("trainingsession");

            entity.HasIndex(e => e.HallIdHall, "FK_TrainingSession_Hall_idx");

            entity.HasIndex(e => e.TrainerUserId, "FK_TrainingSession_Trainer_idx");

            entity.Property(e => e.HallIdHall).HasColumnName("Hall_IdHall");
            entity.Property(e => e.Session).HasMaxLength(50);
            entity.Property(e => e.TrainerUserId).HasColumnName("Trainer_User_Id");

            entity.HasOne(d => d.HallIdHallNavigation).WithMany(p => p.Trainingsessions)
                .HasForeignKey(d => d.HallIdHall)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TrainingSession_Hall");

            entity.HasOne(d => d.TrainerUser).WithMany(p => p.Trainingsessions)
                .HasForeignKey(d => d.TrainerUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TrainingSession_Trainer");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.AccountType).HasMaxLength(20);
            entity.Property(e => e.EmailAddress).HasMaxLength(20);
            entity.Property(e => e.Font).HasMaxLength(45);
            entity.Property(e => e.Password).HasMaxLength(512);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}