using Avaliacao_FBSS.DAL;
using Avaliacao_FBSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Avaliacao_FBSS.Controllers
{
    public class EspecialidadeController : Controller
    {
        public ActionResult Index()
        {
            return View(MedicoDAL.Instance.Get());
        }

        [HttpGet]
        [Route("Especialidade/ListALL/")]
        public ActionResult ListALL()
        {
            return Json(EspecialidadeDAL.Instance.Get(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Especialidade/List/{id}")]
        public ActionResult List(int id)
        {
            return Json(EspecialidadeDAL.Instance.Get(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Includ(Especialidade especialidade)
        {
            EspecialidadeDAL.Instance.Insert(especialidade);
            return Json(especialidade);
        }

        [HttpPut]
        public ActionResult Update(Especialidade especialidade)
        {
            EspecialidadeDAL.Instance.Update(especialidade.Id, especialidade);
            return Json(especialidade);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (EspecialidadeDAL.Instance.Get(id) != null)
            {
                EspecialidadeDAL.Instance.Delete(id);
                return Json(true);
            }
            return Json(new { mensagem = "Especialidade não encontrado"});
        }

    }
}