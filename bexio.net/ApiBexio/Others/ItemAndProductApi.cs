using System.Threading.Tasks;
using bexio.net.Models.Items;

namespace bexio.net
{
	public partial class ItemAndProductApi
	{
		private readonly ApiBexio.BexioApi _api;

        internal ItemAndProductApi(ApiBexio.BexioApi api)
        {
            _api = api;
        }
        
		#region Items and Products

		// TODO

		// Fetch a list of items
		// Create item
		// Search items
		// Fetch an item
		public async Task<Article?> GetArticleAsync(int articleId)
			=> await _api.GetAsync<Article>($"2.0/article/{articleId}");

		// Edit an item
		// Delete an item

		// Fetch a list of stock locations
		// Search stock locations

		// Fetch a list of stock areas
		// Search stock areas

		#endregion
	}
}