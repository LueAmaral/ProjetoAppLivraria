using Microsoft.AspNetCore.Mvc;
using ProjetoAppLivraria.Models;
using ProjetoAppLivraria.Repository.Contract;
using Microsoft.Extensions.Logging;

namespace ProjetoAppLivraria.Controllers
{
    public class AutorController : Controller
    {
        private readonly ILogger<AutorController> _logger;
        private readonly IAutorRepository _autorRepository;

        public AutorController(ILogger<AutorController> logger, IAutorRepository autorRepository)
        {
            _logger = logger;
            _autorRepository = autorRepository;
        }

        public IActionResult Index()
        {
            return View(_autorRepository.ObterTodosAutores());
        }

        public IActionResult EditarAutor(int id)
        {
            var autor = _autorRepository.ObterAutor(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        [HttpGet]
        public IActionResult CadAutor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadAutor(Autor autor)
        {
            if (ModelState.IsValid)
            {
                _autorRepository.CadastrarAutor(autor); // Usando o método do repositório
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        [HttpPost]
        public IActionResult EditarAutor(Autor autor)
        {
            if (ModelState.IsValid)
            {
                _autorRepository.EditarAutor(autor);
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        public IActionResult ExcluirAutor(int id)
        {
            var autor = _autorRepository.ObterAutor(id);
            if (autor == null)
            {
                return NotFound();
            }

            _autorRepository.ExcluirAutor(id);
            return RedirectToAction("Index");
        }
    }
}