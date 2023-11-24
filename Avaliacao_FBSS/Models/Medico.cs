using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avaliacao_FBSS.Models
{
    public class Medico
    {
        public string cpf { get; set; }
        public string nome { get; set; }
        public string  crm { get; set; }
        public int id_especialidade { get; set; }
        public string especialidade { get; set; }
    }
}