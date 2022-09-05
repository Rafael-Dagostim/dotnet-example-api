using System.ComponentModel.DataAnnotations;

namespace OrderStatusApi.Models;

public class OrderStatusCounterDto
{
    [Range(0, int.MaxValue, ErrorMessage = $"A valor deve ser positivo.")]
    public int ClosedOrder { get; set; } = 0;

    [Range(0, int.MaxValue, ErrorMessage = $"A valor deve ser positivo.")]
    public int OpenOrder { get; set; } = 0;

    [Range(0, int.MaxValue, ErrorMessage = $"A valor deve ser positivo.")]
    public int BlockedOrder { get; set; } = 0;
    
    public int TotalOrders
    {
        get { return ClosedOrder + OpenOrder + BlockedOrder; }
    }
}
