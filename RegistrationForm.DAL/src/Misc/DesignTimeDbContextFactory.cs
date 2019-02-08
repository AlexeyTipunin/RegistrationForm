using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RegistrationForm.DAL.src.Context;

namespace RegistrationForm.DAL.src.Misc
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RegistrationDbContext>
    {
        public RegistrationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RegistrationDbContext>();
            optionsBuilder.UseSqlite("DataSource=:memory:");
            return new RegistrationDbContext(optionsBuilder.Options);
        }
    }
}