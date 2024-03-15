using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using EndpointDefinitions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

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


var domain = builder.Configuration["OAuth:Domain"];
var audience = builder.Configuration["OAuth:Audience"];


builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.AddAuthorizationBuilder()
  .AddPolicy("businesspartner", policy =>
        policy
            .RequireRole("businesspartner")
            .RequireClaim("scope", "businesspartner_scope"));


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();




app.UseEndpointDefinitions();
app.Run();
