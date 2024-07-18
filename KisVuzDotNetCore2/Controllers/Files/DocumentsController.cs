using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KisVuzDotNetCore2.Controllers
{
    /// <summary>
    /// Контроллер для работы с документами
    /// </summary>
    [Authorize(Roles = "Администраторы, Бухгалтерия, Учебная часть, Юротдел, Канцелярия, Приёмная комиссия, ЗамДиректораПоСоцРаботе, ЗамДиректораПоВоспРаботе, Отдел кадров, ЗамДиректораПоМолПолВоспИСоцРаботе, ЗамДиректораПоУРиЦТ")]
    public class DocumentsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IFileModelRepository _fileModelRepository;

        public DocumentsController(AppIdentityDBContext context,
            IHostingEnvironment appEnvironment,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _fileModelRepository = fileModelRepository;
        }

        public async Task<IActionResult>  Index(FileDataTypeEnum? doctype)
        {
            if (doctype == null)
                return NotFound();

            var fileToFileTypes = await _context.FileToFileTypes
                .Where(ftft => ftft.FileDataTypeId == (int)doctype)
                .Include(ftft => ftft.FileModel)
                .Include(ftft => ftft.FileDataType.FileDataTypeGroup)
                .ToListAsync();
            ViewData["fileToFileTypes"] = fileToFileTypes;

            ViewData["doctype"] = doctype;

            return View();
        }

        public IActionResult Create(FileDataTypeEnum? doctype)
        {
            ViewData["doctype"] = doctype;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FileDataTypeEnum? doctype, string fileName, IFormFile uploadedFile)
        {
            if (uploadedFile == null || doctype == null)
                return NotFound();

            FileModel fileModelUploaded = await KisVuzDotNetCore2.Models.Files.Files.LoadFile(_context, _appEnvironment, uploadedFile, fileName, (FileDataTypeEnum)doctype);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { doctype });
        }

        public async Task<IActionResult> Edit(int? id, FileDataTypeEnum? doctype)
        {
            if (id==null || doctype == null)
                return NotFound();

            var fileModel = await _context.Files
                .Include(f => f.FileToFileTypes)
                .SingleOrDefaultAsync(f => f.Id == id);
                        
            ViewData["doctype"] = doctype;

            return View(fileModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FileModel fileModel, FileDataTypeEnum? doctype, IFormFile uploadedFile)
        {
            if (fileModel == null || doctype == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    FileModel fileModelUploaded = await KisVuzDotNetCore2.Models.Files.Files.LoadFile(_context, _appEnvironment, uploadedFile, fileModel.Name, (FileDataTypeEnum)doctype);
                    await _context.SaveChangesAsync();
                    KisVuzDotNetCore2.Models.Files.Files.RemoveFile(_context, _appEnvironment, fileModel.Id);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var fileModelToEdit = await _context.Files
                    .Include(f => f.FileToFileTypes)
                    .SingleOrDefaultAsync(f => f.Id == fileModel.Id);
                    fileModelToEdit.Name = fileModel.Name;
                    await _context.SaveChangesAsync();
                }                

                return RedirectToAction(nameof(Index),new { doctype });
            }

            return View(fileModel);
        }

        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, FileDataTypeEnum? doctype)
        {
            if(id!=null)
            {
                var fileModel = await _fileModelRepository.GetFileModelAsync(id);
                await _fileModelRepository.RemoveFileModelAsync(fileModel);
                //KisVuzDotNetCore2.Models.Files.Files.RemoveFile(_context, _appEnvironment, id);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new { doctype });
        }
    }
}