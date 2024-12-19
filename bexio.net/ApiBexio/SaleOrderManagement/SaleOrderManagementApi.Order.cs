using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Sales;
using bexio.net.Models.Sales.Positions;
using bexio.net.Models.Sales.Repetition;
using bexio.net.Responses;

namespace bexio.net.ApiBexio.SaleOrderManagement;

public partial class SaleOrderManagementApi
{
    #region Orders

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

    #endregion
}