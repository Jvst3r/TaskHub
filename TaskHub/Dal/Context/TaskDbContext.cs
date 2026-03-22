using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Context
{
    public sealed class TaskDbContext : DbContext
    {

        public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options)
        {
        }

        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("tasks");

                entity.HasKey(x=>x.Id);

                entity.Property(x => x.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200);

                entity.Property(x => x.CreatedUtc)
                    .HasColumnName("created_at_utc")
                    .IsRequired();

                //entity.HasOne<User>()
                    //.WithMany()
                    //.HasForeignKey(x => x.CreatedByUserId)
                    //.HasConstraintName("FK_tasks_users_created_by_user_id");


                entity.Property(x => x.CreatedByUserId)
                    .HasColumnName("created_by_user_id")
                    .IsRequired();
            });
        }
    }
}
