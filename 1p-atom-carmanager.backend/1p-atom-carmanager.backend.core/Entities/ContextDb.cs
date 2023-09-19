using Microsoft.EntityFrameworkCore;

namespace _1p_atom_carmanager.backend.core.Entities
{
    public class ContextDb : DbContext
    {
        /// <summary>
        /// конуструктор класса контекста бд
        /// </summary>
        /// <param name="options">настройки</param>
        public ContextDb(DbContextOptions<ContextDb> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Машины
        /// </summary>
        public DbSet<Car> Cars { get; set; }

        /// <summary>
        /// Типы
        /// </summary>
        public DbSet<CarType> CarTypes { get; set; }

        /// <summary>
        /// Информации по тахографу
        /// </summary>
        public DbSet<TachographInfo> TachographInfos { get; set; }
    }
}
