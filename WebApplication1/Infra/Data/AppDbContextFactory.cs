using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApplication1.Infra.Data
{
    public class AppDbContextFactory
        : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=aaaaaaa.mssql.somee.com;Database=aaaaaaa;User Id=Willzkynho_SQLLogin_1;Password=abyraf164m;TrustServerCertificate=True;MultipleActiveResultSets=true"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
