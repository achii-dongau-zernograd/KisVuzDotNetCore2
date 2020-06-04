using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Репозиторий договоров
    /// </summary>
    public class ContractRepository : IContractRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IFileModelRepository _fileModelRepository;

        public ContractRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех договоров с заполненными зависимостями
        /// </summary>
        /// <returns></returns>
        public IQueryable<Contract> GetContracts()
        {
            var contracts = _context.Contracts
                .Include(c => c.RowStatus)
                .Include(c => c.AppUser)
                .Include(c => c.ContractType)
                .Include(c => c.ApplicationForAdmission.EduForm)
                .Include(c => c.ApplicationForAdmission.QuotaType)
                .Include(c => c.ApplicationForAdmission.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(c => c.ApplicationForAdmission)
                    .ThenInclude(afa => afa.FileModel.FileToFileTypes)
                .Include(c => c.FileModel.FileToFileTypes);

            return contracts;
        }

        /// <summary>
        /// Создаёт договор
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task CreateContractAsync(Contract contract, IFormFile uploadedFile)
        {
            if (contract == null) return;
            if (uploadedFile == null) return;

            var fileModel = await _fileModelRepository.UploadConsentToEnrollmentFileAsync(uploadedFile);
            if (fileModel == null) return;

            contract.FileModelId = fileModel.Id;

            if (contract.RowStatusId == 0)
            {
                contract.RowStatusId = (int)RowStatusEnum.NotConfirmed;
            }

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает договор
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Contract> GetContractAsync(int id)
        {
            var contract = await GetContracts().SingleOrDefaultAsync(c => c.ContractId == id);

            return contract;
        }

        /// <summary>
        /// Обновляет договор
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task UpdateContractAsync(Contract contract, IFormFile uploadedFile)
        {
            if (contract == null) return;

            if (uploadedFile != null)
            {
                if (contract.FileModelId != null)
                {
                    if (contract.FileModel == null)
                    {
                        var entryFileModel = await _fileModelRepository.GetFileModelAsync(contract.FileModelId);
                        contract.FileModel = entryFileModel;
                    }

                    await _fileModelRepository.ReloadFileAsync(contract.FileModel, uploadedFile);
                }
                else
                {
                    var loadedFileModel = await _fileModelRepository.UploadIndividualAchievmentFile(uploadedFile);
                    contract.FileModel = loadedFileModel;
                    contract.FileModelId = loadedFileModel.Id;
                }
            }

            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет договор
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public async Task RemoveContractAsync(Contract contract)
        {
            if (contract == null) return;

            if(contract.FileModelId != null)
            {
                if(contract.FileModel == null)
                {
                    var entryFileModel = await _fileModelRepository.GetFileModelAsync(contract.FileModelId);
                    contract.FileModel = entryFileModel;
                }
                await _fileModelRepository.RemoveFileModelAsync(contract.FileModel);
            }

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
        }
    }
}
