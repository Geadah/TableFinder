﻿using System;
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EstabList()
        {
            var usuario = new Cadastro() { Id = ((Cadastro)User).Id };
            var lst = new EstabelecimentoDAO().BuscarPorDono(usuario.Id);
            return View(lst);
        }



        public ActionResult Salvar(Estabelecimento obj)
        {
            obj.Usuario = new Cadastro() { Id = ((Cadastro)User).Id };

            if (obj != null && obj.Id > 0)
                new EstabelecimentoDAO().Atualizar(obj);
            else
                new EstabelecimentoDAO().Inserir(obj);

            return RedirectToAction("Index", "Home");
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

        public ActionResult Editar(int id)
        {
            var obj = new EstabelecimentoDAO().BuscarPorId(id);
            return View("Index", obj);


        }

        public ActionResult Excluir(int id)
        {
            new EstabelecimentoDAO().Excluir(id);
            return RedirectToAction("EstabList", "CadEstab");
        }


    }
}