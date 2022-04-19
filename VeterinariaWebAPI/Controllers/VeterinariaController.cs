using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria_Backend.dominio;
using Veterinaria_Backend.servicios;

namespace VeterinariaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeterinariaController : ControllerBase
    {

        private IService service;

        public VeterinariaController()
        {
            service = new ServiceFactoryImp().CrearService();
        }


        [HttpGet("GetClientes")]
        public IActionResult GetClientes()
        {

            return Ok(service.GetCliente());
        }

        [HttpGet("GetMascotas")]
        public IActionResult GetMascotas()
        {

            return Ok(service.GetMascota());
        }

        [HttpGet("GetByFiltersMascota")]
        public IActionResult GetByFiltersMascota(string id_cliente)
        {
            return Ok(service.GetByFiltersMascota(id_cliente));
        }


        [HttpPost("login")]
        public IActionResult Login(List<Parametro> lst)
        {
            if (Convert.ToString(lst[0].Valor) == "" || Convert.ToString(lst[1].Valor) == "")
                return Unauthorized("Se requiere permisos!");

            return Ok(service.Login(Convert.ToString(lst[0].Valor), Convert.ToString(lst[1].Valor)));
        }


        [HttpPost("SaveAtenciones")]
        public IActionResult SaveAtenciones(Atencion oAtencion)
        {
            if (oAtencion == null)
            {
                return BadRequest();
            }

            if (service.SaveAtenciones(oAtencion))
                return Ok("Ok");
            else
                return Ok("No se pudo grabar la Atencion!");
        }
    }
}
