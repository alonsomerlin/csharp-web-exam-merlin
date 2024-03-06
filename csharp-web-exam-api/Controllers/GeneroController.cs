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
    public class GeneroController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<GeneroController> _logger;
        private readonly IMapper _mapper; 

        public GeneroController(AppDbContext context, ILogger<GeneroController> logger, IMapper mapper) 
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        // GET: Toma Lista de genero

        [HttpGet("ListaGenero")]
        public async Task<ActionResult<IEnumerable<GENERO>>> ListaGenero()
        {
            _logger.LogInformation("Obtener informacion del genero");
            return await _context.GENEROs.ToListAsync();
        }

    }
}
