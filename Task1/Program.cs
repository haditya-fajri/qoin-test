using Microsoft.EntityFrameworkCore;
using Task1.Models;
using Task1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<QoinContext>(
    opts => opts.UseMySql(builder.Configuration["ConnectionString"], new MariaDbServerVersion(new Version(11, 3)))
);
builder.Services.AddScoped<ITest01Service, Test01Service>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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