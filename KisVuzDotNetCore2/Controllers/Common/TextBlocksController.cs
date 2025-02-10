using KisVuzDotNetCore2.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Common
{
    [Authorize(Roles = "Администраторы, ОВЗ")]
    public class TextBlocksController : Controller
    {
        private readonly ITextBlockRepository _textBlockRepository;

        public TextBlocksController(ITextBlockRepository textBlockRepository)
        {
            _textBlockRepository = textBlockRepository;
        }

        public async Task<IActionResult> Index(string textBlockTag)
        {
            IQueryable<TextBlock> entries = _textBlockRepository.GetTextBlocks(textBlockTag);
            ViewBag.textBlockTag = textBlockTag;

            return View(await entries.ToListAsync());
        }

        #region Создание
        public IActionResult Create(string textBlockTag)
        {
            ViewBag.textBlockTag = textBlockTag;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TextBlock textBlock)
        {
            await _textBlockRepository.CreateTextBlockAsync(textBlock);

            return RedirectToAction(nameof(Index), new { textBlockTag = textBlock.TextBlockTag });
        }
        #endregion

        #region Редактирование
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();

            var entry = await _textBlockRepository.GetTextBlockAsync(id);

            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TextBlock textBlock)
        {
            await _textBlockRepository.UpdateTextBlockAsync(textBlock);

            return RedirectToAction(nameof(Index), new { textBlockTag = textBlock.TextBlockTag });
        }
        #endregion

        #region Удаление
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return NotFound();

            var entry = await _textBlockRepository.GetTextBlockAsync(id);

            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TextBlock textBlock)
        {
            if (textBlock == null) return NotFound();

            var entry = await _textBlockRepository.GetTextBlockAsync(textBlock.TextBlockId);
            await _textBlockRepository.RemoveTextBlockAsync(entry);

            return RedirectToAction(nameof(Index), new { textBlockTag = entry.TextBlockTag });
        }
        #endregion
    }
}
