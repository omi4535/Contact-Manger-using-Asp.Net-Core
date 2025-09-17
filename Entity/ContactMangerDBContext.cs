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
           // modelBuilder.Entity<Country>().ToTable("Coutry");
            modelBuilder.Entity<Person>().Property(temp => temp.Address)
                .HasColumnName("FullAddress")
                .HasColumnType("varchar(8)")
                .HasDefaultValue("Pune");

            modelBuilder.Entity<Person>()
                .HasCheckConstraint("Chk_fName" ,"len(FirstName)>3");

            modelBuilder.Entity<Person>()
                .HasOne<Country>(temp => temp.country)
                .WithMany(temp => temp.PeopleFromCountry)
                .HasForeignKey(temp => temp.CountryId);
        }
        public async Task<List<Person>> GetAllPerson()
        {
            return await people.FromSqlRaw("exec GetAllPerson").ToListAsync();
        }
    }
}
