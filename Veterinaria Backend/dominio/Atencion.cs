using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria_Backend.dominio
{
    public class Atencion
    {
        public int IdAtencion { get; set; }
        public DateTime Fecha { get; set; }
        public Servicio Descripcion { get; set; }
        public double Importe { get; set; }
        public int IdMascota { get; set; }

        public string Detalles { get; set; }
        public Atencion(Servicio servicio)
        {
            Descripcion = servicio;
        }
        public Atencion()
        {
            Descripcion = new Servicio();
            Detalles = "";
        }

    }
}
