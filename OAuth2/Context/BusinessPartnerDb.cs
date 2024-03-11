using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

class BusinessPartnerDb : DbContext
{
    public BusinessPartnerDb(DbContextOptions<BusinessPartnerDb> options)
        : base(options) { 


            

        }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        

        FakeData.Init(1000);
        var data = FakeData.BusinessPartners.ToArray();
        modelBuilder.Entity<BusinessPartner>().HasData(data);
            
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        

    }
    public DbSet<BusinessPartner> BusinessPartners => Set<BusinessPartner>();
}