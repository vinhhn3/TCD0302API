using Microsoft.EntityFrameworkCore;
using TCD0302API.Models;

namespace TCD0302API.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
    {

    }

    public DbSet<Park> Parks { get; set; }
  }
}
