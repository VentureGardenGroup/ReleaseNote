using System.Web.Mvc;
using ReleaseNote.Repositories.API;
using ReleaseNote.ViewModels;

namespace ReleaseNote.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IOctopusRepositoryReleaseNote _octopusRepository;

       public ProjectController(IOctopusRepositoryReleaseNote octopusRepositoryReleaseNote)
        {
            _octopusRepository = octopusRepositoryReleaseNote;
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
    }
}