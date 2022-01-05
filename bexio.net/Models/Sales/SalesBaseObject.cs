using System;
using System.Collections.Generic;
using bexio.net.Models.Sales.Positions;

namespace bexio.net.Models.Sales
{
    public abstract class SalesBaseObject : IHasPrimaryKey
    {
        public int     Id            { get; set; }
        public string? DocumentNr    { get; set; }
        public string? Title         { get; set; }
        public int?    ContactId     { get; set; }
        public int?    ContactSubId  { get; set; }
        public int?    UserId        { get; set; }
        public int?    ProjectId     { get; set; }
        public int?    LanguageId    { get; set; }
        public int?    BankAccountId { get; set; }
        public int?    CurrencyId    { get; set; }
        public int?    PaymentTypeId { get; set; }

        [Obsolete]
        public int? LogopaperId { get; set; }

        public string?              Header                  { get; set; }
        public string?              Footer                  { get; set; }
        public string?              TotalGross              { get; set; }
        public string?              TotalNet                { get; set; }
        public string?              TotalTaxes              { get; set; }
        public string?              Total                   { get; set; }
        public decimal?             TotalRoundingDifference { get; set; }
        public int                  MwstType                { get; set; } = 0;
        public bool                 MwstIsNet               { get; set; }
        public bool                 ShowPositionTaxes       { get; set; }
        public DateTime?            IsValidFrom             { get; set; }
        public string?              ContactAddress          { get; set; }

        public int?                 KbItemStatusId          { get; set; }
        public string?              ApiReference            { get; set; }
        public string?              ViewedByClientAt        { get; set; }
        public DateTime?            UpdatedAt               { get; set; }
        public string?              TemplateSlug            { get; set; }
        public List<TaxesIncluded>? Taxs                    { get; set; }
        public string?              NetworkLink             { get; set; }

        public List<PositionBase>? Positions { get; set; }

        public class TaxesIncluded
        {
            public string? Percentage { get; set; }
            public string? Value      { get; set; }
        }
    }
}
