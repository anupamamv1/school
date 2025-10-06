using Microsoft.EntityFrameworkCore;
using SchoolMgmt.Web.Models;

namespace SchoolMgmt.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Qualification> Qualifications => Set<Qualification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Student>()
            .HasIndex(s => s.Username).IsUnique();
        modelBuilder.Entity<Student>()
            .HasIndex(s => s.Email).IsUnique();
        modelBuilder.Entity<Student>()
            .Property(s => s.StudentCode).IsRequired();
        modelBuilder.Entity<Qualification>()
            .HasOne(q => q.Student)
            .WithMany(s => s.Qualifications)
            .HasForeignKey(q => q.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
