using Ejercicio4Modulo2.domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ejercicio4Modulo2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region lecturaArchivo

            string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\data.txt";
            var lecturaArchivo = File.ReadAllLines(path);

            #endregion

            #region variables
            var opciones = new DbContextOptionsBuilder<BDContext>();
            opciones.UseSqlServer("Data Source=DESKTOP-76AVVCN\\" +
                "SQLEXPRESS;Initial Catalog=Ejercicio4Modulo2;" +
                "Integrated Security=True;Trust Server Certificate=True");
            var contexto = new BDContext(opciones.Options);

            var fechaParametria = contexto.Parametria.First().value;

            #endregion

            #region lecturaArchivo
            
            foreach (var item in lecturaArchivo)
            {
                var fecha = item.Substring(0, 10);
                var codigo = item.Substring(10, 3);
                var venta = item.Substring(13, 11);
                var ventaE = item.Substring(24, 1);
                var flagVentaE = ventaE.Equals("S") ? 1 : ventaE.Equals("N") ? 0 : -1;

                if (String.IsNullOrWhiteSpace(fecha) || !fechaParametria.ToString().Equals(fecha))
                {
                    var objRechazo = new Rechazos()
                    {
                        error = "Fecha Invalida",
                        registroOriginal = item
                    };

                    contexto.Rechazos.Add(objRechazo);
                }
                else if (String.IsNullOrWhiteSpace(codigo))
                {
                    var objRechazo = new Rechazos()
                    {
                        error = "Codigo de Vendedor Invalido",
                        registroOriginal = item
                    };

                    contexto.Rechazos.Add(objRechazo);
                }
                else if (flagVentaE == -1)
                {
                    var objRechazo = new Rechazos()
                    {
                        error = "Codigo de Venta Empresa Grande Invalido",
                        registroOriginal = item
                    };

                    contexto.Rechazos.Add(objRechazo);
                }
                else
                {
                    var objVentaMensual = new VentasMensuales()
                    {
                        fecha = DateTime.Parse(fecha),
                        codVendedor = codigo,
                        venta = Decimal.Parse(venta),
                        ventaEmpresaGrande = ventaE.Equals("S")
                    };
                    contexto.VentasMensuales.Add(objVentaMensual);
                }
            }
            contexto.SaveChanges();
            
            #endregion

            #region consultas

            // OBTENGO LISTA DE VENDEDORES
            var listVendedores = contexto.VentasMensuales
                .GroupBy(x => new { x.codVendedor })
                .Select(x => new {
                    x.Key.codVendedor,
                    totalVentas = x.Sum(x => x.venta)
                })
                // .Where(x => x.totalVentas > 100000)
                .ToList();
            listVendedores.ForEach(x => {
                if(x.totalVentas >= 100000) Console.WriteLine("El vendedor " + x.codVendedor + " vendio " + x.totalVentas + "\n");
            });
            Console.WriteLine("================================================================\n");

            listVendedores.ForEach(x => {
                if (x.totalVentas < 100000) Console.WriteLine("El vendedor " + x.codVendedor + " vendio " + x.totalVentas + "\n");
            });
            Console.WriteLine("================================================================\n");

            var listVendedores2 = contexto.VentasMensuales.Where(x => x.ventaEmpresaGrande)
                .GroupBy(x => new { x.codVendedor })
                .Select(x => new {
                    x.Key.codVendedor,
                    totalVentasEG = x.Count()
                })
                .Where(x => x.totalVentasEG >= 1)
                .ToList();

            listVendedores2.ForEach(x => {
                Console.WriteLine("El vendedor " + x.codVendedor + " vendio por lo menos una Empresa Grande\n");
            });
            Console.WriteLine("================================================================\n");

            var listVendedores3 = contexto.Rechazos.ToList();

            listVendedores3.ForEach(x => {
                Console.WriteLine("Registro erroneo: " + x.registroOriginal +", causa: " + x.error);
            });
            #endregion
        }

    }
}