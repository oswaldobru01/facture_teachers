using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace facture_teachers.Models.DB
{
    public partial class facture_profesoresContext : DbContext
    {
        public facture_profesoresContext(DbContextOptions<facture_profesoresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=OSWALDOBRU\\OSWALDOBRU;Database=facture_profesores;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Course)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("course");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.DictatedHours).HasColumnName("dictatedHours");

                entity.Property(e => e.IdTeacher).HasColumnName("idTeacher");

                entity.Property(e => e.Value).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teacher_Id");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.HourlyRate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Identification)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentCurrent)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}