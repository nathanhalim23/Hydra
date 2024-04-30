using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HydraDataAccess.Models
{
    public partial class HydraContext : DbContext
    {
        public HydraContext()
        {
        }

        public HydraContext(DbContextOptions<HydraContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BootcampClass> BootcampClasses { get; set; } = null!;
        public virtual DbSet<Candidate> Candidates { get; set; } = null!;
        public virtual DbSet<CandidateEvaluation> CandidateEvaluations { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;
        public virtual DbSet<Trainer> Trainers { get; set; } = null!;
        public virtual DbSet<TrainerSkillDetail> TrainerSkillDetails { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-E7OFDQ3A;Initial Catalog=Hydra;Trusted_Connection=True;User=basilisktf;Password=basilisktf;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BootcampClass>(entity =>
            {
                entity.ToTable("BootcampClass");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Progress).HasDefaultValueSql("((1))");

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.ToTable("Candidate");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Domicile)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(18)
                    .IsUnicode(false);

                entity.HasOne(d => d.BootcampClass)
                    .WithMany(p => p.Candidates)
                    .HasForeignKey(d => d.BootcampClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Candidate__Bootc__5BE2A6F2");
            });

            modelBuilder.Entity<CandidateEvaluation>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.CandidateId })
                    .HasName("PK__Candidat__14D8481E080B10F2");

                entity.ToTable("CandidateEvaluation");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.CandidateEvaluations)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Candidate__Candi__628FA481");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CandidateEvaluations)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Candidate__Cours__4D5F7D71");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Level)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Id)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.EvaluationDate).HasColumnType("date");

                entity.Property(e => e.Progress).HasDefaultValueSql("((1))");

                entity.Property(e => e.SkillId)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.BootcampClass)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.BootcampClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course__Bootcamp__14270015");

                entity.HasOne(d => d.TrainerSkillDetail)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => new { d.TrainerId, d.SkillId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course__59063A47");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("Skill");

                entity.Property(e => e.Id)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Skill__CategoryI__71D1E811");
            });

            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.ToTable("Trainer");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAvailable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(18)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Trainers)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__Trainer__Usernam__49C3F6B7");
            });

            modelBuilder.Entity<TrainerSkillDetail>(entity =>
            {
                entity.HasKey(e => new { e.TrainerId, e.SkillId })
                    .HasName("PK__TrainerS__5B90136453C108DF");

                entity.Property(e => e.SkillId)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.AchievedDate).HasColumnType("date");

                entity.Property(e => e.AssignedDate).HasColumnType("date");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.TrainerSkillDetails)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrainerSk__Skill__5441852A");

                entity.HasOne(d => d.Trainer)
                    .WithMany(p => p.TrainerSkillDetails)
                    .HasForeignKey(d => d.TrainerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrainerSk__Train__534D60F1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__User__536C85E578B0FFE8");

                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UQ__User__A9D1053433DE235C")
                    .IsUnique();

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Usernames)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserRole__RoleId__46E78A0C"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("Username").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserRole__Userna__45F365D3"),
                        j =>
                        {
                            j.HasKey("Username", "RoleId").HasName("PK__UserRole__EBC32904AC7F79F2");

                            j.ToTable("UserRole");

                            j.IndexerProperty<string>("Username").HasMaxLength(20).IsUnicode(false);
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
