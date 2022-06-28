using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using AutoFixture;
using PhoneDirectory.Business.Services;
using Moq;
using PhoneDirectory.DAL.Interfaces;
using PhoneDirectory.Entity.Models;
using Microsoft.AspNetCore.Http;
using PhoneDirectory.Business.Responses;
using PhoneDirectory.Business.Models;

namespace PhoneDirectory.Business.UnitTests.Services
{
    public class PersonelServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IMongoRepository<Person>> _personRepositoryMock;
        private readonly PersonService _personelService;
        private readonly IHttpContextAccessor contextAccessor;

        public PersonelServiceTests()
        {
            _fixture = new Fixture();
            _personRepositoryMock = _fixture.Freeze<Mock<IMongoRepository<Person>>>();
            _personelService = new PersonService(_personRepositoryMock.Object, contextAccessor);
        }


        [Fact]
        public void GetAllPersons_ShouldReturnData_WhenDataFound()
        {
            //Arrange
            var personsMock = _fixture.Create<ServiceResponse<PersonModel>>();
            _personRepositoryMock.Setup(x => x.Aggregate().);

            //Act

            //Assert

        }
    }
}
