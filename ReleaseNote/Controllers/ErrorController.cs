using System.Web.Mvc;

namespace ReleaseNote.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View("Error");
        }
	}
}