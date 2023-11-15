namespace Server.DataAccess
{
    using Microsoft.EntityFrameworkCore;
using Server.DataModels;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<TimeLog> TimeLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=mydatabase.db");
}
}