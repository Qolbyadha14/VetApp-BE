using Microsoft.EntityFrameworkCore;

namespace VetApp_BE.Config
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options)
             : base(options)
        {
        }

    }
}
