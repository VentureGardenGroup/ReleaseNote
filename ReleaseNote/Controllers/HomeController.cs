using System.Web.Mvc;
using ReleaseNote.Repositories.API;

namespace ReleaseNote.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOctopusRepositoryReleaseNote _octopusRepository;

        public HomeController(IOctopusRepositoryReleaseNote octopusRepository)
        {
            _octopusRepository = octopusRepository;
        }

        public ActionResult Index()
        {
            var dasboard  = _octopusRepository.GetDashBoard();
            return View(dasboard);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}