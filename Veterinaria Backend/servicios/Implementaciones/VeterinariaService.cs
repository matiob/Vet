using Veterinaria_Backend.acceso_a_datos.Implementaciones;
using Veterinaria_Backend.acceso_a_datos.Interfaces;
using Veterinaria_Backend.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria_Backend.servicios
{
    public class VeterinariaService : IService
    {
        private IVeterinariaDao dao;

        public VeterinariaService()
        {
            dao = new VeterinariaDao();
        }

        public List<Cliente> GetCliente()
        {
            return dao.GetClientes();
        }

        public List<Mascota> GetMascota()
        {
            return dao.GetMascotas();
        }

        public List<Mascota> GetByFiltersMascota(string id_cliente)
        {
            return dao.GetByFiltersMascota(id_cliente);
        }

        public bool Login(string User, string Password)
        {
            return dao.Login(User, Password);
        }


        public List<Atencion> ConsultarAtenciones(List<Parametro> criterio)
        {
            return dao.ConsultarAtenciones(criterio);
        }

        public bool ConfirmarMascota(Mascota oMascota, Atencion oAtencion)
        {
            return dao.Crear(oMascota, oAtencion);
        }

        public List<Cliente> CargarComboCliente()
        {
            return dao.ListarComboCliente();
        }

        public List<Mascota> CargarComboMascota()
        {
            return dao.ListarComboMascota();
        }
        public Atencion ObtenerAtencionID(int numID)
        {
            return dao.ObtAtencionID(numID);
        }
    

    public List<Tipo> CargarComboTipo()
        {
            return dao.ListarComboTipo();
        }
         public List<Servicio> CargarComboServicio()
        {
        return dao.ListarComboServicio();
        }


        public int GetProximaAtencion()
        {
            return dao.GetProximaAtencion();
        }

        public bool SaveAtenciones(Atencion oAtencion)
        {
            return dao.SaveAtenciones(oAtencion);
        }


        public bool EliminarMascota(int numMascota)
        {
            return dao.Delete(numMascota);
        }





    }
}
