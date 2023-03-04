using L01_2020PF601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020PF601.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class calificacionesController : ControllerBase
        {
            private readonly EquipoContext equiposC;
            public calificacionesController(EquipoContext equipos_context)
            {
                equiposC = equipos_context;
            }

            [HttpGet]
            [Route("GetTodo")]
            public IActionResult Get()
            {
                List<usuarios> listadoEquipo = ((List<usuarios>)(from e in equiposC.calificaciones
                                                select e)).ToList();
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
                usuarios? usuarios = (from e in equiposC.calificaciones
                                      where e.calificacionId == 
                                      select e).FirstOrDefault();
                if (usuarios == null)
                {
                    return NotFound();
                }
                return Ok(calificaciones);
            }
            [HttpGet]
            [Route("buscador/{filtro}")]
            public IActionResult Buscador(string filtro)
            {
                usuarios? usuarios = (from e in equiposC.usuarios
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
                usuarios? equiposActual = (from e in equiposC.usuarios
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

                equiposC.Entry(equiposActual).State = EntityState.Modified;
                equiposC.SaveChanges();

                return Ok(equiposActual);
            }
            [HttpDelete]
            [Route("eliminar/{id}")]
            public IActionResult EliminarEquipo(int id)
            {
                usuarios? usuario
             = (from e in equiposC.usuarios
                where e.usuarioId == id
                select e).FirstOrDefault();
                if (usuario == null)
                {
                    return NotFound();
                }
                equiposC.usuarios.Attach(usuario);
                equiposC.Remove(usuario);
                equiposC.SaveChanges();
                return Ok(usuario);
            }
        }
    }

