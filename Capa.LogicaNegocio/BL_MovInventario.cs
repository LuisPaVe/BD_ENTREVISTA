using Capa.AccesoDatos;
using Capa.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Capa.LogicaNegocio.BL_MovInventario;

namespace Capa.LogicaNegocio
{
    public class BL_MovInventario
    {

        private DA_MovInventario dal = new DA_MovInventario();

        public List<E_MovInventario> Listar()
        {
            return dal.Listar();
        }

        public void Insertar(E_MovInventario obj)
        {
            if (obj.CANTIDAD <= 0)
                throw new Exception("Cantidad inválida");
            // Aquí puedes agregar validaciones
            dal.Insertar(obj);
        }

        public void Actualizar(E_MovInventario obj)
        {
            dal.Actualizar(obj);
        }

        public void Eliminar(E_MovInventario obj)
        {
            dal.Eliminar(obj);
        }
        public E_Paginado<E_MovInventario> ListarPaginado(int pageNumber, int pageSize,DateTime? fechaInicio,DateTime? fechaFin,string tipoMovimiento,string nroDocumento)
        {
            return dal.ListarPaginado(pageNumber, pageSize, fechaInicio, fechaFin, tipoMovimiento, nroDocumento);
        }
        public E_MovInventario Obtener(string id1, string id2, string id3,
                             string id4, string id5, string id6,
                             string id7)
        {
            return dal.Obtener(id1, id2, id3, id4, id5, id6, id7);
        }
    }

}
