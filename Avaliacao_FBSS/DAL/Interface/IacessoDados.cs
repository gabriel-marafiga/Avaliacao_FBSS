using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avaliacao_FBSS.DAL.Interface
{
    public interface IacessoDados<T> where T:new ()
    {
        T Get<k>(k id);
        List<T> Get();
        void Insert(T obj);
        void Update<k>(k id, T obj);
        void Delete<k>(k id);
    }
}