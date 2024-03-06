using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using csharp_web_exam_api.Models;

namespace csharp_web_exam.Models
{
    [Table("ALUMNO", Schema = "test")]
    public class AlumnoViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [ForeignKey("GENERO_ID")]
        public required GENERO GENERO { get; set; }
        [MaxLength(250)]
        public string? CORREO { get; set; }
    }
}
