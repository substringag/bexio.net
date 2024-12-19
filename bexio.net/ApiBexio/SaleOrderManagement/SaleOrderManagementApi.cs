namespace bexio.net.ApiBexio.SaleOrderManagement;

public partial class SaleOrderManagementApi
{
    private readonly BexioApi _api;

    internal SaleOrderManagementApi(BexioApi api)
    {
        _api = api;
    }

}