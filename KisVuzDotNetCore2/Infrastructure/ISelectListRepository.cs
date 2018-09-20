using Microsoft.AspNetCore.Mvc.Rendering;

namespace KisVuzDotNetCore2.Infrastructure
{
    /// <summary>
    /// Интерфейс для работы со списками (справочниками)
    /// </summary>
    public interface ISelectListRepository
    {
        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых направлений подготовки
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListEduNapravlFullNames(int selectedId=0);
    }
}
