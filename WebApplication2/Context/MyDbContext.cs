    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using WebApplication2.Model;
    using WebApplication2.Models;

    namespace WebApplication2.Context;

    public partial class MyDbContext : DbContext
    {

        public virtual DbSet<course> course { get; set; }

        public virtual DbSet<grade> grade { get; set; }

        public virtual DbSet<student> student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(
                "data source=dat154demo.database.windows.net;Initial Catalog=dat154;User ID=dat154_rw;Password=Svart_Katt;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           
            modelBuilder.Entity<course>(entity =>
            {
                entity.HasKey(e => e.coursecode).HasName("pk_course");

                entity.Property(e => e.coursecode).IsFixedLength();
                entity.Property(e => e.semester).IsFixedLength();
            });

            modelBuilder.Entity<grade>(entity =>
            {
                entity.HasKey(e => new { e.coursecode, e.studentid }).HasName("pk_grade");

                entity.Property(e => e.coursecode).IsFixedLength();
                entity.Property(e => e.grade1).IsFixedLength();
                
                

                entity.HasOne(d => d.coursecodeNavigation).WithMany(p => p.grade)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_course");

                entity.HasOne(d => d.student).WithMany(p => p.grade)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_student");
            });

            modelBuilder.Entity<student>(entity =>
            {
                entity.HasKey(e => e.id).HasName("Primary");

                entity.Property(e => e.id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
        
    }
