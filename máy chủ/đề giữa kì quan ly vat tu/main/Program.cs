using ConstructionMaterialsApi.Middlewares;
using ConstructionMaterialsApi.Services.Implementations;
using ConstructionMaterialsApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ConstructionMaterialsApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Cấu hình Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ============================================================
// Đăng ký Dependency Injection (DI)
// ============================================================

// Cách 1: Dùng Method Syntax
builder.Services.AddScoped<IMaterialService, MethodSyntaxMaterialService>();

// Cách 2: Dùng Query Syntax (comment dòng trên, bỏ comment dòng dưới)
// builder.Services.AddScoped<IMaterialService, QuerySyntaxMaterialService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Đăng ký Global Exception Handling Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Construction Materials API v1");
    });
}

app.MapControllers();

app.Run();
