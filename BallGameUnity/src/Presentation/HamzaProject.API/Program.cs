using HamzaProject.Application.Interfaces;
using HamzaProject.Application.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Hamza Project API", 
        Version = "v1",
        Description = "API for Hamza Project"
    });
});

// Service registrations
builder.Services.AddScoped<IMessageService, MessageService>();

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hamza Project API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("AllowReactApp");

// Eğer HTTPS yönlendirmesi sorun yaratıyorsa, bu satırı kaldırabilirsiniz
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run(); 