using _1p_atom_carmanager.backend.core.Abstractions;
using _1p_atom_carmanager.backend.core.Entities;
using _1p_atom_carmanager.backend.core.Mapping;
using _1p_atom_carmanager.backend.core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace _1p_atom_carmanager.backend.core
{
    /// <summary>
    /// Регистрация зависимостей проекта
    /// </summary>
    public static class Entry
    {
        /// <summary>
        /// Регистрация зависимостей проекта
        /// </summary>
        /// <param name="services">сервисы</param>
        /// <returns>Контейнер зависимостей</returns>
        public static IServiceCollection AddCore(this IServiceCollection services, string con)
        {
            if (services is null)
            {
                throw new System.ArgumentNullException(nameof(services));
            }

            services.AddScoped<ICarManagerService, CarManagerService>();

            services.AddDbContext<ContextDb>(options => options.UseSqlServer(con));
            //services.AddMemoryCache();
            services.AddAutoMapper(typeof(AppMappingProfile));
            return services;
        }
    }
}
