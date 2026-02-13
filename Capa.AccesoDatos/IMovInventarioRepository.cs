using Capa.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.AccesoDatos
{
    public interface IMovInventarioRepository
    {
        E_Paginado<E_MovInventario> ListarPaginado(
            int pageNumber,
            int pageSize,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            string tipoMovimiento,
            string nroDocumento);

        E_MovInventario Obtener(
            string id1, string id2, string id3,
            string id4, string id5, string id6, string id7);

        void Insertar(E_MovInventario entity);
        void Actualizar(E_MovInventario entity);
        void Eliminar(E_MovInventario entity);
        List<E_MovInventario> Listar();
    }
    
}
