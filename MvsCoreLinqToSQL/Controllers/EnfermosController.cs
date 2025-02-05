using Microsoft.AspNetCore.Mvc;
using MvsCoreLinqToSQL.Models;
using MvsCoreLinqToSQL.Repositories;

namespace MvsCoreLinqToSQL.Controllers
{
    public class EnfermosController : Controller
    {
        RepositoryEnfermo repo;

        public EnfermosController()
        {
            this.repo = new RepositoryEnfermo();
        }

        public IActionResult Index()
        {
            List<Enfermo> enfermos = this.repo.GetEnfermos();
            return View(enfermos);
        }

        public IActionResult Details(string inscripcion)
        {
            Enfermo enfermo = this.repo.FindEnfermo(inscripcion);
            return View(enfermo);
        }

        public async Task<IActionResult> Delete(string inscripcion)
        {
            await this.repo.DeleteEnfermo(inscripcion);
            return RedirectToAction("Index");
        }
    }
}
