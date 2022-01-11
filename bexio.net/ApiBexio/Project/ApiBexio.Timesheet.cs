using System.Collections.Generic;
using System.Threading.Tasks;
using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Projects.Timesheet;

namespace bexio.net
{
	public partial class BexioApi
	{
 		#region timesheet

        /// <summary>
        /// Gets a list of all timesheets
        /// </summary>
        /// <param name="orderBy">"id" or "date" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<TimesheetFetched>?> GetTimesheetsAsync(string orderBy = "id",
                                                                      int    offset  = 0,
                                                                      int    limit   = 500)
            => await GetAsync<List<TimesheetFetched>>("2.0/timesheet"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<TimesheetFetched?> CreateTimesheetAsync(Timesheet data)
            => await PostAsync<TimesheetFetched>("2.0/timesheet", data);

        /// <summary>
        /// Gets a list of timesheets that match the given filter.<br/>
        /// Valid searchable fields are:<br/>
        /// id, client_service_id, contact_id, user_id, pr_project_id, status_id
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "date" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<TimesheetFetched>?> SearchTimesheetsAsync(List<SearchQuery> data,
                                                                         string            orderBy = "id",
                                                                         int               offset  = 0,
                                                                         int               limit   = 500)
            => await PostAsync<List<TimesheetFetched>>("2.0/timesheet/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timesheetId"></param>
        /// <returns></returns>
        public async Task<TimesheetFetched?> GetTimesheetAsync(int timesheetId)
            => await GetAsync<TimesheetFetched>($"/2.0/timesheet/{timesheetId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timesheetId"></param>
        /// <returns></returns>
        public async Task<TimesheetFetched?> UpdateTimesheetAsync(Timesheet data, int timesheetId)
            => await PostAsync<TimesheetFetched>($"/2.0/timesheet/{timesheetId.ToString()}", data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timesheetId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteTimesheetAsync(int timesheetId)
            => await DeleteAsync($"2.0/timesheet/{timesheetId.ToString()}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetTimesheetStatusAsync(string orderBy = "id",
                                                                                int    offset  = 0,
                                                                                int    limit   = 500)
            => await GetAsync<List<SimpleDictionaryEntry>>("2.0/timesheet_status"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        #endregion
	}
}