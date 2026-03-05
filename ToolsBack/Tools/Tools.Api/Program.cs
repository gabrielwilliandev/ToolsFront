using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Tools.Application.Interfaces;
using Tools.Application.UseCases.Comands;
using Tools.Application.UseCases.CreateTool;
using Tools.Application.UseCases.Queries;
using Tools.Infrastructure.Context;
using Tools.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<ToolsDbContext>(optionsAction =>
{
    optionsAction.UseInMemoryDatabase("ToolsDb");
});

builder.Services.AddScoped<IToolRepository, ToolRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<CreateToolUseCase>();
builder.Services.AddScoped<UpdateToolUseCase>();
builder.Services.AddScoped<DeleteToolUseCase>();
builder.Services.AddScoped<ToolQueryService>();


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