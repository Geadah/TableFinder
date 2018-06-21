using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableFinder.DataAccess;
using TableFinder.Models;

namespace TableFinder.WebUI.Controllers
{
    public class CardapioController : Controller
    {
        public ActionResult Index(int estabelecimento)
        {
            var lst = new CardapioDAO().BuscarPorEstab(estabelecimento);
            return View(lst);
        }
    }

   
}