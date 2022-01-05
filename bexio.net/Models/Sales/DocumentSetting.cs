namespace bexio.net.Models.Sales
{
    public class DocumentSetting : IHasPrimaryKey
    {
        public int     Id                            { get; set; }
        public string? Text                          { get; set; }
        public string? KbItemClass                   { get; set; }
        public string? EnumerationFormat             { get; set; }
        public bool    UseAutomaticEnumeration       { get; set; }
        public bool    UseYearlyEnumeration          { get; set; }
        public int?    NextNr                        { get; set; }
        public int?    NrMinLength                   { get; set; }
        public int?    DefaultTimePeriodInDays       { get; set; }
        public int?    DefaultLogopaperId            { get; set; }
        public int?    DefaultLanguageId             { get; set; }
        public int?    DefaultClientBankAccountNewId { get; set; }
        public int?    DefaultCurrencyId             { get; set; }
        public int?    DefaultMwstType               { get; set; }
        public bool    DefaultMwstIsNet              { get; set; }
        public int?    DefaultNbDecimalsAmount       { get; set; }
        public int?    DefaultNbDecimalsPrice        { get; set; }
        public bool    DefaultShowPositionTaxes      { get; set; }
        public string? DefaultTitle                  { get; set; }
        public bool    DefaultShowEsrOnSamePage      { get; set; }
        public int?    DefaultPaymentTypeId          { get; set; }
        public int?    KbTermsOfPaymentTemplateId    { get; set; }
        public bool    DefaultShowTotal              { get; set; }
    }
}
