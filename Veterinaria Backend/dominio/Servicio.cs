using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria_Backend.dominio
{
    public class Servicio
    {
        public int IdServicio { get; set; }
        public string Service { get; set; }

        public override string ToString()
        {
            return Service;
        }
    }
}
