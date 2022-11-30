using Application.Common.Interfaces.Services;
using Domain.Repositories;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WordCalculatorDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IWordCalculatorDbContext>(provider => provider.GetRequiredService<WordCalculatorDbContext>());

            services
                .AddHttpClient()
                .AddTransient<ITextRepository, TextRepository>()
                .AddTransient<ITextFileService, TextFileService>()
                .AddTransient<IWordCalculatorService, WordCalculatorService>()
                .AddTransient<IWebServiceClient, HttpWebServiceClient>();

            return services;
        }
    }
}
