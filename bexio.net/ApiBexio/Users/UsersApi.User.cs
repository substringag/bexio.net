using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Other.User;

namespace bexio.net.ApiBexio.Users;

public partial class UsersApi
{
	private readonly BexioApi _api;

	internal UsersApi(BexioApi api)
	{
		_api = api;
	}
        
	#region Users

	/// <summary>
	/// 
	/// </summary>
	/// <param name="offset"></param>
	/// <param name="limit">max: 2000</param>
	/// <returns></returns>
	public async Task<PaginatedList<User>?> GetUsersAsync(int offset = 0, int limit = 500)
		=> await _api.GetPaginatedAsync<User>("3.0/users"
			.AddQueryParameter("offset", offset)
			.AddQueryParameter("limit", limit));

	/// <summary>
	/// 
	/// </summary>
	/// <param name="userId"></param>
	/// <returns></returns>
	public async Task<User?> GetUserAsync(int userId)
		=> await _api.GetAsync<User>($"3.0/users/{userId.ToString()}");

	#endregion

}