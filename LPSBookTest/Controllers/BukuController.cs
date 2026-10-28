using LPSBookTest.Models;
using LPSBookTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Text;

namespace LPSBookTest.Controllers
{
    public class BukuController : Controller
    {
        private readonly IBukuRepository _bukuRepository;
        private readonly IJenisBukuRepository _jenisBukuRepository;
        private readonly IBukuHistoryRepository _bukuHistoryRepository;

        public BukuController(
            IBukuRepository bukuRepository,
            IJenisBukuRepository jenisBukuRepository, IBukuHistoryRepository bukuHistoryRepository)
        {
            _bukuRepository = bukuRepository;
            _jenisBukuRepository = jenisBukuRepository;
            _bukuHistoryRepository = bukuHistoryRepository;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var data = await _bukuRepository
                .GetAllWithJenisBukuAsync(search);

            ViewBag.Search = search;

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Export()
        {
            var data = await _bukuRepository
                .GetAllWithJenisBukuAsync();

            var csv = new StringBuilder();

            csv.AppendLine(
                "Judul,Penulis,Jenis Buku,Tanggal Rilis,Jumlah Halaman");

            foreach (var buku in data)
            {
                
                var judul = EscapeCsv(buku.Judul);
                var penulis = EscapeCsv(buku.Penulis);
                var jenisBuku = EscapeCsv(
                    buku.JenisBuku?.JenisBuku ?? "");

                var tanggalRilis =
                    buku.TanggalRilis.ToString("yyyy-MM-dd HH:mm:ss");

                var jumlahHalaman =
                    buku.JumlahHalaman.ToString();

                csv.AppendLine(
                    $"{judul},{penulis},{jenisBuku},{tanggalRilis},{jumlahHalaman}");
            }

            var bytes = Encoding.UTF8.GetPreamble()
                .Concat(Encoding.UTF8.GetBytes(csv.ToString()))
                .ToArray();

            return File(
                bytes,
                "text/csv; charset=utf-8",
                $"DataBuku_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
        }

        private static string EscapeCsv(string? value)
        {
            value ??= "";
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }


        public async Task<IActionResult> Create()
        {
            await LoadJenisBukuAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BukuModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadJenisBukuAsync(model.JenisBukuId);

                return View(model);
            }

            await _bukuRepository.AddAsync(model);
            await _bukuRepository.SaveAsync();
            await CatatHistoryAsync(
            model.Id,
            "Create",
            null,
            BuatSnapshot(model));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _bukuRepository.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            await LoadJenisBukuAsync(data.JenisBukuId);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BukuModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                await LoadJenisBukuAsync(model.JenisBukuId);

                return View(model);
            }

            var existingData =
                await _bukuRepository.GetByIdAsync(id);

            if (existingData == null)
            {
                return NotFound();
            }
            var bukuLama = await _bukuRepository.GetByIdAsync(model.Id);

            if (bukuLama == null)
            {
                return NotFound();
            }

            


            existingData.Judul = model.Judul;
            existingData.Penulis = model.Penulis;
            existingData.JenisBukuId = model.JenisBukuId;
            existingData.TanggalRilis = model.TanggalRilis;
            existingData.JumlahHalaman = model.JumlahHalaman;

            _bukuRepository.Update(existingData);

            await _bukuRepository.SaveAsync();
            
            string oldData = BuatSnapshot(bukuLama);

            bukuLama.Judul = model.Judul;
            bukuLama.Penulis = model.Penulis;
            bukuLama.JenisBukuId = model.JenisBukuId;
            bukuLama.TanggalRilis = model.TanggalRilis;
            bukuLama.JumlahHalaman = model.JumlahHalaman;
            _bukuRepository.Update(bukuLama);
            await _bukuRepository.SaveAsync();

            string newData = BuatSnapshot(bukuLama);

            await CatatHistoryAsync(
                bukuLama.Id,
                "Edit",
                oldData,
                newData);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _bukuRepository.GetByIdAsync(id);

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
            var data = await _bukuRepository.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            string oldData = BuatSnapshot(data);

            await CatatHistoryAsync(
                data.Id,
                "Delete",
                oldData,
                null);      

            _bukuRepository.Delete(data);

            await _bukuRepository.SaveAsync();         

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadJenisBukuAsync(int? selectedId = null)
        {
            var jenisBuku =
                await _jenisBukuRepository.GetAllAsync();

            ViewBag.JenisBuku = new SelectList(
                jenisBuku,
                "Id",
                "JenisBuku",
                selectedId);
        }


        private static string BuatSnapshot(BukuModel buku)
        {
            return
                $"Judul={buku.Judul}; " +
                $"Penulis={buku.Penulis}; " +
                $"JenisBukuId={buku.JenisBukuId}; " +
                $"TanggalRilis={buku.TanggalRilis:yyyy-MM-dd}; " +
                $"JumlahHalaman={buku.JumlahHalaman}";
        }


        private async Task CatatHistoryAsync(
            int? bukuId,
            string action,
            string? oldData,
            string? newData)
        {
            var history = new BukuHistoryModel
            {
                BukuId = bukuId,
                Action = action,
                OldData = oldData,
                NewData = newData,
                ChangedAt = DateTime.Now
            };

            await _bukuHistoryRepository.AddAsync(history);
            await _bukuHistoryRepository.SaveAsync();
        }


    }
}