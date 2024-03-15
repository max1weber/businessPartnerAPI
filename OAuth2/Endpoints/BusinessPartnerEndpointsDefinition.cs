using Asp.Versioning;
using EndpointDefinitions;
using Microsoft.EntityFrameworkCore;

namespace OAuth2.Endpoints
{
    public  class BusinessPartnerEndpointsDefinition : IEndpointDefinition
    {
        

       

        public  void DefineEndpoints(WebApplication app)
        {

            var versionset = app.NewApiVersionSet()
                    .HasApiVersion(new ApiVersion(2))
                    .HasDeprecatedApiVersion(new ApiVersion(1))
                    .Build();


                RouteGroupBuilder routeGroup =   app.MapGroup("api/v{apiVersion:apiVersion}").WithApiVersionSet(versionset);
            
            routeGroup.MapGet("/businesspartners", async (BusinessPartnerDb db) =>
            {
                db.Database.EnsureCreated();
                var result = await db.BusinessPartners.ToListAsync();
                return result;

            }).WithName("GetBusinessPartners");

            routeGroup.MapGet("/businesspartners/active", async (BusinessPartnerDb db) =>
                await db.BusinessPartners.Where(t => t.IsActive).ToListAsync()).WithName("GetActiveBusinessPartners").RequireAuthorization();

            routeGroup.MapGet("/businesspartners/{id}", async (Guid id, BusinessPartnerDb db) =>
                await db.BusinessPartners.FindAsync(id)
                    is BusinessPartner bp
                        ? Results.Ok(bp)
                        : Results.NotFound());

            routeGroup.MapPost("/businesspartners", async (BusinessPartner bp, BusinessPartnerDb db) =>
            {
                db.BusinessPartners.Add(bp);
                await db.SaveChangesAsync();

                return Results.Created($"/businesspartners/{bp.Id}", bp);
            })
            .WithName("CreateBusinessPartner")
            .RequireAuthorization();

            routeGroup.MapPut("/businesspartners/{id}", async (Guid id, BusinessPartner inputBusinessPartner, BusinessPartnerDb db) =>
            {
                var bp = await db.BusinessPartners.FindAsync(id);

                if (bp is null) return Results.NotFound();

                bp.Name = inputBusinessPartner.Name;
                bp.IsActive = inputBusinessPartner.IsActive;

                await db.SaveChangesAsync();

                return Results.NoContent();
            }).WithName("UpdateBusinessPartners")
            .RequireAuthorization();
;

            routeGroup.MapDelete("/businesspartners/{id}", async (Guid id, BusinessPartnerDb db) =>
            {
                if (await db.BusinessPartners.FindAsync(id) is BusinessPartner bp)
                {
                    db.BusinessPartners.Remove(bp);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }

                return Results.NotFound();
            }).WithOpenApi(generatedOperation =>
            {
                var parameter = generatedOperation.Parameters[0];
                parameter.Description = "The ID associated with the BusinessPartner to be deleted";



                return generatedOperation;
            })
            .WithName("DeleteBusinessPartnersById")
            .RequireAuthorization();



            routeGroup.MapGet("/", () => "Hello BusinessPartner!");
        }

        public  void DefineServices(IServiceCollection services)
        {
           services.AddDatabaseDeveloperPageExceptionFilter();
           services.AddDbContext<BusinessPartnerDb>(opt => opt.UseInMemoryDatabase("BusinessPartnerList"));
        }
    }
}