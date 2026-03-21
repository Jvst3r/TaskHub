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
        base.OnModelCreating(modelBuilder);
    }
}