namespace OrderStatusApi.Models;

public struct OrderStatusPercentageDto
{
    public double ClosedOrder { get; init; }

    public double OpenOrder { get; init; }

    public double BlockedOrder { get; init; }

}
