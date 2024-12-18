using bexio.net.Helpers;
using bexio.net.Models;
using bexio.net.Models.Projects;
using bexio.net.Responses;

namespace bexio.net.ApiBexio.Project
{
	public partial class ProjectApi
	{

        private readonly BexioApi _api;

        internal ProjectApi(BexioApi api)
        {
            _api = api;
        }
        
		#region Projects

        /// <summary>
        /// https://docs.bexio.com/#tag/Projects/operation/v2ListProjects
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<Models.Projects.Project>?> GetProjectsAsync(string orderBy = "id", int offset = 0, int limit = 500)
            => await _api.GetAsync<List<Models.Projects.Project>>("2.0/pr_project"
                .AddQueryParameter("order_by", orderBy)
                .AddQueryParameter("offset", offset)
                .AddQueryParameter("limit", limit));

        /// <summary>
        /// https://docs.bexio.com/#tag/Projects/operation/v2CreateProject
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<Models.Projects.Project?> CreateProjectAsync(Models.Projects.Project project)
            => await _api.PostAsync<Models.Projects.Project>("2.0/pr_project", project);

        /// <summary>
        /// Searchable fields: "name", "contact_id", "pr_state_id"
        /// https://docs.bexio.com/#tag/Projects/operation/v2SearchProjects
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <param name="offset"></param>
        /// <param name="limit">max: 2000</param>
        /// <returns></returns>
        public async Task<List<Models.Projects.Project>?> SearchProjectsAsync(List<SearchQuery> data,
                                                              string            orderBy = "id",
                                                              int               offset  = 0,
                                                              int               limit   = 500)
            => await _api.PostAsync<List<Models.Projects.Project>>("2.0/pr_project/search"
                    .AddQueryParameter("order_by", orderBy)
                    .AddQueryParameter("offset", offset)
                    .AddQueryParameter("limit", limit),
                data);

        /// <summary>
        /// https://docs.bexio.com/#tag/Projects/operation/v2ShowProject
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<Models.Projects.Project?> GetProjectAsync(int projectId)
            => await _api.GetAsync<Models.Projects.Project>($"2.0/pr_project/{projectId}");

        /// <summary>
        /// https://docs.bexio.com/#tag/Projects/operation/v2EditProject
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="project">Payload</param>
        /// <returns></returns>
        public async Task<Models.Projects.Project?> UpdateProjectAsync(int projectId, Models.Projects.Project project)
            => await _api.PostAsync<Models.Projects.Project>($"2.0/pr_project/{projectId}", project);

        /// <summary>
        /// https://docs.bexio.com/#tag/Projects/operation/DeleteProject
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteProjectAsync(int projectId)
            => await _api.DeleteAsync($"2.0/pr_project/{projectId}");

        /// <summary>
        /// https://docs.bexio.com/#tag/Projects/operation/v2ArchiveProject
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<bool?> ArchiveProjectAsync(int projectId)
            => (await _api.PostAsync<BooleanResponse>($"2.0/pr_project/{projectId}/archive", null))?.Success;

        /// <summary>
        /// https://docs.bexio.com/#tag/Projects/operation/v2UnarchiveProject
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<bool?> UnarchiveProjectAsync(int projectId)
            => (await _api.PostAsync<BooleanResponse>($"2.0/pr_project/{projectId}/reactivate", null))?.Success;

        /// <summary>
        /// https://docs.bexio.com/#tag/Projects/operation/v2ListProjectStatus
        /// </summary>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetProjectStatusesAsync()
            => await _api.GetAsync<List<SimpleDictionaryEntry>>("2.0/pr_project_state");

        /// <summary>
        /// Custom method to get the project status by id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<string?> GetProjectStatusAsync(int projectId)
            => (await GetProjectStatusesAsync())?.Find(e => e.Id == projectId)?.Name;

        /// <summary>
        /// https://docs.bexio.com/#tag/Projects/operation/v2ListProjectType
        /// </summary>
        /// <param name="orderBy">"id" or "name" // may append _desc</param>
        /// <returns></returns>
        public async Task<List<SimpleDictionaryEntry>?> GetProjectTypesAsync(string orderBy = "id")
            => await _api.GetAsync<List<SimpleDictionaryEntry>>("2.0/pr_project_type"
                .AddQueryParameter("order_by", orderBy));

        /// <summary>
        /// Custom method to get the project type by id
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
            => await _api.GetAsync<List<Milestone>>($"3.0/projects/{projectId}/milestones"
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
            => await _api.PostAsync<Milestone>($"3.0/projects/{projectId}/milestones", milestone);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="milestoneId"></param>
        /// <returns></returns>
        public async Task<Milestone?> GetMilestoneAsync(int projectId, int milestoneId)
            => await _api.GetAsync<Milestone>($"3.0/projects/{projectId}/milestones/{milestoneId}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="milestoneId"></param>
        /// <param name="milestone"></param>
        /// <returns></returns>
        public async Task<Milestone?> UpdateMilestoneAsync(int projectId, int milestoneId, Milestone milestone)
            => await _api.PostAsync<Milestone>($"3.0/projects/{projectId}/milestones/{milestoneId}", milestone);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="milestoneId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteMilestoneAsync(int projectId, int milestoneId)
            => await _api.DeleteAsync($"3.0/projects/{projectId}/milestones/{milestoneId}");

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
            => await _api.GetPaginatedAsync<WorkPackage>($"3.0/projects/{projectId}/packages"
                .AddQueryParameter("limit", limit)
                .AddQueryParameter("offset", offset));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="workPackage"></param>
        /// <returns></returns>
        public async Task<WorkPackage?> CreateWorkPackageAsync(int projectId, WorkPackage workPackage)
            => await _api.PostAsync<WorkPackage>($"3.0/projects/{projectId}/packages", workPackage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="workPackageId"></param>
        /// <returns></returns>
        public async Task<WorkPackage?> GetWorkPackageAsync(int projectId, int workPackageId)
            => await _api.GetAsync<WorkPackage>($"3.0/projects/{projectId}/packages/{workPackageId}");

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
            => await _api.PostAsync<WorkPackage>($"3.0/projects/{projectId}/packages/{workPackageId}", workPackage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="workPackageId"></param>
        /// <returns></returns>
        public async Task<bool?> DeleteWorkPackageAsync(int projectId, int workPackageId)
            => await _api.DeleteAsync($"3.0/projects/{projectId}/packages/{workPackageId}");

		#endregion Projects
	}
}