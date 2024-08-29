using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoAppLivraria.Models;
using ProjetoAppLivraria.Repository.Contract;
using Microsoft.Extensions.Logging;

namespace ProjetoAppLivraria.Controllers
{
    public class AutorController : Controller
    {
        private readonly ILogger<AutorController> _logger;
        private readonly IAutorRepository _autorRepository;
        private readonly IStatusRepository _statusRepository;

        public AutorController(ILogger<AutorController> logger, IAutorRepository autorRepository, IStatusRepository statusRepository)
        {
            _logger = logger;
            _autorRepository = autorRepository;
            _statusRepository = statusRepository;
        }

        public IActionResult Index()
        {
            return View(_autorRepository.ObterTodosAutores());
        }

        [HttpGet]
        public IActionResult CadAutor()
        {
            // Carrega a lista de status
            ViewBag.Statuses = _statusRepository.ObterTodosStatus()
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                });
            return View();
        }

        [HttpPost]
        public IActionResult CadAutor(Autor autor)
        {
            if (ModelState.IsValid)
            {
                _autorRepository.CadastrarAutor(autor);
                return RedirectToAction(nameof(Index));
            }

            // Recarrega a lista de status em caso de erro
            ViewBag.Statuses = _statusRepository.ObterTodosStatus()
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                });

            return View(autor);
        }

        [HttpGet]
        public IActionResult EditarAutor(int id)
        {
            var autor = _autorRepository.ObterAutor(id);
            if (autor == null)
            {
                return NotFound();
            }

            // Carrega a lista de status
            ViewBag.Statuses = _statusRepository.ObterTodosStatus()
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome,
                    Selected = s.Id == autor.StatusId
                });

            return View(autor);
        }

        [HttpPost]
        public IActionResult EditarAutor(Autor autor)
        {
            if (ModelState.IsValid)
            {
                _autorRepository.EditarAutor(autor);
                return RedirectToAction(nameof(Index));
            }

            // Recarrega a lista de status em caso de erro
            ViewBag.Statuses = _statusRepository.ObterTodosStatus()
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome,
                    Selected = s.Id == autor.StatusId
                });

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
            return RedirectToAction(nameof(Index));
        }
    }
}
