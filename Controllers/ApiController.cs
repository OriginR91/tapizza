using Microsoft.AspNetCore.Mvc;
using pizza_mama.Data;
using pizza_mama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pizza_mama.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly DataContext _context;

        public ApiController(DataContext context)
        {
            this._context = context;
        }

        public List<Pizza> Pizza { get; set; }

        // GET /api/GetPizzas
        [HttpGet]
        [Route("GetPizzas")]
        public IActionResult GetPizzas()
        {
            var listPizzas = _context.Pizzas.ToList();

            return Json(listPizzas);
        }

        // GET/id /api/GetPizza/{id}
        [HttpGet]
        [Route("GetPizza/{ID}")]
        public IActionResult GetPizza(int id)
        {
            var pizza = "";

            return Json(pizza);
        }
    }
}
