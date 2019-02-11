using System;
using AutoMapper;
using FluentAssertions;
using Moq;
using RegistrationForm.Infrastructure.AutoMapper;
using RegistrationForm.Infrastructure.Security;
using RegistrationForm.Models;
using Xunit;
using Entities = RegistrationForm.DAL.src.Entities;

namespace RegistrationForm.Tests
{
    public class AutoMapperTest
    {
        private readonly IMapper _mapper;

        public AutoMapperTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new ModelsProfile()));
            _mapper = config.CreateMapper();
        }


        [Fact]
        public void EntityCountryToModelCountry()
        {
            var entity = new Entities.Country()
            {
                CountryId = 1,
                Name = "Country1"
            };
            //
            var model = _mapper.Map<Country>(entity);
            //
            model.CountryId.Should().Be(entity.CountryId);
            model.Name.Should().Be(entity.Name);
        }

        [Fact]
        public void EntityProvinceToModelProvince()
        {
            var entity = new Entities.Province()
            {
                ProvinceId = 1,
                CountryId = 2,
                Name = "Country1"
            };
            //
            var model = _mapper.Map<Province>(entity);
            //
            model.ProvinceId.Should().Be(entity.ProvinceId);
            model.Name.Should().Be(entity.Name);
        }

        [Fact]
        public void EntityAccountToModelAccount()
        {
            var entity = new Entities.Account()
            {
                AccountId = 1,
                ProvinceId = 2,
                Agreement = true,
                Login = "login",
                PasswordHash = "password"
            };
            //
            var model = _mapper.Map<Account>(entity);
            //
            model.AccountId.Should().Be(entity.AccountId);
            model.ProvinceId.Should().Be(entity.ProvinceId);
            model.Agreement.Should().Be(entity.Agreement);
            model.Login.Should().Be(entity.Login);
        }

        [Fact]
        public void ModelAccountWithPasswordToEntityAccount()
        {
            var model = new AccountWithPassword()
            {
                AccountId = 1,
                ProvinceId = 2,
                Agreement = true,
                Login = "login",
                Password = "password",
                PasswordConfirmation = "passwordConfirmation"
            };
            //
            var entity = _mapper.Map<Account>(model);
            //
            entity.AccountId.Should().Be(entity.AccountId);
            entity.ProvinceId.Should().Be(entity.ProvinceId);
            entity.Agreement.Should().Be(entity.Agreement);
            entity.Login.Should().Be(entity.Login);
        }
    }
}
