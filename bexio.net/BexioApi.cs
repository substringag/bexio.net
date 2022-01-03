using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Other.FictionalUser;
using bexio.net.Models.Projects;
using bexio.net.Models.Projects.Timesheet;
using bexio.net.Models.User;
using bexio.net.Responses;

namespace bexio.net
{
    public class BexioApi
    {
        public const string VERSION = "1.0.0";

        private readonly string                  _url;
        private readonly string                  _apiToken;
        private readonly UnsuccessfulReturnStyle _unsuccessfulReturnStyle;
        private readonly JsonSerializerOptions   _serializeOptions;
        private readonly HttpClient              _httpClient;

        public Encoding Encoding { get; set; } = Encoding.UTF8;


        public BexioApi(string                  apiToken,
                        string                  url                     = "https://api.bexio.com",
                        UnsuccessfulReturnStyle unsuccessfulReturnStyle = UnsuccessfulReturnStyle.ReturnNull)
        {
            _url                     = url;
            _unsuccessfulReturnStyle = unsuccessfulReturnStyle;
            _apiToken                = apiToken;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent",
                $"BexioApi/{VERSION} (DotNet/{Environment.Version}/{Environment.OSVersion})");

            _serializeOptions = new JsonSerializerOptions
            {
                // PropertyNamingPolicy allows us to name CamelCase in C# Models, but the json will have snake_case
                PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
                // We dont want to include useless spaces in the json
                WriteIndented = false,
                // DefaultIgnoreCondition omits properties with 'default' value, which is useful for Create-APIs
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };
            _serializeOptions.Converters.Add(new DateTimeParseConverter());
            // The universal converter, that can cast any type to string. Not in use, in favor of 'Number'
            // _serializeOptions.Converters.Add(new AnyToStringConverter());
            _serializeOptions.Converters.Add(new NullableDecimalConverter());
        }

        #region Projects

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<Project>?> GetProjects(string orderBy = "id", int offset = 0, int limit = 500)
            => await GetAsync<List<Project>>("2.0/pr_project"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<Project?> CreateProject(Project project)
            => await PostAsync<Project>("2.0/pr_project", project);

        /// <summary>
        /// Searchable fields: "name", "contact_id", "pr_state_id"
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<Project>?> SearchProjects(List<SearchQuery> data,
                                                         string            orderBy = "id",
                                                         int               offset  = 0,
                                                         int               limit   = 500)
            => await PostAsync<List<Project>>("2.0/pr_project/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<Project?> GetProject(int projectId)
            => await GetAsync<Project>($"2.0/pr_project/{projectId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<Project?> UpdateProject(int projectId, Project project)
            => await PostAsync<Project>($"2.0/pr_project/{projectId}", project);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteProject(int projectId)
            => await DeleteAsync($"2.0/pr_project/{projectId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<bool?> ArchiveProject(int projectId)
            => (await PostAsync<BooleanResponse>($"2.0/pr_project/{projectId}/archive", null))?.Success;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<bool?> UnarchiveProject(int projectId)
            => (await PostAsync<BooleanResponse>($"2.0/pr_project/{projectId}/reactivate", null))?.Success;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetProjectStatuses()
            => await GetAsync<List<SimpleDictionaryEntry>>("2.0/pr_project_state");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<string?> GetProjectStatus(int projectId)
            => (await GetProjectStatuses())?.Find(e => e.Id == projectId)?.Name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetProjectTypes(string orderBy = "id")
            => await GetAsync<List<SimpleDictionaryEntry>>("2.0/pr_project_type"
                .AddQueryParameter("order_by", orderBy));

        public async Task<string?> GetProjectType(int projectId)
            => (await GetProjectTypes())?.Find(e => e.Id == projectId)?.Name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="limit">max: 2000</param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<List<Milestone>?> GetProjectMilestones(int projectId,
                                                                 int limit  = 500,
                                                                 int offset = 0)
            => await GetAsync<List<Milestone>>($"3.0/projects/{projectId}/milestones"
                .AddQueryParameter("limit", limit)
                .AddQueryParameter("offset", offset));
        // TODO this method returns a paginated list and returns header values.
        // see https://docs.bexio.com/#operation/ListMilestones

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="milestone"></param>
        /// <returns></returns>
        public async Task<Milestone?> CreateMilestone(int projectId, Milestone milestone)
            => await PostAsync<Milestone>($"3.0/projects/{projectId}/milestones", milestone);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="milestoneId"></param>
        /// <returns></returns>
        public async Task<Milestone?> GetMilestone(int projectId, int milestoneId)
            => await GetAsync<Milestone>($"3.0/projects/{projectId}/milestones/{milestoneId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="milestoneId"></param>
        /// <param name="milestone"></param>
        /// <returns></returns>
        public async Task<Milestone?> UpdateMilestone(int projectId, int milestoneId, Milestone milestone)
            => await PostAsync<Milestone>($"3.0/projects/{projectId}/milestones/{milestoneId}", milestone);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="milestoneId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteMilestone(int projectId, int milestoneId)
            => await DeleteAsync($"3.0/projects/{projectId}/milestones/{milestoneId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="limit">max: 2000</param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<List<WorkPackage>?> GetWorkPackages(int projectId,
                                                              int limit  = 500,
                                                              int offset = 0)
            => await GetAsync<List<WorkPackage>>($"3.0/projects/{projectId}/packages"
                .AddQueryParameter("limit", limit)
                .AddQueryParameter("offset", offset));
        // TODO this method returns a paginated list and returns header values.
        // see https://docs.bexio.com/#operation/ListWorkPackages

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="workPackage"></param>
        /// <returns></returns>
        public async Task<WorkPackage?> CreateWorkPackage(int projectId, WorkPackage workPackage)
            => await PostAsync<WorkPackage>($"3.0/projects/{projectId}/packages", workPackage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="workPackageId"></param>
        /// <returns></returns>
        public async Task<WorkPackage?> GetWorkPackage(int projectId, int workPackageId)
            => await GetAsync<WorkPackage>($"3.0/projects/{projectId}/packages/{workPackageId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="workPackageId"></param>
        /// <param name="workPackage"></param>
        /// <returns></returns>
        public async Task<WorkPackage?> UpdateWorkPackage(int projectId, int workPackageId, WorkPackage workPackage)
            => await PostAsync<WorkPackage>($"3.0/projects/{projectId}/packages/{workPackageId}", workPackage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="workPackageId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteWorkPackage(int projectId, int workPackageId)
            => await DeleteAsync($"3.0/projects/{projectId}/packages/{workPackageId}");


        #region timesheet

        /// <summary>
        /// Gets a list of all timesheets
        /// </summary>
        /// <param name="orderBy">"id" or "date" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<TimesheetFetched>?> GetTimesheets(string orderBy = "id",
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
        public async Task<TimesheetFetched?> CreateTimesheet(Timesheet data)
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
        public async Task<List<TimesheetFetched>?> SearchTimesheets(List<SearchQuery> data,
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
        public async Task<TimesheetFetched?> GetTimesheet(int timesheetId)
            => await GetAsync<TimesheetFetched>($"/2.0/timesheet/{timesheetId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timesheetId"></param>
        /// <returns></returns>
        public async Task<TimesheetFetched?> UpdateTimesheet(Timesheet data, int timesheetId)
            => await PostAsync<TimesheetFetched>($"/2.0/timesheet/{timesheetId.ToString()}", data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timesheetId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteTimesheet(int timesheetId)
            => await DeleteAsync($"2.0/timesheet/{timesheetId.ToString()}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<TimesheetStatus>?> GetTimesheetStatus(string orderBy = "id",
                                                                     int    offset  = 0,
                                                                     int    limit   = 500)
            => await GetAsync<List<TimesheetStatus>>("2.0/timesheet_status"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        #endregion

        #region BusinessActivities

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<BusinessActivity>?> GetBusinessActivities(
            string orderBy = "name",
            int    offset  = 0,
            int    limit   = 500)
            => await GetAsync<List<BusinessActivity>>("2.0/client_service"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// Create a business activity
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<BusinessActivity?> CreateBusinessActivity(BusinessActivity data)
            => await PostAsync<BusinessActivity>("2.0/client_service", data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<BusinessActivity>?> SearchBusinessActivities(List<SearchQuery> data,
                                                                            string            orderBy = "id",
                                                                            int               offset  = 0,
                                                                            int               limit   = 500)
            => await PostAsync<List<BusinessActivity>>("2.0/client_service/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        #endregion

        #region Communication types

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetCommunicationTypes(
            string orderBy = "id",
            int    offset  = 0,
            int    limit   = 500)
            => await GetAsync<List<SimpleDictionaryEntry>>("2.0/communication_kind"
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
        public async Task<List<SimpleDictionaryEntry>?> SearchCommunicationTypes(List<SearchQuery> data,
            string                                                                                 orderBy = "id",
            int                                                                                    offset  = 0,
            int                                                                                    limit   = 500)
            => await PostAsync<List<SimpleDictionaryEntry>>("2.0/communication_kind/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        #endregion

        #endregion

        #region Other Endpoints

        #region Users

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<List<User>?> GetUsers(int offset = 0, int limit = 1000)
            => await GetAsync<List<User>>("3.0/users"
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<User?> GetUser(int userId)
            => await GetAsync<User>($"3.0/users/{userId.ToString()}");

        #endregion


        #region FictionalUsers

        /// <summary>
        /// FictionalUser = "Ansprechpartner" in german"
        ///-> This uses API /3.0/, not API /2.0/
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<List<FictionalUser>?> GetFictionalUsers(int offset = 0, int limit = 1000)
            => await GetAsync<List<FictionalUser>>("3.0/fictional_users"
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fictionalUserId"></param>
        /// <returns></returns>
        public async Task<FictionalUser?> GetFictionalUser(int fictionalUserId)
            => await GetAsync<FictionalUser>($"3.0/fictional_users/{fictionalUserId.ToString()}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<FictionalUser?> InsertFictionalUser(FictionalUserBase data)
            => await PostAsync<FictionalUser>("3.0/fictional_users/", data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fictionalUserId"></param>
        /// <returns></returns>
        public async Task<FictionalUser?> UpdateFictionalUser(FictionalUserBase data, int fictionalUserId = -1)
            => await PatchAsync<FictionalUser>($"3.0/fictional_users/{fictionalUserId.ToString()}", data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fictionalUserId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteFictionalUser(int fictionalUserId)
            => await DeleteAsync($"3.0/fictional_users/{fictionalUserId.ToString()}");

        #endregion

        #endregion


        #region Internal Methods

        internal async Task<TResponse?> GetAsync<TResponse>(string url)
            where TResponse : class
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method     = HttpMethod.Get,
                RequestUri = new Uri(JoinUriSegments(_url, url)),
            };
            return await ExecuteRequestInternal<TResponse>(httpRequestMessage);
        }


        internal async Task<TResponse?> PostAsync<TResponse>(string url, object? payload)
            where TResponse : class
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method     = HttpMethod.Post,
                RequestUri = new Uri(JoinUriSegments(_url, url)),
                Content = new StringContent(
                    JsonSerializer.Serialize(payload, _serializeOptions),
                    Encoding,
                    "application/json")
            };
            return await ExecuteRequestInternal<TResponse>(httpRequestMessage);
        }

        internal async Task<TResponse?> PatchAsync<TResponse>(string url, object? payload)
            where TResponse : class
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method     = HttpMethod.Patch,
                RequestUri = new Uri(JoinUriSegments(_url, url)),
                Content = new StringContent(
                    JsonSerializer.Serialize(payload, _serializeOptions),
                    Encoding,
                    "application/json")
            };
            return await ExecuteRequestInternal<TResponse>(httpRequestMessage);
        }


        internal async Task<TResponse?> PutAsync<TResponse>(string url, object? payload)
            where TResponse : class
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method     = HttpMethod.Put,
                RequestUri = new Uri(JoinUriSegments(_url, url)),
                Content = new StringContent(
                    JsonSerializer.Serialize(payload, _serializeOptions),
                    Encoding,
                    "application/json")
            };
            return await ExecuteRequestInternal<TResponse>(httpRequestMessage);
        }

        internal async Task<bool?> DeleteAsync(string url)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method     = HttpMethod.Delete,
                RequestUri = new Uri(JoinUriSegments(_url, url)),
            };

            return (await ExecuteRequestInternal<BooleanResponse>(httpRequestMessage))?.Success == true;
        }

        private async Task<TResponse?> ExecuteRequestInternal<TResponse>(HttpRequestMessage request)
            where TResponse : class
        {
            try
            {
                request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "Bearer " + _apiToken);
                request.Headers.Add(HttpRequestHeader.Accept.ToString(), "application/json");

                var httpResponse = await _httpClient.SendAsync(request);

                if (httpResponse.StatusCode != HttpStatusCode.OK &&
                    httpResponse.StatusCode != HttpStatusCode.Created &&
                    httpResponse.StatusCode != HttpStatusCode.NotModified)
                {
                    // TODO: log to ILogger
                    Console.WriteLine("Error: " + (int)httpResponse.StatusCode);
                    Console.WriteLine(await httpResponse.Content.ReadAsStringAsync());
                    if (_unsuccessfulReturnStyle == UnsuccessfulReturnStyle.Throw)
                        throw new UnsuccessfulException((int)httpResponse.StatusCode);
                    return null;
                }

                string responseContentString = await httpResponse.Content.ReadAsStringAsync();

#if DEBUG
                Console.WriteLine("Response-Content:");
                Console.WriteLine(responseContentString);
                Console.WriteLine("---");
#endif

                if (string.IsNullOrEmpty(responseContentString))
                {
                    if (_unsuccessfulReturnStyle == UnsuccessfulReturnStyle.Throw)
                        throw new UnsuccessfulException((int)httpResponse.StatusCode, "Empty response");
                    return null;
                }

                return JsonSerializer.Deserialize<TResponse>(responseContentString,
                    _serializeOptions);
            }
            catch (UnsuccessfulException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // TODO Log exception
                Console.WriteLine(ex);
                return null;
            }
        }

        private static string JoinUriSegments(string uri, params string[]? segments)
        {
            if (segments == null || segments.Length == 0)
                return uri;

            return segments.Aggregate(uri, (current, segment) => $"{current.TrimEnd('/')}/{segment.TrimStart('/')}");
        }

        #endregion
    }
}
