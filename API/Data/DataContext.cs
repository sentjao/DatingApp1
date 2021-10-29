using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        #region ctor
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        #endregion

        #region properties
        public DbSet<AppUser> Users { get; set; }
        #endregion
    }
}