using GameStore.Payment.Core.Enums;

namespace GameStore.Payment.Core.Models;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime Date { get; set; }

    public Guid CustomerId { get; set; }

    public OrderStatus Status { get; set; }
}