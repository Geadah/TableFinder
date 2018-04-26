using System.Web.Mvc;
using System.Web.Script.Serialization;
using TableFinder.DataAccess;
using TableFinder.Models;

namespace TableFinder.WebUI.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Entrar(Cadastro obj)
        {
            var cadastro = new CadastroDAO().Logar(obj);

            if (cadastro == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var userData = new JavaScriptSerializer().Serialize(cadastro);
            FormsAuthenticationUtil.SetCustomAuthCookie(cadastro.Email, userData, false);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            FormsAuthenticationUtil.SignOut();

            return RedirectToAction("Index", "Login");
        }


    }
}