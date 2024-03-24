namespace EmployeeApp;


using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext
{
    public DbSet<Employee> Employees { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Port=7777; Database=uvsproject; Username=postgres; Password=guest");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("employees"); 

            entity.Property(e => e.EmployeeId).HasColumnName("employeeid"); 
            entity.Property(e => e.EmployeeName).HasColumnName("employeename"); 
            entity.Property(e => e.EmployeeSalary).HasColumnName("employeesalary"); 
        });
    }
}
