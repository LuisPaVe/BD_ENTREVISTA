using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.AccesoDatos
{
    public interface IUnitOfWork : IDisposable
    {
        IMovInventarioRepository MovInventario { get; }

        void Commit();
    }

}
