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
            //Database.EnsureCreated();
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarType> CarTypes { get; set; }

        public DbSet<TachographInfo> TachographInfos { get; set; }
    }
}
