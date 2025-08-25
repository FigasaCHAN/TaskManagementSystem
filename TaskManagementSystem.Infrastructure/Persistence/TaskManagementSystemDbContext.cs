using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Core.Domain.AggregateRoots;
using Task = TaskManagementSystem.Core.Domain.AggregateRoots.Task;

namespace TaskManagementSystem.Infrastructure.Persistence;

public class TaskManagementSystemDbContext : DbContext
{
    public TaskManagementSystemDbContext(DbContextOptions<TaskManagementSystemDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Task> Tasks => Set<Task>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        UserEntityConfiguration(modelBuilder);
        TaskEntityConfiguration(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void UserEntityConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.OwnsOne(u => u.Username, username =>
            {
                username.Property(e => e.Value)
                    .HasColumnName("Username")
                    .IsRequired()
                    .HasMaxLength(50);
            });

            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .IsRequired();
            });

            builder.OwnsOne(u => u.Password, password =>
            {
                password.Property(p => p.Value)
                    .HasColumnName("PasswordHash")
                    .IsRequired();
            });

            builder.Property(u => u.Deleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(u => u.Version)
                .IsConcurrencyToken();
        });
    }

    private static void TaskEntityConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>(builder =>
        {
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Description)
                .HasMaxLength(1000);

            builder.Property(t => t.Status)
                .IsRequired();

            builder.OwnsOne(t => t.CreatedBy, createdInfo =>
            {
                createdInfo.Property(e => e.CreatedAt)
                    .HasColumnName("CreatedAt")
                    .IsRequired();
                createdInfo.Property(e => e.CreatedBy)
                    .HasColumnName("CreatedBy")
                    .IsRequired();
            });

            builder.OwnsOne(t => t.LastModified, createdInfo =>
            {
                createdInfo.Property(e => e.LastModifiedAt)
                    .HasColumnName("LastModifiedAt")
                    .IsRequired(false);
                createdInfo.Property(e => e.LastModifiedBy)
                    .HasColumnName("LastModifiedBy")
                    .IsRequired(false);
            });

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.History)
                .WithOne()
                .HasForeignKey(h => h.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(t => t.Version)
                .IsConcurrencyToken();
        });
    }
}