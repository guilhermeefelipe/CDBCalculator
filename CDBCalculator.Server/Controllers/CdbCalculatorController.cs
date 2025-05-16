namespace CDBCalculator.Api.Controllers;

using CDBCalculator.Domain.Model;
using CDBCalculator.Service.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[EnableCors("AllowAngular")] 
public class CdbCalculatorController : ControllerBase
{
    private readonly ICdbCalculationService cdbCalculationService;

    public CdbCalculatorController(ICdbCalculationService cdbCalculationService)
    {
        this.cdbCalculationService = cdbCalculationService;
    }

    [HttpPost("calculate")]
    public ActionResult<CdbCalculationResponse> CalculateCdb([FromBody] CdbCalculationRequest request)
    {
        if (request.InitialValue <= 0)
            return BadRequest("O valor inicial deve ser positivo.");

        if (request.Months < 2)
            return BadRequest("O prazo mínimo é de 2 meses.");

        try
        {
            var result = cdbCalculationService.CalculateCdb(request.InitialValue, request.Months);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao calcular o investimento: {ex.Message}");
        }
    }
}