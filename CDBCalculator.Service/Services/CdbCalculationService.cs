using CDBCalculator.Domain.Model;

namespace CDBCalculator.Service.Service;

public interface ICdbCalculationService
{
    CdbCalculationResponse CalculateCdb(decimal initialValue, int months);
    decimal CalculateGrossValue(decimal initialValue, int months);
    decimal CalculateNetValue(decimal initialValue, decimal grossValue, int months);
}

public class CdbCalculationService : ICdbCalculationService
{
    private const decimal CDI = 0.009m; // 0.9%
    private const decimal TB = 1.08m; // 108%

    public CdbCalculationResponse CalculateCdb(decimal initialValue, int months)
    {
        decimal grossValue = CalculateGrossValue(initialValue, months);
        decimal netValue = CalculateNetValue(initialValue, grossValue, months);

        return new CdbCalculationResponse
        {
            GrossValue = Math.Round(grossValue, 2),
            NetValue = Math.Round(netValue, 2),
        };
    }

    public decimal CalculateGrossValue(decimal initialValue, int months)
    {
        decimal grossAmount = initialValue;
        decimal monthlyRate = CDI * TB;

        for (int i = 0; i < months; i++)
        {
            grossAmount *= (1 + monthlyRate);
        }

        return grossAmount;
    }
    public decimal CalculateNetValue(decimal initialValue, decimal grossValue, int months)
    {
        decimal incomeTaxRate;

        if (months <= 6) incomeTaxRate = 0.225m;
        else if (months <= 12) incomeTaxRate = 0.20m;
        else if (months <= 24) incomeTaxRate = 0.175m;
        else incomeTaxRate = 0.15m;

        decimal incomeTax = (grossValue - initialValue) * incomeTaxRate;
        return grossValue - incomeTax;
    }

}