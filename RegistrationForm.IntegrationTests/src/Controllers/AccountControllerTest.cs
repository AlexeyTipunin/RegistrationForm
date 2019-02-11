using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RegistrationForm.IntegrationTests.src.Misc;
using RegistrationForm.Models;
using Xunit;

namespace RegistrationForm.IntegrationTests.src.Controllers
{
    public class AccountControllerTest
    {
        protected const string Url = "api/Accounts";

        [Fact]
        public async Task Post_Success()
        {
            using (var server = new TestServerFixture())
            {
                var account = GetInitializedAccount();
                //
                var postResponse = await server.Client.PostAsync(Url,
                    new StringContent(account.Serialize(), Encoding.UTF8, "application/json"));
                var postResponseString = await postResponse.Content.ReadAsStringAsync();
                var addedAccount = postResponseString.DeSerialize<Account>();
                //
                var getResponse = await server.Client.GetAsync(postResponse.Headers.Location.AbsolutePath);
                var getResponseString = await getResponse.Content.ReadAsStringAsync();
                var gotAccount = getResponseString.DeSerialize<Account>();
                //
                postResponse.StatusCode.Should().Be(StatusCodes.Status201Created);
                addedAccount.Should().NotBeNull();
                addedAccount.AccountId.Should().NotBe(0);
                addedAccount.Login.Should().Be(account.Login);
                addedAccount.Agreement.Should().Be(account.Agreement);
                addedAccount.ProvinceId.Should().Be(account.ProvinceId);
                postResponse.Headers.Location.AbsolutePath.Should().EndWith($"/{addedAccount.AccountId}");
                //
                getResponse.StatusCode.Should().Be(StatusCodes.Status200OK);
                gotAccount.Should().BeEquivalentTo(addedAccount);
            }
        }

        [Fact]
        private async Task Post_NoAgreement()
        {
            using (var server = new TestServerFixture())
            {
                var account = GetInitializedAccount();
                account.Agreement = false;
                //
                var postResponse = await server.Client.PostAsync(Url,
                    new StringContent(account.Serialize(), Encoding.UTF8, "application/json"));
                var postResponseString = await postResponse.Content.ReadAsStringAsync();
                var error = JObject.Parse(postResponseString);
                //
                postResponse.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
                error["errors"]["Agreement"][0].Value<string>().Should().Be("You should agree to work for food.");
            }
        }

        [Fact]
        private async Task Post_PasswordConfirmationDoesNotMissMuch()
        {
            using (var server = new TestServerFixture())
            {
                var account = GetInitializedAccount();
                account.PasswordConfirmation = "otherPassword";
                //
                var postResponse = await server.Client.PostAsync(Url,
                    new StringContent(account.Serialize(), Encoding.UTF8, "application/json"));
                var postResponseString = await postResponse.Content.ReadAsStringAsync();
                var error = JObject.Parse(postResponseString);
                //
                postResponse.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
                error["errors"]["PasswordConfirmation"][0].Value<string>().Should().Be("Password confirmation must be the same with password.");
            }
        }

        [Fact]
        private async Task Post_PasswordRequired()
        {
            using (var server = new TestServerFixture())
            {
                var account = GetInitializedAccount();
                account.Password = null;
                //
                var postResponse = await server.Client.PostAsync(Url,
                    new StringContent(account.Serialize(), Encoding.UTF8, "application/json"));
                var postResponseString = await postResponse.Content.ReadAsStringAsync();
                var error = JObject.Parse(postResponseString);
                //
                postResponse.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
                error["errors"]["Password"][0].Value<string>().Should().Be("Password is required.");
            }
        }

        [Fact]
        private async Task Post_PasswordBadComplexity()
        {
            using (var server = new TestServerFixture())
            {
                var account = GetInitializedAccount();
                account.Password = "simplePassword";
                account.PasswordConfirmation = "simplePassword";
                //
                var postResponse = await server.Client.PostAsync(Url,
                    new StringContent(account.Serialize(), Encoding.UTF8, "application/json"));
                var postResponseString = await postResponse.Content.ReadAsStringAsync();
                var error = JObject.Parse(postResponseString);
                //
                postResponse.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
                error["errors"]["Password"][0].Value<string>().Should().Be("Password must contains min 1 digit and min 1 letter.");
            }
        }


        [Fact]
        private async Task Post_DuplicatedLogin()
        {
            using (var server = new TestServerFixture())
            {
                var account = GetInitializedAccount();
                var preparePostResponse = await server.Client.PostAsync(Url,
                    new StringContent(account.Serialize(), Encoding.UTF8, "application/json"));
                //
                var postResponse = await server.Client.PostAsync(Url,
                    new StringContent(account.Serialize(), Encoding.UTF8, "application/json"));
                var postResponseString = await postResponse.Content.ReadAsStringAsync();
                //
                postResponse.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
                postResponseString.Should().Be($"Account with login {account.Login} already exist.");
            }
        }

        //[Fact]
        //public async Task GetCountries()
        //{
        //    using (var server = new TestServerFixture())
        //    {
        //        var response = await server.Client.GetAsync("api/Countries");
        //        var responseString = await response.Content.ReadAsStringAsync();
        //        response.EnsureSuccessStatusCode();

        //    }
        //}

        //[Fact]
        //public async Task GetAccounts()
        //{
        //    using (var server = new TestServerFixture())
        //    {
        //        var response = await server.Client.GetAsync("api/Accounts");
        //        var responseString = await response.Content.ReadAsStringAsync();
        //        response.EnsureSuccessStatusCode();

        //    }
        //}

        private AccountWithPassword GetInitializedAccount() => new AccountWithPassword
        {
            AccountId = 0,
            ProvinceId = 2,
            Login = "login",
            Agreement = true,
            Password = "a1",
            PasswordConfirmation = "a1"
        };
    }
}
