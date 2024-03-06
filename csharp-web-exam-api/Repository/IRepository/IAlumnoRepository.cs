using csharp_web_exam_api.Models;
using csharp_web_exam_api.Models.Dto;

namespace csharp_web_exam_api.Repository.IRepository
{
    public interface IAlumnoRepository : IRepository<ALUMNO>
    {
        Task<ALUMNO> Actulizar(ALUMNO entidad);
    }
}
