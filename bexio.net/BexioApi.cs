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
using bexio.net.Models.Contacts;
using bexio.net.Models.Other.User;
using bexio.net.Models.Projects;
using bexio.net.Models.Projects.Timesheet;
using bexio.net.Responses;

namespace bexio.net
{
    public class BexioApi
    {
        // official version "2021-10-18" - see https://docs.bexio.com/#section/Changelog
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
        public async Task<List<Project>?> GetProjectsAsync(string orderBy = "id", int offset = 0, int limit = 500)
            => await GetAsync<List<Project>>("2.0/pr_project"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<Project?> CreateProjectAsync(Project project)
            => await PostAsync<Project>("2.0/pr_project", project);

        /// <summary>
        /// Searchable fields: "name", "contact_id", "pr_state_id"
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<Project>?> SearchProjectsAsync(List<SearchQuery> data,
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
        public async Task<Project?> GetProjectAsync(int projectId)
            => await GetAsync<Project>($"2.0/pr_project/{projectId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<Project?> UpdateProjectAsync(int projectId, Project project)
            => await PostAsync<Project>($"2.0/pr_project/{projectId}", project);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteProjectAsync(int projectId)
            => await DeleteAsync($"2.0/pr_project/{projectId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<bool?> ArchiveProjectAsync(int projectId)
            => (await PostAsync<BooleanResponse>($"2.0/pr_project/{projectId}/archive", null))?.Success;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<bool?> UnarchiveProjectAsync(int projectId)
            => (await PostAsync<BooleanResponse>($"2.0/pr_project/{projectId}/reactivate", null))?.Success;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetProjectStatusesAsync()
            => await GetAsync<List<SimpleDictionaryEntry>>("2.0/pr_project_state");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<string?> GetProjectStatusAsync(int projectId)
            => (await GetProjectStatusesAsync())?.Find(e => e.Id == projectId)?.Name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetProjectTypesAsync(string orderBy = "id")
            => await GetAsync<List<SimpleDictionaryEntry>>("2.0/pr_project_type"
                .AddQueryParameter("order_by", orderBy));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<string?> GetProjectType(int projectId)
            => (await GetProjectTypesAsync())?.Find(e => e.Id == projectId)?.Name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="limit">max: 2000</param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<List<Milestone>?> GetProjectMilestonesAsync(int projectId,
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
        public async Task<Milestone?> CreateMilestoneAsync(int projectId, Milestone milestone)
            => await PostAsync<Milestone>($"3.0/projects/{projectId}/milestones", milestone);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="milestoneId"></param>
        /// <returns></returns>
        public async Task<Milestone?> GetMilestoneAsync(int projectId, int milestoneId)
            => await GetAsync<Milestone>($"3.0/projects/{projectId}/milestones/{milestoneId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="milestoneId"></param>
        /// <param name="milestone"></param>
        /// <returns></returns>
        public async Task<Milestone?> UpdateMilestoneAsync(int projectId, int milestoneId, Milestone milestone)
            => await PostAsync<Milestone>($"3.0/projects/{projectId}/milestones/{milestoneId}", milestone);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="milestoneId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteMilestoneAsync(int projectId, int milestoneId)
            => await DeleteAsync($"3.0/projects/{projectId}/milestones/{milestoneId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="limit">max: 2000</param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<PaginatedList<WorkPackage>?> GetWorkPackagesAsync(int projectId,
                                                                            int limit  = 500,
                                                                            int offset = 0)
            => await GetPaginatedAsync<WorkPackage>($"3.0/projects/{projectId}/packages"
                .AddQueryParameter("limit", limit)
                .AddQueryParameter("offset", offset));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="workPackage"></param>
        /// <returns></returns>
        public async Task<WorkPackage?> CreateWorkPackageAsync(int projectId, WorkPackage workPackage)
            => await PostAsync<WorkPackage>($"3.0/projects/{projectId}/packages", workPackage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="workPackageId"></param>
        /// <returns></returns>
        public async Task<WorkPackage?> GetWorkPackageAsync(int projectId, int workPackageId)
            => await GetAsync<WorkPackage>($"3.0/projects/{projectId}/packages/{workPackageId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="workPackageId"></param>
        /// <param name="workPackage"></param>
        /// <returns></returns>
        public async Task<WorkPackage?> UpdateWorkPackageAsync(int         projectId,
                                                               int         workPackageId,
                                                               WorkPackage workPackage)
            => await PostAsync<WorkPackage>($"3.0/projects/{projectId}/packages/{workPackageId}", workPackage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="workPackageId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteWorkPackageAsync(int projectId, int workPackageId)
            => await DeleteAsync($"3.0/projects/{projectId}/packages/{workPackageId}");


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
        public async Task<List<TimesheetStatus>?> GetTimesheetStatusAsync(string orderBy = "id",
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
        public async Task<List<BusinessActivity>?> GetBusinessActivitiesAsync(
            string orderBy = "id",
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
        public async Task<BusinessActivity?> CreateBusinessActivityAsync(BusinessActivity data)
            => await PostAsync<BusinessActivity>("2.0/client_service", data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<BusinessActivity>?> SearchBusinessActivitiesAsync(List<SearchQuery> data,
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
        public async Task<List<SimpleDictionaryEntry>?> GetCommunicationTypesAsync(
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
        public async Task<List<SimpleDictionaryEntry>?> SearchCommunicationTypesAsync(List<SearchQuery> data,
                                                                                      string            orderBy = "id",
                                                                                      int               offset  = 0,
                                                                                      int               limit   = 500)
            => await PostAsync<List<SimpleDictionaryEntry>>("2.0/communication_kind/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        #endregion

        #endregion

        #region Contacts

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "nr" or "name_1" or "name_2" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<Contact>?> GetContactsAsync(int orderBy = 0, int offset = 0, int limit = 500)
            => await GetAsync<List<Contact>>("2.0/contact"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Contact?> CreateContactAsync(Contact data)
            => await PostAsync<Contact>("2.0/contact", data);

        /// <summary>
        /// possible search fields: "id", "name_1", "name_2",
        /// "nr", "address", "mail", "mail_second", "postcode",
        /// "city", "country_id", "contact_group_ids", "contact_type_id",
        /// "updated_at", "user_id", "phone_fixed", "phone_mobile", "fax"
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "nr" or "name_1" or "updated_at" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<Contact>?> SearchContactsAsync(List<SearchQuery> data,
                                                              string            orderBy = "id",
                                                              int               offset  = 0,
                                                              int               limit   = 500)
            => await PostAsync<List<Contact>>("2.0/contact/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Contact?> GetContactAsync(int id)
            => await GetAsync<Contact>("2.0/contact/" + id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Contact?> UpdateContactAsync(int id, Contact data)
            => await PostAsync<Contact>("2.0/contact/" + id, data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteContactAsync(int id)
            => await DeleteAsync("2.0/contact/" + id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<List<Contact>?> CreateContactsAsync(List<Contact> data)
            => await PostAsync<List<Contact>>("2.0/contact/_bulk_create", data);

        #region Contact relations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "contact_id" or "contact_sub_id" or "updated_at" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<ContactRelation>?> GetContactRelationsAsync(string orderBy = "id",
                                                                           int    offset  = 0,
                                                                           int    limit   = 500)
            => await GetAsync<List<ContactRelation>>("2.0/contact_relation"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ContactRelation?> CreateContactRelationAsync(ContactRelation data)
            => await PostAsync<ContactRelation>("2.0/contact_relation", data);

        /// <summary>
        /// Searchable fields: "contact_id" or "contact_sub_id" or "updated_at"
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" "contact_id" "contact_sub_id" "updated_at"</param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<List<ContactRelation>?> SearchContactRelationsAsync(List<SearchQuery> data,
                                                                              string            orderBy = "id",
                                                                              int               offset  = 0,
                                                                              int               limit   = 500)
            => await PostAsync<List<ContactRelation>>("2.0/contact_relation/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactRelationId"></param>
        /// <returns></returns>
        public async Task<ContactRelation?> GetContactRelationAsync(int contactRelationId)
            => await GetAsync<ContactRelation>("2.0/contact_relation/" + contactRelationId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactRelationId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ContactRelation?> UpdateContactRelationAsync(int contactRelationId, ContactRelation data)
            => await PostAsync<ContactRelation>("2.0/contact_relation/" + contactRelationId, data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactRelationId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteContactRelationAsync(int contactRelationId)
            => await DeleteAsync("2.0/contact_relation/" + contactRelationId);

        #endregion

        #region Contact group

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetContactGroupsAsync(string orderBy = "id",
                                                                              int    offset  = 0,
                                                                              int    limit   = 500)
            => await GetAsync<List<SimpleDictionaryEntry>>("2.0/contact_group"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<long?> CreateContactGroupAsync(string name)
            => (await PostAsync<SimpleDictionaryEntry>("2.0/contact_group", new { name }))
                ?.Id;

        /// <summary>
        /// Searchable fields: name
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> SearchContactGroupsAsync(List<SearchQuery> data,
                                                                                 string            orderBy = "id",
                                                                                 int               offset  = 0,
                                                                                 int               limit   = 500)
            => await PostAsync<List<SimpleDictionaryEntry>>("2.0/contact_group/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactGroupId"></param>
        /// <returns></returns>
        public async Task<string?> GetContactGroupAsync(int contactGroupId)
            => (await GetAsync<SimpleDictionaryEntry>("2.0/contact_group/" + contactGroupId))
                ?.Name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactGroupId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<SimpleDictionaryEntry?> UpdateContactGroupAsync(int     contactGroupId,
                                                                          string? name)
            => await PostAsync<SimpleDictionaryEntry>("2.0/contact_group/" + contactGroupId, new { name });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactGroupId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteContactGroupAsync(int contactGroupId)
            => await DeleteAsync("2.0/contact_group/" + contactGroupId);

        #endregion

        #region Contact sectors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetContactSectorsAsync(string orderBy = "id",
                                                                               int    offset  = 0,
                                                                               int    limit   = 500)
            => await GetAsync<List<SimpleDictionaryEntry>>("2.0/contact_branch"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string?> GetContactSectorAsync(int id)
            => (await SearchContactSectorsAsync(new List<SearchQuery> { new("id", id.ToString()) }))
                ?.FirstOrDefault()
                ?.Name;

        /// <summary>
        /// Searchable fields: name
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> SearchContactSectorsAsync(List<SearchQuery> data,
                                                                                  string            orderBy = "id",
                                                                                  int               offset  = 0,
                                                                                  int               limit   = 500)
            => await PostAsync<List<SimpleDictionaryEntry>>("2.0/contact_branch/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        #endregion

        #region Additional addresses

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="orderBy">"id" or "name" or "postcode" or "country_id" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<AdditionalAddress>?> GetAdditionalAddressesAsync(int    contactId,
                                                                                string orderBy = "id",
                                                                                int    offset  = 0,
                                                                                int    limit   = 500)
            => await GetAsync<List<AdditionalAddress>>("2.0/contact/" + contactId + "/additional_address"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public async Task<AdditionalAddress?> CreateAdditionalAddressAsync(AdditionalAddress data, int contactId)
            => await PostAsync<AdditionalAddress>("2.0/contact/" + contactId + "/additional_address", data);

        /// <summary>
        /// Searchable fields: name, address, postcode, city, country_id, subject, email
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name" or "postcode" or "country_id" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<AdditionalAddress>?> SearchAdditionalAddressesAsync(int               contactId,
                                                                                   List<SearchQuery> data,
                                                                                   string            orderBy = "id",
                                                                                   int               offset  = 0,
                                                                                   int               limit   = 500)
            => await PostAsync<List<AdditionalAddress>>($"2.0/contact/{contactId}/additional_address/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="additionalAddressId"></param>
        /// <returns></returns>
        public async Task<AdditionalAddress?> GetAdditionalAddressAsync(int contactId, int additionalAddressId)
            => await GetAsync<AdditionalAddress>($"2.0/contact/{contactId}/additional_address/{additionalAddressId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contactId"></param>
        /// <param name="additionalAddressId"></param>
        /// <returns></returns>
        public async Task<AdditionalAddress?> UpdateAdditionalAddressAsync(AdditionalAddress data,
                                                                           int               contactId,
                                                                           int               additionalAddressId)
            => await PostAsync<AdditionalAddress>($"2.0/contact/{contactId}/additional_address/{additionalAddressId}",
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="additionalAddressId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteAdditionalAddressAsync(int contactId, int additionalAddressId)
            => await DeleteAsync($"2.0/contact/{contactId}/additional_address/{additionalAddressId}");

        #endregion

        #region Salutations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetSalutationsAsync(int offset = 0, int limit = 500)
            => await GetAsync<List<SimpleDictionaryEntry>>("2.0/salutation"
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<SimpleDictionaryEntry?> CreateSalutationAsync(string name)
            => await PostAsync<SimpleDictionaryEntry>("2.0/salutation", new { name });

        /// <summary>
        /// Searchable fields: name
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> SearchSalutationsAsync(List<SearchQuery> data,
                                                                               int               offset = 0,
                                                                               int               limit  = 500)
            => await PostAsync<List<SimpleDictionaryEntry>>("2.0/salutation/search"
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salutationId"></param>
        /// <returns></returns>
        public async Task<SimpleDictionaryEntry?> GetSalutationAsync(int salutationId)
            => await GetAsync<SimpleDictionaryEntry>($"2.0/salutation/{salutationId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salutationId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<SimpleDictionaryEntry?> UpdateSalutationAsync(int salutationId, string name)
            => await PostAsync<SimpleDictionaryEntry>($"2.0/salutation/{salutationId}", new { name });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salutationId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteSalutationAsync(int salutationId)
            => await DeleteAsync($"2.0/salutation/{salutationId}");

        #endregion

        #region Titles

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy">"id" or "name"</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetTitlesAsync(string orderBy = "id",
                                                                       int    offset  = 0,
                                                                       int    limit   = 500)
            => await GetAsync<List<SimpleDictionaryEntry>>("2.0/title"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<SimpleDictionaryEntry?> CreateTitleAsync(string name)
            => await PostAsync<SimpleDictionaryEntry>("2.0/title", new { name });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name"</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> SearchTitlesAsync(List<SearchQuery> data,
                                                                          string            orderBy = "id",
                                                                          int               offset  = 0,
                                                                          int               limit   = 500)
            => await PostAsync<List<SimpleDictionaryEntry>>("2.0/title/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titleId"></param>
        /// <returns></returns>
        public async Task<SimpleDictionaryEntry?> GetTitleAsync(int titleId)
            => await GetAsync<SimpleDictionaryEntry>($"2.0/title/{titleId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titleId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<SimpleDictionaryEntry?> UpdateTitleAsync(int titleId, string name)
            => await PostAsync<SimpleDictionaryEntry>($"2.0/title/{titleId}", new { name });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titleId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteTitleAsync(int titleId)
            => await DeleteAsync($"2.0/title/{titleId}");

        #endregion

        #endregion

        #region Sales Order Management

        // TODO

        // Fetch a list of quotes
        // Create quote
        // Search quotes
        // Fetch a quote
        // Edit a quote
        // Delete a quote
        // Issue a quote
        // Revert issue a quote
        // Accept a quote
        // Decline a quote
        // Reissue a quote
        // Mark quote as sent
        // Show PDF
        // Send a quote
        // Copy a quote
        // Create order from quote
        // Create invoice from quote

        // Fetch a list of orders
        // Create order
        // Search orders
        // Fetch an order
        // Edit an order
        // Delete an order
        // Create delivery from order
        // Create invoice from order
        // Show PDF
        // Show repetition
        // Edit a repetition
        // Delete a repetition

        // Fetch a list of deliveries
        // Fetch a delivery
        // Issue a delivery

        // Fetch a list of invoices
        // Create invoice
        // Search invoices
        // Fetch an invoice
        // Edit an invoice
        // Delete an invoice
        // Show PDF
        // Copy a invoice
        // Issue an invoice
        // Sets issued invoice to draft
        // Cancel an invoice
        // Mark invoice as sent
        // Send an invoice
        // Fetch a list of payments
        // Create payment
        // Fetch a payment
        // Delete a payment
        // Fetch a list of reminders
        // Create reminder
        // Search invoice reminders
        // Fetch a reminder
        // Delete a reminder
        // Mark reminder as sent
        // Mark reminder as unsent
        // Send a reminder
        // Show reminder PDF

        // Fetch a list of document settings

        // Fetch a list of comments
        // Create a comment
        // Fetch a comment

        // Fetch a list of default positions
        // Create a default position
        // Fetch a default position
        // Edit a default position
        // Delete a default position

        // Fetch a list of item positions
        // Create an item position
        // Fetch an item position
        // Edit an item position
        // Delete a item position

        // Fetch a list of text positions
        // Create a text position
        // Fetch a text position
        // Edit a text position
        // Delete a text position

        // Fetch a list of subtotal positions
        // Create a subtotal position
        // Fetch a subtotal position
        // Edit a subtotal position
        // Delete a subtotal position

        // Fetch a list of discount positions
        // Create a discount position
        // Fetch a discount position
        // Edit a discount position
        // Delete a discount position

        // Fetch a list of pagebreak positions
        // Create a pagebreak position
        // Fetch a pagebreak position
        // Edit a pagebreak position
        // Delete a pagebreak position

        // Fetch a list of sub positions
        // Create a sub position
        // Fetch a sub position
        // Edit a sub position
        // Delete a sub position

        // List document templates

        #endregion

        #region Purchase

        // TODO

        // Get Bills
        // Create new Bill
        // Get Bill
        // Update Bill
        // Delete Bill
        // Update Bill status
        // Execute Bill action
        // Validate whether document number is available or not

        // Get Expenses
        // Create new Expense
        // Get Expense
        // Update Expense
        // Delete Expense
        // Update Expense status
        // Execute Expense action
        // Validate whether document number is available or not

        // Fetch a list of purchase orders
        // Create a purchase order
        // Fetch a single purchase order
        // Update a single purchase order
        // Delete a purchase order

        // Retrieve Outgoing Payments
        // Create new Outgoing Payment
        // Get Outgoing Payment
        // Delete Outgoing Payment
        // Retrieve Outgoing Payments
        // Create new Outgoing Payment
        // Get Outgoing Payment
        // Delete Outgoing Payment

        #endregion

        #region Accounting

        // TODO

        // Fetch a list of accounts
        // Search Accounts

        // Fetch a list of account groups

        // Fetch a list of calendar years
        // Create calendar year.
        // Search calendar years
        // Fetch a calendar year

        // Fetch a list of business years
        // Fetch a business year

        // Fetch a list of currencies
        // Create a currency
        // Fetch a currency
        // Delete a currency
        // Update a currency
        // Fetch exchange rates for currencies
        // Fetch all possible currency codes

        // Fetch a list of manual entries
        // Create manual entry
        // Get next reference number
        // Fetch files of accounting entry line
        // Add file to accounting entry line
        // Fetch file of accounting entry line
        // Delete file of accounting entry line

        // Journal

        // Fetch a list of taxes
        // Fetch a tax
        // Delete a tax

        // Fetch a list of vat periods
        // Fetch a vat period

        #endregion

        #region Banking

        // TODO

        // Fetch a list of bank accounts
        // Fetch a single bank account

        // Create IBAN payment
        // Get IBAN payment
        // Update IBAN payment

        // Create ISR payment
        // Get ISR payment
        // Update ISR payment

        // Create IS payment
        // Get IS payment
        // Update IS payment

        // Create QR payment
        // Get QR payment
        // Update QR payment

        // Fetch a list of payments
        // Cancel a payment
        // Delete a payment

        #endregion

        #region Items and Products

        // TODO

        // Fetch a list of items
        // Create item
        // Search items
        // Fetch an item
        // Edit an item
        // Delete an item

        // Fetch a list of stock locations
        // Search stock locations

        // Fetch a list of stock areas
        // Search stock areas

        #endregion


        #region Files

        // TODO

        // Fetch a list of files
        // Create new file
        // Search files
        // Get single file
        // Delete a existing file
        // Update existing file
        // Download file
        // Get file preview
        // Show file usage

        #endregion


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

        #region Users

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<PaginatedList<User>?> GetUsersAsync(int offset = 0, int limit = 500)
            => await GetPaginatedAsync<User>("3.0/users"
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<User?> GetUserAsync(int userId)
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

        #endregion


        #region Internal Methods

        internal async Task<TResponse?> GetAsync<TResponse>(string url)
            where TResponse : class
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get, RequestUri = new Uri(JoinUriSegments(_url, url)),
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
                Method = HttpMethod.Delete, RequestUri = new Uri(JoinUriSegments(_url, url)),
            };

            return (await ExecuteRequestInternal<BooleanResponse>(httpRequestMessage))?.Success == true;
        }


        internal async Task<PaginatedList<TResponse>?> GetPaginatedAsync<TResponse>(string url)
            where TResponse : class
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get, RequestUri = new Uri(JoinUriSegments(_url, url)),
            };
            var response = await ExecuteHttpRequest(httpRequestMessage);
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

        private async Task<TResponse?> ExecuteRequestInternal<TResponse>(HttpRequestMessage request)
            where TResponse : class
        {
            try
            {
                var httpResponse = await ExecuteHttpRequest(request);
                if (httpResponse == null)
                    return null;

                string responseContentString = await httpResponse.Content.ReadAsStringAsync();

#if DEBUG
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
            catch (Exception ex)
            {
                // TODO Log exception
                Console.WriteLine(ex);
                return null;
            }
        }

        private async Task<HttpResponseMessage?> ExecuteHttpRequest(HttpRequestMessage request)
        {
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "Bearer " + _apiToken);
            request.Headers.Add(HttpRequestHeader.Accept.ToString(), "application/json");

            var httpResponse = await _httpClient.SendAsync(request);

            if (httpResponse.StatusCode != HttpStatusCode.OK &&
                httpResponse.StatusCode != HttpStatusCode.Created &&
                httpResponse.StatusCode != HttpStatusCode.NotModified)
            {
                // TODO: log to ILogger
                Console.WriteLine("### Error: " + (int)httpResponse.StatusCode);
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
