using Microsoft.AspNetCore.Mvc;
using MvcCorePaginacionRegistros.Models;
using MvcCorePaginacionRegistros.Repositories;
namespace MvcCorePaginacionRegistros.Controllers
{
    public class PaginacionController : Controller
    {
        private RepositoryDepartamentos repo;

        public PaginacionController(RepositoryDepartamentos repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PaginarEmpleadoOficio(int? posicion)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PaginarEmpleadoOficio(string oficio, int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int numeroRegistros = this.repo.GetNumeroOficios(oficio);
            ViewData["REGISTROS"] = numeroRegistros;
            ViewData["OFICIO"] = oficio;
            List<Empleado> empleados = await this.repo.GetGrupoEmpleadoOficio(oficio, posicion.Value);
            return View(empleados);
        }

        public async Task<IActionResult> PaginarGrupoDepartamentos(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int numeroRegistros = this.repo.GetNumeroRegistrosVistaDepartamentos();
            ViewData["REGISTROS"] = numeroRegistros;
            List<Departamento> departamentos = await this.repo.GetGrupoDepartamentosAsync(posicion.Value);
            return View(departamentos);
        }

        public async Task<IActionResult> PaginarGrupoEmpleados(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int numeroRegistros = this.repo.GetNumeroRegistrosEmpleados();
            ViewData["REGISTROS"] = numeroRegistros;
            List<Empleado> empleados = await this.repo.GetGrupoEmpleadosAsync(posicion.Value);
            return View(empleados);
        }

        public async Task<IActionResult> PaginacionGroupVistaDepartamento(int? posicion)
        {
            if(posicion == null)
            {
                posicion = 1;
            }
            int numRegistros = this.repo.GetNumeroRegistrosVistaDepartamentos();
            ViewData["REGISTROS"] = numRegistros;
            //int numeroPagina = 1;
            //necesitamos crear un bucle que vaya de n en n
            //dependiendo del numero de registros a paginar
            //legaremos hasta el numero de registros
            //string html = "<div>";
            //for (int i = 1; i <= numRegistros; i += 2)
            //{
            //    html += "<a href='PaginacionGroupVistaDepartamento?posicion="
            //        + i + "'>Página " + numeroPagina + "</a> | ";
            //    numeroPagina += 1;
            //}
            //html += "<div>";
            //ViewData["LINKS"] = html;
            List<VistaDepartamento> departamentos = await this.repo.GetGroupVistaDepartamentoAsync(posicion.Value);
            return View(departamentos);
        }

        public async Task<IActionResult> PaginarRegistroVistaDepartamento(int? posicion)
        {
            if(posicion == null)
            {
                posicion = 1;
            }
            int numeroregistros = this.repo.GetNumeroRegistrosVistaDepartamentos();
            //estamos en la posicion 1, que tenemos que devover a la vista?
            int siguiente = posicion.Value + 1;
            if (siguiente > numeroregistros)
            {
                //efecto optico
                siguiente = numeroregistros;
            }
            int anterior = posicion.Value - 1;
            if(anterior < 1)
            {
                anterior = 1;
            }
            VistaDepartamento vistaDepartamento = await this.repo.GetVistaDepartamentoAsync(posicion.Value);
            ViewData["ULTIMO"] = numeroregistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            return View(vistaDepartamento);
        }
    }
}
