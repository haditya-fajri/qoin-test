using Microsoft.EntityFrameworkCore;

namespace Task1.Models;

public class QoinContext : DbContext
{
    public QoinContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Test01>? Test01s { get; set; }
}