using System.Collections.Generic;

namespace KisVuzDotNetCore2.Models.Nir
{
    public interface IMonografRepository
    {
        List<Monograf> GetMonografs();
        Monograf GetMonograf(int? id);
        void AddMonograf(Monograf monograf);
        void UpdateMonograf(Monograf monografEntry, Monograf monograf);
        void RemoveMonografAsync(int monografId);
    }
}