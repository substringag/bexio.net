using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Sales;
using bexio.net.Models.Sales.Positions;
using bexio.net.Models.Sales.Repetition;
using bexio.net.Responses;

namespace bexio.net.ApiBexio.SaleOrderManagement;

public class SaleOrderManagementApi
{
    private readonly BexioApi _api;

    internal SaleOrderManagementApi(BexioApi api)
    {
        _api = api;
    }

    #region Sales Order ManagementItemAndProduct

    // TODO

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderBy">"id" "total" "total_net" "total_gross" "updated_at" / may append _desc</param>
    /// <param name="offset"></param>
    /// <param name="limit">max: 2000</param>
    /// <returns></returns>
    public async Task<List<Order>?> GetOrdersAsync(string orderBy = "id", int offset = 0, int limit = 500)
        => await _api.GetAsync<List<Order>>("2.0/kb_order"
            .AddQueryParameter("order_by", orderBy)
            .AddQueryParameter("offset", offset)
            .AddQueryParameter("limit", limit));

    // Create order
    public async Task<Order?> CreateOrderAsync(Order order)
        => await _api.PostAsync<Order>("2.0/kb_order", order);

    // Search orders
    public async Task<List<Order>?> SearchOrdersAsync(List<SearchQuery> data,
        string            orderBy = "id",
        int               offset  = 0,
        int               limit   = 500)
        => await _api.PostAsync<List<Order>>("2.0/kb_order/search"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit),
            data);

    // Fetch an order
    public async Task<Order?> GetOrderAsync(int orderId)
        => await _api.GetAsync<Order>($"2.0/kb_order/{orderId}");

    // Edit an order
    public async Task<Order?> EditOrderAsync(int orderId, Order order)
        => await _api.PostAsync<Order>($"2.0/kb_order/{orderId}", order);

    // Delete an order
    public async Task<bool?> DeleteOrderAsync(int orderId)
        => await _api.DeleteAsync($"2.0/kb_order/{orderId}");

    /// <summary>
    /// Create delivery from order.
    /// Note that each article can only be shipped once. So if you call this method
    /// without the second parameter, the delivery will be created for all articles,
    /// that means you can not execute this method a second time (unless the amount of
    /// articles in the offer has changed).
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="onlySpecificPositions"></param>
    /// <returns></returns>
    public async Task<Delivery?> CreateDeliveryFromOrderAsync(int                  orderId,
        List<PositionEntry>? onlySpecificPositions = null)
        => await _api.PostAsync<Delivery>($"2.0/kb_order/{orderId}/delivery",
            onlySpecificPositions == null || onlySpecificPositions.Count == 0
                ? null
                : new { positions = onlySpecificPositions });

    /// <summary>
    /// Create invoice from order.
    /// Note that you can not invoice an order a second time.
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="onlySpecificPositions"></param>
    /// <returns></returns>
    public async Task<Invoice?> CreateInvoiceFromOrderAsync(int                  orderId,
        List<PositionEntry>? onlySpecificPositions = null)
        => await _api.PostAsync<Invoice>($"2.0/kb_order/{orderId}/invoice",
            onlySpecificPositions == null || onlySpecificPositions.Count == 0
                ? null
                : new { positions = onlySpecificPositions });

    // Show PDF
    public async Task<FileContentResponse?> GetOrderPdfAsync(int orderId)
        => await _api.GetAsync<FileContentResponse>($"2.0/kb_order/{orderId}/pdf");

    // Show repetition
    public async Task<OrderRepetition?> GetOrderRepetitionAsync(int orderId)
        => await _api.GetAsync<OrderRepetition>($"2.0/kb_order/{orderId}/repetition");

    // Edit a repetition
    public async Task<OrderRepetition?> EditOrderRepetitionAsync(int orderId, OrderRepetition repetition)
        => await _api.PostAsync<OrderRepetition>($"2.0/kb_order/{orderId}/repetition", repetition);

    // Delete a repetition
    public async Task<bool?> DeleteOrderRepetitionAsync(int orderId)
        => await _api.DeleteAsync($"2.0/kb_order/{orderId}/repetition");


    // Fetch a list of deliveries
    public async Task<List<Delivery>?> GetDeliveriesAsync(string orderBy = "id",
        int    offset  = 0,
        int    limit   = 500)
        => await _api.GetAsync<List<Delivery>>($"2.0/kb_delivery"
            .AddQueryParameter("order_by", orderBy)
            .AddQueryParameter("offset", offset)
            .AddQueryParameter("limit", limit));

    // Fetch a delivery
    public async Task<Delivery?> GetDeliveryAsync(int deliveryId)
        => await _api.GetAsync<Delivery>($"2.0/kb_delivery/{deliveryId}");

    // Issue a delivery
    public async Task<bool?> IssueDeliveryAsync(int deliveryId)
        => await _api.PostActionAsync($"2.0/kb_delivery/{deliveryId}/issue");


    // Fetch a list of invoices
    public async Task<List<Invoice>?> GetInvoicesAsync(string orderBy = "id", int offset = 0, int limit = 500)
        => await _api.GetAsync<List<Invoice>>("2.0/kb_invoice"
            .AddQueryParameter("order_by", orderBy)
            .AddQueryParameter("offset", offset)
            .AddQueryParameter("limit", limit));

    // Create invoice
    public async Task<Invoice?> CreateInvoiceAsync(Invoice invoice)
        => await _api.PostAsync<Invoice>("2.0/kb_invoice", invoice);

    // Search invoices
    public async Task<List<Invoice>?> SearchInvoicesAsync(List<SearchQuery> data,
        string            orderBy = "id",
        int               offset  = 0,
        int               limit   = 500)
        => await _api.PostAsync<List<Invoice>>("2.0/kb_invoice/search"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit),
            data);

    // Fetch an invoice
    public async Task<Invoice?> GetInvoiceAsync(int invoiceId)
        => await _api.GetAsync<Invoice>($"2.0/kb_invoice/{invoiceId}");

    // Edit an invoice
    public async Task<Invoice?> EditInvoiceAsync(int invoiceId, Invoice invoice)
        => await _api.PostAsync<Invoice>($"2.0/kb_invoice/{invoiceId}", invoice);

    // Delete an invoice
    public async Task<bool?> DeleteInvoiceAsync(int invoiceId)
        => await _api.DeleteAsync($"2.0/kb_invoice/{invoiceId}");

    // Show PDF
    public async Task<FileContentResponse?> GetInvoicePdfAsync(int invoiceId)
        => await _api.GetAsync<FileContentResponse>($"2.0/kb_invoice/{invoiceId}/pdf");

    // Copy a invoice
    public async Task<Invoice?> CopyInvoiceAsync(int invoiceId, CopyRequest data)
        => await _api.PostAsync<Invoice>($"2.0/kb_invoice/{invoiceId}/copy", data);

    // Issue an invoice
    public async Task<bool?> IssueInvoiceAsync(int invoiceId)
        => await _api.PostActionAsync($"2.0/kb_invoice/{invoiceId}/issue");

    // Sets issued invoice to draft
    public async Task<bool?> SetInvoiceToDraftAsync(int invoiceId)
        => await _api.PostActionAsync($"2.0/kb_invoice/{invoiceId}/revert_issue");

    // Cancel an invoice
    public async Task<bool?> CancelInvoiceAsync(int invoiceId)
        => await _api.PostActionAsync($"2.0/kb_invoice/{invoiceId}/cancel");

    // Mark invoice as sent
    public async Task<bool?> MarkInvoiceAsSentAsync(int invoiceId)
        => await _api.PostActionAsync($"2.0/kb_invoice/{invoiceId}/mark_as_sent");

    // Send an invoice
    public async Task<bool?> SendInvoiceAsync(int invoiceId, SendMailRequest data)
        => await _api.PostActionAsync($"2.0/kb_invoice/{invoiceId}/send", data);

    // Fetch a list of payments
    public async Task<List<Payment>?> GetPaymentsAsync(int invoiceId,
        int offset = 0,
        int limit  = 500)
        => await _api.GetAsync<List<Payment>>($"2.0/kb_invoice/{invoiceId}/payment"
            .AddQueryParameter("offset", offset)
            .AddQueryParameter("limit", limit));

    // Create payment
    public async Task<Payment?> CreatePaymentAsync(int invoiceId, Payment payment)
        => await _api.PostAsync<Payment>($"2.0/kb_invoice/{invoiceId}/payment", payment);

    // Fetch a payment
    public async Task<Payment?> GetPaymentAsync(int invoiceId, int paymentId)
        => await _api.GetAsync<Payment>($"2.0/kb_invoice/{invoiceId}/payment/{paymentId}");

    // Delete a payment
    public async Task<bool?> DeletePaymentAsync(int invoiceId, int paymentId)
        => await _api.DeleteAsync($"2.0/kb_invoice/{invoiceId}/payment/{paymentId}");

    // Fetch a list of reminders
    public async Task<List<Reminder>?> GetRemindersAsync(int invoiceId,
        int offset = 0,
        int limit  = 500)
        => await _api.GetAsync<List<Reminder>>($"2.0/kb_invoice/{invoiceId}/kb_reminder"
            .AddQueryParameter("offset", offset)
            .AddQueryParameter("limit", limit));

    // Create reminder
    public async Task<Reminder?> CreateReminderAsync(int invoiceId)
        => await _api.PostAsync<Reminder>($"2.0/kb_invoice/{invoiceId}/kb_reminder", null);

    // Search invoice reminders
    public async Task<List<Reminder>?> SearchRemindersAsync(List<SearchQuery> data, int invoiceId)
        => await _api.PostAsync<List<Reminder>>($"2.0/kb_invoice/{invoiceId}/kb_reminder/search", data);

    // Fetch a reminder
    public async Task<Reminder?> GetReminderAsync(int invoiceId, int reminderId)
        => await _api.GetAsync<Reminder>($"2.0/kb_invoice/{invoiceId}/kb_reminder/{reminderId}");

    // Delete a reminder
    public async Task<bool?> DeleteReminderAsync(int invoiceId, int reminderId)
        => await _api.DeleteAsync($"2.0/kb_invoice/{invoiceId}/kb_reminder/{reminderId}");

    // Mark reminder as sent
    public async Task<bool?> MarkReminderAsSentAsync(int invoiceId, int reminderId)
        => await _api.PostActionAsync($"2.0/kb_invoice/{invoiceId}/kb_reminder/{reminderId}/mark_as_sent");

    // Mark reminder as unsent
    public async Task<bool?> MarkReminderAsUnsentAsync(int invoiceId, int reminderId)
        => await _api.PostActionAsync($"2.0/kb_invoice/{invoiceId}/kb_reminder/{reminderId}/mark_as_unsent");

    // Send a reminder
    public async Task<bool?> SendReminderAsync(int invoiceId, int reminderId, SendMailRequest data)
        => await _api.PostActionAsync($"2.0/kb_invoice/{invoiceId}/kb_reminder/{reminderId}/send", data);

    // Show reminder PDF
    public async Task<FileContentResponse?> ShowReminderPdfAsync(int invoiceId, int reminderId)
        => await _api.GetAsync<FileContentResponse>($"2.0/kb_invoice/{invoiceId}/kb_reminder/{reminderId}/pdf");

    // Fetch a list of document settings
    public async Task<List<DocumentSetting>?> GetDocumentSettingsAsync(string orderBy = "id")
        => await _api.GetAsync<List<DocumentSetting>>("2.0/kb_item_setting"
            .AddQueryParameter("order_by", orderBy));

    // Fetch a list of comments
    // Create a comment
    // Fetch a comment

    // Fetch a list of default positions
    // Create a default position
    // Fetch a default position
    // Edit a default position
    // Delete a default position

    // Fetch a list of item positions
    // Create an item position
    // Fetch an item position
    // Edit an item position
    // Delete a item position

    // Fetch a list of text positions
    // Create a text position
    // Fetch a text position
    // Edit a text position
    // Delete a text position

    // Fetch a list of subtotal positions
    // Create a subtotal position
    // Fetch a subtotal position
    // Edit a subtotal position
    // Delete a subtotal position

    // Fetch a list of discount positions
    // Create a discount position
    // Fetch a discount position
    // Edit a discount position
    // Delete a discount position

    // Fetch a list of pagebreak positions
    // Create a pagebreak position
    // Fetch a pagebreak position
    // Edit a pagebreak position
    // Delete a pagebreak position

    // Fetch a list of sub positions
    // Create a sub position
    // Fetch a sub position
    // Edit a sub position
    // Delete a sub position

    // List document templates

    #endregion
}