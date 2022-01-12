using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using bexio.net.Converter;
using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Contacts;
using bexio.net.Models.Items;
using bexio.net.Models.Other.User;
using bexio.net.Models.Projects;
using bexio.net.Models.Projects.Timesheet;
using bexio.net.Models.Sales;
using bexio.net.Models.Sales.Positions;
using bexio.net.Models.Sales.Repetition;
using bexio.net.Responses;

namespace bexio.net
{
	public partial class ContactApi
	{
		#region Contact relations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "contact_id" or "contact_sub_id" or "updated_at" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<ContactRelation>?> GetContactRelationsAsync(string orderBy = "id",
                                                                           int    offset  = 0,
                                                                           int    limit   = 500)
            => await _api.GetAsync<List<ContactRelation>>("2.0/contact_relation"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ContactRelation?> CreateContactRelationAsync(ContactRelation data)
            => await _api.PostAsync<ContactRelation>("2.0/contact_relation", data);

        /// <summary>
        /// Searchable fields: "contact_id" or "contact_sub_id" or "updated_at"
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" "contact_id" "contact_sub_id" "updated_at"</param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<List<ContactRelation>?> SearchContactRelationsAsync(List<SearchQuery> data,
                                                                              string            orderBy = "id",
                                                                              int               offset  = 0,
                                                                              int               limit   = 500)
            => await _api.PostAsync<List<ContactRelation>>("2.0/contact_relation/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactRelationId"></param>
        /// <returns></returns>
        public async Task<ContactRelation?> GetContactRelationAsync(int contactRelationId)
            => await _api.GetAsync<ContactRelation>("2.0/contact_relation/" + contactRelationId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactRelationId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ContactRelation?> UpdateContactRelationAsync(int contactRelationId, ContactRelation data)
            => await _api.PostAsync<ContactRelation>("2.0/contact_relation/" + contactRelationId, data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactRelationId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteContactRelationAsync(int contactRelationId)
            => await _api.DeleteAsync("2.0/contact_relation/" + contactRelationId);

        #endregion
	}
}