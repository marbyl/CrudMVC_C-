using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class ClimaBO
    {
        public int id { get; set; }
        public int? idCiudad { get; set; }
        public string nombreCiudad { get; set; }
        public string descripcion { get; set; }
        public int? temperatura { get; set; }
        public DateTime? fecha { get; set; }
    }
}
