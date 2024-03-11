using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using EndpointDefinitions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc.TagHelpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAllEndpointDefinitions();
builder.Services.AddApiVersioning( option => {

                                    option.DefaultApiVersion = new ApiVersion(1);
                                    option.ReportApiVersions = true;
                                    option.ApiVersionReader =  ApiVersionReader.Combine(
                                                    new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-apiversion"));})
                .AddApiExplorer(options => {
                options.GroupNameFormat= "'v'V";
                options.SubstituteApiVersionInUrl =true;
                });



builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();




var app = builder.Build();






app.UseEndpointDefinitions();
app.Run();
