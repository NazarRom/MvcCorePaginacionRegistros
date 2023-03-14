using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCorePaginacionRegistros.Data;
using MvcCorePaginacionRegistros.Models;
#region 

//create view v_departamentos_individual
//as
//select cast(
//ROW_NUMBER() over(order by dept_no) as int) as posicion,
//ISNULL(dept_no, 0) as dept_no, dnombre, loc
//from dept

//declare @posicion int 
//set @posicion = 1
//select * from  v_departamentos_individual
//where posicion >= @posicion and posicion < (@posicion + 2)


//create procedure sp_grupo_departamentos
//(@posicion int)
//as
//select DEPT_NO, DNOMBRE, LOC from  v_departamentos_individual
//where posicion >= @posicion and posicion < (@posicion + 2)
//go


//create view v_grupo_empleados
//as
//select cast(
//ROW_NUMBER()over(order by emp_no) as int) as posicion,
//ISNULL(emp_no, 0) as emp_no, apellido, oficio, fecha_alt, salario, dept_no
//from emp
//go

//create procedure sp_grupo_empleados
//(@posicion int)
//as
//select emp_no, apellido, oficio, fecha_alt, salario, dept_no from  v_grupo_empleados
//where posicion >= @posicion and posicion < (@posicion + 3)
//go


//create procedure sp_grupo_empleados_oficio
//(@oficio nvarchar(50) , @posicion int)
//as
//select* from
//(select cast(ROW_NUMBER() over(order by apellido) as int) as posicion,
//EMP_NO, APELLIDO, OFICIO, SALARIO, DEPT_NO
// from EMP
//where OFICIO = @oficio) as query
//where query.posicion >= @posicion and query.posicion<(@posicion + 3)
#endregion
namespace MvcCorePaginacionRegistros.Repositories
{
    public class RepositoryDepartamentos
    {
        private EmpleadoContext context;

        public RepositoryDepartamentos(EmpleadoContext context)
        {
            this.context = context;
        }

        public async Task<List<VistaDepartamento>> GetGroupVistaDepartamentoAsync(int posicion)
        {
            //select * from v_departamentos_individual
            //where posiscion >= 1 and posicion < 3
            var consulta = from datos in this.context.VistaDepartamentos
                           where datos.Posicion >= posicion
                           && datos.Posicion < (posicion + 2)
                           select datos;
            return await consulta.ToListAsync();
        }

        public int GetNumeroRegistrosVistaDepartamentos()
        {
            return this.context.VistaDepartamentos.Count();
        }
        public int GetNumeroRegistrosEmpleados()
        {
            return this.context.Empleados.Count();
        }

        public int GetNumeroOficios(string oficio)
        {
            var consulta = (from data in this.context.Empleados
                            where data.Oficio == oficio
                            select data).Count();
            return consulta;
        }

        public async Task<VistaDepartamento> GetVistaDepartamentoAsync(int posicion)
        {
            VistaDepartamento vista = await this.context.VistaDepartamentos.FirstOrDefaultAsync(x => x.Posicion == posicion);
            return vista;
        } 

        public async Task<List<Departamento>> GetGrupoDepartamentosAsync(int posicion)
        {
            string sql = "sp_grupo_departamentos @posicion";
            SqlParameter pamposicion = new SqlParameter("@posicion", posicion);
            var consulta = this.context.Departamentos.FromSqlRaw(sql, pamposicion);
            return await consulta.ToListAsync();
        }

        public async Task<List<Empleado>> GetGrupoEmpleadosAsync(int posicion)
        {
            string sql = "sp_grupo_empleados @posicion";
            SqlParameter pamposicion = new SqlParameter("@posicion", posicion);
            var consulta = this.context.Empleados.FromSqlRaw(sql, pamposicion);
            return await consulta.ToListAsync();
        }

        public async Task<List<Empleado>> GetGrupoEmpleadoOficio(string oficio, int posicion)
        {
            string sql = "sp_grupo_empleados_oficio @oficio, @posicion";
            SqlParameter pamposicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamoficio = new SqlParameter("@oficio", oficio);
            var consulta = this.context.Empleados.FromSqlRaw(sql, pamoficio, pamposicion);
            return await consulta.ToListAsync();

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
