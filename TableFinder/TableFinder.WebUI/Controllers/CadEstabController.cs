using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableFinder.DataAccess;
using TableFinder.Models;

namespace TableFinder.WebUI.Controllers
{
    [Authorize]
    public class CadEstabController : Controller
    {
        // GET: CadEstab
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadEstab()
        {
            return View();
        }

        public ActionResult Salvar(Estabelecimento obj)
        {
            new EstabelecimentoDAO().Inserir(obj);

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public JsonResult Upload()
        {
            try
            {
                if (!Directory.Exists(Server.MapPath("~/Images")))
                    Directory.CreateDirectory(Server.MapPath("~/Images"));

                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase f = Request.Files[fileName];
                    string savedFileName = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(f.FileName));
                    FileInfo fi = new FileInfo(savedFileName);
                    f.SaveAs(savedFileName);
                    return Json(fi.Name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(null);
        }
    }
}