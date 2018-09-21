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

        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых профилей подготовки
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListEduProfileFullNames(int selectedId = 0);

        /// <summary>
        /// Возвращает список методкомиссий
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListMetodKomissiyaNames(int selectedId = 0);

        /// <summary>
        /// Возвращает список ФИО преподавателей
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListTeacherFio(int selectedId = 0);
    }
}
