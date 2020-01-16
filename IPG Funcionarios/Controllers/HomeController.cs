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

            var prof = (from q in _contextB.Professor select q).Count();
            var func = (from q in _contextB.Funcionario select q).Count();
            var dept = (from q in _contextB.Departamento select q).Count();
            var serv = (from q in _contextB.Servico select q).Count();
            var escl = (from q in _contextB.Escola select q).Count();
            var tarf = (from q in _contextB.Tarefa select q).Count();
            var carg = (from q in _contextB.Cargo select q).Count();
            var fers = (from q in _contextB.Ferias select q).Count();


            ViewData["AllProfessores"] = MyFn.ParseDbCount(prof);
            ViewData["AllFuncionario"] = MyFn.ParseDbCount(func);
            ViewData["AllDepartamento"] = MyFn.ParseDbCount(dept);
            ViewData["AllServicos"] = MyFn.ParseDbCount(serv);
            ViewData["AllEscolas"] = MyFn.ParseDbCount(escl);
            ViewData["AllTarefas"] = MyFn.ParseDbCount(tarf);
            ViewData["AllCargos"] = MyFn.ParseDbCount(carg);
            ViewData["AllFerias"] = MyFn.ParseDbCount(fers);

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

    }
}
