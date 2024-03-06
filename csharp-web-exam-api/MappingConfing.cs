using AutoMapper;
using csharp_web_exam_api.Models;
using csharp_web_exam_api.Models.Dto;

namespace csharp_web_exam_api
{
    public class MappingConfing : Profile
    {
        public MappingConfing()
        {
            CreateMap<ALUMNO, AlumnoDto>();
            CreateMap<AlumnoDto, ALUMNO>();

            CreateMap<ALUMNO, AlumnoCreateDto>();
            CreateMap<AlumnoCreateDto, ALUMNO>();

            CreateMap<ALUMNO, AlumnoUpdateDto>();
            CreateMap<AlumnoUpdateDto, ALUMNO>();


            CreateMap<GENERO, GENERO>();
        }
    }
}