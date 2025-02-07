using MyProjectManager.Api;
using MyProjectManager.Application.DependencyInjection;
using MyProjectManager.DAL.DependencyInjection;
using MyProjectManager.Domain.Settings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// вытаскиваем данные про jwt и appsettings.json
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.DefaultSection));

builder.Services.AddControllers();
builder.Services.AddAuthenticationAndAuthorization(builder);
builder.Services.AddSwagger();


// подключаем Serilog
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// регистрируем весь слой DAL одним методом
builder.Services.AddDataAccessLayer(builder.Configuration);
// регистрируем весь слой Application
builder.Services.AddApplication();

// добавляем CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => {
        builder.WithOrigins("http://localhost:5104")
           .AllowAnyMethod()
           .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PJM Swagger v1.0");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "PJM Swagger v2.0");
        //c.RoutePrefix = string.Empty; // с этой строчкой не работает

    });
}

app.UseCors();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
