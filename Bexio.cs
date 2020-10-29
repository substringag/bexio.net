using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using bexio.net.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace bexio.net
{
    public class Bexio
    {
        public string _url;
        public string _apiToken;
        public HttpClient _httpClient = new HttpClient();

        public Bexio(string url, string apiToken) {
            _url = url;
            _apiToken = apiToken;

            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _httpClient.BaseAddress = new Uri(url);

            _httpClient.DefaultRequestHeaders.Authorization = GenerateHeader();
        }

        /// GET pr_project
        /// https://docs.bexio.com/#tag/Projects
        public async Task<IEnumerable<Project>> GetProjectsAsync() {
            var response = await GetAsync("pr_project");
            IEnumerable<Project> list = JsonConvert.DeserializeObject<IEnumerable<Project>>(response);
            return list;
        }

        /// GET timesheet
        /// https://docs.bexio.com/#tag/Timesheet
        public async Task<IEnumerable<Timesheet>> GetTimesheetsAsync() {
            var response = await GetAsync("timesheet");
            IEnumerable<Timesheet> list = JsonConvert.DeserializeObject<IEnumerable<Timesheet>>(response);
            return list;
        }

        /// POST timesheet
        /// https://docs.bexio.com/#operation/v2CreateTimesheet
        public async Task<Timesheet> SaveTimesheetAsync(Timesheet data)
        {
            var response = await PostAsync("timesheet", ToDictionary(data));
            Timesheet sheet = JsonConvert.DeserializeObject<Timesheet>(response);
            return sheet;
        }

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
