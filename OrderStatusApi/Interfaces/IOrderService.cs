using OrderStatusApi.Enums;
using OrderStatusApi.Models;

namespace OrderStatusApi.Interfaces;

public interface IOrderService : IBaseModelService<Order>
{
    public OrderStatusPercentageDto CalculateOrderStatusPercentage(OrderStatusCounterDto orderStatusCounter);

    public Task<OrderStatusPercentageDto> CalculateOrderStatusPercentageAsync();
    
    public Task<List<Order>> GetByStatusAsync(OrderStatus status);

    public Task<int> GetTotalByStatusAsync(OrderStatus status);
}