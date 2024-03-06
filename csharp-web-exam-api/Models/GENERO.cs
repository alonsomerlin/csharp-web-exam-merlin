using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace csharp_web_exam_api.Models
{
    [Table("GENERO", Schema = "test")]
    public class GENERO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GENERO_ID { get; set; }
        [Required]
        [MaxLength(250)]
        public string? GENERO_DESC { get; set; }

    }
}
