using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableFinder.DataAccess;
using TableFinder.Models;

namespace TableFinder.WebUI.Controllers
{
    public class NavegacaoController : Controller
    {
        public ActionResult Index(int id)
        {
            var obj = new EstabelecimentoDAO().BuscarPorId(id);
            obj.Opinioes = new FeedbackDAO().BuscarPorEstabelecimento(obj.Id);
            return View(obj);
        }

        [Authorize]
        public ActionResult EnviarMsg(Feedback obj)
        {
            obj.Data_Hora = DateTime.Now;
            obj.Usuario = new Cadastro() { Id = ((Cadastro)User).Id };

            new FeedbackDAO().Inserir(obj);

            return RedirectToAction("Index", "Navegacao", new { @id = obj.Estabelecimento.Id });
        }
    }
}