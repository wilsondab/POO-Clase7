using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio4Modulo2.domain
{
    [Table("rechazos")]
    internal class Rechazos
    {
        public int id { get; set; }

        public string error { get; set; }

        [Column("registro_original")]
        public string registroOriginal { get; set; }
    }
}
