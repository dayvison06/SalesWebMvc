using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Controllers;
using System.Collections.Generic;

namespace SalesWebMvc.Controllers
{
    public class DepartamentsController : Controller
    {
        public IActionResult Index()
        {

            List<Departament> list = new List<Departament>();
            list.Add(new Departament { Id = 1, Name = "Eletronics" });
            list.Add(new Departament { Id = 2, Name = "Fashion" });
            list.Add(new Departament { Id = 3, Name = "Deposit" });
            list.Add(new Departament { Id = 4, Name = "Bank" });
            list.Add(new Departament { Id = 5, Name = "Office" });


            return View(list);
        }
    }
}
