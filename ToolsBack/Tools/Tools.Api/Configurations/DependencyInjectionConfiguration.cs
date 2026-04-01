using Tools.Api.Filters;
using Tools.Application.Interfaces;
using Tools.Application.Notifications;
using Tools.Application.Services;
using Tools.Application.Services.Auth;
using Tools.Application.Services.Contacts;
using Tools.Application.Services.Email;
using Tools.Infrastructure.Repositories;

namespace Tools.Api.Configurations;
public static class DependencyInjectionConfiguration
{
    public static void Configuration(this IServiceCollection services)
    {
          // Repositories
          services.AddScoped<IToolRepository, ToolRepository>();
          services.AddScoped<ITagRepository, TagRepository>();
          services.AddScoped<IContactRepository, ContactRepository>();
          services.AddScoped<IUserRepository, UserRepository>();

          // Services
          services.AddScoped<IToolService, ToolService>();
          services.AddScoped<NotificationContext>();
          services.AddScoped<NotificationFilter>();        
          services.AddScoped<IContactService, ContactService>();
          services.AddScoped<IEmailService, EmailService>();
          services.AddScoped<IAuthService, AuthService>();
          services.AddScoped<TokenService>();
    }
}
