using Microsoft.EntityFrameworkCore;
using MvcCorePaginacionRegistros.Models;

namespace MvcCorePaginacionRegistros.Data
{
    public class EmpleadoContext : DbContext
    {
        public EmpleadoContext(DbContextOptions<EmpleadoContext> options)
        :base(options){ }
       public DbSet<Empleado> Empleados { get; set; }
       public DbSet<Departamento> Departamentos { get; set; }
       public DbSet<VistaDepartamento> VistaDepartamentos { get; set; }
        
    }
}
