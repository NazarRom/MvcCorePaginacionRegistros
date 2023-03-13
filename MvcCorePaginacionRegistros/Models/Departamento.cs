using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCorePaginacionRegistros.Models
{
    [Table("DEPT")]
    public class Departamento
    {
        [Key]
        [Column("DEPT_NO")]
        public int Dept_no { get; set; }
        [Column("DNOMBRE")]
        public string Dnombre { get; set; }
        [Column("LOC")]
        public string Localidad { get; set; }
    }
}
