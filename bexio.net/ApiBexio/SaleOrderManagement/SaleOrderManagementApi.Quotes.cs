using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Sales;
using bexio.net.Models.Sales.Positions;
using bexio.net.Responses;

namespace bexio.net.ApiBexio.SaleOrderManagement;

public partial class SaleOrderManagementApi
{
    private readonly BexioApi _api;

    internal SaleOrderManagementApi(BexioApi api)
    {
        _api = api;
    }

    #region Quotes

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderBy"></param>
    /// <param name="offset"></param>
    /// <param name="limit">max: 2000</param>
    /// <returns></returns>
    public async Task<List<Quote>?> GetQuotesAsync(string orderBy = "id",
        int    offset  = 0,
        int    limit   = 500)
        => await _api.GetAsync<List<Quote>>("2.0/kb_offer"
            .AddQueryParameter("order_by", orderBy)
            .AddQueryParameter("offset", offset)
            .AddQueryParameter("limit", limit));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="quote"></param>
    /// <returns></returns>
    public async Task<Quote?> CreateQuoteAsync(Quote quote)
        => await _api.PostAsync<Quote>("2.0/kb_offer", quote);

    /// <summary>
    /// Searchable fields: id, kb_item_status_id, document_nr, title,
    /// contact_id, contact_sub_id, user_id, currency_id, total_gross,
    /// total_net, total, is_valid_from, is_valid_to, is_valid_until, updated_at
    /// </summary>
    /// <param name="data"></param>
    /// <param name="orderBy"></param>
    /// <param name="offset"></param>
    /// <param name="limit">max: 2000</param>
    /// <returns></returns>
    public async Task<List<Quote>?> SearchQuotesAsync(List<SearchQuery> data,
        string            orderBy = "id",
        int               offset  = 0,
        int               limit   = 500)
        => await _api.PostAsync<List<Quote>>("2.0/kb_offer/search"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit),
            data);

    // Fetch a quote
    public async Task<Quote?> GetQuoteAsync(int quoteId)
        => await _api.GetAsync<Quote>($"2.0/kb_offer/{quoteId}");

    // Edit a quote
    public async Task<Quote?> UpdateQuoteAsync(int quoteId, Quote quote)
        => await _api.PostAsync<Quote>($"2.0/kb_offer/{quoteId}", quote);

    // Delete a quote
    public async Task<bool?> DeleteQuoteAsync(int quoteId)
        => await _api.DeleteAsync($"2.0/kb_offer/{quoteId}");

    // Issue a quote
    public async Task<bool?> IssueQuoteAsync(int quoteId)
        => await _api.PostActionAsync($"2.0/kb_offer/{quoteId}/issue");

    // Revert issue a quote
    public async Task<bool?> RevertIssueQuoteAsync(int quoteId)
        => await _api.PostActionAsync($"2.0/kb_offer/{quoteId}/revertIssue");

    // Accept a quote
    public async Task<bool?> AcceptQuoteAsync(int quoteId)
        => await _api.PostActionAsync($"2.0/kb_offer/{quoteId}/accept");

    // Decline a quote
    public async Task<bool?> DeclineQuoteAsync(int quoteId)
        => await _api.PostActionAsync($"2.0/kb_offer/{quoteId}/reject");

    // Reissue a quote
    public async Task<bool?> ReissueQuoteAsync(int quoteId)
        => await _api.PostActionAsync($"2.0/kb_offer/{quoteId}/reissue");

    // Mark quote as sent
    public async Task<bool?> MarkQuoteAsSentAsync(int quoteId)
        => await _api.PostActionAsync($"2.0/kb_offer/{quoteId}/mark_as_sent");

    // Show PDF
    public async Task<FileContentResponse?> GetQuotePdfAsync(int quoteId)
        => await _api.GetAsync<FileContentResponse>($"2.0/kb_offer/{quoteId}/pdf");

    // Send a quote
    public async Task<bool?> SendQuoteAsync(int quoteId, SendMailRequest data)
        => await _api.PostActionAsync($"2.0/kb_offer/{quoteId}/send", data);

    // Copy a quote
    public async Task<Quote?> CopyQuoteAsync(int quoteId, CopyRequest data)
        => await _api.PostAsync<Quote>($"2.0/kb_offer/{quoteId}/copy", data);

    // Create order from quote
    public async Task<Order?> CreateOrderFromQuoteAsync(int                  quoteId,
        List<PositionEntry>? onlySpecificPositions = null)
        => await _api.PostAsync<Order>($"2.0/kb_offer/{quoteId}/order", new { positions = onlySpecificPositions });

    // Create invoice from quote
    public async Task<Invoice?> CreateInvoiceFromQuoteAsync(int                  quoteId,
        List<PositionEntry>? onlySpecificPositions = null)
        => await _api.PostAsync<Invoice>($"2.0/kb_offer/{quoteId}/invoice", new { positions = onlySpecificPositions });

    #endregion
}