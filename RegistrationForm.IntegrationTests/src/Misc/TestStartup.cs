using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RegistrationForm.IntegrationTests.src.Misc
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureDatabase(IServiceCollection services)
        {
            // here we can change DB context injection
            base.ConfigureDatabase(services);
        }
    }
}