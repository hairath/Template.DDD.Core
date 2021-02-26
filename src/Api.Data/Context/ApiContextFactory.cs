using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ApiContextFactory
    : IDesignTimeDbContextFactory<ApiContext>
    {
        public ApiContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=HAI-NOTE\\SQLDEV;Database=TSTDBDDD;User Id=sa; Password=h@irath001;";
            var optionBuilder = new DbContextOptionsBuilder<ApiContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new ApiContext(optionBuilder.Options);
        }
    }
}
