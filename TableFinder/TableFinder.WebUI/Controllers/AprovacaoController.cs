using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableFinder.DataAccess;

namespace TableFinder.WebUI.Controllers
{
    public class AprovacaoController : Controller
    {
        // GET: Aprovacao
        public ActionResult Index()

        {
            var lst = new EstabelecimentoDAO().BuscarNaoAprovados();
            return View(lst);
        }

        public ActionResult Aprovar(int id_estabelecimento)
        {
            new EstabelecimentoDAO().Aprovar(id_estabelecimento);
            return RedirectToAction("Index", "Aprovacao");
        }
    }
}