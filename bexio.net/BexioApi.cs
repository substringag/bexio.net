using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using bexio.net.Helpers;
using bexio.net.Models.BusinessActivity;
using bexio.net.Models.FictionalUser;
using bexio.net.Models.Project;
using bexio.net.Models.Timesheet;
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
                PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
                WriteIndented        = false
            };
            _serializeOptions.Converters.Add(new DateTimeParseConverter());
            _serializeOptions.Converters.Add(new AnyToStringConverter());
        }

        #region projects

        /// GET pr_project
        /// https://docs.bexio.com/#tag/Projects
        public async Task<List<Project>?> GetProjects(string orderBy = "name", int offset = 0, int limit = 1000)
            => await GetAsync<List<Project>>("2.0/pr_project"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        #endregion

        #region Users

        // https://docs.bexio.com/#operation/v3ListUsers
        public async Task<List<User>?> GetUsers(int offset = 0, int limit = 1000)
            => await GetAsync<List<User>>("3.0/users"
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        // https://docs.bexio.com/#operation/v3ShowUser
        public async Task<User?> GetUser(int userId)
            => await GetAsync<User>($"3.0/users/{userId.ToString()}");

        #endregion

        #region timesheet

        /// GET timesheet
        /// https://docs.bexio.com/#operation/v2ListTimesheets
        public async Task<List<Timesheet>?> GetTimesheets(string orderBy = "id", int offset = 0, int limit = 1000)
            => await GetAsync<List<Timesheet>>("2.0/timesheet"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset));

        /// POST timesheet/search
        /// https://docs.bexio.com/#operation/v2SearchTimesheets
        public async Task<List<Timesheet>?> SearchTimesheets(List<TimesheetSearchBody> data,
                                                             string                    orderBy = "date_desc",
                                                             int                       offset  = 0)
            => await PostAsync<List<Timesheet>>("2.0/timesheet/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset),
                data);

        /// https://docs.bexio.com/#operation/v2CreateTimesheet
        public async Task<Timesheet?> CreateTimesheet(TimesheetBase data)
            => await PostAsync<Timesheet>("2.0/timesheet", data);


        // https://docs.bexio.com/#operation/v2EditTimesheet
        public async Task<TimesheetUpdate?> UpdateTimesheet(TimesheetUpdate data, int timesheetId)
            => await PostAsync<TimesheetUpdate>($"/2.0/timesheet/{timesheetId.ToString()}", data);

        // https://docs.bexio.com/#operation/DeleteTimesheet
        public async Task<bool> DeleteTimesheet(int timesheetId)
            => await DeleteAsync($"2.0/timesheet/{timesheetId.ToString()}") == true;

        /// GET timesheet status
        /// https://docs.bexio.com/#operation/v2ListTimeSheetStatus
        public async Task<List<TimesheetStatus>?> GetTimesheetStatus(string orderBy = "name",
                                                                     int    offset  = 0,
                                                                     int    limit   = 1000)
            => await GetAsync<List<TimesheetStatus>>("2.0/timesheet_status"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        #endregion


        #region FictionalUsers

        // FictionalUser = "Ansprechpartner" in german"
        // https://docs.bexio.com/#operation/v3ShowFictionalUser
        //-> This uses API /3.0/, not API /2.0/
        public async Task<List<FictionalUser>?> GetFictionalUsers(int offset = 0, int limit = 1000)
            => await GetAsync<List<FictionalUser>>("3.0/fictional_users"
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        public async Task<FictionalUser?> GetFictionalUser(int fictionalUserId)
            => await GetAsync<FictionalUser>($"3.0/fictional_users/{fictionalUserId.ToString()}");

        //https://docs.bexio.com/#operation/v3CreateFictionalUser
        public async Task<FictionalUser?> InsertFictionalUser(FictionalUserBase data)
            => await PostAsync<FictionalUser>("3.0/fictional_users/", data);

        //https://docs.bexio.com/#operation/v3UpdateFictionalUser
        public async Task<FictionalUser?> UpdateFictionalUser(FictionalUserBase data, int fictionalUserId = -1)
            => await PatchAsync<FictionalUser>($"3.0/fictional_users/{fictionalUserId.ToString()}", data);

        //https://docs.bexio.com/#operation/v3DeleteFictionalUser
        public async Task<bool> DeleteFictionalUser(int fictionalUserId)
            => await DeleteAsync($"3.0/fictional_users/{fictionalUserId.ToString()}") == true;

        #endregion


        #region BusinessActivities

        /// GET business activites
        /// https://docs.bexio.com/#operation/v2ListBusinessActivities
        public async Task<List<BusinessActivity>?> GetBusinessActivities(
            string orderBy = "name",
            int    offset  = 0,
            int    limit   = 1000)
            => await GetAsync<List<BusinessActivity>>("2.0/client_service"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

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

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    // TODO: log to ILogger
                    Console.WriteLine("Error: " + httpResponse.StatusCode);
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
