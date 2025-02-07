using MyProjectManager.Api;
using MyProjectManager.Application.DependencyInjection;
using MyProjectManager.DAL.DependencyInjection;
using MyProjectManager.Domain.Settings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ����������� ������ ��� jwt � appsettings.json
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.DefaultSection));

builder.Services.AddControllers();
builder.Services.AddAuthenticationAndAuthorization(builder);
builder.Services.AddSwagger();


// ���������� Serilog
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// ������������ ���� ���� DAL ����� �������
builder.Services.AddDataAccessLayer(builder.Configuration);
// ������������ ���� ���� Application
builder.Services.AddApplication();

// ��������� CORS
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
        //c.RoutePrefix = string.Empty; // � ���� �������� �� ��������

    });
}

app.UseCors();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
