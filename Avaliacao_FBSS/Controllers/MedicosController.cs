using Avaliacao_FBSS.DAL;
using Avaliacao_FBSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Avaliacao_FBSS.Controllers
{
    public class MedicosController : Controller
    {
        public ActionResult Index()
        {
            return View(MedicoDAL.Instance.Get());
        }

        [HttpGet]
        public ActionResult ListALL()
        {
            return Json(MedicoDAL.Instance.Get(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Medicos/List/{cpf}")]
        public ActionResult List(string cpf)
        {
            return Json(MedicoDAL.Instance.Get(cpf), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Includ(Medico medico)
        {
            MedicoDAL.Instance.Insert(medico);
            return Json(medico);
        }

        [HttpPut]
        public ActionResult Update(Medico medico)
        {
            MedicoDAL.Instance.Update(medico.cpf, medico);
            return Json(medico);
        }

        [HttpGet]
        [Route("Medicos/Delete/{cpf}")]
        public ActionResult Delete(string cpf)
        {
            if (MedicoDAL.Instance.Get(cpf) != null)
            {
                MedicoDAL.Instance.Delete(cpf);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(new { mensagem = "Medico não encontrado"}, JsonRequestBehavior.AllowGet);
        }

    }
}