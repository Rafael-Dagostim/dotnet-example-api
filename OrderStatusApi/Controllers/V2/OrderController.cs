using Microsoft.AspNetCore.Mvc;
using OrderStatusApi.Models;
using OrderStatusApi.Interfaces;
using System.Net.Mime;

namespace OrderStatusApi.V2.Controllers;

[ApiController]
[ApiVersion("2.0")]
[ApiExplorerSettings(GroupName = "v2")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class OrderController : ControllerBase
{
    /// <summary>
    /// Busca todos os os pedidos salvos no banco de dados.
    /// </summary>
    /// <returns>Uma lista com todos os pedidos encontrados.</returns>
    /// <response code="200">Retorna uma lista com todos os pedidos encontrados.</response>
    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetAll(
        [FromServices] IOrderService orderService
    )
    {
        List<Order> orders = await orderService.GetAllAsync();

        return orders;
    }

    /// <summary>
    /// Busca um registro específico dentro do banco de dados com base no id.
    /// </summary>
    /// <param name="id">Código UUID de identificação do registro.</param>
    /// <returns>O pedido com o id informado</returns>
    /// <response code="200">Retorna o pedido.</response>
    /// <response code="404">Caso não encontre o pedido.</response>
    /// <response code="400">Caso haja um má formação na requisição.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetById(
        [FromServices] IOrderService orderService,
        [FromRoute] Guid id
    )
    {
        Order? order = await orderService.GetByIdAsync(id);

        return order is not null
            ? Ok(order)
            : NotFound();
    }

    /// <summary>
    /// Cria um novo pedido e salva no banco de dados.
    /// </summary>
    /// <param name="order">Objeto pedido a ser salvo no banco</param>
    /// <response code="201">Retorna o pedido que foi criado.</response>
    /// <response code="400">Caso haja um má formação na requisição.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]

    public async Task<ActionResult<Order>> Create(
        [FromServices] IOrderService orderService,
        [FromBody] Order order)
    {
        Order newOrder = await orderService.CreateAsync(order);

        return CreatedAtAction(nameof(GetById), new { id = newOrder.Id.ToString() }, newOrder);
    }

    /// <summary>
    /// Atualiza um pedido salvo no banco de dados.
    /// </summary>
    /// <param name="order">Objeto pedido com as atualizações.</param>
    /// <returns>O pedido com as novas alterações.</returns>
    /// <response code="200">Retorna o pedido alterado.</response>
    /// <response code="404">Caso não encontre o pedido a ser alterado.</response>
    /// <response code="400">Caso haja um má formação na requisição.</response>
    [HttpPut]
    public async Task<ActionResult<Order>> Alter(
        [FromServices] IOrderService orderService,
        [FromBody] Order order)
    {
        Order? currentOrder = await orderService.GetByIdAsync(order.Id);
        if (currentOrder is null) return NotFound();

        Order newOrder = await orderService.AlterAsync(order);

        return Ok(newOrder);
    }

    /// <summary>
    /// Deleta um registro de pedido com base no id informado.
    /// </summary>
    /// <param name="id">Código UUID de identificação do registro.</param>
    /// <returns>Pedido deletado do banco.</returns>
    /// <response code="200">Retorna o pedido deletado.</response>
    /// <response code="404">Caso não encontre o pedido a ser deletado.</response>
    /// <response code="400">Caso haja um má formação na requisição.</response>
    [HttpDelete]
    public async Task<ActionResult<Order>> Delete(
        [FromServices] IOrderService orderService,
        [FromRoute] Guid id
    )
    {
        Order? deletedOrder = await orderService.DeleteAsync(id);

        return deletedOrder is not null
            ? Ok(deletedOrder)
            : NotFound();
    }

    /// <summary>
    /// Gera o percentual de cada "OrderStatus" com base nos registros salvos no banco de dados.
    /// </summary>
    /// <returns>O Percentual de cada status em OrderStatus.</returns>
    /// <response code="200">Retorna o percentual de cada status sobre o total.</response>
    /// <response code="400">Caso haja um má formação na requisição.</response>
    [HttpGet("CalculateStoredPercentage")]
    public async Task<ActionResult<OrderStatusCounterDto>> CalculateStoredOrderStatusPercentage(
        [FromServices] IOrderService orderService
    )
    {
        var orderStatusPercentage = await orderService
            .CalculateOrderStatusPercentageAsync();

        return Ok(orderStatusPercentage);
    }
}
