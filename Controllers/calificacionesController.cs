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
                List<calificaciones> listadocalificaciones = (from e in equiposC.calificaciones
                                            select e).ToList();
            if (listadocalificaciones.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadocalificaciones);
            }

            [HttpGet]
            [Route("GetCalfPub/{pubid}")]
            public IActionResult GetCalfPub(int pubid)
            {
                List<calificaciones> listPub = (from e in equiposC.calificaciones
                                      where e.publicacionId == pubid
                                      select e).ToList();
                if (listPub.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listPub);
            }


        [HttpGet]
            [Route("GetById/{idC}")]
            public IActionResult GetById(int idC)
            {
                calificaciones? calificaciones = (from e in equiposC.calificaciones
                                      where e.calificacionId == idC
                                                  select e).FirstOrDefault();
                if (calificaciones == null)
                {
                    return NotFound();
                }
                return Ok(calificaciones);
            }
            [HttpGet]
            [Route("buscador/{filtro}")]
            public IActionResult Buscador(int filtro)
            {
                calificaciones? calificaciones = (from e in equiposC.calificaciones
                                      where e.calificacionId == filtro
                                      select e).FirstOrDefault();
                if (calificaciones == null)
                {
                    return NotFound();
                }
                return Ok(calificaciones);
            }
            [HttpPut]
            [Route("actualizar/{id}")]
            public IActionResult Actualizar(int id, [FromBody] calificaciones equipoModificar)
            {
                calificaciones? equiposActual = (from e in equiposC.calificaciones
                                           where e.usuarioId == id
                                           select e).FirstOrDefault();
                if (equiposActual == null)
                {
                    return NotFound();
                }
                equiposActual.calificacionId = equipoModificar.calificacionId;
                equiposActual.publicacionId = equipoModificar.publicacionId;
                equiposActual.usuarioId = equipoModificar.usuarioId;
                equiposActual.calificacion = equipoModificar.calificacion;

                equiposC.Entry(equiposActual).State = EntityState.Modified;
                equiposC.SaveChanges();

                return Ok(equiposActual);
            }
            [HttpDelete]
            [Route("eliminar/{id}")]
            public IActionResult EliminarCalificacion(int id)
            {
                calificaciones? calificaciones = (from e in equiposC.calificaciones
                where e.calificacionId == id
                select e).FirstOrDefault();
                if (calificaciones == null)
                {
                    return NotFound();
                }
                equiposC.calificaciones.Attach(calificaciones);
                equiposC.Remove(calificaciones);
                equiposC.SaveChanges();
                return Ok(calificaciones);
            }
        }
    }

