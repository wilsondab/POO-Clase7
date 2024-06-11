using Ejercicio4Modulo2.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio4Modulo2
{
    internal class BDContext : DbContext
    {
        public DbSet<Parametria> Parametria { get; set; }
        public DbSet<Rechazos> Rechazos { get; set; }
        public DbSet<VentasMensuales> VentasMensuales { get; set; }
        public BDContext(DbContextOptions<BDContext> options) : base(options) { }
    }

}
