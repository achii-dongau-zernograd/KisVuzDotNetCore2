using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.UchPosobiya;
using Microsoft.AspNetCore.Http;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.UchPosobiya
{
    [Authorize(Roles = "Администраторы")]
    public class UchPosobiesController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public UchPosobiesController(AppIdentityDBContext context,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: UchPosobies
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.UchPosobie
                .Include(u => u.FileModel)
                .Include(u => u.UchPosobieFormaIzdaniya)
                .Include(u => u.UchPosobieVid);
            return View(await appIdentityDBContext.ToListAsync());
        }

        /// <summary>
        /// Передаёт в представление Preview объект, содержащий
        /// учебные пособия, изданные для направления подготовки id
        /// </summary>
        /// <param name="id">Идентификатор направления подготовки</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Preview(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            int EduNapravlId = (int)id;
            var uchPosList = await _context.UchPosobieEduNapravl
                .Where(pn => pn.EduNapravlId == EduNapravlId)
                .Include(pn=>pn.EduNapravl)
                .Include(pn => pn.UchPosobie.UchPosobieFormaIzdaniya)
                .Include(pn=>pn.UchPosobie.FileModel)
                .Include(pn => pn.UchPosobie.UchPosobieAuthors)
                .Include(pn => pn.UchPosobie.UchPosobieDisciplineNames)
                    .ThenInclude(pd=>pd.DisciplineName)
                .Include(pn => pn.UchPosobie.UchPosobieVid)
                .ToListAsync();

            var eduNapravl = await _context.EduNapravls.SingleOrDefaultAsync(n => n.EduNapravlId == EduNapravlId);
            if(eduNapravl!=null)
            {
                ViewBag.NapravlCaption = eduNapravl.EduNapravlCode + " - " + eduNapravl.EduNapravlName;
            }

            var uchPosobieFormaIzdaniya = await _context.UchPosobieFormaIzdaniya.ToListAsync();
            ViewBag.uchPosobieFormaIzdaniya = uchPosobieFormaIzdaniya;

            var uchPosVidList = await _context.UchPosobieVid.ToListAsync();
            ViewBag.uchPosVidList = uchPosVidList;            

            return View(uchPosList);
        }

        // GET: UchPosobies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobie = await _context.UchPosobie
                .Include(u => u.FileModel)
                .Include(u => u.UchPosobieFormaIzdaniya)
                .Include(u => u.UchPosobieVid)
                .SingleOrDefaultAsync(m => m.UchPosobieId == id);
            if (uchPosobie == null)
            {
                return NotFound();
            }

            return View(uchPosobie);
        }

        // GET: UchPosobies/Create
        public IActionResult Create()
        {
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id");
            ViewData["UchPosobieFormaIzdaniyaId"] = new SelectList(_context.UchPosobieFormaIzdaniya, "UchPosobieFormaIzdaniyaId", "UchPosobieFormaIzdaniyaName");
            ViewData["UchPosobieVidId"] = new SelectList(_context.UchPosobieVid, "UchPosobieVidId", "UchPosobieVidName");
            return View();
        }

        // POST: UchPosobies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UchPosobieId,UchPosobieVidId,GodIzdaniya,UchPosobieName,BiblOpisanie,UchPosobieFormaIzdaniyaId,FileModelId")] UchPosobie uchPosobie, IFormFile uploadedFile)
        {
            if (ModelState.IsValid && uploadedFile != null)
            {
                FileDataTypeEnum fileDataTypeEnum = FileDataTypeEnum.UchebnoePosobie;

                string uchPosobieVidName = (await _context.UchPosobieVid.SingleOrDefaultAsync(v=>v.UchPosobieVidId == uchPosobie.UchPosobieVidId)).UchPosobieVidName;
                var fileDataType = await _context.FileDataTypes.SingleOrDefaultAsync(fdt => fdt.FileDataTypeName == uchPosobieVidName);
                if(fileDataType!=null)
                {
                    fileDataTypeEnum = (FileDataTypeEnum)fileDataType.FileDataTypeId;
                }
                
                FileModel fileModel = await KisVuzDotNetCore2.Models.Files.Files.LoadFile(_context, _appEnvironment, uploadedFile, uchPosobie.UchPosobieName, fileDataTypeEnum);
                uchPosobie.FileModelId = fileModel.Id;
                _context.Add(uchPosobie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", uchPosobie.FileModelId);
            ViewData["UchPosobieFormaIzdaniyaId"] = new SelectList(_context.UchPosobieFormaIzdaniya, "UchPosobieFormaIzdaniyaId", "UchPosobieFormaIzdaniyaId", uchPosobie.UchPosobieFormaIzdaniyaId);
            ViewData["UchPosobieVidId"] = new SelectList(_context.UchPosobieVid, "UchPosobieVidId", "UchPosobieVidId", uchPosobie.UchPosobieVidId);
            return View(uchPosobie);
        }

        // GET: UchPosobies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobie = await _context.UchPosobie
                .Include(u => u.FileModel)
                .SingleOrDefaultAsync(m => m.UchPosobieId == id);
                        
            if (uchPosobie == null)
            {
                return NotFound();
            }
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", uchPosobie.FileModelId);
            ViewData["UchPosobieFormaIzdaniyaId"] = new SelectList(_context.UchPosobieFormaIzdaniya, "UchPosobieFormaIzdaniyaId", "UchPosobieFormaIzdaniyaName", uchPosobie.UchPosobieFormaIzdaniyaId);
            ViewData["UchPosobieVidId"] = new SelectList(_context.UchPosobieVid, "UchPosobieVidId", "UchPosobieVidName", uchPosobie.UchPosobieVidId);
            return View(uchPosobie);
        }

        // POST: UchPosobies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UchPosobieId,UchPosobieVidId,GodIzdaniya,UchPosobieName,BiblOpisanie,UchPosobieFormaIzdaniyaId,FileModelId")] UchPosobie uchPosobie, IFormFile uploadedFile)
        {
            if (id != uchPosobie.UchPosobieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                FileDataTypeEnum fileDataTypeEnum = FileDataTypeEnum.UchebnoePosobie;
                string uchPosobieVidName = (await _context.UchPosobieVid.SingleOrDefaultAsync(v => v.UchPosobieVidId == uchPosobie.UchPosobieVidId)).UchPosobieVidName;
                var fileDataType = await _context.FileDataTypes.SingleOrDefaultAsync(fdt => fdt.FileDataTypeName == uchPosobieVidName);
                if (fileDataType != null)
                {
                    fileDataTypeEnum = (FileDataTypeEnum)fileDataType.FileDataTypeId;
                }

                if (uploadedFile != null)
                {
                    FileModel fileModel = await KisVuzDotNetCore2.Models.Files.Files.LoadFile(_context, _appEnvironment, uploadedFile, uchPosobie.UchPosobieName, fileDataTypeEnum);
                    await _context.SaveChangesAsync();                    
                    int? fileToRemoveId = uchPosobie.FileModelId;
                    uchPosobie.FileModelId = fileModel.Id;
                    await _context.SaveChangesAsync();
                    KisVuzDotNetCore2.Models.Files.Files.RemoveFile(_context, _appEnvironment, fileToRemoveId);
                }
                
                try
                {
                    _context.Update(uchPosobie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UchPosobieExists(uchPosobie.UchPosobieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", uchPosobie.FileModelId);
            ViewData["UchPosobieFormaIzdaniyaId"] = new SelectList(_context.UchPosobieFormaIzdaniya, "UchPosobieFormaIzdaniyaId", "UchPosobieFormaIzdaniyaId", uchPosobie.UchPosobieFormaIzdaniyaId);
            ViewData["UchPosobieVidId"] = new SelectList(_context.UchPosobieVid, "UchPosobieVidId", "UchPosobieVidId", uchPosobie.UchPosobieVidId);
            return View(uchPosobie);
        }

        // GET: UchPosobies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobie = await _context.UchPosobie
                .Include(u => u.FileModel)
                .Include(u => u.UchPosobieFormaIzdaniya)
                .Include(u => u.UchPosobieVid)
                .SingleOrDefaultAsync(m => m.UchPosobieId == id);
            if (uchPosobie == null)
            {
                return NotFound();
            }

            return View(uchPosobie);
        }

        // POST: UchPosobies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uchPosobie = await _context.UchPosobie.SingleOrDefaultAsync(m => m.UchPosobieId == id);
            _context.UchPosobie.Remove(uchPosobie);

            KisVuzDotNetCore2.Models.Files.Files.RemoveFile(_context, _appEnvironment, uchPosobie?.FileModelId);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UchPosobieExists(int id)
        {
            return _context.UchPosobie.Any(e => e.UchPosobieId == id);
        }
    }
}
