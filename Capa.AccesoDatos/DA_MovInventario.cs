using Capa.Entidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.AccesoDatos
{
    public class DA_MovInventario
    {
        private string cn =
           ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

        public List<E_MovInventario> Listar()
        {
            List<E_MovInventario> lista = new List<E_MovInventario>();

            using (SqlConnection con = new SqlConnection(cn))
            using (SqlCommand cmd = new SqlCommand("USP_MOV_INVENTARIO_LIST", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new E_MovInventario
                    {
                        COD_CIA = dr["COD_CIA"].ToString(),
                        COMPANIA_VENTA_3 = dr["COMPANIA_VENTA_3"].ToString(),
                        ALMACEN_VENTA = dr["ALMACEN_VENTA"].ToString(),
                        TIPO_MOVIMIENTO = dr["TIPO_MOVIMIENTO"].ToString(),
                        TIPO_DOCUMENTO = dr["TIPO_DOCUMENTO"].ToString(),
                        NRO_DOCUMENTO = dr["NRO_DOCUMENTO"].ToString(),
                        COD_ITEM_2 = dr["COD_ITEM_2"].ToString(),
                        PROVEEDOR = dr["PROVEEDOR"]?.ToString(),
                        CANTIDAD = dr["CANTIDAD"] as int?
                    });
                }
            }

            return lista;
        }

        public void Insertar(E_MovInventario obj)
        {
            using (SqlConnection con = new SqlConnection(cn))
            using (SqlCommand cmd = new SqlCommand("USP_MOV_INVENTARIO_INSERT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                AgregarParametros(cmd, obj);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(E_MovInventario obj)
        {
            using (SqlConnection con = new SqlConnection(cn))
            using (SqlCommand cmd = new SqlCommand("USP_MOV_INVENTARIO_UPDATE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                AgregarParametros(cmd, obj);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(E_MovInventario obj)
        {
            using (SqlConnection con = new SqlConnection(cn))
            using (SqlCommand cmd = new SqlCommand("USP_MOV_INVENTARIO_DELETE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@COD_CIA", obj.COD_CIA);
                cmd.Parameters.AddWithValue("@COMPANIA_VENTA_3", obj.COMPANIA_VENTA_3);
                cmd.Parameters.AddWithValue("@ALMACEN_VENTA", obj.ALMACEN_VENTA);
                cmd.Parameters.AddWithValue("@TIPO_MOVIMIENTO", obj.TIPO_MOVIMIENTO);
                cmd.Parameters.AddWithValue("@TIPO_DOCUMENTO", obj.TIPO_DOCUMENTO);
                cmd.Parameters.AddWithValue("@NRO_DOCUMENTO", obj.NRO_DOCUMENTO);
                cmd.Parameters.AddWithValue("@COD_ITEM_2", obj.COD_ITEM_2);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void AgregarParametros(SqlCommand cmd, E_MovInventario obj)
        {
            cmd.Parameters.AddWithValue("@COD_CIA", obj.COD_CIA);
            cmd.Parameters.AddWithValue("@COMPANIA_VENTA_3", obj.COMPANIA_VENTA_3);
            cmd.Parameters.AddWithValue("@ALMACEN_VENTA", obj.ALMACEN_VENTA);
            cmd.Parameters.AddWithValue("@TIPO_MOVIMIENTO", obj.TIPO_MOVIMIENTO);
            cmd.Parameters.AddWithValue("@TIPO_DOCUMENTO", obj.TIPO_DOCUMENTO);
            cmd.Parameters.AddWithValue("@NRO_DOCUMENTO", obj.NRO_DOCUMENTO);
            cmd.Parameters.AddWithValue("@COD_ITEM_2", obj.COD_ITEM_2);
            cmd.Parameters.AddWithValue("@PROVEEDOR", (object)obj.PROVEEDOR ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ALMACEN_DESTINO", (object)obj.ALMACEN_DESTINO ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CANTIDAD", (object)obj.CANTIDAD ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DOC_REF_1", (object)obj.DOC_REF_1 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DOC_REF_2", (object)obj.DOC_REF_2 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DOC_REF_3", (object)obj.DOC_REF_3 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DOC_REF_4", (object)obj.DOC_REF_4 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DOC_REF_5", (object)obj.DOC_REF_5 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FECHA_TRANSACCION", (object)obj.FECHA_TRANSACCION ?? DBNull.Value);
        }
        public E_Paginado<E_MovInventario> ListarPaginado(int pageNumber, int pageSize, DateTime? fechaInicio, DateTime? fechaFin, string tipoMovimiento, string nroDocumento)
        {
            E_Paginado<E_MovInventario> result = new E_Paginado<E_MovInventario>();

            using (SqlConnection con = new SqlConnection(cn))
            using (SqlCommand cmd = new SqlCommand("USP_MOV_INVENTARIO_CONSULTA", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", fechaFin);
                cmd.Parameters.AddWithValue("@TipoMovimiento", tipoMovimiento);
                cmd.Parameters.AddWithValue("@NroDocumento ", nroDocumento);
                con.Open();
                
                /*
                SqlDataReader dr = cmd.ExecuteReader();

                result.Data = new List<E_MovInventario>();

                while (dr.Read())
                {
                    result.Data.Add(new E_MovInventario
                    {
                  
                        COD_CIA = dr["COD_CIA"].ToString(),
                        COMPANIA_VENTA_3 = dr["COMPANIA_VENTA_3"].ToString(),
                        ALMACEN_VENTA = dr["ALMACEN_VENTA"].ToString(),
                        TIPO_MOVIMIENTO = dr["TIPO_MOVIMIENTO"].ToString(),
                        TIPO_DOCUMENTO = dr["TIPO_DOCUMENTO"].ToString(),
                        NRO_DOCUMENTO = dr["NRO_DOCUMENTO"].ToString(),
                        COD_ITEM_2 = dr["COD_ITEM_2"].ToString(),
                        PROVEEDOR = dr["PROVEEDOR"]?.ToString(),
                        CANTIDAD = dr["CANTIDAD"] as int?
                    });
                }

                if (dr.NextResult())
                {
                    if (dr.Read())
                        result.TotalRecords = (int)dr["TotalRegistros"];
                }
                */
               
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    int total = Convert.ToInt32(dr["TotalRegistros"]);

                    dr.NextResult();

                    List<E_MovInventario> lista = new List<E_MovInventario>();

                    while (dr.Read())
                    {
                        lista.Add(new E_MovInventario
                        {
                            COD_CIA = dr["COD_CIA"].ToString(),
                            COMPANIA_VENTA_3 = dr["COMPANIA_VENTA_3"].ToString(),
                            ALMACEN_VENTA = dr["ALMACEN_VENTA"].ToString(),
                            TIPO_MOVIMIENTO = dr["TIPO_MOVIMIENTO"].ToString(),
                            TIPO_DOCUMENTO = dr["TIPO_DOCUMENTO"].ToString(),
                            NRO_DOCUMENTO = dr["NRO_DOCUMENTO"].ToString(),
                            COD_ITEM_2 = dr["COD_ITEM_2"].ToString(),
                            PROVEEDOR = dr["PROVEEDOR"]?.ToString(),
                            CANTIDAD = dr["CANTIDAD"] as int?
                        });
                    }

                    result.Data = lista;
                    result.TotalRecords = total;
                    result.PageNumber = pageNumber;
                    result.PageSize = pageSize;
                }
            }
            /*
            result.PageNumber = pageNumber;
            result.PageSize = pageSize;
            */
            return result;
        }

        public E_MovInventario Obtener(string id1, string id2, string id3,
                             string id4, string id5, string id6,
                             string id7)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                string sql = @"SELECT * FROM MOV_INVENTARIO
                       WHERE COD_CIA = @COD_CIA
                       AND COMPANIA_VENTA_3 = @COMPANIA_VENTA_3
                       AND ALMACEN_VENTA = @ALMACEN_VENTA
                       AND TIPO_MOVIMIENTO = @TIPO_MOVIMIENTO
                       AND TIPO_DOCUMENTO = @TIPO_DOCUMENTO
                       AND NRO_DOCUMENTO = @NRO_DOCUMENTO
                       AND COD_ITEM_2 = @COD_ITEM_2";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@COD_CIA", id1);
                cmd.Parameters.AddWithValue("@COMPANIA_VENTA_3", id2);
                cmd.Parameters.AddWithValue("@ALMACEN_VENTA", id3);
                cmd.Parameters.AddWithValue("@TIPO_MOVIMIENTO", id4);
                cmd.Parameters.AddWithValue("@TIPO_DOCUMENTO", id5);
                cmd.Parameters.AddWithValue("@NRO_DOCUMENTO", id6);
                cmd.Parameters.AddWithValue("@COD_ITEM_2", id7);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new E_MovInventario
                    {
                        COD_CIA = dr["COD_CIA"].ToString(),
                        COMPANIA_VENTA_3 = dr["COMPANIA_VENTA_3"].ToString(),
                        ALMACEN_VENTA = dr["ALMACEN_VENTA"].ToString(),
                        TIPO_MOVIMIENTO = dr["TIPO_MOVIMIENTO"].ToString(),
                        TIPO_DOCUMENTO = dr["TIPO_DOCUMENTO"].ToString(),
                        NRO_DOCUMENTO = dr["NRO_DOCUMENTO"].ToString(),
                        COD_ITEM_2 = dr["COD_ITEM_2"].ToString(),
                        CANTIDAD = Convert.ToInt16(dr["CANTIDAD"])
                    };
                }

                return null;
            }
        }


    }
}
