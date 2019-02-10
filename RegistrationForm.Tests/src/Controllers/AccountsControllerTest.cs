using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RegistrationForm.Controllers;
using RegistrationForm.Infrastructure.DAL.Country;
using RegistrationForm.Infrastructure.Security;
using RegistrationForm.Models;
using Xunit;

namespace RegistrationForm.Tests.src.Controllers
{
    public class AccountsControllerTest
    {
        [Fact]
        public async Task GetAccountsList_ReturnsEmptyList()
        {
            var mediatorMoq = new Mock<IMediator>();
            mediatorMoq.Setup(x => x.Send(It.IsAny<GetAccountsListRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new Account[0]);
            var controller = ControllerFactory(mediatorMoq.Object);
            //
            var result = await controller.Get();
            //
            mediatorMoq.Verify(x => x.Send(It.IsAny<GetAccountsListRequest>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMoq.VerifyNoOtherCalls();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var resultValue = okResult.Value.Should().BeAssignableTo<IEnumerable<Account>>().Subject;
            resultValue.Should().NotBeNull();
            resultValue.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAccountsList_ReturnsNotEmptyList()
        {
            var mediatorMoq = new Mock<IMediator>();
            mediatorMoq.Setup(x => x.Send(It.IsAny<GetAccountsListRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new[] {new Account(), new Account(), new Account()});
            var controller = ControllerFactory(mediatorMoq.Object);
            //
            var result = await controller.Get();
            //
            mediatorMoq.Verify(x => x.Send(It.IsAny<GetAccountsListRequest>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMoq.VerifyNoOtherCalls();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var resultValue = okResult.Value.Should().BeAssignableTo<IEnumerable<Account>>().Subject;
            resultValue.Should().NotBeNull();
            resultValue.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetAccountById_ReturnsNotFound()
        {
            var requiredAccountId = 1;
            var mediatorMoq = new Mock<IMediator>();
            mediatorMoq.Setup(x => x.Send(It.Is<GetAccountByIdRequest>(i => i.AccountId == requiredAccountId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);
            var controller = ControllerFactory(mediatorMoq.Object);
            //
            var result = await controller.Get(requiredAccountId);
            //
            mediatorMoq.Verify(x => x.Send(It.Is<GetAccountByIdRequest>(i => i.AccountId == requiredAccountId), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMoq.VerifyNoOtherCalls();
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetAccountById_ReturnsAccount()
        {
            var requiredAccountId = 1;
            var account = new Account() { AccountId = requiredAccountId };
            var mediatorMoq = new Mock<IMediator>();
            mediatorMoq.Setup(x => x.Send(It.Is<GetAccountByIdRequest>(i => i.AccountId == requiredAccountId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => account);
            var controller = ControllerFactory(mediatorMoq.Object);
            //
            var result = await controller.Get(requiredAccountId);
            //
            mediatorMoq.Verify(x => x.Send(It.Is<GetAccountByIdRequest>(i => i.AccountId == requiredAccountId), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMoq.VerifyNoOtherCalls();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var resultValue = okResult.Value.Should().BeOfType<Account>().Subject;
            resultValue.Should().NotBeNull();
            resultValue.Should().Be(account);
        }

        [Fact]
        public async Task GetAccountByLogin_ReturnsNotFound()
        {
            var requiredAccountLogin = "login";
            var mediatorMoq = new Mock<IMediator>();
            mediatorMoq.Setup(x => x.Send(It.Is<GetAccountByLoginIdRequest>(i => i.Login == requiredAccountLogin), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);
            var controller = ControllerFactory(mediatorMoq.Object);
            //
            var result = await controller.GetByLogin(requiredAccountLogin);
            //
            mediatorMoq.Verify(x => x.Send(It.Is<GetAccountByLoginIdRequest>(i => i.Login == requiredAccountLogin), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMoq.VerifyNoOtherCalls();
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetAccountByLogin_ReturnsAccount()
        {
            var requiredAccountLogin = "login";
            var account = new Account() { Login = requiredAccountLogin };
            var mediatorMoq = new Mock<IMediator>();
            mediatorMoq.Setup(x => x.Send(It.Is<GetAccountByLoginIdRequest>(i => i.Login == requiredAccountLogin), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => account);
            var controller = ControllerFactory(mediatorMoq.Object);
            //
            var result = await controller.GetByLogin(requiredAccountLogin);
            //
            mediatorMoq.Verify(x => x.Send(It.Is<GetAccountByLoginIdRequest>(i => i.Login == requiredAccountLogin), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMoq.VerifyNoOtherCalls();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var resultValue = okResult.Value.Should().BeOfType<Account>().Subject;
            resultValue.Should().NotBeNull();
            resultValue.Should().Be(account);
        }

        [Fact]
        private async Task Post_NoAgreement()
        {
            var account = GetInitializedAccount();
            account.AgreeToWorkForFood = false;
            var mediatorMoq = new Mock<IMediator>();
            var controller = ControllerFactory(mediatorMoq.Object);
            //
            var result = await controller.Post(GetPasswordComplexityChecker(), account);
            //
            mediatorMoq.VerifyNoOtherCalls();
            var badResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badResult.Value.Should().Be("You should agree to work for food.");
        }

        [Fact]
        private async Task Post_PasswordConfirmationDoesNotMissMuch()
        {
            var account = GetInitializedAccount();
            account.PasswordConfirmation = "notMisMuch";
            var mediatorMoq = new Mock<IMediator>();
            var controller = ControllerFactory(mediatorMoq.Object);
            //
            var result = await controller.Post(GetPasswordComplexityChecker(), account);
            //
            mediatorMoq.VerifyNoOtherCalls();
            var badResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badResult.Value.Should().Be("Account password confirmation does not miss much.");
        }

        [Fact]
        private async Task Post_PasswordBadComplexity()
        {
            var account = GetInitializedAccount();
            var mediatorMoq = new Mock<IMediator>();
            var controller = ControllerFactory(mediatorMoq.Object);
            //
            var result = await controller.Post(GetPasswordComplexityChecker(false), account);
            //
            mediatorMoq.VerifyNoOtherCalls();
            var badResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badResult.Value.Should().Be($"Account password is weak.");
        }


        [Fact]
        private async Task Post_DuplicatedLogin()
        {
            var account = GetInitializedAccount();
            var mediatorMoq = new Mock<IMediator>();
            mediatorMoq.Setup(x => x.Send(It.Is<CreateAccountRequest>(i => i.Account == account), It.IsAny<CancellationToken>()))
                .Throws(new DuplicatedLoginDALException($"Account with login {account.Login} already exist."));
            var controller = ControllerFactory(mediatorMoq.Object);
            //
            var result = await controller.Post(GetPasswordComplexityChecker(), account);
            //
            mediatorMoq.Verify(x => x.Send(It.Is<CreateAccountRequest>(i => i.Account == account), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMoq.VerifyNoOtherCalls();
            var badResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badResult.Value.Should().Be($"Account with login {account.Login} already exist.");
        }

        [Fact]
        private async Task Post_AttemptFailed()
        {
            var account = GetInitializedAccount();
            var mediatorMoq = new Mock<IMediator>();
            mediatorMoq.Setup(x => x.Send(It.Is<CreateAccountRequest>(i => i.Account == account), It.IsAny<CancellationToken>()))
                .Throws(new DALException("Some message"));
            var controller = ControllerFactory(mediatorMoq.Object);
            //
            var result = await controller.Post(GetPasswordComplexityChecker(), account);
            //
            mediatorMoq.Verify(x => x.Send(It.Is<CreateAccountRequest>(i => i.Account == account), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMoq.VerifyNoOtherCalls();
            var badResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badResult.Value.Should().Be($"Attempt to add account failed.");
        }

        [Fact]
        private async Task Post_Success()
        {
            var account = GetInitializedAccount();
            var createdAccount = new Account()
            {
                AccountId = 1,
                Login = account.Login,
                AgreeToWorkForFood = account.AgreeToWorkForFood,
                ProvinceId = account.ProvinceId
            };
            var mediatorMoq = new Mock<IMediator>();
            mediatorMoq.Setup(x => x.Send(It.Is<CreateAccountRequest>(i => i.Account == account), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdAccount);
            var controller = ControllerFactory(mediatorMoq.Object);
            //
            var result = await controller.Post(GetPasswordComplexityChecker(), account);
            //
            mediatorMoq.Verify(x => x.Send(It.Is<CreateAccountRequest>(i => i.Account == account), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMoq.VerifyNoOtherCalls();
            
            var okResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            okResult.ActionName.Should().Be(nameof(controller.Get));
            okResult.RouteValues.Keys.First().Should().Be("id");
            okResult.RouteValues.Values.First().Should().Be(1);
            var person = okResult.Value.Should().BeOfType<Account>().Subject;
            person.Should().Be(createdAccount);
        }

        private AccountWithPassword GetInitializedAccount() => new AccountWithPassword
        {
            AccountId = 0,
            ProvinceId = 1,
            Login = "login",
            AgreeToWorkForFood = true,
            Password = "a1",
            PasswordConfirmation = "a1"
        };

        private IPasswordComplexityChecker GetPasswordComplexityChecker(bool checkResult = true)
        {
            var passwordCheckerMoq = new Mock<IPasswordComplexityChecker>();
            passwordCheckerMoq.Setup(x => x.Check(It.IsAny<string>())).Returns(checkResult);
            return passwordCheckerMoq.Object;
        }

        private AccountsController ControllerFactory(IMediator mediator)
        {
            var loggerStub = new Mock<ILogger<AccountsController>>();
            return new AccountsController(mediator, loggerStub.Object);
        }
    }
}
