namespace bexio.net.Models.Sales;

public record Reminder : IHasPrimaryKey
{
    public int       Id                   { get; set; }
    public int?      KbInvoiceId          { get; set; }
    public string?   Title                { get; set; }
    public DateTime? IsValidFrom          { get; set; }
    public DateTime? IsValidTo            { get; set; }
    public int?      ReminderPeriodInDays { get; set; }
    public int?      ReminderLevel        { get; set; }
    public bool      ShowPositions        { get; set; }
    public string?   RemainingPrice       { get; set; }
    public string?   ReceivedTotal        { get; set; }
    public bool      IsSent               { get; set; }
    public string?   Header               { get; set; }
    public string?   Footer               { get; set; }
}