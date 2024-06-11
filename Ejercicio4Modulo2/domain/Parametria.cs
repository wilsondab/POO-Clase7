using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio4Modulo2.domain
{
    [Table("parametria")]
    internal class Parametria
    {
        public int id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }
}
