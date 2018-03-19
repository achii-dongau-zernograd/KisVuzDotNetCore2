using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using KisVuzDotNetCore2.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;

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

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
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
                FileModel file = new FileModel { Name = Path.GetFileName(uploadedFile.FileName), Path = path };
                _context.Files.Add(file);
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
    }
}
