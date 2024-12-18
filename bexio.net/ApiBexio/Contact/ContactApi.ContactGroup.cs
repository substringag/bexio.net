using bexio.net.Helpers;
using bexio.net.Models;

namespace bexio.net.ApiBexio.Contact
{
	public partial class ContactApi
	{
		#region Contact group

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetContactGroupsAsync(string orderBy = "id",
                                                                              int    offset  = 0,
                                                                              int    limit   = 500)
            => await _api.GetAsync<List<SimpleDictionaryEntry>>("2.0/contact_group"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<long?> CreateContactGroupAsync(string name)
            => (await _api.PostAsync<SimpleDictionaryEntry>("2.0/contact_group", new { name }))
                ?.Id;

        /// <summary>
        /// Searchable fields: name
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> SearchContactGroupsAsync(List<SearchQuery> data,
                                                                                 string            orderBy = "id",
                                                                                 int               offset  = 0,
                                                                                 int               limit   = 500)
            => await _api.PostAsync<List<SimpleDictionaryEntry>>("2.0/contact_group/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactGroupId"></param>
        /// <returns></returns>
        public async Task<string?> GetContactGroupAsync(int contactGroupId)
            => (await _api.GetAsync<SimpleDictionaryEntry>("2.0/contact_group/" + contactGroupId))
                ?.Name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactGroupId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<SimpleDictionaryEntry?> UpdateContactGroupAsync(int     contactGroupId,
                                                                          string? name)
            => await _api.PostAsync<SimpleDictionaryEntry>("2.0/contact_group/" + contactGroupId, new { name });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactGroupId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteContactGroupAsync(int contactGroupId)
            => await _api.DeleteAsync("2.0/contact_group/" + contactGroupId);

        #endregion
	}
}