using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Entidad
{
    public class MovInventarioDTO
    {
        public string COD_CIA { get; set; }
        public string COMPANIA_VENTA_3 { get; set; }
        public string ALMACEN_VENTA { get; set; }
        public string TIPO_MOVIMIENTO { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public string NRO_DOCUMENTO { get; set; }
        public string COD_ITEM_2 { get; set; }
        public int? CANTIDAD { get; set; }
        public DateTime? FECHA_TRANSACCION { get; set; }
    }
}
