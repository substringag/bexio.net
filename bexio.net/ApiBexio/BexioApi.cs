using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using bexio.net.Converter;
using bexio.net.Exceptions;
using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Responses;

namespace bexio.net.ApiBexio
{
    /* TODO
     * 
     * Some DateTimes (those which only represent Dates) are not serialized correctly (see Creating/Editing)
     *   they should serialize as string "yyyy-MM-dd"
     */

    public class BexioApi
    {
        // official version "2024-10-22" - see https://docs.bexio.com/#section/Changelog
        private const string VERSION = "3.0.0";

        private readonly string                  _url;
        private readonly string                  _apiToken;
        private readonly UnsuccessfulReturnStyle _unsuccessfulReturnStyle;
        private readonly JsonSerializerOptions   _serializeOptions;
        private readonly HttpClient              _httpClient;

        private Encoding Encoding { get; set; } = Encoding.UTF8;

        public Contact.ContactApi Contact { get; }
        public Project.ProjectApi Project { get; }
        public UsersApi Users { get; }
        public ItemAndProductApi ItemAndProduct { get; }
        public SaleOrderManagementApi SaleOrderManagement { get; }

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

            Contact                  = new Contact.ContactApi(this);
            Project                  = new Project.ProjectApi(this);
            Users                    = new UsersApi(this);
            ItemAndProduct           = new ItemAndProductApi(this);
            SaleOrderManagement      = new SaleOrderManagementApi(this);

            _serializeOptions = new JsonSerializerOptions
            {
                // PropertyNamingPolicy allows us to name CamelCase in C# Models, but the json will have snake_case
                PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
                // We don't want to include useless spaces in the json
                WriteIndented = false,
                // DefaultIgnoreCondition omits properties with 'default' value, which is useful for Create-APIs
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            _serializeOptions.Converters.Add(new DateTimeParseConverter());
            // The universal converter, that can cast any type to string. Not in use, in favor of 'Number'
            // _serializeOptions.Converters.Add(new AnyToStringConverter());
            _serializeOptions.Converters.Add(new NullableDecimalConverter());
            _serializeOptions.Converters.Add(new InvoicePositionsConverter());
            _serializeOptions.Converters.Add(new OrderRepetitionConverter());
        }

        #region Other Endpoints

        // TODO

        // Fetch a list of company profiles
        // Show company profile

        // Fetch a list of countries
        // Create country
        // Search countries
        // Fetch a country
        // Edit a country
        // Delete a country

        // Fetch a list of languages
        // Search languages

        // Fetch a list of notes
        // Create note
        // Search notes
        // Fetch a note
        // Edit a note
        // Delete a note

        // Fetch a list of payment types
        // Search payment types

        // Get access information of logged in user

        // Fetch a list of tasks
        // Create task
        // Search tasks
        // Fetch a task
        // Edit a task
        // Delete a task
        // Task priorities
        // Task status

        // Fetch a list of units
        // Create unit
        // Search units
        // Fetch a unit
        // Edit a unit
        // Delete a unit

        #endregion
        

        #region Internal Methods

        internal async Task<TResponse?> GetAsync<TResponse>(string url, [CallerMemberName] string callerName = "")
            where TResponse : class
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method     = HttpMethod.Get,
                RequestUri = new Uri(JoinUriSegments(_url, url)),
            };
            return await ExecuteRequestInternal<TResponse>(httpRequestMessage, callerName);
        }


        internal async Task<TResponse?> PostAsync<TResponse>(string url, object? payload)
            where TResponse : class
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method     = HttpMethod.Post,
                RequestUri = new Uri(JoinUriSegments(_url, url)),
                Content = payload == null
                    ? null
                    : new StringContent(
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

        internal async Task<bool?> PostActionAsync(string url, object? payload = null)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method     = HttpMethod.Post,
                RequestUri = new Uri(JoinUriSegments(_url, url)),
                Content = payload == null
                    ? null
                    : new StringContent(
                        JsonSerializer.Serialize(payload, _serializeOptions),
                        Encoding,
                        "application/json")
            };

            return (await ExecuteRequestInternal<BooleanResponse>(httpRequestMessage))?.Success == true;
        }

        internal async Task<PaginatedList<TResponse>?> GetPaginatedAsync<TResponse>(string url)
            where TResponse : class
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method     = HttpMethod.Get,
                RequestUri = new Uri(JoinUriSegments(_url, url)),
            };
            HttpResponseMessage? response = await ExecuteHttpRequest(httpRequestMessage);
            if (response == null)
                return null;

            string responseContentString = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseContentString))
            {
                if (_unsuccessfulReturnStyle == UnsuccessfulReturnStyle.Throw)
                    throw new UnsuccessfulException((int)response.StatusCode, "Empty response");
                return null;
            }

            var list = JsonSerializer.Deserialize<List<TResponse>>(responseContentString, _serializeOptions);
            if (list == null)
            {
                // can not serialize the response
                if (_unsuccessfulReturnStyle == UnsuccessfulReturnStyle.Throw)
                    throw new UnsuccessfulException((int)response.StatusCode, "Response is an invalid format");
                return null;
            }

            // read headers
            if (int.TryParse(response.Headers.GetValues("X-Limit").FirstOrDefault(), out int limit) &&
                int.TryParse(response.Headers.GetValues("X-Offset").FirstOrDefault(), out int offset) &&
                int.TryParse(response.Headers.GetValues("X-Total-Count").FirstOrDefault(), out int totalCount))
            {
                return new PaginatedList<TResponse>(list, limit, offset, totalCount);
            }

            // not a paginated response. Return simple list
            return new PaginatedList<TResponse>(list);
        }

        private async Task<TResponse?> ExecuteRequestInternal<TResponse>(HttpRequestMessage request, string callerName = "")
            where TResponse : class
        {
            try
            {
                HttpResponseMessage? httpResponse = await ExecuteHttpRequest(request);
                if (httpResponse == null)
                    return null;

                string responseContentString = await httpResponse.Content.ReadAsStringAsync();

#if DEBUG
                // Save response to file with method name
                string rootPath = Path.Combine(Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName, $"Responses/{callerName}.txt");
                await File.WriteAllTextAsync(rootPath, responseContentString);
                
                Console.WriteLine("### Response-Content: " + responseContentString);
                Console.WriteLine("### ---");
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
            catch (Exception exception)
            {
                // TODO: log to ILogger
                Console.WriteLine(exception);
                return null;
            }
        }

        private async Task<HttpResponseMessage?> ExecuteHttpRequest(HttpRequestMessage request)
        {
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "Bearer " + _apiToken);
            request.Headers.Add(HttpRequestHeader.Accept.ToString(), "application/json");

            HttpResponseMessage httpResponse = await _httpClient.SendAsync(request);

            if (httpResponse.StatusCode != HttpStatusCode.OK &&
                httpResponse.StatusCode != HttpStatusCode.Created &&
                httpResponse.StatusCode != HttpStatusCode.NotModified)
            {
                // TODO: log to ILogger
                Console.WriteLine("### Error: " + (int)httpResponse.StatusCode);
                Console.WriteLine("### Content: " + httpResponse.Content);
                Console.WriteLine("### Headers: " + httpResponse.Headers);
                Console.WriteLine("### RequestMessage: " + httpResponse.RequestMessage);
                Console.WriteLine("### request: " + request);
                Console.WriteLine("### RequestUri: " + request.RequestUri);
                Console.WriteLine("### Headers: " + request.Headers);
                Console.WriteLine("### Content: " + request.Content);

                Console.WriteLine(await httpResponse.Content.ReadAsStringAsync());
                if (_unsuccessfulReturnStyle == UnsuccessfulReturnStyle.Throw)
                    throw new UnsuccessfulException((int)httpResponse.StatusCode);
                return null;
            }

            return httpResponse;
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
