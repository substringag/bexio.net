using bexio.net.Helpers;
using bexio.net.Models.Sales;

namespace bexio.net.ApiBexio.SaleOrderManagement;

public partial class SaleOrderManagementApi
{
    #region Delivery

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

    #endregion
}