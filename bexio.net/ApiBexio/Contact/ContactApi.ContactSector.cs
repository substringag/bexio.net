using bexio.net.Helpers;
using bexio.net.Models;

namespace bexio.net.ApiBexio.Contact
{
	public partial class ContactApi
	{
		#region Contact sectors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetContactSectorsAsync(string orderBy = "id",
                                                                               int    offset  = 0,
                                                                               int    limit   = 500)
            => await _api.GetAsync<List<SimpleDictionaryEntry>>("2.0/contact_branch"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string?> GetContactSectorAsync(int id)
            => (await SearchContactSectorsAsync(new List<SearchQuery> { new("id", id.ToString()) }))
                ?.FirstOrDefault()
                ?.Name;

        /// <summary>
        /// Searchable fields: name
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> SearchContactSectorsAsync(List<SearchQuery> data,
                                                                                  string            orderBy = "id",
                                                                                  int               offset  = 0,
                                                                                  int               limit   = 500)
            => await _api.PostAsync<List<SimpleDictionaryEntry>>("2.0/contact_branch/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        #endregion
	}
}
