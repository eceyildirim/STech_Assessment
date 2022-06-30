using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Report.API.Controllers;
using Report.Business.Interfaces;
using Report.Business.Models;
using Report.Business.Responses;
using System;
using System.Collections.Generic;
using Xunit;

namespace Report.API.UnitTests
{
    public class ReportControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IReportService> _reportServiceMock;
        private readonly ReportController _reportController;

        public ReportControllerTest()
        {
            _fixture = new Fixture();
            _reportServiceMock = _fixture.Freeze<Mock<IReportService>>();
            _reportController = new ReportController(_reportServiceMock.Object); //creates the implementation in-memory
        }

        [Fact]
        public void GetAllReports_ShouldReturnOkResponse_WhenDataFound()
        {
            var res = new ServiceResponse<List<ReportModel>>();

            //Arrange
            var reportsMock = _fixture.Create<ServiceResponse<List<ReportModel>>>();
            _reportServiceMock.Setup(x => x.GetReports()).Returns(reportsMock);

            //Act
            var result = _reportController.GetAllReports();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            _reportServiceMock.Verify(x => x.GetReports(), Times.Once());
        }

        [Fact]
        public void GetAllReports_ShouldReturnNotFound_WhenDataNotFound()
        {
            var response = new ServiceResponse<List<ReportModel>>();

            //Arrange
            response = null;
            _reportServiceMock.Setup(x => x.GetReports()).Returns(response);

            //Act
            var result = _reportController.GetAllReports();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();
            _reportServiceMock.Verify(x => x.GetReports(), Times.Once());
        }


        [Fact]
        public void GetReportById_ShouldReturnOkResponse_WhenValidInput()
        {
            //Arrange
            var reportMock = _fixture.Create<ServiceResponse<ReportModel>>();
            var id = _fixture.Create<string>();
            _reportServiceMock.Setup(x => x.GetReportsById(id)).Returns(reportMock);

            //Act
            var result = _reportController.GetReportById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();

            _reportServiceMock.Verify(x => x.GetReportsById(id), Times.Once());
        }

        [Fact]
        public void GetReportById_ShouldReturnNotResponse_WhenNoDataFound()
        {
            //Arrange
            var response = new ServiceResponse<ReportModel>();
            response = null;

            var id = _fixture.Create<string>();
            _reportServiceMock.Setup(x => x.GetReportsById(id)).Returns(response);

            //Act
            var result = _reportController.GetReportById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();
            _reportServiceMock.Verify(x => x.GetReportsById(id), Times.Once());
        }

        [Fact]
        public void CreateReport_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            //Arrange
            var request = _fixture.Create<ReportModel>();
            _reportController.ModelState.AddModelError("Subject", "The Subject field is required.");
            var response = _fixture.Create<ServiceResponse<ReportModel>>();
            _reportServiceMock.Setup(x => x.GenerateReport(request)).Returns(response);

            //Act
            var result = _reportController.CreateReport(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _reportServiceMock.Verify(x => x.GenerateReport(request), Times.Once());
        }
    }
}
