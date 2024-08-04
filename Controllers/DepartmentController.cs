using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMC_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : Controller
    {
        [HttpGet("{ID}")]
        public ActionResult GetByID()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create()
        {
            try
            {
                return RedirectToAction();
            }
            catch
            {
                return View();
            }
        }

        [HttpPut]
        public ActionResult Update()
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
