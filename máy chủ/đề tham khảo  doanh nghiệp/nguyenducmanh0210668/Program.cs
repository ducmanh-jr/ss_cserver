using Microsoft.EntityFrameworkCore;
using nguyenducmanh0210668.DbContexts;
using nguyenducmanh0210668.Exceptions;
using nguyenducmanh0210668.Services.Implements;
using nguyenducmanh0210668.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext0210668De1>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEnterpriseService0210668De1, EnterpriseService0210668De1>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware0210668De1>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
