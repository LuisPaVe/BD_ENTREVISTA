using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.AccesoDatos
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(params object[] ids);
        void Insert(T entity);
        void Update(T entity);
        void Delete(params object[] ids);
    }
}
