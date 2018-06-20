using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableFinder.DataAccess;
using TableFinder.Models;

namespace TableFinder.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var lst = new EstabelecimentoDAO().BuscarAprovados();
            ViewBag.Tipos = new TipoComidaDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult Buscar(int[] tipos)
        {
            ViewBag.Tipos = new TipoComidaDAO().BuscarTodos();
            var lst = new EstabelecimentoDAO().BuscarAprovados();
            lst.ForEach(o =>
            {
                o.Cardapio = new CardapioDAO().BuscarPorEstab(o.Id).ToList();
            });

            var resultado = new List<Estabelecimento>();
            foreach (var estab in lst)
            {
                if (estab.Cardapio.Any(c => tipos.Contains(c.Tipo.TipoId)))
                {
                    resultado.Add(estab);
                }
            }

            return View("Index", resultado);
        }
    }
}