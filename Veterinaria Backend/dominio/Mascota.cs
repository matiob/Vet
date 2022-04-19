using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria_Backend.dominio
{
    public class Mascota
    {
        public int IdMascota { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNac { get; set; }

        public Tipo Tipos { get; set; }

        public List<Atencion> Atenciones { get; set; }
        public int IdCliente { get; set; }
        public DateTime Fallecio { get; set; }

        public Mascota()
        {
            Tipos = new Tipo();
            Atenciones = new List<Atencion>();
        }
        public void AgregarAtencion(Atencion atencion)
        {
            Atenciones.Add(atencion);
        }
        public void QuitarAtencion(int indice)
        {
            Atenciones.RemoveAt(indice);
        }
        public double CalcularTotal()
        {
            double total = 0;
            foreach (Atencion item in Atenciones)
            {
                total += item.Importe;
            }
            return total;
        }

        public override string ToString()
        {
            return Nombre;
        }

    }
}
