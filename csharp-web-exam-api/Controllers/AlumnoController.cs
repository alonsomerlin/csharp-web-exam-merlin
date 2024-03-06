using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using csharp_web_exam_api.Context;
using csharp_web_exam_api.Models;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using csharp_web_exam_api.Repository.IRepository;
using csharp_web_exam_api.Models.Dto;

namespace csharp_web_exam_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAlumnoRepository _alumnoRepo;
        private readonly ILogger<AlumnoController> _logger;
        private readonly IMapper _mapper; 

        public AlumnoController(AppDbContext context, IAlumnoRepository alumnoRepo, ILogger<AlumnoController> logger, IMapper mapper) 
        {
            _logger = logger;
            _context = context;
            _alumnoRepo = alumnoRepo;
            _mapper = mapper;
        }

        //Obtiene alumnos por ID
        [HttpGet("mostrar/{idAlumno}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AlumnoDto>>> mostrarAlumno(int idAlumno)
        {
            if (idAlumno == 0)
            {
                _logger.LogError("Error al traer el alumno con ID " + idAlumno);
                return BadRequest();
            }
            var varAlumno = await _alumnoRepo.Obtener(x => x.ALUMNO_ID == idAlumno);
            if (varAlumno == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AlumnoDto>(varAlumno));
        }
        //Toma lista de alumnos
        [HttpGet("listaAlumno")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ALUMNO>>> listaAlumno()
        {
            _logger.LogInformation("Obtener alumno");

            //IEnumerable<ALUMNO> Alumnolist = await _alumnoRepo.ObtenerTodos();

            IEnumerable<ALUMNO> Alumnolist = await _context.ALUMNOs
                                                    .Include(s => s.GENERO)
                                                    .AsNoTracking().ToListAsync();

            if (Alumnolist == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ALUMNO>>(Alumnolist));
        }

        //Inserta alumno
        [HttpPost("inserta")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AlumnoCreateDto>> insertaAlumno([FromBody] AlumnoCreateDto Alumno)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Alumno == null)
            {
                return BadRequest(Alumno);
            }
            ALUMNO moAlumno = _mapper.Map<ALUMNO>(Alumno);
            await _alumnoRepo.Crear(moAlumno);

            return Ok(moAlumno);
        }

        // DELETE: borra alumno por id

        [HttpDelete("borrar/{idAlumno}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> borrarAlumno(int idAlumno)
        {
            if (idAlumno == 0)
            {
                return BadRequest();
            }
            var VarAlumno = await _alumnoRepo.Obtener(x => x.ALUMNO_ID == idAlumno);
            if (VarAlumno == null)
            {
                return NotFound();
            }

            await _alumnoRepo.Remover(VarAlumno);

            return NoContent();
        }

        // PUT: Actualiza todos los capos de alumno por id

        [HttpPut("actualizar/{idAlumno}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> actualizarAlumno(int idAlumno, AlumnoUpdateDto ipAlumno)
        {
            if (idAlumno != ipAlumno.ALUMNO_ID)
            {
                return BadRequest();
            }

            var varAlumnos = await _alumnoRepo.Obtener(x => x.ALUMNO_ID == ipAlumno.ALUMNO_ID);
            if (varAlumnos == null)
            {
                ModelState.AddModelError("El id ya existe", "El alumno con ese ID ya existe");
                return BadRequest(ModelState);
            }

            ALUMNO moAlumno = _mapper.Map<ALUMNO>(ipAlumno);          


            await _alumnoRepo.Actulizar(moAlumno);

            

            return Ok();
        }
        // Patch: Actualiza alumno por campo
        [HttpPatch("actualizarcampo/{idAlumno}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        
        public async Task<IActionResult> actualizarcampoAlumno(int idAlumno, JsonPatchDocument<ALUMNO> Alumno)
        {
            if (idAlumno == 0 || Alumno == null)
            {
                return BadRequest();
            }

            var varAlumno  = await _alumnoRepo.Obtener(x => x.ALUMNO_ID == idAlumno, tracked:false);

            if (varAlumno == null)
            {
                return NotFound();
            }
            Alumno.ApplyTo(varAlumno, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ALUMNO moAlumno = _mapper.Map<ALUMNO>(varAlumno);


            await _alumnoRepo.Actulizar(moAlumno);


            return Ok();
        }


    }
}
