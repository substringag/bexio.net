using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using bexio.net.Models;
using RestSharp;

namespace bexio.net
{
    public class Bexio
    {
        private readonly string _url;
        private readonly string _apiToken;
        public HttpClient _httpClient = new HttpClient();

        private readonly RestClient _restClient;

        public Bexio(string url, string apiToken) {
            _url = url;
            _apiToken = apiToken;

            _restClient = new RestClient();

            _restClient.BaseUrl = new Uri(url);
            _restClient.Timeout = -1;
        }


        #region projects  
        /// GET pr_project
        /// https://docs.bexio.com/#tag/Projects
        public List<Project> GetProjects(string orderBy = "id", int offset = 0, int limit = 1000) {
            var request = new RestRequest($"2.0/pr_project?order_by={orderBy}&offset={offset}&limit={limit}", Method.GET);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            List<Project> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Project>>(response.Content);
            return list;
        }
        #endregion

        #region Users
        // https://docs.bexio.com/#operation/v3ListUsers
        public List<User> GetUsers(int offset = 0, int limit = 1000) {
            var request = new RestRequest($"3.0/users?offset={offset}&limit={limit}", Method.GET);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            List<User> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(response.Content);
            return list;
        }
        // https://docs.bexio.com/#operation/v3ShowUser
        public User GetUser(int UserId) {
            var request = new RestRequest($"3.0/users/{UserId.ToString()}", Method.GET);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(response.Content);
            return user;
        }
        #endregion

        #region timesheet 
       /// GET timesheet
        /// https://docs.bexio.com/#tag/Timesheet
        public List<Timesheet> GetTimesheets(string orderBy = "id", int offset = 0, int limit = 1000) {
            var request = new RestRequest($"2.0/timesheet?order_by={orderBy}&offset={offset}&limit={limit}", Method.GET);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            List<Timesheet> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Timesheet>>(response.Content);
            return list;
        }
 
        /// POST timesheet/search
        /// https://docs.bexio.com/#operation/v2SearchTimesheets
        public List<Timesheet> SearchTimesheets(List<TimesheetSearchBody> data)
        {
            var request = new RestRequest($"2.0/timesheet/search", Method.POST);
            string jsonString;
            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", jsonString,  ParameterType.RequestBody);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            List<Timesheet> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Timesheet>>(response.Content);
            return list;
        }
        /// https://docs.bexio.com/#operation/v2CreateTimesheet
        public Timesheet InsertTimesheet(TimesheetBase data)
        {
            var request = new RestRequest($"/2.0/timesheet", Method.POST);
            string jsonPayload;
            jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", jsonPayload,  ParameterType.RequestBody);

            IRestResponse response = ExecuteRestRequestWithBearer(request);
            Timesheet retval = Newtonsoft.Json.JsonConvert.DeserializeObject<Timesheet>(response.Content);
            return retval;
        }


        //https://docs.bexio.com/#operation/v2EditTimesheet
        public Timesheet UpdateTimesheet(Timesheet data, int timesheetId)
        {
            var request = new RestRequest($"/2.0/timesheet/{timesheetId.ToString()}", Method.PATCH);
            string jsonPayload;
            jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", jsonPayload,  ParameterType.RequestBody);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            Timesheet retval = Newtonsoft.Json.JsonConvert.DeserializeObject<Timesheet>(response.Content);
            return retval;
        }

        //https://docs.bexio.com/#operation/DeleteTimesheet
        public Boolean DeleteTimesheet(int timesheetId)
        {
            var request = new RestRequest($"/2.0/timesheet/{timesheetId.ToString()}", Method.DELETE);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            return readSuccessFromResponse(response);   
        }
        #endregion


        #region FictionalUsers 
        // FictionalUser = "Ansprechpartner" in german"
        // https://docs.bexio.com/#operation/v3ShowFictionalUser
        //-> This uses API /3.0/, not API /2.0/
        public List<FictionalUser> GetFictionalUsers(int offset = 0, int limit = 1000) {
            var request = new RestRequest($"3.0/fictional_users?offset={offset}&limit={limit}", Method.GET);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            List<FictionalUser> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FictionalUser>>(response.Content);
            return list;
        }
        public FictionalUser GetFictionalUser(int fictionalUserId) {
            var request = new RestRequest($"3.0/fictional_users/{fictionalUserId.ToString()}", Method.GET);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            FictionalUser user = Newtonsoft.Json.JsonConvert.DeserializeObject<FictionalUser>(response.Content);
            return user;
        }


        //https://docs.bexio.com/#operation/v3CreateFictionalUser
        public FictionalUser InsertFictionalUser(FictionalUserBase data) {
            
            var request = new RestRequest($"3.0/fictional_users/", Method.POST);
            string jsonPayload;
            jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", jsonPayload,  ParameterType.RequestBody);

            IRestResponse response = ExecuteRestRequestWithBearer(request);
            FictionalUser user = Newtonsoft.Json.JsonConvert.DeserializeObject<FictionalUser>(response.Content);
            return user;
        }

        //https://docs.bexio.com/#operation/v3UpdateFictionalUser
        public FictionalUser UpdateFictionalUser(FictionalUserBase data, int fictionalUserId = -1) {
            
            var request = new RestRequest($"3.0/fictional_users/{fictionalUserId.ToString()}", Method.PATCH);
            string jsonPayload;
            jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", jsonPayload,  ParameterType.RequestBody);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            FictionalUser user = Newtonsoft.Json.JsonConvert.DeserializeObject<FictionalUser>(response.Content);
            return user;
        }


        //https://docs.bexio.com/#operation/v3DeleteFictionalUser
        public Boolean DeleteFictionalUser(int fictionalUserId) {
            var request = new RestRequest($"3.0/fictional_users/{fictionalUserId.ToString()}", Method.DELETE);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            return readSuccessFromResponse(response);   
;   
        }

        #endregion


        #region BusinessActivities
        /// GET business activites
        /// https://docs.bexio.com/#operation/v2ListBusinessActivities
        public List<BusinessActivity> GetBusinessActivities(string orderBy = "id", int offset = 0, int limit = 1000) {
            var request = new RestRequest($"2.0/client_service?order_by={orderBy}&offset={offset}&limit={limit}", Method.GET);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            List<BusinessActivity> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BusinessActivity>>(response.Content);
            return list;
        }
        #endregion


        #region Helpers

        private void AddHeaders(RestRequest request) {
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        }

        private void AddBearer(RestRequest request, string accessToken = null) {
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + accessToken);
        }
        private bool readSuccessFromResponse(IRestResponse response){

            // read success flag from JSON Answer:
            //{
            // "success": true
            //}
            dynamic tmp = JObject.Parse(response.Content);
            bool success   = tmp ?.success;

            return success;   
        }

        private IRestResponse ExecuteRestRequestWithBearer(RestRequest request, int trial = 0)
        {
            AddBearer(request, _apiToken);
            IRestResponse response = _restClient.Execute(request);

            if (response.ErrorException == null)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
                    if (trial <= 3) {
                        response = ExecuteRestRequestWithBearer(request, ++trial);
                    } else {
                        throw new Exception("Internal REST API call experienced too many trials.");
                    }
                } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new Exception("Internal REST API call could not connect to endpoint.");
                }
            } else {
                throw new Exception("Internal REST API call experienced an issue.");
            }
            return response;
        }

        #endregion








        private AuthenticationHeaderValue GenerateHeader() {
            return new AuthenticationHeaderValue("Bearer", _apiToken);
        }

        private async Task<string> GetAsync(string path)
        {
            // TODO: not good, make it work for any url
            Uri uri = new Uri(_url + path);
            
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                string message = String.Format("Get failed. Received HTTP {0}", response.StatusCode);
                throw new ApplicationException(message);
            }

            string responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        private async Task<string> PostAsync(string path, Dictionary<string, string> body)
        {
            // TODO: not good, make it work for any url
            Uri uri = new Uri(_url + path);

            FormUrlEncodedContent content = new FormUrlEncodedContent(body);

            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                string message = String.Format("POST failed. Received HTTP {0}", response.StatusCode);
                throw new ApplicationException(message);
            }

            string responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        } 

        public static Dictionary<string, string> ToDictionary(object obj)
        {       
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);   
            return dictionary;
        }
    }
}
