using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Репозиторий обработки платажей
    /// </summary>
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IFileModelRepository _fileModelRepository;

        public PaymentRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }

        /// <summary>
        /// Добавляет платёж
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task AddPaymentAsync(Payment payment, IFormFile uploadedFile)
        {
            if(uploadedFile != null)
            {
                var fileModel = await _fileModelRepository.UploadPaymentAsync(uploadedFile);
                 if(fileModel != null)
                {
                    payment.FileModel = fileModel;
                    payment.FileModelId = fileModel.Id;
                }
            }

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }
    }
}
