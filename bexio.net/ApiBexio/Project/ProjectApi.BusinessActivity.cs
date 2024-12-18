using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Projects;

namespace bexio.net.ApiBexio.Project
{
	public partial class ProjectApi
	{
		#region BusinessActivities

        /// <summary>
        /// https://docs.bexio.com/#tag/Business-Activities/operation/v2ListBusinessActivities
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<BusinessActivity>?> GetBusinessActivitiesAsync(
            string orderBy = "id",
            int    offset  = 0,
            int    limit   = 500)
            => await _api.GetAsync<List<BusinessActivity>>("2.0/client_service"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// https://docs.bexio.com/#tag/Business-Activities/operation/v2CreateBusinessActivity
        /// Create a business activity
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<BusinessActivity?> CreateBusinessActivityAsync(BusinessActivity data)
            => await _api.PostAsync<BusinessActivity>("2.0/client_service", data);

        /// <summary>
        /// https://docs.bexio.com/#tag/Business-Activities/operation/v2SearchBusinessActivities
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<BusinessActivity>?> SearchBusinessActivitiesAsync(List<SearchQuery> data,
                                                                                 string            orderBy = "id",
                                                                                 int               offset  = 0,
                                                                                 int               limit   = 500)
            => await _api.PostAsync<List<BusinessActivity>>("2.0/client_service/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        #endregion
	}
}