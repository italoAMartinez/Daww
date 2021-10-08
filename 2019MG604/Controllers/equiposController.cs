using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _2019MG604.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace _2019MG604.Controllers
{
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly _2019MG604Context _contexto;

        public equiposController(_2019MG604Context miContexto){
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/equipos")]
        public IActionResult Get(){
            IEnumerable<equipos> equiposList = from e in _contexto.equipos
                                               select e;

            if (equiposList.Count() > 0)
            {
                return Ok(equiposList);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/equipos/{id}")]
        public IActionResult getbyId(int id)
        {
            equipos unEquipo = (from e in _contexto.equipos
                                where e.id_equipos == id
                                select e).FirstOrDefault();
            if (unEquipo != null)
            {
                return Ok(unEquipo);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/equipo/buscanombre/{buscarnombre}")]

        public IActionResult getNombre(string buscarnombre)
        {
            IEnumerable<equipos> equiposPorNombre = from e in _contexto.equipos
                                                    where e.nombre.Contains(buscarnombre)
                                                    select e;
            if (equiposPorNombre.Count() > 0)
            {
                return Ok(equiposPorNombre);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("api/equipos")]
        public IActionResult guardarEquipo([FromBody] equipos equipoNuevo)
        {
            try
            {
                IEnumerable<equipos> equipoExiste = from e in _contexto.equipos
                                                    where e.nombre == equipoNuevo.nombre
                                                    select e;
                if (equipoExiste.Count() == 0)
                {
                    _contexto.equipos.Add(equipoNuevo);
                    _contexto.SaveChanges();
                    return Ok(equipoNuevo);
                }
                return BadRequest(equipoExiste);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/equipos")]
        public IActionResult updateEquipo([FromBody]  equipos equipoModificar)
        {
            equipos equipoExiste = (from e in _contexto.equipos
                                    where e.id_equipos == equipoModificar.id_equipos
                                    select e).FirstOrDefault();
            if(equipoExiste is null)
            {
                return NotFound();
            }

            equipoExiste.nombre = equipoModificar.nombre;
            equipoExiste.descripcion = equipoModificar.descripcion;
            equipoExiste.modelo = equipoModificar.modelo;

            _contexto.Entry(equipoExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(equipoExiste);
        }
    }
}