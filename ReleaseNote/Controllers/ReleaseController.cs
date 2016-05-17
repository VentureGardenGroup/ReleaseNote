using System.Linq;
using System.Web.Mvc;
using ReleaseNote.Models;
using ReleaseNote.Repositories.API;
using ReleaseNote.ViewModels;

namespace ReleaseNote.Controllers
{
    public class ReleaseController : Controller
    {
        private readonly IOctopusRepositoryReleaseNote _octopusRepository;

        public ReleaseController(IOctopusRepositoryReleaseNote octopusRepositoryReleaseNote)
        {
           _octopusRepository = octopusRepositoryReleaseNote;
        }
        public ActionResult Index(string id)
        {
            var release = _octopusRepository.GetRelease(id);

            return View(release);
        }

        [HttpPost]
        public ActionResult UpdateNote(string id, ReleaseViewModel model)
        {
            var release = _octopusRepository.GetRelease(id);
            if (ModelState.IsValid)
            {
                release = new  OctopusRelease()
                {
                    Id = model.Id,
                    Version = model.Version,
                    ProjectId = model.ProjectId,
                    ChannelId =  model.ChannelId,
                    ReleaseNotes =  string.Join(" ", model.ReleaseNotes.Split('^')),
                    SelectedPackages =  model.SelectedPackages.Split('^').Select(x=> new OctopusSelectedPackage()
                    {   
                        StepName =  x,
                        Version = model.Version
                    }).ToList()
                };
               _octopusRepository.UpdateReleaseNote(release);
            }
            return View("Index", release);
        }
    }
}