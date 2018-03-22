using System.Web.Mvc;
using TableFinder.DataAccess;
using TableFinder.Models;

namespace TableFinders.Controllers
{
    public class CadastroController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Salvar(Cadastro obj)
        {
            new CadastroDAO().Inserir(obj);

            return RedirectToAction("Index", "Home");
        }
    }
}