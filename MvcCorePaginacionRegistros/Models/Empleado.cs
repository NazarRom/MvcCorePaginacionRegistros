using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCorePaginacionRegistros.Models
{
    [Table("EMP")]
    public class Empleado
    {
        [Key]
        [Column("EMP_NO")]
        public int Emp_no { get; set; }
        [Column("Apellido")]
        public string Apellido { get; set; }
        [Column("Oficio")]
        public string Oficio { get; set; }
        [Column("Fecha_alt")]
        public DateTime Fecha_Alt { get; set; }
        [Column("Salario")]
        public int Salario { get; set; }
        [Column("DEPT_NO")]
        public int Dept_no { get; set; }
    }
}
