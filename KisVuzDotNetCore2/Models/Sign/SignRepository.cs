using KisVuzDotNetCore2.Models.Files;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sign
{
    public class SignRepository : ISignRepository
    {
        AppIdentityDBContext _context;
        IFileModelRepository _fileModelRepository;

        public SignRepository(AppIdentityDBContext context, IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }

        /// <summary>
        /// Создаёт ЭЦП для всех неподписанных документов
        /// </summary>
        /// <returns></returns>
        public async Task<int> CreateSignForUnsignedDocuments()
        {
            //var unsignedDocs = await _context.Files
            //    .Include(f => f.FileToFileTypes)
            //        .ThenInclude(ftft=>ftft.FileDataType)
            //    .Include(f => f.SignList)
            //    .Where(f => f.SignList == null || f.SignList.Count == 0)
            //    .Take(1000)
            //    .ToListAsync();
            var unsignedDocs = await _context.FileToFileTypes
                .Include(ftft=>ftft.FileDataType)
                .Include(ftft => ftft.FileModel.SignList)
                .Where(ftft => ftft.FileDataType.FileDataTypeGroupId == 1
                                || ftft.FileDataType.FileDataTypeGroupId == 2
                                || ftft.FileDataType.FileDataTypeGroupId == 3)                
                .Select(ftft=>ftft.FileModel)
                .ToListAsync();

            int counter = 0;
            foreach (var unsignedDoc in unsignedDocs)
            {
                if (unsignedDoc.SignList == null)
                    unsignedDoc.SignList = new List<Sign>();

                if (unsignedDoc.SignList.Count == 0)
                {
                    string newKey = Guid.NewGuid().ToString().Replace("-","");
                    Sign newSign = new Sign { LastName = "Бутенко", FirstName = "Александр", Patronymic = "Фёдорович", Post = "Директор", Key = newKey  };
                    unsignedDoc.SignList.Add(newSign);

                    counter++;
                    await _context.SaveChangesAsync();
                }                
            }
            return counter;
        }
    }
}
