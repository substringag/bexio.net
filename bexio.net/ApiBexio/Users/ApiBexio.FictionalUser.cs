
using System.Threading.Tasks;
using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Other.User;

namespace bexio.net
{
	public partial class BexioApi
	{
		#region FictionalUsers

		/// <summary>
		/// FictionalUser = "Ansprechpartner" in german"
		///-> This uses API /3.0/, not API /2.0/
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="limit"></param>
		/// <returns></returns>
		public async Task<PaginatedList<FictionalUser>?> GetFictionalUsersAsync(int offset = 0, int limit = 500)
			=> await GetPaginatedAsync<FictionalUser>("3.0/fictional_users"
				.AddQueryParameter("offset", offset)
				.AddQueryParameter("limit", limit));

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fictionalUserId"></param>
		/// <returns></returns>
		public async Task<FictionalUser?> GetFictionalUserAsync(int fictionalUserId)
			=> await GetAsync<FictionalUser>($"3.0/fictional_users/{fictionalUserId.ToString()}");

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public async Task<FictionalUser?> InsertFictionalUserAsync(FictionalUser data)
			=> await PostAsync<FictionalUser>("3.0/fictional_users/", data);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <param name="fictionalUserId"></param>
		/// <returns></returns>
		public async Task<FictionalUser?> UpdateFictionalUserAsync(FictionalUser data, int fictionalUserId = -1)
			=> await PatchAsync<FictionalUser>($"3.0/fictional_users/{fictionalUserId.ToString()}", data);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fictionalUserId"></param>
		/// <returns></returns>
		public async Task<bool?> DeleteFictionalUserAsync(int fictionalUserId)
			=> await DeleteAsync($"3.0/fictional_users/{fictionalUserId.ToString()}");

		#endregion
	}
}