using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.AccesoDatos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;

        public IMovInventarioRepository MovInventario { get; private set; }

        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
            MovInventario = new MovInventarioRepository(_connectionString);
        }

        public void Commit()
        {
            // Si usaras EF → SaveChanges()
            // En ADO.NET puro normalmente no hace nada
        }

        public void Dispose()
        {
            // Liberar recursos si fuera necesario
        }
    }

}
