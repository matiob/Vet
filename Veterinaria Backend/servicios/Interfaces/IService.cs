using Veterinaria_Backend.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria_Backend.servicios
{
    public interface IService
    {

        public List<Cliente> GetCliente();
        public List<Mascota> GetMascota();
        public List<Mascota> GetByFiltersMascota(string id_cliente);
        bool Login(string User, string Password);

        public List<Atencion> ConsultarAtenciones(List<Parametro> criterio);
        public bool ConfirmarMascota(Mascota oMascota, Atencion oAtencion);


        public List<Cliente> CargarComboCliente();

        public List<Mascota> CargarComboMascota();

        public List<Tipo> CargarComboTipo();
        public List<Servicio> CargarComboServicio();

        int GetProximaAtencion();
        bool SaveAtenciones(Atencion oAtencion);


        public bool EliminarMascota(int numMascota);

        public Atencion ObtenerAtencionID(int numID);







    }
}
