using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Tools.Application.Interfaces;
using Tools.Application.Services;
using Tools.Infrastructure.Context;
using Tools.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<ToolsDbContext>(options =>
{
    options.UseSqlite("Data Source=tools.db");
});

builder.Services.AddScoped<IToolRepository, ToolRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IToolService, ToolService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Tools API",
        Version = "v1",
        Description = "API para gerenciamento de ferramentas"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tools API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();