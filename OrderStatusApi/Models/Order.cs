using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrderStatusApi.Enums;

namespace OrderStatusApi.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required, MaxLength(100)]
    public string? Name { get; set; }

    [DefaultValue(OrderStatus.Open)]
    public OrderStatus Status { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
