using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoAppLivraria.Models;
using ProjetoAppLivraria.Repository.Contract;

namespace ProjetoAppLivraria.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILogger<LivroController> _logger;
        private ILivroRepository _livroRepository;
        private IAutorRepository _autorRepository;
        public LivroController(ILogger<LivroController> logger, ILivroRepository livroRepository, IAutorRepository autorRepository)
        {
            _logger = logger;
            _livroRepository = livroRepository;
            _autorRepository = autorRepository;
        }
        public IActionResult Index()
        {
            return View(_livroRepository.ObterTodosLivros());
        }
        [HttpGet]
        public IActionResult CadLivro()
        {
            // Carrega a lista de autor
            var listaAutor = _autorRepository.ObterTodosAutores();
            ViewBag.ListaAutor = new SelectList(listaAutor, "Id", "nomeAutor");
            return View();
        }
        [HttpPost]
        public IActionResult CadLivro(Livro livro)
        {
            // Lista de autores
            var listaAutor = _autorRepository.ObterTodosAutores();
            ViewBag.ListaAutor = new SelectList(listaAutor, "Id", "nomeAutor");
            _livroRepository.CadastrarLivro(livro);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditarLivro(int id)
        {
            // Carrega a lista de autor
            var listaAutor = _autorRepository.ObterTodosAutores();
            var ObjAutor = new Livro
            {
                ListaAutor = (List<Autor>)listaAutor
            };
            ViewBag.ListaAutores = new SelectList(listaAutor, "Id", "nomeAutor");

            return View(_livroRepository.ObterLivro(id));
        }

        [HttpPost]
        public IActionResult EditarLivro(Livro livro)
        {
            //***Lista de autores***
            var listaAutor = _autorRepository.ObterTodosAutores();
            ViewBag.ListaAutores = new SelectList(listaAutor, "Id", "nomeAutor");

            _livroRepository.AtualizarLivro(livro);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ExcluirLivro(int id)
        {
            _livroRepository.ExcluirLivro(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
