namespace GameStore.Payment.Core.Dtos;

public class PaymentResponse
{
    public Guid UserId { get; set; }

    public Guid OrderId { get; set; }

    public DateTime PaymentDate { get; set; }

    public double Total { get; set; }

    public byte[]? FileContent { get; set; }
}