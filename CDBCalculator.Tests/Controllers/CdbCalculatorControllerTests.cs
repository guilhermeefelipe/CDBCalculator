using CDBCalculator.Api.Controllers;
using CDBCalculator.Domain.Model;
using CDBCalculator.Service.Service;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CDBCalculator.Tests.Controllers;

public class CdbCalculatorControllerTests
{
    private readonly Mock<ICdbCalculationService> _mockService;
    private readonly CdbCalculatorController _controller;

    public CdbCalculatorControllerTests()
    {
        _mockService = new Mock<ICdbCalculationService>();
        _controller = new CdbCalculatorController(_mockService.Object);
    }

    [Fact]
    public void Calculate_ReturnsBadRequest_WhenInvalidInput()
    {
        // Arrange
        _controller.ModelState.AddModelError("Amount", "Required");

        // Act
        var result = _controller.CalculateCdb(new CdbCalculationRequest());

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public void Calculate_ReturnsOk_WithValidResult()
    {
        // Act
        var result = _controller.CalculateCdb(new CdbCalculationRequest {  InitialValue = 1000, Months = 12 });

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Theory]
    [InlineData(-100, 12)]    // Valor negativo
    [InlineData(0, 12)]       // Valor zero
    [InlineData(1000, 1)]     // Prazo inválido
    public void Calculate_ShouldThrowForInvalidInputs(decimal value, int months)
    {
        // Act
        var result = _controller.CalculateCdb(new CdbCalculationRequest { InitialValue = value, Months = months });

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
}
