namespace GameStore.Payment.Core.Dtos;

public class CardDetails
{
    public string Holder { get; set; }

    public string CardNumber { get; set; }

    public int MonthExpire { get; set; }

    public int YearExpire { get; set; }

    public int Cvv2 { get; set; }
}