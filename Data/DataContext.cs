using demo.WebApiDotNetCore8.Minimal.Models;
using Microsoft.EntityFrameworkCore;

namespace demo.WebApiDotNetCore8.Minimal.Data
{
   public class DataContext: DbContext
   {
      public DataContext(DbContextOptions<DataContext> options) : base(options) { }

      public DbSet<Court> Courts { get; set; }
   }
}
