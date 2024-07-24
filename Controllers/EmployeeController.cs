using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMC_Project.Controllers
{
    public class EmployeeController : Controller
    { 
        public ActionResult GetByID()
        {
            return View();
        }
        
        public ActionResult GetAll(int id)
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
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

        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
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
        
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
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
