namespace bexio.net.Models.Sales;

public record Payment : IHasPrimaryKey
{
    public int       Id                        { get; set; } = default;
    public DateTime? Date                      { get; set; }
    public string?   Value                     { get; set; }
    public int?      BankAccountId             { get; set; }
    public int?      Title                     { get; set; }
    public int?      PaymentServiceId          { get; set; }
    public bool      IsClientAccountRedemption { get; set; }
    public bool      IsCashDiscount            { get; set; }
    public int?      KbInvoiceId               { get; set; }
    public int?      KbCreditVoucherId         { get; set; }
    public int?      KbBillId                  { get; set; }
    public string?   KbCreditVoucherText       { get; set; }
}