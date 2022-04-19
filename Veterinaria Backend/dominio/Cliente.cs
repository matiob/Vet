using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria_Backend.dominio
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }

        public List<Mascota> Mascotas { get; set; }

        public Cliente()
        {
            Mascotas = new List<Mascota>();


        }
        public void AgregarMascota(Mascota mascota)
        {
            Mascotas.Add(mascota);
        }
        public void QuitarAtencion(int indice)
        {
            Mascotas.RemoveAt(indice);
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
