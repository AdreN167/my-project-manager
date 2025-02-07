using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyProjectManager.Domain.Settings;
using System.Text;

namespace MyProjectManager.Api;

public static class Startup
{
    /// <summary>
    /// Подключение авторизации
    /// </summary>
    /// <param name="services"></param>
    public static void AddAuthenticationAndAuthorization(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddAuthorization();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var options = builder.Configuration.GetSection(JwtSettings.DefaultSection).Get<JwtSettings>();

            o.Authority = options.Authority;
            o.RequireHttpsMetadata = false;
            o.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = options.Issuer,
                ValidAudience = options.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.JwtKey)),
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });
    }

    /// <summary>
    /// Подключение Swagger
    /// </summary>
    /// <param name="app"></param>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddApiVersioning()
            .AddApiExplorer(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "PJM",
                Description = "This is version 1.0",
                Contact = new OpenApiContact()
                {
                    Name = "Phone number: 88005553535",
                    Url = new Uri("https://vk.com/andreychudinov123")
                }
            });

            options.SwaggerDoc("v2", new OpenApiInfo()
            {
                Version = "v2",
                Title = "PJM",
                Description = "This is version 2.0",
                Contact = new OpenApiContact()
                {
                    Name = "Phone number: 88005553535",
                    Url = new Uri("https://vk.com/andreychudinov123")
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
                In = ParameterLocation.Header,
                Description = "Введите валидный токен, пожалуйста",
                Name = "Авторизация",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { 
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    Array.Empty<string>()
                }
            });
        });


    }
}
