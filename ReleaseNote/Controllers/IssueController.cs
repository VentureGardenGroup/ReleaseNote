using System.Linq;
using System.Web.Mvc;
using ReleaseNote.Models;
using ReleaseNote.Repositories.API;
using ReleaseNote.Repositories.Data;

namespace ReleaseNote.Controllers
{
    public class IssueController : Controller
    {
        private readonly IJiraRepository _jiraRepository;
        private readonly IRepository<Project> _projectRepository;
        public IssueController(IJiraRepository jiraRepository, IRepository<Project> repository )
        {
            _jiraRepository = jiraRepository;
            _projectRepository = repository;
        }
        //
        // GET: /Issue/
        public ActionResult Index(string id)
        {
            var project = _projectRepository.Where(x => x.Octopus == id).FirstOrDefault();
            if (project == null)
            {
                return Json(new { tickets = (Issue)null}, JsonRequestBehavior.AllowGet);
            }
            var issues = _jiraRepository.GetJiraIssues(project.Jira);
            var issuesDto = issues.issues.Select(x => new
            {
                Key = x.key, Summary = x.fields.summary, Description = x.fields.description, StatusName = x.fields.status.name,
                IssueType = x.fields.issuetype.name
            }).ToList();
            return Json(new {tickets = issuesDto}, JsonRequestBehavior.AllowGet);
        }
	}
}