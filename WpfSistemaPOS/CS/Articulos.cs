using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSistemaPOS.CS
{
    public class Articulo
    {
        public string IdCompuesto { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Existencia { get; set; }
        public int Cantidad { get; set; } 
        public double Importe { get; set; }
    }

}
