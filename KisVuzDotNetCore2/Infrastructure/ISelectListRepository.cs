using KisVuzDotNetCore2.Models.Struct;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

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
        /// Возвращает список полных наименований
        /// реализуемых профилей подготовки, доступных методкомиссиям
        /// </summary>
        /// <param name="metodKomissii">Перечисление методкомиссий</param>
        /// <param name="selectedId">УИД выбранного объекта</param>
        /// <returns></returns>
        SelectList GetSelectListEduProfileFullNamesOfMethodicalCommission(IEnumerable<MetodKomissiya> metodKomissii, int selectedId = 0);

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

        /// <summary>
        /// Возвращает список форм обучения
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListEduForms(int selectedId = 0);

        /// <summary>
        /// Возвращает список программ подготовки
        /// </summary>
        /// <param name="eduProgramPodgId"></param>
        /// <returns></returns>
        SelectList GetSelectListEduProgramPodg(int selectedId = 0);

        /// <summary>
        /// Возвращает список сроков обучения
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListEduSrok(int selectedId = 0);

        /// <summary>
        /// Возвращает список кафедр
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListStructKaf(int selectedId = 0);

        /// <summary>
        /// Возвращает список дисциплин,
        /// содержащих заданную строку
        /// </summary>
        /// <param name="disciplineNameSearchString"></param>
        /// <returns></returns>
        SelectList GetSelectListDisciplineNames(string disciplineNameSearchString);

        /// <summary>
        /// Возвращает список помещений
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListPomesheniya(int selectedId = 0);
    }
}
