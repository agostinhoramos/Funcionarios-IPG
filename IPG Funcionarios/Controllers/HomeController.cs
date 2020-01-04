using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IPG_Funcionarios.Models;
using Microsoft.AspNetCore.Http;
using IPG_Funcionarios.Data;
using System.Net.Http;

namespace IPG_Funcionarios.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _contextA;
        private readonly IPGFuncionariosDbContext _contextB;
        public HomeController
            (
            ApplicationDbContext contextA,
            IPGFuncionariosDbContext contextB
            )
        {
            _contextA = contextA;
            _contextB = contextB;
        }

        public IActionResult Default() {

            var prof = from q in _contextB.Professor select q;
            var func = from q in _contextB.Funcionario select q;
            var dept = from q in _contextB.Departamento select q;

            ViewData["AllProfessores"] = ParseDbCount(prof.Count());
            ViewData["AllFuncionario"] = ParseDbCount(func.Count());
            ViewData["AllDepartamento"] = ParseDbCount(dept.Count());
            ViewData["AllServicos"] = "0";
            ViewData["AllEscola"] = "0";
            ViewData["AllTarefa"] = "0";
            ViewData["AllCargos"] = "0";

            if (User.Identity.IsAuthenticated)
            {
                return View("Index");
            }
            
            return View();
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            else {
                return View("Default");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static int ParseDbCount(decimal data) {
            return (int)Math.Ceiling(data);
        }
    }
}
