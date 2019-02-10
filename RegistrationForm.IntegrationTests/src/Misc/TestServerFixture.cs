using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using RegistrationForm.DAL.src.Context;

namespace RegistrationForm.IntegrationTests.src.Misc
{
    public class TestServerFixture : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<TestStartup>()
                .UseEnvironment("Development")
                .UseContentRoot(GetContentRootPath());

            _testServer = new TestServer(builder);

            var host = _testServer.Host;
            using (var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var db = scope.ServiceProvider.GetService<RegistrationDbContext>())
            {
                db.Database.Migrate();
            }

            Client = _testServer.CreateClient();
        }

        private string GetContentRootPath()
        {
            var dir = new DirectoryInfo(PlatformServices.Default.Application.ApplicationBasePath);
            for (var i = 0; i < 4; i++)
                dir = dir.Parent;
            const string relativePathToHostProject = "RegistrationForm";
            return Path.Combine(dir.FullName, relativePathToHostProject);
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}