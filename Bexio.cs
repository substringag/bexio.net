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

        /// GET pr_project
        /// https://docs.bexio.com/#tag/Projects
        public List<Project> GetProjects(string orderBy = "id", int offset = 0, int limit = 1000) {
            var request = new RestRequest($"pr_project?order_by={orderBy}&offset={offset}&limit={limit}", Method.GET);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            List<Project> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Project>>(response.Content);
            return list;
        }

       /// GET timesheet
        /// https://docs.bexio.com/#tag/Timesheet
        public List<Timesheet> GetTimesheets(string orderBy = "id", int offset = 0, int limit = 1000) {
            var request = new RestRequest($"timesheet?order_by={orderBy}&offset={offset}&limit={limit}", Method.GET);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            List<Timesheet> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Timesheet>>(response.Content);
            return list;
        }
 
        /// POST timesheet/search
        /// https://docs.bexio.com/#operation/v2SearchTimesheets
        public List<Timesheet> SearchTimesheets(List<TimesheetSearchBody> data)
        {
            var request = new RestRequest($"timesheet/search", Method.POST);
            string jsonString;
            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", jsonString,  ParameterType.RequestBody);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            List<Timesheet> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Timesheet>>(response.Content);
            return list;
        }
/*
        /// POST timesheet
        /// https://docs.bexio.com/#operation/v2CreateTimesheet
        public async Task<Timesheet> SaveTimesheetAsync(Timesheet data)
        {
            var response = await PostAsync("timesheet", ToDictionary(data));
            Timesheet sheet = JsonConvert.DeserializeObject<Timesheet>(response);
            return sheet;
        }*/

        /// GET business activites
        /// https://docs.bexio.com/#operation/v2ListBusinessActivities
        public List<BusinessActivity> GetBusinessActivities(string orderBy = "id", int offset = 0, int limit = 1000) {
            var request = new RestRequest($"client_service?order_by={orderBy}&offset={offset}&limit={limit}", Method.GET);
            IRestResponse response = ExecuteRestRequestWithBearer(request);
            List<BusinessActivity> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BusinessActivity>>(response.Content);
            return list;
        }
        




        #region Helpers

        private void AddHeaders(RestRequest request) {
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        }

        private void AddBearer(RestRequest request, string accessToken = null) {
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + accessToken);
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
