using AppUsuario.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppUsuario.Controllers
{
    public class CuentasController : Controller
    {
        // GET: CuentasController
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Registro()
        {
            RegistroVM registroVM = new RegistroVM();
            return View(registroVM);
        }
        // GET: CuentasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CuentasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CuentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: CuentasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CuentasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: CuentasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CuentasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
