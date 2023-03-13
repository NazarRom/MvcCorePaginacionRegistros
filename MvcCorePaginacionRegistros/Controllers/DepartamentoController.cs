using Microsoft.AspNetCore.Mvc;
using MvcCorePaginacionRegistros.Models;
using MvcCorePaginacionRegistros.Repositories;

namespace MvcCorePaginacionRegistros.Controllers
{
    public class DepartamentoController : Controller
    {
        private RepositoryDepartamentos repo;

        public DepartamentoController(RepositoryDepartamentos repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int deptno)
        {
            List<Empleado> empleados = this.repo.FindEmpleados(deptno);
            return View(empleados);
        }
    }
}
