using Microsoft.EntityFrameworkCore;
using OrderStatusApi.Enums;
using OrderStatusApi.Interfaces;
using OrderStatusApi.Models;

namespace OrderStatusApi.Services;

public class OrderService : BaseModelService<Order>, IOrderService
{
    public OrderService(OrderDbContext context) : base(context)
    {
    }

    private double CalculatePercentual(int currentValue, int total)
    {
        return Math.Round((double)(currentValue * 100) / total, 2);
    }

    public OrderStatusPercentageDto CalculateOrderStatusPercentage(OrderStatusCounterDto orderStatusCounter)
    {
        int total = orderStatusCounter.TotalOrders;

        return new OrderStatusPercentageDto()
        {
            ClosedOrder = CalculatePercentual(orderStatusCounter.ClosedOrder, total),
            OpenOrder = CalculatePercentual(orderStatusCounter.OpenOrder, total),
            BlockedOrder = CalculatePercentual(orderStatusCounter.BlockedOrder, total),
        };
    }

    public async Task<OrderStatusPercentageDto> CalculateOrderStatusPercentageAsync()
    {
        var orderStatusCounter = new OrderStatusCounterDto()
        {
            ClosedOrder = await GetTotalByStatusAsync(OrderStatus.Closed),
            OpenOrder = await GetTotalByStatusAsync(OrderStatus.Open),
            BlockedOrder = await GetTotalByStatusAsync(OrderStatus.Blocked),
        };

        return CalculateOrderStatusPercentage(orderStatusCounter);
    }

    public async Task<List<Order>> GetByStatusAsync(OrderStatus status)
    {
        return await _context.Orders
            .Where(order => order.Status == status)
            .ToListAsync();
    }

    public async Task<int> GetTotalByStatusAsync(OrderStatus status)
    {
        return await _context.Orders
            .Where(order => order.Status == status)
            .CountAsync();
    }
}
