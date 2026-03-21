using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Context;

/// <summary>
/// Контекст базы данных пользователей
/// </summary>
public sealed class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Пользователи и Задачи
    /// </summary>
    public DbSet<User> Users => Set<User>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(200);

            entity.Property(x => x.LastActivityUtc)
                .HasColumnName("last_activity_utc")
                .IsRequired();
        });
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

        base.OnModelCreating(modelBuilder);
    }
}