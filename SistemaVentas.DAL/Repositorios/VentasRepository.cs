using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.DAL.DBContext;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVentas.Model;

namespace SistemaVentas.DAL.Repositorios
{
    public class VentasRepository : GenericRepository<Ventas>, IVentaRepository
    {
        private readonly DbVentasContext _dbContext;

        public VentasRepository(DbVentasContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Ventas> Registrar(Ventas modelo)
        {
            Ventas venta = new Ventas();
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                //Para restar la cantidad en stock de cada producto de la venta.
                foreach (DetalleVenta dv in modelo.DetalleVenta)
                {
                    Productos productoEncontrado = _dbContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();
                    productoEncontrado.Stock -= dv.Cantidad;
                    _dbContext.Productos.Update(productoEncontrado);

                }
                await _dbContext.SaveChangesAsync();

                //Para guardar la numeración del último documento.
                NumerosDocumento correlativo = _dbContext.NumerosDocumentos.First();
                correlativo.UltimoNumero += 1;
                correlativo.FechaRegistro = DateTime.Now;
                _dbContext.NumerosDocumentos.Update(correlativo);
                await _dbContext.SaveChangesAsync();

                //Para crear la numeración
                int cantDigitos = 4;
                string ceros = string.Concat(Enumerable.Repeat("0", cantDigitos));
                string numVenta = ceros + correlativo.UltimoNumero.ToString();
                numVenta = numVenta.Substring(numVenta.Length - cantDigitos, cantDigitos);
                modelo.NumeroDocumento = numVenta;
                await _dbContext.Ventas.AddAsync(modelo);
                await _dbContext.SaveChangesAsync();

                venta = modelo;

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            return venta;
        }
    }
}
