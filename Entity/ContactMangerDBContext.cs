using Entity;
using Microsoft.EntityFrameworkCore;

namespace services
{
    public class ContactMangerDBContext :DbContext
    {
        public ContactMangerDBContext(DbContextOptions<ContactMangerDBContext> dBContextOptions) :base
            (dBContextOptions)
        {
            
        }
        public DbSet<Country> countries { get; set; }
        public DbSet<Person> people { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Country>().ToTable("Coutry");
        }
    }
}
