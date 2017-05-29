using System.Threading.Tasks;
using System.Web.Mvc;
using Model.Common.ViewModels;
using Service.Common;

namespace MVC.Controllers
{
    public class WarningController : Controller
    {
        protected IWarningService service;
        public WarningController(IWarningService service)
        {
            this.service = service;
        }
        public async Task<ActionResult> Index()
        {
            IWarningViewModel warning = await service.CreateWarningViewModel();

            return View(warning);
        }

    }
}
