using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Tools.Api.Filters;
using Tools.Api.Middleware;
using Tools.Application.Interfaces;
using Tools.Application.Notifications;
using Tools.Application.Services;
using Tools.Application.Validators;
using Tools.Infrastructure.Context;
using Tools.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<NotificationFilter>();
});


builder.Services.AddValidatorsFromAssemblyContaining<CreateToolRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateToolRequestValidator>();
builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddDbContext<ToolsDbContext>(options =>
{
    options.UseSqlite("Data Source=tools.db");
});

builder.Services.AddScoped<IToolRepository, ToolRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IToolService, ToolService>();
builder.Services.AddScoped<NotificationContext>();
builder.Services.AddScoped<NotificationFilter>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Tools API",
        Version = "v1",
        Description = "API para gerenciamento de ferramentas"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

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