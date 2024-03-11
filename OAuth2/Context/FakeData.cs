


using System.Reflection.Metadata;
using Bogus;
using Microsoft.IdentityModel.Tokens;

static  class FakeData 
{

     public static List<BusinessPartner> BusinessPartners = new List<BusinessPartner>();

      public static void Init(int count)
      {
          
        Randomizer.Seed = new Random(8675309);
       
        var faker = new Faker<BusinessPartner>()
            .CustomInstantiator(f => new BusinessPartner( Random.Shared.Next().ToString("D" + 10)))
            .RuleFor(p => p.Name, f=> f.Company.CompanyName() )
            .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
            .RuleFor(p => p.Id, f=> Guid.NewGuid() )
            .RuleFor(p=>p.BusinessPartnerId, f=> f.Company.CompanyName())
            .RuleFor(p => p.dateTime, f=> f.Date.Past(1, DateTime.Now) )
            .RuleFor(p=>p.IsActive, f=> f.Random.Bool());


        var generated = faker.Generate(count);

        FakeData.BusinessPartners.AddRange(generated);

      }
   
}