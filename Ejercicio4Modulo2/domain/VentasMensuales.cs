using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio4Modulo2.domain
{
    [Table("ventas_mensuales")]
    internal class VentasMensuales
    {
        public int id { get; set; }

        public DateTime fecha { get; set; }

        [Column("cod_vendedor")]
        public string codVendedor { get; set; }

        public Decimal venta { get; set; }

        [Column("venta_empresa_grande")]
        public bool ventaEmpresaGrande { get; set; }
    }
}
