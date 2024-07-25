using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMC_Project.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPut]
        public ActionResult Edit()
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
        
        [HttpDelete ]
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
