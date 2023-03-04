using L01_2020PF601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020PF601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuarioController : ControllerBase
    {
        private readonly EquipoContext _equipos_context;
        public usuarioController(EquipoContext equipos_context)
        {
            _equipos_context = equipos_context;
        }

        [HttpGet]
        [Route("GetTodo")]
        public IActionResult Get()
        {
            List<usuarios> listadoEquipo = (from e in _equipos_context.Usuarios
                                           select e).ToList();
            if (listadoEquipo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);
        }
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            usuarios? usuarios = (from e in _equipos_context.usuarios
                                where e.usuarioId == id
                                select e).FirstOrDefault();
            if (usuarios == null)
            {
                return NotFound();
            }
            return Ok(usuarios);
        }
        [HttpGet]
        [Route("buscador/{filtro}")]
        public IActionResult Buscador(string filtro)
        {
            usuarios? usuarios = (from e in _equipos_context.usuarios
                               where e.nombre.Contains(filtro)
                               select e).FirstOrDefault();
            if (usuarios == null)
            {
                return NotFound();
            }
            return Ok(usuarios);
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizar(int id, [FromBody] usuarios equipoModificar)
        {
            usuarios? equiposActual = (from e in _equipos_context.usuarios
                                      where e.usuarioId == id
                                      select e).FirstOrDefault();
            if (equiposActual == null)
            {
                return NotFound();
            }
            equiposActual.nombre = equipoModificar.nombre;
            equiposActual.rolId = equipoModificar.rolId;
            equiposActual.nombreUsuario = equipoModificar.nombreUsuario;
            equiposActual.clave = equipoModificar.clave;
            equiposActual.apellido = equipoModificar.apellido;

            _equipos_context.Entry(equiposActual).State = EntityState.Modified;
            _equipos_context.SaveChanges();

            return Ok(equiposActual);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            usuarios? usuario
         = (from e in _equipos_context.usuarios
                                where e.usuarioId == id
                                select e).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }
            _equipos_context.usuarios.Attach(usuario);
            _equipos_context.Remove(usuario);
            _equipos_context.SaveChanges();
            return Ok(usuario);
        }
    }

}
}
