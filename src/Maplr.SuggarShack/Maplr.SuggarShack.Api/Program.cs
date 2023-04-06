using Maplr.SuggarShack.Api.Features.Cart;
using Maplr.SuggarShack.Api.Features.Order;
using Maplr.SuggarShack.Api.Features.Products;
using Maplr.SuggarShack.Api.Infrastructure;
using Maplr.SuggarShack.Api.Security;
using Maplr.SuggarShack.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddControllersWithViews()
                    .AddJsonOptions(
                        opts =>
                        {
                            var enumConverter = new JsonStringEnumConverter();
                            opts.JsonSerializerOptions.Converters.Add(enumConverter);
                        });

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<MaplrContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

builder.Services
    .AddLogging(logging => logging.AddConsole())
    .AddRepositories()
    .AddServices();


builder.Services.AddCors(options =>
{
    options.AddPolicy("Access-Control-Allow-Origin", builder =>
    {
        builder.SetIsOriginAllowed(origin =>
        {
            var uri = new Uri(origin);

            var isAllowed = uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);

            return isAllowed;
        })
        .AllowAnyHeader()
        .AllowAnyMethod()
        .Build();
    });
});

builder.Services.AddSession();

// configure basic authentication 
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<CustomBasicAuthenticationSchemeOptions, CustomBasicAuthenticationHandler>("BasicAuthentication", null);
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCartRoutes();
app.MapProductRoutes();
app.MapOrderRoutes();

app.UseCors("Access-Control-Allow-Origin");

app.UseSession();

app.Run();