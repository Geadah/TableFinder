using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableFinder.DataAccess;

namespace TableFinder.WebUI.Controllers
{
    public class NavegacaoController : Controller
    {
        public ActionResult Index(int id)
        {
            var obj = new EstabelecimentoDAO().BuscarPorId(id);
            return View(obj);
        }
    }
}