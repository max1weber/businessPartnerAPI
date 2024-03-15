



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
       
            app.UseSwagger();
            app.UseSwaggerUI( c=> c.SwaggerEndpoint("/swagger/v1/swagger.json", _title));
       
    }

    public void DefineServices(IServiceCollection services)
    {


            var securityScheme = new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JSON Web Token based security;  Enter the token with the `Bearer: ` prefix, e.g. 'Bearer abcde12345' "
                };

                var securityReq = new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                };



        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen( c=> {

            c.SwaggerDoc("v1", new OpenApiInfo(){ Title = _title, Description = _description, Version = _version, Contact = _openApiContact});
            c.AddSecurityDefinition("Bearer",securityScheme );
            c.AddSecurityRequirement(securityReq);
        });
    }
}
