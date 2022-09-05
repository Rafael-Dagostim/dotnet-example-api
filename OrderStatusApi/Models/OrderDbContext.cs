using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OrderStatusApi.Models;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    { }

    public DbSet<Order> Orders { get; set; }
}
