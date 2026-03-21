using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Context
{
    public sealed class TaskDbContext : DbContext
    {

        public TaskDbContext(DbContextOptions<UserDbContext> options)
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
                    .HasColumnName("Created_At_Utc")
                    .IsRequired();

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(x => x.CreatedByUserId);

                entity.Property(x => x.CreatedByUserId)
                    .HasColumnName("Created_By_User_Id")
                    .IsRequired();
            });

            }
    }
}
