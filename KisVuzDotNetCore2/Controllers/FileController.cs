using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using KisVuzDotNetCore2.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Controllers
{
    public class FilesController : Controller
    {
        AppIdentityDBContext _context;
        IHostingEnvironment _appEnvironment;

        public FilesController(AppIdentityDBContext context,
        IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View(_context.Files.ToList());
        }

        public async Task<IActionResult> Details(int id)
        {
            //List<FileDataTypeGroup> fileDataTypeGroups = _context.FileDataTypeGroups
            //    .Include(fileDataTypeGroup => fileDataTypeGroup.FileDataTypes)
            //    .ThenInclude(fileDataType => fileDataType.FileToFileTypes)
            //    .ToList();
            //List<FileDataType> fileDataTypes = await _context.FileDataTypes.Include(p => p.FileDataTypeGroup).Include(p => p.FileToFileTypes).ToListAsync();
            FileModel file = await _context.Files
                .Include(f=>f.FileToFileTypes)
                .ThenInclude(fileTofileType=> fileTofileType.FileDataType)
                .ThenInclude(fileDataType => fileDataType.FileDataTypeGroup)
                .SingleOrDefaultAsync(f => f.Id == id);
            if (file != null)
            {
                return View(file);
            }
            return NotFound();
        }

        public IActionResult AddFile()
        {
            ViewData["FileDataTypeGroups"] = _context.FileDataTypeGroups.ToList();
            ViewData["FileDataTypes"] = _context.FileDataTypes.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile, int[] SelectedFileDataTypeIds, FileModel fileModel)
        {
            if (uploadedFile != null)
            {                         
                string fileName = DateTime.Now.ToString("yyyy-MM-dd-") +
                    Guid.NewGuid().ToString() +                    
                    Path.GetExtension(uploadedFile.FileName);
                // путь к папке files
                string[] paths = { _appEnvironment.WebRootPath, "files", fileName };
                string path = Path.Combine(paths);
                // сохраняем файл в папку files в каталоге wwwroot
                using (var fileStream = new FileStream( path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                                
                fileModel.FileName = Path.GetFileName(uploadedFile.FileName);
                fileModel.Path = path;

                if (Path.GetExtension(uploadedFile.FileName)==".pdf")
                {
                    fileModel.ContentType = "application/pdf";
                }
                
                _context.Files.Add(fileModel);
                _context.SaveChanges();

                // Добавляем привязку к списку типов файлов
                foreach (var selectedFileDataTypeId in SelectedFileDataTypeIds)
                {
                    FileToFileType newElement = new FileToFileType();
                    newElement.FileDataTypeId = selectedFileDataTypeId;
                    newElement.FileModelId = fileModel.Id;
                    _context.FileToFileTypes.Add(newElement);
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Удаляет файл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveFile(int id)
        {
            FileModel file=_context.Files.SingleOrDefault(f => f.Id == id);
            if(file!=null)
            {
                _context.Files.Remove(file);
                if (System.IO.File.Exists(file.Path))
                {
                    System.IO.File.Delete(file.Path);
                }
                await _context.SaveChangesAsync();
                    
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Возвращает файл, имеющий указанный идентификатор
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFile(int id)
        {
            FileModel file = _context.Files.SingleOrDefault(f => f.Id == id);
            if (file != null)
            {
                // Путь к файлу
                string file_path = file.Path;
                // Тип файла - content-type
                string file_type = file.ContentType;
                // Имя файла - необязательно
                string file_name = file.FileName;
                return PhysicalFile(file_path, file_type, file_name);
            }
            return NotFound();
        }



        // GET: EduLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileModel = await _context.Files.SingleOrDefaultAsync(m => m.Id == id);
            if (fileModel == null)
            {
                return NotFound();
            }
            return View(fileModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FileModel fileModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FileModel file = _context.Files.SingleOrDefault(f => f.Id == fileModel.Id);

                    if (file == null)
                    {
                        return NotFound(); ;
                    }

                    file.Name = fileModel.Name;
                    file.ContentType = fileModel.ContentType;

                    _context.Update(file);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {                    
                    return NotFound();                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
