using System.Collections.Generic;
using System.Threading.Tasks;
using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Projects;

namespace bexio.net
{
	public partial class BexioApi
	{
		#region BusinessActivities

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<BusinessActivity>?> GetBusinessActivitiesAsync(
            string orderBy = "id",
            int    offset  = 0,
            int    limit   = 500)
            => await GetAsync<List<BusinessActivity>>("2.0/client_service"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// Create a business activity
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<BusinessActivity?> CreateBusinessActivityAsync(BusinessActivity data)
            => await PostAsync<BusinessActivity>("2.0/client_service", data);

        /// <summary>
        /// 
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
            => await PostAsync<List<BusinessActivity>>("2.0/client_service/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        #endregion
	}
}