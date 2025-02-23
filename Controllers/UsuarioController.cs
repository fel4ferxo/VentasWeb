using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VentasWeb.Models;
using VentasWeb.Services;

namespace VentasWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly MongoDbService _mongoDbService;
        
        public UsuarioController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _mongoDbService.GetAsync();
            return View(usuarios);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if(ModelState.IsValid)
            {
                await _mongoDbService.CreateAsync(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }
    }
}
