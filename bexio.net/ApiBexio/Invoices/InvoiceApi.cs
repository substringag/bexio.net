using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Sales;
using bexio.net.Responses;

namespace bexio.net.ApiBexio.Invoices;

public class InvoiceApi
{
    private readonly BexioApi _api;

    internal InvoiceApi(BexioApi api)
    {
        _api = api;
    }

    #region Basic CRUD Operations

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2ListInvoices
    /// Fetch a list of invoices
    /// </summary>
    /// <param name="orderBy">"id" or other valid field // may append _desc</param>
    /// <param name="offset"></param>
    /// <param name="limit">max: 2000</param>
    /// <returns></returns>
    public async Task<List<Invoice>?> GetInvoicesAsync(string orderBy = "id", int offset = 0, int limit = 500)
        => await _api.GetAsync<List<Invoice>>("2.0/invoice"
            .AddQueryParameter("order_by", orderBy)
            .AddQueryParameter("offset", offset)
            .AddQueryParameter("limit", limit));

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2CreateInvoice
    /// Create invoice
    /// </summary>
    /// <param name="invoice"></param>
    /// <returns></returns>
    public async Task<Invoice?> CreateInvoiceAsync(Invoice invoice)
        => await _api.PostAsync<Invoice>("2.0/invoice", invoice);

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2SearchInvoices
    /// Search invoices
    /// </summary>
    /// <param name="data"></param>
    /// <param name="orderBy">"id" or other valid field // may append _desc</param>
    /// <param name="offset"></param>
    /// <param name="limit">max: 2000</param>
    /// <returns></returns>
    public async Task<List<Invoice>?> SearchInvoicesAsync(List<SearchQuery> data,
        string            orderBy = "id",
        int               offset  = 0,
        int               limit   = 500)
        => await _api.PostAsync<List<Invoice>>("2.0/invoice/search"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit),
            data);

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2ShowInvoice
    /// Fetch an invoice
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <returns></returns>
    public async Task<Invoice?> GetInvoiceAsync(int invoiceId)
        => await _api.GetAsync<Invoice>($"2.0/invoice/{invoiceId}");

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2EditInvoice
    /// Edit an invoice
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <param name="invoice"></param>
    /// <returns></returns>
    public async Task<Invoice?> EditInvoiceAsync(int invoiceId, Invoice invoice)
        => await _api.PostAsync<Invoice>($"2.0/invoice/{invoiceId}", invoice);

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2DeleteInvoice
    /// Delete an invoice
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <returns></returns>
    public async Task<bool?> DeleteInvoiceAsync(int invoiceId)
        => await _api.DeleteAsync($"2.0/invoice/{invoiceId}");

    #endregion

    #region Invoice Actions

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2ShowInvoicePDF
    /// Show PDF
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <returns></returns>
    public async Task<FileContentResponse?> GetInvoicePdfAsync(int invoiceId)
        => await _api.GetAsync<FileContentResponse>($"2.0/invoice/{invoiceId}/pdf");

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2CopyInvoice
    /// Copy an invoice
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<Invoice?> CopyInvoiceAsync(int invoiceId, CopyRequest data)
        => await _api.PostAsync<Invoice>($"2.0/invoice/{invoiceId}/copy", data);

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2IssueInvoice
    /// Issue an invoice
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <returns></returns>
    public async Task<bool?> IssueInvoiceAsync(int invoiceId)
        => await _api.PostActionAsync($"2.0/invoice/{invoiceId}/issue");

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2RevertIssueInvoice
    /// Set issued invoice to draft
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <returns></returns>
    public async Task<bool?> SetInvoiceToDraftAsync(int invoiceId)
        => await _api.PostActionAsync($"2.0/invoice/{invoiceId}/revert_issue");

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2CancelInvoice
    /// Cancel an invoice
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <returns></returns>
    public async Task<bool?> CancelInvoiceAsync(int invoiceId)
        => await _api.PostActionAsync($"2.0/invoice/{invoiceId}/cancel");

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2MarkInvoiceAsSent
    /// Mark invoice as sent
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <returns></returns>
    public async Task<bool?> MarkInvoiceAsSentAsync(int invoiceId)
        => await _api.PostActionAsync($"2.0/invoice/{invoiceId}/mark_as_sent");

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2SendInvoice
    /// Send an invoice
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<bool?> SendInvoiceAsync(int invoiceId, SendMailRequest data)
        => await _api.PostActionAsync($"2.0/invoice/{invoiceId}/send", data);

    #endregion

    #region Payment Management

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2ListInvoicePayments
    /// Fetch a list of payments
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <param name="offset"></param>
    /// <param name="limit">max: 2000</param>
    /// <returns></returns>
    public async Task<List<Payment>?> GetPaymentsAsync(int invoiceId,
        int offset = 0,
        int limit  = 500)
        => await _api.GetAsync<List<Payment>>($"2.0/invoice/{invoiceId}/payment"
            .AddQueryParameter("offset", offset)
            .AddQueryParameter("limit", limit));

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2CreateInvoicePayment
    /// Create payment
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <param name="payment"></param>
    /// <returns></returns>
    public async Task<Payment?> CreatePaymentAsync(int invoiceId, Payment payment)
        => await _api.PostAsync<Payment>($"2.0/invoice/{invoiceId}/payment", payment);

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2ShowInvoicePayment
    /// Fetch a payment
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <param name="paymentId"></param>
    /// <returns></returns>
    public async Task<Payment?> GetPaymentAsync(int invoiceId, int paymentId)
        => await _api.GetAsync<Payment>($"2.0/invoice/{invoiceId}/payment/{paymentId}");

    /// <summary>
    /// https://docs.bexio.com/#tag/Invoices/operation/v2DeleteInvoicePayment
    /// Delete a payment
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <param name="paymentId"></param>
    /// <returns></returns>
    public async Task<bool?> DeletePaymentAsync(int invoiceId, int paymentId)
        => await _api.DeleteAsync($"2.0/invoice/{invoiceId}/payment/{paymentId}");

    #endregion
}