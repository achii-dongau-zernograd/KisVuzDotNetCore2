using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Abitur;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Priem
{
    public class ApplicationForAdmissionsController : Controller
    {
        private readonly IApplicationForAdmissionRepository _applicationForAdmissionRepository;
        private readonly ISelectListRepository _selectListRepository;

        public ApplicationForAdmissionsController(IApplicationForAdmissionRepository applicationForAdmissionRepository,
            ISelectListRepository selectListRepository)
        {
            _applicationForAdmissionRepository = applicationForAdmissionRepository;
            _selectListRepository = selectListRepository;
        }

        public async Task<IActionResult> Index()
        {
            var applicationForAdmission = _applicationForAdmissionRepository.GetApplicationForAdmissions();
            applicationForAdmission = applicationForAdmission.OrderByDescending(q => q.FileModel.UploadDate);

            return View(await applicationForAdmission.ToListAsync());
        }
    }
}
