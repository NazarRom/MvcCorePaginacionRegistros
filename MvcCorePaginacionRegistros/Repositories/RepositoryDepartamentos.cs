using MvcCorePaginacionRegistros.Data;
using MvcCorePaginacionRegistros.Models;

namespace MvcCorePaginacionRegistros.Repositories
{
    public class RepositoryDepartamentos
    {
        private EmpleadoContext context;

        public RepositoryDepartamentos(EmpleadoContext context)
        {
            this.context = context;
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from data in this.context.Departamentos
                           select data;
            return consulta.ToList();
        } 

        public List<Empleado> FindEmpleados(int dept_no)
        {
            var consulta = from data in this.context.Empleados
                           where data.Dept_no == dept_no
                           select data;
            return consulta.ToList();
        }
    }
}
