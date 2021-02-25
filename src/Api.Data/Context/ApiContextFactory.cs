using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ApiContextFactory : IDesignTimeDbContextFactory<ApiContext>
    {
        public ApiContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost;Port=3307;Database=API_DDD_DB;User=hai;Password=H@irath001;";
            var optionBuilder = new DbContextOptionsBuilder<ApiContext>();
            optionBuilder.UseMySql(connectionString);

            return new ApiContext(optionBuilder.Options);
        }
    }
}
