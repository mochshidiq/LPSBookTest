using LPSBookTest.Models;
using LPSBookTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LPSBookTest.Controllers
{
    public class JenisBukuController : Controller
    {
        private readonly IJenisBukuRepository _repository;

        public JenisBukuController(
            IJenisBukuRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _repository.GetAllAsync();

            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JenisBukuModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _repository.AddAsync(model);
            await _repository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _repository.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            JenisBukuModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingData = await _repository.GetByIdAsync(id);

            if (existingData == null)
            {
                return NotFound();
            }

            existingData.JenisBuku = model.JenisBuku;

            _repository.Update(existingData);
            await _repository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _repository.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await _repository.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            _repository.Delete(data);
            await _repository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}