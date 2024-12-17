using bexio.net.Helpers;
using bexio.net.Models;

namespace bexio.net.ApiBexio.Project
{
	public partial class ProjectApi
	{
        #region Communication types

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetCommunicationTypesAsync(
            string orderBy = "id",
            int    offset  = 0,
            int    limit   = 500)
            => await _api.GetAsync<List<SimpleDictionaryEntry>>("2.0/communication_kind"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// Valid search fields: "name"
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> SearchCommunicationTypesAsync(List<SearchQuery> data,
                                                                                      string            orderBy = "id",
                                                                                      int               offset  = 0,
                                                                                      int               limit   = 500)
            => await _api.PostAsync<List<SimpleDictionaryEntry>>("2.0/communication_kind/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        #endregion
	}
}