using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Sales;
using bexio.net.Responses;

namespace bexio.net.ApiBexio.SaleOrderManagement;

public partial class SaleOrderManagementApi
{
    
    #region Invoice
    
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

    #endregion
}