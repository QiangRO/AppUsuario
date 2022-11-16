using AppUsuario.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppUsuario.Controllers
{
    public class CuentasController : Controller
    {
        //NICE
        //Inyeccion de dependencias
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public CuentasController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        //NICE
        [HttpGet]
        public async Task<IActionResult> Registro()
        {
            RegistroVM registroVM = new RegistroVM();
            return View(registroVM);
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
                    Url = registroVM.Url,
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
        //NICE
        private void ValidarErrores(IdentityResult resultado)
        {
            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }
        }
        //NICE
        [HttpGet]
        public IActionResult Acceso()
        {
            return View();
        }
        //NICE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Acceso(AccesoVM accesoVM)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(accesoVM.Email,
                    accesoVM.Password,
                    accesoVM.RememberMe,
                    lockoutOnFailure: true);
                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (resultado.IsLockedOut)
                {
                    return View("Bloqueada");
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

        //NICE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalirAplicacion()
        {
            //SALE DE LA APLICACION Y DESTRUYE LAS COOKIES
            await _signInManager.SignOutAsync();
            //REDIRECCIONA A LA PAGINA DE INICIO
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [HttpGet]
        public IActionResult OlvidoContraseña()
        {
            return View();
        }
        [HttpGet]
        public IActionResult OlvidoPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OlvidoPassword(OlvidoPasswordVM olvidoPasswordVM)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByEmailAsync(olvidoPasswordVM.Email);
                if (usuario != null)
                {
                    return RedirectToAction("ConfirmarOlvidoPassword");
                }
                var codigo = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                var urlRetorno = Url.Action("ResetPassword", "Cuentas",
                    new { userId = usuario.Id, code = codigo }, protocol: HttpContext.Request.Scheme);

                await _emailSender.SendEmailAsync(olvidoPasswordVM.Email, "Resetear contraseña - Proyecto Identity",
                    "Por favor recupere su contraseña haciendo click en el siguiente enlace: <a href=\"" + urlRetorno + "\">link</a>");
                return RedirectToAction("ConfirmacionOlvidoPassword");
            }
            return View(olvidoPasswordVM);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ConfirmacionOlvidoPassword()
        {
            return View();
        }
    }
}