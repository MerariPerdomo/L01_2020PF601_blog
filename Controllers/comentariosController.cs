using L01_2020PF601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020PF601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
            private readonly EquipoContext _equipos_context;
            public comentariosController(EquipoContext equipos_context)
            {
                _equipos_context = equipos_context;
            }

            [HttpGet]
            [Route("GetTodo")]
            public IActionResult Get()
            {
                List<comentarios> listadoEquipo = (from e in _equipos_context.comentarios
                                                select e).ToList();
                if (listadoEquipo.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(listadoEquipo);
            }

            [HttpGet]
            [Route("GetByUser/{user}")]
            public IActionResult GetByUser(int user)
            {
                comentarios? comentarios = (from e in _equipos_context.comentarios
                                      where e.usuarioId== user
                                      select e).FirstOrDefault();
                if (comentarios == null)
                {
                    return NotFound();
                }
                return Ok(comentarios);
            }

        [HttpGet]
        [Route("GetComentariosporId/{userId}")]
        public IActionResult GetComentariosporId(int userId)
        {
            List<comentarios> comentarios = (from e in _equipos_context.comentarios
                                        where e.usuarioId == userId
                                        select e).ToList();
            if (comentarios == null)
            {
                return NotFound();
            }
            return Ok(comentarios);
        }
            [HttpGet]
            [Route("buscador/{filtro}")]
            public IActionResult Buscador(int filtro)
            {
                comentarios? comentarios = (from e in _equipos_context.comentarios
                                      where e.usuarioId ==filtro
                                      select e).FirstOrDefault();
                if (comentarios == null)
                {
                    return NotFound();
                }
                return Ok(comentarios);
            }
            [HttpPut]
            [Route("actualizar/{id}")]
            public IActionResult Actualizar(int id, [FromBody] comentarios equipoModificar)
            {
                comentarios? equiposActual = (from e in _equipos_context.comentarios
                                           where e.cometarioId == id
                                           select e).FirstOrDefault();
                if (equiposActual == null)
                {
                    return NotFound();
                }
                equiposActual.cometarioId = equipoModificar.cometarioId;
                equiposActual.publicacionId= equipoModificar.publicacionId;
                equiposActual.comentario = equipoModificar.comentario;
                equiposActual.usuarioId = equipoModificar.usuarioId;

                _equipos_context.Entry(equiposActual).State = EntityState.Modified;
                _equipos_context.SaveChanges();

                return Ok(equiposActual);
            }
            [HttpDelete]
            [Route("eliminar/{id}")]
            public IActionResult EliminarEquipo(int id)
            {
                comentarios? comentarios= (from e in _equipos_context.comentarios
                where e.cometarioId == id
                select e).FirstOrDefault();
                if (comentarios == null)
                {
                    return NotFound();
                }
                _equipos_context.comentarios.Attach(comentarios);
                _equipos_context.Remove(comentarios);
                _equipos_context.SaveChanges();
                return Ok(comentarios);
            }
    }

}

