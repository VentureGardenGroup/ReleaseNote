using System.Web.Mvc;
using ReleaseNote.Repositories.API;

namespace ReleaseNote.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IOctopusRepositoryReleaseNote _octopusRepository;
        private readonly IJiraRepository _jiraRepository;
       public ProjectController(IOctopusRepositoryReleaseNote octopusRepositoryReleaseNote, IJiraRepository jiraRepository)
        {
            _octopusRepository = octopusRepositoryReleaseNote;
           _jiraRepository = jiraRepository;
        }
        public ActionResult Index(string id)
        {
            var project = _octopusRepository.GetProject(id);
            
            return View(project);
        }

        public ActionResult Releases(string id)
        {
            var releases = _octopusRepository.GetReleases(id);
            return Json(new { releases = releases }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Jira()
        {
            var projects = _jiraRepository.GetAllProjects();
            return  Json(new {projects = projects},JsonRequestBehavior.AllowGet);
        }

        public ActionResult Octopus()
        {
            var projects = _octopusRepository.GetDashBoard();
            return Json(new { projects = projects }, JsonRequestBehavior.AllowGet);
        }

    }
}