



using EndpointDefinitions;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;

public class SwaggerEndpointDefinition : IEndpointDefinition
{


    string _version = "v1";
    string _title = "BusinessPartner API";

    string _description = "BusinessPartner API POC voor EntraId External Identities";

    OpenApiContact _openApiContact = new OpenApiContact() { Email = "m.j.weber@gasunie.nl", Name = "Weber"};
    public void DefineEndpoints(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI( c=> c.SwaggerEndpoint("/swagger/v1/swagger.json", _title));
        }
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen( c=> {

            c.SwaggerDoc("v1", new OpenApiInfo(){ Title = _title, Description = _description, Version = _version, Contact = _openApiContact});

        });
    }
}
