using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Projects.Timesheet;

namespace bexio.net.ApiBexio.Project;

public partial class ProjectApi
{
    #region timesheet

    /// <summary>
    /// Gets a list of all timesheets
    /// https://docs.bexio.com/#tag/Timesheets/operation/v2ListTimesheets
    /// </summary>
    /// <param name="orderBy">"id" or "date" // may append _desc</param>
    /// <param name="offset"></param>
    /// <param name="limit">max: 2000</param>
    /// <returns></returns>
    public async Task<List<TimesheetFetched>?> GetTimesheetsAsync(string orderBy = "id",
        int    offset  = 0,
        int    limit   = 500)
        => await _api.GetAsync<List<TimesheetFetched>>("2.0/timesheet"
            .AddQueryParameter("order_by", orderBy)
            .AddQueryParameter("offset", offset));

    /// <summary>
    /// https://docs.bexio.com/#tag/Timesheets/operation/v2CreateTimesheet
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<TimesheetFetched?> CreateTimesheetAsync(Timesheet data)
        => await _api.PostAsync<TimesheetFetched>("2.0/timesheet", data);

    /// <summary>
    /// https://docs.bexio.com/#tag/Timesheets/operation/v2SearchTimesheets
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
        => await _api.PostAsync<List<TimesheetFetched>>("2.0/timesheet/search"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit),
            data);

    /// <summary>
    /// https://docs.bexio.com/#tag/Timesheets/operation/v2ShowTimesheet
    /// </summary>
    /// <param name="timesheetId"></param>
    /// <returns></returns>
    public async Task<TimesheetFetched?> GetTimesheetAsync(int timesheetId)
        => await _api.GetAsync<TimesheetFetched>($"/2.0/timesheet/{timesheetId}");

    /// <summary>
    /// https://docs.bexio.com/#tag/Timesheets/operation/v2EditTimesheet
    /// </summary>
    /// <param name="data"></param>
    /// <param name="timesheetId"></param>
    /// <returns></returns>
    public async Task<Timesheet?> UpdateTimesheetAsync(Timesheet data, int timesheetId)
        => await _api.PostAsync<Timesheet>($"/2.0/timesheet/{timesheetId.ToString()}", data);

    /// <summary>
    /// https://docs.bexio.com/#tag/Timesheets/operation/DeleteTimesheet
    /// </summary>
    /// <param name="timesheetId"></param>
    /// <returns></returns>
    public async Task<bool?> DeleteTimesheetAsync(int timesheetId)
        => await _api.DeleteAsync($"2.0/timesheet/{timesheetId.ToString()}");

    /// <summary>
    /// https://docs.bexio.com/#tag/Timesheets/operation/v2ListTimeSheetStatus
    /// </summary>
    /// <param name="orderBy">"id" or "name" // may append _desc</param>
    /// <param name="offset"></param>
    /// <param name="limit">max: 2000</param>
    /// <returns></returns>
    public async Task<List<SimpleDictionaryEntry>?> GetTimesheetStatusAsync(string orderBy = "id",
        int    offset  = 0,
        int    limit   = 500)
        => await _api.GetAsync<List<SimpleDictionaryEntry>>("2.0/timesheet_status"
            .AddQueryParameter("order_by", orderBy)
            .AddQueryParameter("offset", offset)
            .AddQueryParameter("limit", limit));

    #endregion
}