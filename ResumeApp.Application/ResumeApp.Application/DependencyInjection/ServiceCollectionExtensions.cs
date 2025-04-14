using Microsoft.Extensions.DependencyInjection;
using ResumeApp.Application.Interfaces;
using ResumeApp.Application.Services;

namespace ResumeApp.Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IDegreeService, DegreeService>();

            return services;
        }
    }
}
