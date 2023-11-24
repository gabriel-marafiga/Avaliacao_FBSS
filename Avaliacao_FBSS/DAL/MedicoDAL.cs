using Avaliacao_FBSS.DAL.Interface;
using Avaliacao_FBSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avaliacao_FBSS.DAL
{
    public class MedicoDAL : IacessoDados<Medico>
    {
        private static MedicoDAL _instance;
        public static MedicoDAL Instance
        {
            get { 
                if (_instance == null)
                {
                    _instance = new MedicoDAL();
                }
                return _instance; 
            }
        }

        public void Delete<k>(k id)
        {
            Contexto.ExecutarQuery("DELETE FROM medicos WHERE cpf=" + id);
        }

        public Medico Get<k>(k id)
        {
            var query = "select  cpf, nome, crm, id_especialidade, descricao as especialidade from medicos m inner join especialidade e on m.id_especialidade = e.id WHERE cpf = " + id;
            return Contexto.ExecutarQueryDapper<Medico>(query);
        }


        public List<Medico> Get()
        {
            var ret = Contexto.ExecutarQueryDapperList<Medico>("SELECT cpf, nome, crm, id_especialidade, descricao as especialidade from medicos m inner join especialidade e on m.id_especialidade = e.id");
            return ret;
        }

        public void Insert(Medico obj)
        {
            Contexto.ExecutarQuery(string.Format("INSERT INTO medicos (cpf, nome,   crm,id_especialidade) VALUES({0}, {1}, '{2}', '{3}')",obj.cpf, obj.nome, obj.crm, obj.id_especialidade));
        }

        public void Update<k>(k id, Medico obj)
        {
            var q = string.Format("UPDATE medicos SET id_especialidade={0}, nome='{1}', crm='{2}' WHERE cpf={3}", obj.id_especialidade, obj.nome, obj.crm, id);
            Contexto.ExecutarQuery(q);
        }


    }
}