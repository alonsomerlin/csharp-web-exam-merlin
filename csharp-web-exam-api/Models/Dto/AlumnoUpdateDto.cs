﻿using System.ComponentModel.DataAnnotations;

namespace csharp_web_exam_api.Models.Dto
{
    public class AlumnoUpdateDto
    {
        [Required]
        public int ALUMNO_ID { get; set; }
        [Required]
        [MaxLength(250)]
        public string? NOMBRE { get; set; }
        [Required]
        [MaxLength(250)]
        public string? PATERNO { get; set; }
        [Required]
        [MaxLength(250)]
        public string? MATERNO { get; set; }
        [Required]
        public int GENERO_ID { get; set; }
        public string? CORREO { get; set; }
    }
}
