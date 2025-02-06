using Microsoft.AspNetCore.Mvc;
using MvsCoreLinqToSQL.Models;
using MvsCoreLinqToSQL.Repositories;

namespace MvsCoreLinqToSQL.Controllers
{
    public class EmpleadosController : Controller
    {
        RepositoryEmpleados repo;

        public EmpleadosController()
        {
            this.repo = new RepositoryEmpleados();
        }

        public IActionResult Index()
        {
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult Details(int idempleado)
        {
            Empleado empleado = this.repo.FindEmpleado(idempleado);
            return View(empleado);
        }

        public IActionResult BuscadorEmpleados()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BuscadorEmpleados(string oficio, int salario)
        {
            List<Empleado> empleados = this.repo.GetEmpleadosOficioSalario(oficio, salario);

            if (empleados == null)
            {
                ViewData["MENSAJE"] = "No existen empleados con oficio "
                    + oficio + " y salario superior o igual " + salario;
                return View();
            }
            else
            {
                return View(empleados);
            }            
        }

        public IActionResult EmpleadosOficio()
        {
            ViewData["OFICIOS"] = this.repo.GetOficios();
            return View();
        }

        [HttpPost]
        public IActionResult EmpleadosOficio(string oficio)
        {
            ViewData["OFICIOS"] = this.repo.GetOficios();
            ResumenEmpleado resumen = this.repo.GetEmpleadosOficio(oficio);
            return View(resumen);
        }
    }
}
