using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceColletion)
        {
            serviceColletion.AddDbContext<ApiContext>(options => options.UseSqlServer("Server=HAI-NOTE\\SQLDEV;Database=TSTDBDDD;User Id=sa; Password=h@irath001;"));

            serviceColletion.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        }
    }
}
