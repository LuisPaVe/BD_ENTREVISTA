using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Entidad
{
    public class E_MovInventario
    {
        [Required(ErrorMessage = "La compañía es obligatoria")]
        [StringLength(5)]
        [Display(Name = "Código Compañía")]
        public string COD_CIA { get; set; }

        [Required(ErrorMessage = "Compañía venta es obligatoria")]
        [StringLength(5)]
        public string COMPANIA_VENTA_3 { get; set; }

        [Required]
        [StringLength(10)]
        public string ALMACEN_VENTA { get; set; }

        [Required]
        [StringLength(2)]
        public string TIPO_MOVIMIENTO { get; set; }

        [Required]
        [StringLength(2)]
        public string TIPO_DOCUMENTO { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Número Documento")]
        public string NRO_DOCUMENTO { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Código Item")]
        public string COD_ITEM_2 { get; set; }

        [StringLength(100)]
        public string PROVEEDOR { get; set; }

        [StringLength(50)]
        public string ALMACEN_DESTINO { get; set; }

        [Range(1, 1000000, ErrorMessage = "Cantidad debe ser mayor a 0")]
        public int? CANTIDAD { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha Transacción")]
        public DateTime? FECHA_TRANSACCION { get; set; }

        public string DOC_REF_1 { get; set; }
        public string DOC_REF_2 { get; set; }
        public string DOC_REF_3 { get; set; }
        public string DOC_REF_4 { get; set; }
        public string DOC_REF_5 { get; set; }
        //public string IdSeguro { get; set; }
    }
}
