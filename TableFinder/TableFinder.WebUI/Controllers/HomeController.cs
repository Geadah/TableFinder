using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableFinder.DataAccess;

namespace TableFinder.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var lst = new EstabelecimentoDAO().BuscarAprovados();
            return View(lst);
        }
    }
}