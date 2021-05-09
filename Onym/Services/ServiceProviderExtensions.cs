using Microsoft.Extensions.DependencyInjection;

namespace Onym.Services
{
    public static class ServiceProviderExtensions
    {
        public static void AddEmailService(this IServiceCollection services)
        {
            services.AddTransient<EmailService>();
        }
    }
}