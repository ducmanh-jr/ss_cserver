using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDucManh0210668.DbContexts;
using NguyenDucManh0210668.Services.Implements;
using NguyenDucManh0210668.Services.Interfaces;
using NguyenDucManh0210668.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext0210668De1>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 0)));
});

builder.Services.AddScoped<INhanVienService0210668De1, NhanVienService0210668De1>();
builder.Services.AddScoped<IDuAnService0210668De1, DuAnService0210668De1>();
builder.Services.AddScoped<IPhanCongService0210668De1, PhanCongService0210668De1>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(modelState => modelState.Value?.Errors.Count > 0)
            .SelectMany(modelState => modelState.Value!.Errors.Select(error => string.IsNullOrWhiteSpace(error.ErrorMessage)
                ? "Dữ liệu không hợp lệ."
                : error.ErrorMessage))
            .ToList();

        var response = ApiResponse0210668De1<object>.Fail(
            string.Join(" ", errors),
            StatusCodes.Status400BadRequest);

        return new BadRequestObjectResult(response);
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware0210668De1>();
app.MapControllers();
app.Run();
