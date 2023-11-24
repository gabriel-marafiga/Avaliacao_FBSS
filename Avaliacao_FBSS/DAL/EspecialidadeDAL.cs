using Avaliacao_FBSS.DAL.Interface;
using Avaliacao_FBSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avaliacao_FBSS.DAL
{
    public class EspecialidadeDAL : IacessoDados<Especialidade>
    {
        private static EspecialidadeDAL _instance;

        public static EspecialidadeDAL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EspecialidadeDAL();
                }
                return _instance;
            }
        }
        public void Delete<k>(k id)
        {

            Contexto.ExecutarQuery("DELETE FROM especialidade WHERE id=" + id);
        }

        public Especialidade Get<k>(k id)
        {
            return Contexto.ExecutarQueryDapper<Especialidade>("SELECT id, descricao FROM especialidade WHERE id = " + id);
        }

        public List<Especialidade> Get()
        {
            return Contexto.ExecutarQueryDapperList<Especialidade>("SELECT id, descricao FROM especialidade");
        }

        public void Insert(Especialidade obj)
        {
            Contexto.ExecutarQuery(String.Format("INSERT INTO especialidade (descricao) VALUES('{0}')", obj.Descricao));
        }

        public void Update<k>(k id, Especialidade obj)
        {
            Contexto.ExecutarQuery(string.Format("UPDATE especialidade SET descricao='{0}' WHERE id={1}", obj.Descricao, id));
        }
    }
}