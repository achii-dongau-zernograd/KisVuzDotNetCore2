using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    public class TextBlockRepository : ITextBlockRepository
    {
        private readonly AppIdentityDBContext _context;

        public TextBlockRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        public async Task CreateTextBlockAsync(TextBlock textBlock)
        {
            await _context.AddAsync(textBlock);
            await _context.SaveChangesAsync();
        }

        public async Task<TextBlock> GetTextBlockAsync(int id)
        {
            return await GetTextBlocks()
                .FirstOrDefaultAsync(tb=>tb.TextBlockId == id);
        }

        public IQueryable<TextBlock> GetTextBlocks()
        {
            return _context.TextBlocks.AsQueryable();
        }

        public IQueryable<TextBlock> GetTextBlocks(string textBlockTag)
        {
            return GetTextBlocks()
                .Where(tb=>tb.TextBlockTag == textBlockTag);
        }

        public async Task RemoveTextBlockAsync(TextBlock entry)
        {
            _context.Remove(entry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTextBlockAsync(TextBlock textBlock)
        {
            _context.Update(textBlock);
            await _context.SaveChangesAsync();
        }
    }
}
