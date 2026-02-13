using Capa.AccesoDatos;
using Capa.Entidad;
using Capa.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace Capa.WebApiNet.Controllers
{
    public class MovInventarioApiController : ApiController
    {
        private readonly MovInventarioService _service;
        public MovInventarioApiController()
        {
            string cn = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            var repo = new MovInventarioRepository(cn);
            _service = new MovInventarioService(repo);
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var data = _service.Listar()
                .Select(x => new MovInventarioDTO
                {
                    COD_CIA = x.COD_CIA,
                    COMPANIA_VENTA_3 = x.COMPANIA_VENTA_3,
                    ALMACEN_VENTA = x.ALMACEN_VENTA,
                    TIPO_MOVIMIENTO = x.TIPO_MOVIMIENTO,
                    TIPO_DOCUMENTO = x.TIPO_DOCUMENTO,
                    NRO_DOCUMENTO = x.NRO_DOCUMENTO,
                    COD_ITEM_2 = x.COD_ITEM_2,
                    CANTIDAD = x.CANTIDAD,
                    FECHA_TRANSACCION = x.FECHA_TRANSACCION
                });

            return Ok(data);
        }

        [HttpGet]
        [Route("obtener")]
        public IHttpActionResult Get(
            string id1, string id2, string id3,
            string id4, string id5, string id6, string id7)
        {
            var entity = _service.Obtener(id1, id2, id3, id4, id5, id6, id7);

            if (entity == null)
                return NotFound();

            var dto = new MovInventarioDTO
            {
                COD_CIA = entity.COD_CIA,
                COMPANIA_VENTA_3 = entity.COMPANIA_VENTA_3,
                ALMACEN_VENTA = entity.ALMACEN_VENTA,
                TIPO_MOVIMIENTO = entity.TIPO_MOVIMIENTO,
                TIPO_DOCUMENTO = entity.TIPO_DOCUMENTO,
                NRO_DOCUMENTO = entity.NRO_DOCUMENTO,
                COD_ITEM_2 = entity.COD_ITEM_2,
                CANTIDAD = entity.CANTIDAD,
                FECHA_TRANSACCION = entity.FECHA_TRANSACCION
            };

            return Ok(dto);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(MovInventarioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = new E_MovInventario
            {
                COD_CIA = dto.COD_CIA,
                COMPANIA_VENTA_3 = dto.COMPANIA_VENTA_3,
                ALMACEN_VENTA = dto.ALMACEN_VENTA,
                TIPO_MOVIMIENTO = dto.TIPO_MOVIMIENTO,
                TIPO_DOCUMENTO = dto.TIPO_DOCUMENTO,
                NRO_DOCUMENTO = dto.NRO_DOCUMENTO,
                COD_ITEM_2 = dto.COD_ITEM_2,
                CANTIDAD = dto.CANTIDAD,
                FECHA_TRANSACCION = dto.FECHA_TRANSACCION
            };

            _service.Insertar(entity);

            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Put(MovInventarioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = new E_MovInventario
            {
                COD_CIA = dto.COD_CIA,
                COMPANIA_VENTA_3 = dto.COMPANIA_VENTA_3,
                ALMACEN_VENTA = dto.ALMACEN_VENTA,
                TIPO_MOVIMIENTO = dto.TIPO_MOVIMIENTO,
                TIPO_DOCUMENTO = dto.TIPO_DOCUMENTO,
                NRO_DOCUMENTO = dto.NRO_DOCUMENTO,
                COD_ITEM_2 = dto.COD_ITEM_2,
                CANTIDAD = dto.CANTIDAD,
                FECHA_TRANSACCION = dto.FECHA_TRANSACCION
            };

            _service.Actualizar(entity);

            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete(
            string id1, string id2, string id3,
            string id4, string id5, string id6, string id7)
        {
            var entity = new E_MovInventario
            {
                COD_CIA = id1,
                COMPANIA_VENTA_3 = id2,
                ALMACEN_VENTA = id3,
                TIPO_MOVIMIENTO = id4,
                TIPO_DOCUMENTO = id5,
                NRO_DOCUMENTO = id6,
                COD_ITEM_2 = id7
            };

            _service.Eliminar(entity);

            return Ok();
        }
    }
}