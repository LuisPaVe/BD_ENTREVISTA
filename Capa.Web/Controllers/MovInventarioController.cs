using Capa.AccesoDatos;
using Capa.Entidad;
using Capa.LogicaNegocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;


namespace Capa.Web.Controllers
{

    public class MovInventarioController : Controller
    {
        //BL_MovInventario bl = new BL_MovInventario();

        private readonly MovInventarioService _service;
        private readonly IEncryptionService _encryption;
        public MovInventarioController()
        {
            string cn = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            var repo = new MovInventarioRepository(cn);
            _service = new MovInventarioService(repo);
            _encryption = new EncryptionService();
        }
        public ActionResult Index(
            int page = 1,
            DateTime? fechaInicio = null,
            DateTime? fechaFin = null,
            string tipoMovimiento = null,
            string nroDocumento = null)
        {
            int pageSize = 10;

             var result = _service.ListarPaginado(
                 page,
                 pageSize,
                 fechaInicio,
                 fechaFin,
                 tipoMovimiento,
                 nroDocumento);

            var modelo = new E_Paginado<MovInventarioViewModel>
            {
                TotalRecords = result.TotalRecords,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                Data = result.Data.Select(item => new MovInventarioViewModel
                {
                    COD_CIA = item.COD_CIA,                    
                    TIPO_DOCUMENTO = item.TIPO_DOCUMENTO,
                    NRO_DOCUMENTO = item.NRO_DOCUMENTO,
                    COD_ITEM_2 = item.COD_ITEM_2,
                    CANTIDAD = item.CANTIDAD,
                    FECHA_TRANSACCION = item.FECHA_TRANSACCION,
                    IdSeguro = GenerarIdSeguro(item)
                }).ToList()
            };

            // Guardar filtros en ViewBag para mantenerlos
            ViewBag.FechaInicio = fechaInicio;
             ViewBag.FechaFin = fechaFin;
             ViewBag.TipoMovimiento = tipoMovimiento;
             ViewBag.NroDocumento = nroDocumento;

             return View(modelo);
        }
        public ActionResult Details(string id)
        {
            //var data = _service.Listar().FirstOrDefault(x =>
            //    x.COD_CIA == id1 &&
            //    x.COMPANIA_VENTA_3 == id2 &&
            //    x.ALMACEN_VENTA == id3 &&
            //    x.TIPO_MOVIMIENTO == id4 &&
            //    x.TIPO_DOCUMENTO == id5 &&
            //    x.NRO_DOCUMENTO == id6 &&
            //    x.COD_ITEM_2 == id7);
            ////var data = bl.Obtener(id1, id2, id3, id4, id5, id6, id7);

            //if (data == null)
            //    return HttpNotFound();
            //return View(data);
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(400);

            string decrypted;

            try
            {
                decrypted = _encryption.Decrypt(id);
            }
            catch
            {
                return new HttpStatusCodeResult(400);
            }

            var keys = decrypted.Split('|');

            if (keys.Length != 7)
                return new HttpStatusCodeResult(400);

            var data = _service.Listar().FirstOrDefault(x =>
                x.COD_CIA == keys[0] &&
                x.COMPANIA_VENTA_3 == keys[1] &&
                x.ALMACEN_VENTA == keys[2] &&
                x.TIPO_MOVIMIENTO == keys[3] &&
                x.TIPO_DOCUMENTO == keys[4] &&
                x.NRO_DOCUMENTO == keys[5] &&
                x.COD_ITEM_2 == keys[6]);

            if (data == null)
                return HttpNotFound();

            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(E_MovInventario obj)
        {
            if (!ModelState.IsValid)
                return View(obj);

            _service.Insertar(obj);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string id)
        {
            //var data = _service.Listar().FirstOrDefault(x =>
            //    x.COD_CIA == id1 &&
            //    x.COMPANIA_VENTA_3 == id2 &&
            //    x.ALMACEN_VENTA == id3 &&
            //    x.TIPO_MOVIMIENTO == id4 &&
            //    x.TIPO_DOCUMENTO == id5 &&
            //    x.NRO_DOCUMENTO == id6 &&
            //    x.COD_ITEM_2 == id7);

            //return View(data);
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(400);

            string decrypted = _encryption.Decrypt(id);
            var keys = decrypted.Split('|');

            var data = _service.Listar().FirstOrDefault(x =>
                x.COD_CIA == keys[0] &&
                x.COMPANIA_VENTA_3 == keys[1] &&
                x.ALMACEN_VENTA == keys[2] &&
                x.TIPO_MOVIMIENTO == keys[3] &&
                x.TIPO_DOCUMENTO == keys[4] &&
                x.NRO_DOCUMENTO == keys[5] &&
                x.COD_ITEM_2 == keys[6]);

            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(E_MovInventario obj)
        {
            if (ModelState.IsValid)
            {
                _service.Actualizar(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public ActionResult Delete(string id)
        {
            //E_MovInventario obj = new E_MovInventario
            //{
            //    COD_CIA = id1,
            //    COMPANIA_VENTA_3 = id2,
            //    ALMACEN_VENTA = id3,
            //    TIPO_MOVIMIENTO = id4,
            //    TIPO_DOCUMENTO = id5,
            //    NRO_DOCUMENTO = id6,
            //    COD_ITEM_2 = id7
            //};

            //_service.Eliminar(obj);
            //return RedirectToAction("Index");
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(400);

            string decrypted = _encryption.Decrypt(id);
            var keys = decrypted.Split('|');

            var obj = new E_MovInventario
            {
                COD_CIA = keys[0],
                COMPANIA_VENTA_3 = keys[1],
                ALMACEN_VENTA = keys[2],
                TIPO_MOVIMIENTO = keys[3],
                TIPO_DOCUMENTO = keys[4],
                NRO_DOCUMENTO = keys[5],
                COD_ITEM_2 = keys[6]
            };

            _service.Eliminar(obj);

            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(E_MovInventario obj)
        {
            _service.Eliminar(obj);
            return RedirectToAction("Index");
        }
        private string GenerarIdSeguro(E_MovInventario item)
        {
            return _encryption.Encrypt(string.Join("|",
                item.COD_CIA,
                item.COMPANIA_VENTA_3,
                item.ALMACEN_VENTA,
                item.TIPO_MOVIMIENTO,
                item.TIPO_DOCUMENTO,
                item.NRO_DOCUMENTO,
                item.COD_ITEM_2));
        }
    }
}