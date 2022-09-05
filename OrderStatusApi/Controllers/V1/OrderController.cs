using Microsoft.AspNetCore.Mvc;
using OrderStatusApi.Models;
using OrderStatusApi.Interfaces;
using System.Net.Mime;

namespace OrderStatusApi.V1.Controllers;

[ApiController]
[ApiVersion("1.0", Deprecated = true)]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class OrderStatusController : ControllerBase
{
    /// <summary>
    /// Gera o percentual de cada "OrderStatus" com base no total de todos os tipos de status disponíveis.
    /// </summary>
    /// <param name="orderStatusCounter">Contagem de cada status em "OrderStatus" dentro do contexto local da função(Scooped).</param>
    /// <returns>O Percentual de cada status em OrderStatus.</returns>
    /// <response code="200">Retorna o percentual de cada status sobre o total.</response>
    /// <response code="400">Caso a estrutura do JSON esteja mal formada ou os valores fornecidos não estiverem dentro do modelo.</response>
    [HttpPost("CalculatePercentage")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public ActionResult<OrderStatusCounterDto> CalculateOrderStatusPercentage(
        [FromServices] IOrderService orderService,
        [FromBody] OrderStatusCounterDto orderStatusCounter
    )
    {
        var orderStatusPercentage = orderService
            .CalculateOrderStatusPercentage(orderStatusCounter);

        return Ok(orderStatusPercentage);
    }
}