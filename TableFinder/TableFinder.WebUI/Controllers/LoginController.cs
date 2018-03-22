using System.Web.Mvc;
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

        public ActionResult Logar(Cadastro obj)
        {
            var cadastro = new CadastroDAO().Logar(obj);

            if (cadastro == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return RedirectToAction("Index", "Navegacao");
        }
    }
}