using csharp_web_exam_api.Context;
using csharp_web_exam_api.Models;
using csharp_web_exam_api.Models.Dto;
using csharp_web_exam_api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace csharp_web_exam_api.Repository
{
    public class AlumnoRepository : cRepository<ALUMNO>, IAlumnoRepository
    {
        private readonly AppDbContext _db;
        public AlumnoRepository(AppDbContext db) :base(db)
        {
            _db = db;
        }
        public async Task<ALUMNO> Actulizar(ALUMNO entidad)
        {
            var varAlumnos = await _db.ALUMNOs.FirstOrDefaultAsync(x => x.ALUMNO_ID == entidad.ALUMNO_ID);

            if (entidad.ALUMNO_ID == 0 || varAlumnos == null)
            {
                return entidad;
            }          
            varAlumnos.CORREO = string.IsNullOrEmpty(entidad.CORREO) ? entidad.CORREO : entidad.CORREO.ToLower();
            varAlumnos.NOMBRE = string.IsNullOrEmpty(entidad.NOMBRE) ? entidad.NOMBRE : entidad.NOMBRE.ToUpper();
            varAlumnos.PATERNO = string.IsNullOrEmpty(entidad.PATERNO) ? entidad.NOMBRE : entidad.PATERNO.ToUpper();
            varAlumnos.MATERNO = string.IsNullOrEmpty(entidad.MATERNO) ? entidad.NOMBRE : entidad.MATERNO.ToUpper();
            varAlumnos.GENERO_ID = entidad.GENERO_ID != 0 ? entidad.GENERO_ID : 0;
            await _db.SaveChangesAsync();
            return entidad;
        }

    }
}
