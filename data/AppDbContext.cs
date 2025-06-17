using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.models;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.data
{
    // Data/AppDbContext.cs
    public class AppDbContext : DbContext
    {
        public DbSet<State> States => Set<State>();
        public DbSet<TaskItem> Tasks => Set<TaskItem>();
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>().HasIndex(s => s.Name).IsUnique();

            modelBuilder.Entity<State>().HasData(
                new State { Id = 1, Name = "Pendiente", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new State { Id = 2, Name = "En Progreso", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new State { Id = 3, Name = "Completado", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) }
            );
            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Email = "admin@gmail.com",
                Password = "123456" // ⚠️ Solo para pruebas. En producción debe ser hasheado.
            }
            );


        }
    }

}
