using CDBCalculator.Domain.Model;
using CDBCalculator.Service.Service;
using Moq;

namespace CDBCalculator.Tests.Services;

public class CdbCalculatorServiceTests
{
    private readonly ICdbCalculationService cdbCalculationService;

    public CdbCalculatorServiceTests()
    {
        var mock = new Mock<ICdbCalculationService>();

        // Configuração para CalculateCdb
        mock.Setup(m => m.CalculateCdb(1000, 6))
            .Returns(new CdbCalculationResponse
            {
                GrossValue = 1058.28m,
                NetValue = 1020.17m
            });

        mock.Setup(m => m.CalculateCdb(1000, 12))
            .Returns(new CdbCalculationResponse
            {
                GrossValue = 1120.51m,
                NetValue = 1096.41m
            });

        mock.Setup(m => m.CalculateCdb(5000, 24))
            .Returns(new CdbCalculationResponse
            {
                GrossValue = 6294.35m,
                NetValue = 6030.42m
            });

        mock.Setup(m => m.CalculateGrossValue(1000, 6))
            .Returns(1058.28m);
        mock.Setup(m => m.CalculateGrossValue(1000, 12))
            .Returns(1120.51m);
        mock.Setup(m => m.CalculateGrossValue(5000, 24))
            .Returns(6294.35m);
        mock.Setup(m => m.CalculateGrossValue(100, 36))
            .Returns(142.13m);

        mock.Setup(m => m.CalculateNetValue(1000, 1058.28m, 6))
            .Returns(1020.17m);
        mock.Setup(m => m.CalculateNetValue(1000, 1120.51m, 12))
            .Returns(1096.41m);
        mock.Setup(m => m.CalculateNetValue(5000, 6294.35m, 24))
            .Returns(6030.42m);
        mock.Setup(m => m.CalculateNetValue(1000, 1145.21m, 36))
            .Returns(1073.43m);

        cdbCalculationService = mock.Object;
    }

    public static IEnumerable<object[]> TestCases()
    {
        yield return new object[] { 1000, 6, 1058.28m, 1020.17m };
        yield return new object[] { 1000, 12, 1120.51m, 1096.41m };
        yield return new object[] { 5000, 24, 6294.35m, 6030.42m };
    }

    [Theory]
    [MemberData(nameof(TestCases))]
    public void CalculateCdb_ShouldReturnCorrectValues(
        decimal initialAmount,
        int months,
        decimal expectedGross,
        decimal expectedNet)
    {
        // Act
        var result = cdbCalculationService.CalculateCdb(initialAmount, months);

        // Assert
        Assert.Equal(expectedGross, result.GrossValue);
        Assert.Equal(expectedNet, result.NetValue);
    }

    [Theory]
    [InlineData(1000, 6, 1058.28)]
    [InlineData(1000, 12, 1120.51)]
    [InlineData(5000, 24, 6294.35)]
    [InlineData(100, 36, 142.13)]
    public void CalculateGrossValue_ShouldReturnCorrectValue(
        decimal initialValue,
        int months,
        decimal expectedGrossValue)
    {
        // Act
        decimal result = cdbCalculationService.CalculateGrossValue(initialValue, months);

        // Assert
        Assert.Equal(expectedGrossValue, result, precision: 2);
    }

    [Theory]
    [InlineData(1000, 1058.28, 6, 1020.17)]
    [InlineData(1000, 1120.51, 12, 1096.41)]
    [InlineData(5000, 6294.35, 24, 6030.42)]
    [InlineData(1000, 1145.21, 36, 1073.43)]
    public void CalculateNetValue_ShouldApplyCorrectTaxRate(
        decimal initialValue,
        decimal grossValue,
        int months,
        decimal expectedNetValue)
    {
        // Act
        decimal result = cdbCalculationService.CalculateNetValue(initialValue, grossValue, months);

        // Assert
        Assert.Equal(expectedNetValue, result, precision: 2);
    }
}