using System;
using Xunit;
using AutoFixture;
using Moq;
using FluentAssertions;
using PhoneDirectory.Business.Interfaces;
using PhoneDirectory.API.Controllers;
using MassTransit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Business.Models;
using PhoneDirectory.Business.Responses;
using System.Collections.Generic;
using Shared.Models.Interfaces;
using PhoneDirectory.Core.Requests;

namespace PhoneDirectory.API.UnitTests.Controllers
{
    public class PersonControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IPersonService> _personServiceMock;
        private readonly PersonController _personController;
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IBus _bus;

        public PersonControllerTest()
        {
            _fixture = new Fixture();
            _personServiceMock = _fixture.Freeze<Mock<IPersonService>>();
            _personController = new PersonController(_personServiceMock.Object, _bus, _rabbitMQService); //creates the implementation in-memory
        }

        [Fact]
        public void CreatePerson_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            //Arrange
            var request = _fixture.Create<PersonModel>();
            _personController.ModelState.AddModelError("Subject", "The Subject field is required.");
            var response = _fixture.Create<ServiceResponse<PersonModel>>();
            _personServiceMock.Setup(x => x.CreatePerson(request)).Returns(response);

            //Act
            var result = _personController.CreatePerson(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _personServiceMock.Verify(x => x.CreatePerson(request), Times.Never());
        }

        [Fact]
        public void CreatePerson_ShouldReturnBadRequest_WhenBlankRequest()
        {
            //Arrange
            var request = new PersonModel();
            _personController.ModelState.AddModelError("Subject", "The Subject field is required.");
            var response = _fixture.Create<ServiceResponse<PersonModel>>();
            _personServiceMock.Setup(x => x.CreatePerson(request)).Returns(response);

            //Act
            var result = _personController.CreatePerson(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _personServiceMock.Verify(x => x.CreatePerson(request), Times.Never());
        }


        [Fact]
        public void GetPersons_ShouldReturnOkResponse_WhenDataFound()
        {
            var res = new ServiceResponse<List<PersonModel>>();

            //Arrange
            var personsMock = _fixture.Create<ServiceResponse<List<PersonModel>>>();
            _personServiceMock.Setup(x => x.GetAllPersons()).Returns(personsMock);

            //Act
            var result = _personController.GetAllPersons();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            _personServiceMock.Verify(x => x.GetAllPersons(), Times.Once());
        }


        [Fact]
        public void GetPersons_ShouldReturnNotFound_WhenDataNotFound()
        {
            var response = new ServiceResponse<List<PersonModel>>();

            //Arrange
            response = null;
            _personServiceMock.Setup(x => x.GetAllPersons()).Returns(response);

            //Act
            var result = _personController.GetAllPersons();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();
            _personServiceMock.Verify(x => x.GetAllPersons(), Times.Once());
        }

        [Fact]
        public void GetPersonById_ShouldReturnOkResponse_WhenValidInput()
        {
            //Arrange
            var personMock = _fixture.Create<ServiceResponse<PersonModel>>();
            var id = _fixture.Create<string>();
            _personServiceMock.Setup(x => x.GetPersonById(id)).Returns(personMock);

            //Act
            var result = _personController.GetPersonById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();

            _personServiceMock.Verify(x => x.GetPersonById(id), Times.Once());
        }

        [Fact]
        public void GetPersonById_ShouldReturnNotResponse_WhenNoDataFound()
        {
            //Arrange
            var response = new ServiceResponse<PersonModel>();
            response = null;

            var id = _fixture.Create<string>();
            _personServiceMock.Setup(x => x.GetPersonById(id)).Returns(response);

            //Act
            var result = _personController.GetPersonById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();
            _personServiceMock.Verify(x => x.GetPersonById(id), Times.Once());
        }

        [Fact]
        public void GetPersonById_ShouldReturnNotFound_WhenBlankRequest()
        {
            //Arrange
            var response = new ServiceResponse<PersonModel>();
            response = null;

            var id = "";
            _personServiceMock.Setup(x => x.GetPersonById(id)).Returns(response);

            //Act
            var result = _personController.GetPersonById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();
            _personServiceMock.Verify(x => x.GetPersonById(id), Times.Once());
        }

        [Fact]
        public void DeletePerson_ShouldReturnNoContents_WhenDeletedARecord()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var response = _fixture.Create<ServiceResponse<PersonModel>>();
            _personServiceMock.Setup(x => x.DeletePerson(id)).Returns(response);

            //Act
            var result = _personController.DeletePersonById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _personServiceMock.Verify(x => x.DeleteContact(id), Times.Never());
        }

        [Fact]
        public void DeletePerson_ShouldReturnNotFound_WhenRecordNotFound()
        {
            var response = _fixture.Create<ServiceResponse<PersonModel>>();

            //Arrange
            var id = _fixture.Create<string>();
            _personServiceMock.Setup(x => x.DeletePerson(id)).Returns(response);

            //Act
            var result = _personController.DeleteContact(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();
            _personServiceMock.Verify(x => x.DeletePerson(id), Times.Never());
        }

        [Fact]
        public void DeleteContact_ShouldReturnBadResponse_WhenInputIsZero()
        {
            //Arrange
            string id = "";
            var request = _fixture.Create<ServiceResponse<PersonModel>>();
            _personServiceMock.Setup(x => x.DeleteContact(id)).Returns(request);

            //Act
            var result = _personController.DeleteContact(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _personServiceMock.Verify(x => x.DeleteContact(id), Times.Once());
        }

        [Fact]
        public void DeleteContact_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var request = _fixture.Create<PersonModel>();
            _personController.ModelState.AddModelError("Subject", "The Subject field is required");

            var response = _fixture.Create<ServiceResponse<PersonModel>>();
            _personServiceMock.Setup(x => x.DeleteContact(id)).Returns(response);

            //Act
            var result = _personController.DeleteContact(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _personServiceMock.Verify(x => x.DeleteContact(id), Times.Once());
        }
    }
}
