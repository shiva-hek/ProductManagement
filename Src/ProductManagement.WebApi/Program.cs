using ProductManagement.Domain.ServiceConfiguration;
using ProductManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Infrastructure.Persistence.ServiceConfiguration;
using ProductManagement.WebApi.ServiceConfiguration;
using ProductManagement.Application.ServiceConfiguration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ProductDbContext>(options => options.UseSqlServer("Server=localhost;Database=ProductDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
//builder.Services.AddSwaggerGen();
builder.Services.AddDomainServices().AddRepositories().AddApplicationServices().AddApiServices();
builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
