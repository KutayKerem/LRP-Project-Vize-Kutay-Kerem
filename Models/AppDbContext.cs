using Microsoft.EntityFrameworkCore;

namespace LabYonetimSistemi.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Lab> Labs { get; set; }
    public DbSet<Computer> Computers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Issue> Issues { get; set; }
}