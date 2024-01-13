using Microsoft.EntityFrameworkCore;

namespace Task2.Models;

public class QoinContext:DbContext
{
    private readonly IConfiguration _configuration;

    public QoinContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serverVersion = new MariaDbServerVersion(new Version(10, 7));
        optionsBuilder.UseMySql(_configuration["ConnectionString"], serverVersion);
    }


    public DbSet<Test01>? Test01s { get; set; }
}