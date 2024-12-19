using bexio.net.Helpers;
using bexio.net.Models.Sales;

namespace bexio.net.ApiBexio.SaleOrderManagement;

public partial class SaleOrderManagementApi
{
    #region Document

    // Fetch a list of document settings
    public async Task<List<DocumentSetting>?> GetDocumentSettingsAsync(string orderBy = "id")
        => await _api.GetAsync<List<DocumentSetting>>("2.0/kb_item_setting"
            .AddQueryParameter("order_by", orderBy));

    #endregion
}