using Microsoft.AspNetCore.Mvc;
using ProjetoAppLivraria.Models;
using ProjetoAppLivraria.Repository.Contract;

namespace ProjetoAppLivraria.Controllers
{
    public class StatusController : Controller
    {
        private readonly IStatusRepository _statusRepository;

        public StatusController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public IActionResult Index()
        {
            var statusList = _statusRepository.ObterTodosStatus();
            return View(statusList);
        }

        public IActionResult CadastrarStatus()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarStatus(Status status)
        {
            if (ModelState.IsValid)
            {
                _statusRepository.CadastrarStatus(status);
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        public IActionResult EditarStatus(int id)
        {
            var status = _statusRepository.ObterStatusPorID(id);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        [HttpPost]
        public IActionResult EditarStatus(int id, Status status)
        {
            if (id != status.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _statusRepository.EditarStatus(status);
                }
                catch
                {
                    return View(status);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        public IActionResult ExcluirStatus(int id)
        {
            var status = _statusRepository.ObterStatusPorID(id);
            if (status == null)
            {
                return NotFound();
            }

            _statusRepository.ExcluirStatus(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
