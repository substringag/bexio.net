namespace bexio.net.Models.Items
{
    public class Article : IHasPrimaryKey
    {
        public int      Id                   { get; set; }
        public int?     UserId               { get; set; }
        public int?     ArticleTypeId        { get; set; }
        public int?     ContactId            { get; set; }
        public string?  DelivererCode        { get; set; }
        public string?  DelivererName        { get; set; }
        public string?  DelivererDescription { get; set; }
        public string?  InternCode           { get; set; }
        public string?  InternName           { get; set; }
        public string?  InternDescription    { get; set; }
        public decimal? PurchasePrice        { get; set; }
        public decimal? SalePrice            { get; set; }
        public decimal? PurchaseTotal        { get; set; }
        public decimal? SaleTotal            { get; set; }
        public int?     CurrencyId           { get; set; }
        public int?     TaxIncomeId          { get; set; }
        public int?     TaxId                { get; set; }
        public int?     TaxExpenseId         { get; set; }
        public int?     UnitId               { get; set; }
        public bool     IsStock              { get; set; }
        public int?     StockId              { get; set; }
        public int?     StockPlaceId         { get; set; }
        public int?     StockNr              { get; set; }
        public int?     StockMinNr           { get; set; }
        public int?     StockReservedNr      { get; set; }
        public int?     StockAvailableNr     { get; set; }
        public int?     StockPickedNr        { get; set; }
        public int?     StockDisposedNr      { get; set; }
        public int?     StockOrderedNr       { get; set; }
        public int?     Width                { get; set; }
        public int?     Height               { get; set; }
        public int?     Weight               { get; set; }
        public int?     Volume               { get; set; }
        public string?  HtmlText             { get; set; }
        public string?  Remarks              { get; set; }
        public decimal? DeliveryPrice        { get; set; }
        public int?     ArticleGroupId       { get; set; }
    }
}
