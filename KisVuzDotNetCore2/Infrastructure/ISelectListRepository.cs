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
        /// Возвращает список форм издания
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListUchPosobieFormaIzdaniya(int selectedId = 0);

        /// <summary>
        /// Возвращает список специальностей научных работников согласно номенклатуре
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListNirSpecials(int selectedId = 0);

        /// <summary>
        /// Возвращает список научных журналов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListScienceJournals(int selectedId = 0);

        /// <summary>
        /// Возвращает список видов учебных пособий
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListUchPosobieVid(int selectedId = 0);

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
        /// Возвращает список структурных подразделений
        /// </summary>
        /// <param name="structSubvisionId"></param>
        /// <returns></returns>
        SelectList GetSelectListStructSubvisions(int? structSubvisionId = null);

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

        /// <summary>
        /// Возвращает список помещений
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListAuthors(int selectedId = 0);
                

        /// <summary>
        /// Возвращает список пользователей по строке ФИО
        /// </summary>
        /// <param name="fio"></param>
        /// <returns></returns>
        SelectList GetSelectListAppUsersByFirstName(string fio);

        /// <summary>
        /// Возвращает список аккаунтов
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListAppUsers(string selectedId = null);

        /// <summary>
        /// Возвращает список дисциплин
        /// </summary>
        SelectList GetSelectListDisciplines(int selectedId = 0);        
    }
}
