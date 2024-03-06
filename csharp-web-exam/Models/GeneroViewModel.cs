using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_web_exam.Models
{
    [Table("GENERO", Schema = "test")]
    public class GeneroViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GENERO_ID { get; set; }
        [Required]
        [MaxLength(250)]
        public string? GENERO_DESC { get; set; }
    }
}
