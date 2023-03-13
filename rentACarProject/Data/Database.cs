using Microsoft.EntityFrameworkCore;
using rentACarProject.Models.Domain;

namespace rentACarProject.Data
{
    public class Database : DbContext // veritabanımız hazır gelen Dbcontextin özelliklerini implemente eder.
    {
        internal readonly object Customer;

        public Database(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Müşteriler { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}
