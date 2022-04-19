using Veterinaria_Backend.dominio;
using Veterinaria_Backend.servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria_Backend.acceso_a_datos.Interfaces
{
    interface IVeterinariaDao
    {


        List<Cliente> GetClientes();
        List<Mascota> GetMascotas();
        List<Mascota> GetByFiltersMascota(string id_cliente);
        bool Crear(Mascota oMascota, Atencion oAtencion);
        bool Login(string User, string Password);
        List<Atencion> ConsultarAtenciones(List<Parametro> criterios);
        public bool Delete(int mascotaNum);

        List<Cliente> ListarComboCliente();

        List<Mascota> ListarComboMascota();

        List<Tipo> ListarComboTipo();

        List<Servicio> ListarComboServicio();

        int GetProximaAtencion();
        bool SaveAtenciones(Atencion oAtencion);

       Atencion ObtAtencionID(int numID);






    }
}
