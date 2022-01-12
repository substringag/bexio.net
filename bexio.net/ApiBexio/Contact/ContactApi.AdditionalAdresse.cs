using System.Collections.Generic;
using System.Threading.Tasks;
using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Contacts;

namespace bexio.net
{
	public partial class ContactApi
	{
		#region Additional addresses

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="orderBy">"id" or "name" or "postcode" or "country_id" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<AdditionalAddress>?> GetAdditionalAddressesAsync(int    contactId,
                                                                                string orderBy = "id",
                                                                                int    offset  = 0,
                                                                                int    limit   = 500)
            => await _api.GetAsync<List<AdditionalAddress>>("2.0/contact/" + contactId + "/additional_address"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public async Task<AdditionalAddress?> CreateAdditionalAddressAsync(AdditionalAddress data, int contactId)
            => await _api.PostAsync<AdditionalAddress>("2.0/contact/" + contactId + "/additional_address", data);

        /// <summary>
        /// Searchable fields: name, address, postcode, city, country_id, subject, email
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name" or "postcode" or "country_id" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<AdditionalAddress>?> SearchAdditionalAddressesAsync(int               contactId,
                                                                                   List<SearchQuery> data,
                                                                                   string            orderBy = "id",
                                                                                   int               offset  = 0,
                                                                                   int               limit   = 500)
            => await _api.PostAsync<List<AdditionalAddress>>($"2.0/contact/{contactId}/additional_address/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="additionalAddressId"></param>
        /// <returns></returns>
        public async Task<AdditionalAddress?> GetAdditionalAddressAsync(int contactId, int additionalAddressId)
            => await _api.GetAsync<AdditionalAddress>($"2.0/contact/{contactId}/additional_address/{additionalAddressId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contactId"></param>
        /// <param name="additionalAddressId"></param>
        /// <returns></returns>
        public async Task<AdditionalAddress?> UpdateAdditionalAddressAsync(AdditionalAddress data,
                                                                           int               contactId,
                                                                           int               additionalAddressId)
            => await _api.PostAsync<AdditionalAddress>($"2.0/contact/{contactId}/additional_address/{additionalAddressId}",
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="additionalAddressId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteAdditionalAddressAsync(int contactId, int additionalAddressId)
            => await _api.DeleteAsync($"2.0/contact/{contactId}/additional_address/{additionalAddressId}");

        #endregion
	}
}