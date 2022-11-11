using AppUsuario.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppUsuario.Controllers
{
    public class CuentasController : Controller
    {
        //Inyeccion de dependencias
        private readonly UserManager<IdentityUser>? _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public CuentasController(UserManager<IdentityUser>? userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroVM registroVM)
        {
            if (ModelState.IsValid)
            {
                var usuario = new UserModel
                {
                    UserName = registroVM.Email,
                    Email = registroVM.Email,
                    Nombre = registroVM.Nombre,
                    Url= registroVM.Url,
                    CodigoPais = registroVM.CodigoPais,
                    Telefono = registroVM.Telefono,
                    Pais = registroVM.Pais,
                    Ciudad = registroVM.Ciudad,
                    Direccion = registroVM.Direccion,
                    FechaNacimiento = registroVM.FechaNacimiento,
                    Estado = registroVM.Estado

                };
                var resultado = await _userManager.CreateAsync(usuario, registroVM.Password);
                if (resultado.Succeeded)
                {
                    await _signInManager.SignInAsync(usuario, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                ValidarErrores(resultado);
            }

            return View(registroVM);
        }
        private void ValidarErrores(IdentityResult resultado)
        {
            foreach(var error in resultado.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }
        }
        [HttpGet]
        public IActionResult Acceso()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Acceso(AccesoVM accesoVM)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(accesoVM.Email,
                    accesoVM.Password,
                    accesoVM.RememberMe,
                    lockoutOnFailure: false);
                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Acceso invalido");
                    return View(accesoVM);
                }
            }
            return View(accesoVM);
        }
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
        public async Task<IActionResult> SalirAplicacion()
        {
            //SALE DE LA APLICACION Y DESTRUYE LAS COOKIES
            await _signInManager.SignOutAsync();
            //REDIRECCIONA A LA PAGINA DE INICIO
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
