using System.Collections.Generic;
using System.Threading.Tasks;
using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Contacts;

namespace bexio.net
{
	public partial class ContactApi
	{
        private readonly BexioApi _api;

        internal ContactApi(BexioApi api)
        {
            _api = api;
        }

		#region Contacts

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "nr" or "name_1" or "name_2" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<Contact>?> GetContactsAsync(int orderBy = 0, int offset = 0, int limit = 500)
            => await _api.GetAsync<List<Contact>>("2.0/contact"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Contact?> CreateContactAsync(Contact data)
            => await _api.PostAsync<Contact>("2.0/contact", data);

        /// <summary>
        /// possible search fields: "id", "name_1", "name_2",
        /// "nr", "address", "mail", "mail_second", "postcode",
        /// "city", "country_id", "contact_group_ids", "contact_type_id",
        /// "updated_at", "user_id", "phone_fixed", "phone_mobile", "fax"
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "nr" or "name_1" or "updated_at" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<Contact>?> SearchContactsAsync(List<SearchQuery> data,
                                                              string            orderBy = "id",
                                                              int               offset  = 0,
                                                              int               limit   = 500)
            => await _api.PostAsync<List<Contact>>("2.0/contact/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Contact?> GetContactAsync(int id)
            => await _api.GetAsync<Contact>("2.0/contact/" + id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Contact?> UpdateContactAsync(int id, Contact data)
            => await _api.PostAsync<Contact>("2.0/contact/" + id, data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteContactAsync(int id)
            => await _api.DeleteAsync("2.0/contact/" + id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<List<Contact>?> CreateContactsAsync(List<Contact> data)
            => await _api.PostAsync<List<Contact>>("2.0/contact/_bulk_create", data);



        #region Salutations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetSalutationsAsync(int offset = 0, int limit = 500)
            => await _api.GetAsync<List<SimpleDictionaryEntry>>("2.0/salutation"
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<SimpleDictionaryEntry?> CreateSalutationAsync(string name)
            => await _api.PostAsync<SimpleDictionaryEntry>("2.0/salutation", new { name });

        /// <summary>
        /// Searchable fields: name
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> SearchSalutationsAsync(List<SearchQuery> data,
                                                                               int               offset = 0,
                                                                               int               limit  = 500)
            => await _api.PostAsync<List<SimpleDictionaryEntry>>("2.0/salutation/search"
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salutationId"></param>
        /// <returns></returns>
        public async Task<SimpleDictionaryEntry?> GetSalutationAsync(int salutationId)
            => await _api.GetAsync<SimpleDictionaryEntry>($"2.0/salutation/{salutationId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salutationId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<SimpleDictionaryEntry?> UpdateSalutationAsync(int salutationId, string name)
            => await _api.PostAsync<SimpleDictionaryEntry>($"2.0/salutation/{salutationId}", new { name });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salutationId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteSalutationAsync(int salutationId)
            => await _api.DeleteAsync($"2.0/salutation/{salutationId}");

        #endregion

        #region Titles

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name"</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetTitlesAsync(string orderBy = "id",
                                                                       int    offset  = 0,
                                                                       int    limit   = 500)
            => await _api.GetAsync<List<SimpleDictionaryEntry>>("2.0/title"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<SimpleDictionaryEntry?> CreateTitleAsync(string name)
            => await _api.PostAsync<SimpleDictionaryEntry>("2.0/title", new { name });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name"</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> SearchTitlesAsync(List<SearchQuery> data,
                                                                          string            orderBy = "id",
                                                                          int               offset  = 0,
                                                                          int               limit   = 500)
            => await _api.PostAsync<List<SimpleDictionaryEntry>>("2.0/title/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titleId"></param>
        /// <returns></returns>
        public async Task<SimpleDictionaryEntry?> GetTitleAsync(int titleId)
            => await _api.GetAsync<SimpleDictionaryEntry>($"2.0/title/{titleId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titleId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<SimpleDictionaryEntry?> UpdateTitleAsync(int titleId, string name)
            => await _api.PostAsync<SimpleDictionaryEntry>($"2.0/title/{titleId}", new { name });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titleId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteTitleAsync(int titleId)
            => await _api.DeleteAsync($"2.0/title/{titleId}");

        #endregion Titles

		#endregion Contacts
	}
}