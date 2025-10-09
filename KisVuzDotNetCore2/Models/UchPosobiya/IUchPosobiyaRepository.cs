using System.Collections.Generic;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.UchPosobiya;

namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    /// <summary>
    /// Репозиторий учебных пособий
    /// </summary>
    public interface IUchPosobiyaRepository
    {
        /// <summary>
        /// Возвращает список учебных пособий
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UchPosobie>> GetUchPosobiyaAsync(UchPosobieFilterModel uchPosobieFilterModel);

        /// <summary>
        /// Возвращает учебное пособие по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UchPosobie> GetUchPosobieByIdAsync(int id);

        /// <summary>
        /// Обновляет учебное пособие
        /// </summary>
        /// <param name="uchPosobieEditViewModel"></param>
        /// <returns></returns>
        Task UpdateUchPosobie(UchPosobieEditViewModel uchPosobieEditViewModel);

        /// <summary>
        /// Удаляет учебное пособие
        /// </summary>
        /// <param name="uchPosobie"></param>
        /// <returns></returns>
        Task RemoveUchPosobieAsync(UchPosobie uchPosobie);

        /// <summary>
        /// Обновляет данные об авторе учебного пособия
        /// </summary>
        /// <param name="uchPosobieAuthor"></param>
        Task UpdateUchPosobieAuthorAsync(UchPosobieAuthor uchPosobieAuthor);

        /// <summary>
        /// Возвращает объект привязки автора к учебному пособию по УИД
        /// </summary>
        /// <param name="uchPosobieAuthorId"></param>
        /// <returns></returns>
        Task<UchPosobieAuthor> GetUchPosobieAuthorByIdAsync(int uchPosobieAuthorId);

        /// <summary>
        /// Назначает пользователя автором
        /// </summary>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        Task<Author> CreateAuthorByAppUserIdAsync(string appUserId);

        /// <summary>
        /// Удаляет объект привязки автора к учебному пособию по УИД
        /// </summary>
        /// <param name="uchPosobieAuthorId"></param>
        /// <returns></returns>
        Task RemoveUchPosobieAuthorByIdAsync(int uchPosobieAuthorId);

        /// <summary>
        /// Обновляет объект привязки автора к учебному пособию
        /// </summary>
        /// <param name="uchPosobieDisciplineName"></param>
        /// <returns></returns>
        Task UpdateUchPosobieDisciplineNameAsync(UchPosobieDisciplineName uchPosobieDisciplineName);

        /// <summary>
        /// Возвращает привязку "Учебное пособие - Дисциплина" по УИД
        /// </summary>
        /// <param name="uchPosobieDisciplineNameId"></param>
        /// <returns></returns>
        Task<UchPosobieDisciplineName> GetUchPosobieDisciplineNameByIdAsync(int uchPosobieDisciplineNameId);

        /// <summary>
        /// Удаление привязки "Учебное пособие - Дисциплина"
        /// </summary>
        /// <param name="uchPosobieDisciplineName"></param>
        /// <returns></returns>
        Task RemoveUchPosobieDisciplineNameAsync(UchPosobieDisciplineName uchPosobieDisciplineName);

        /// <summary>
        /// Обновление привязки "Учебное пособие - Направление"
        /// </summary>
        /// <param name="uchPosobieEduNapravl"></param>
        /// <returns></returns>
        Task UpdateUchPosobieEduNapravlAsync(UchPosobieEduNapravl uchPosobieEduNapravl);

        /// <summary>
        /// Возвращает привязку "Учебное пособие - Направление" по УИД
        /// </summary>
        /// <param name="uchPosobieEduNapravlId"></param>
        /// <returns></returns>
        Task<UchPosobieEduNapravl> GetUchPosobieEduNapravlByIdAsync(int uchPosobieEduNapravlId);

        /// <summary>
        /// Удаляет привязку "Учебное пособие - Направление"
        /// </summary>
        /// <param name="uchPosobieEduNapravl"></param>
        /// <returns></returns>
        Task RemoveUchPosobieEduNapravlAsync(UchPosobieEduNapravl uchPosobieEduNapravl);

        /// <summary>
        /// Возвращает список форм обучения
        /// </summary>
        /// <returns></returns>
        List<EduForm> GetEduForms();

        /// <summary>
        /// Обновляет привязку "Учебное пособие - Форма обучения"
        /// </summary>
        /// <param name="uchPosobieEduForm"></param>
        /// <returns></returns>
        Task UpdateUchPosobieEduFormAsync(UchPosobieEduForm uchPosobieEduForm);

        /// <summary>
        /// Возвращает привязку "Учебное пособие - Форма обучения" по УИД
        /// </summary>
        /// <param name="uchPosobieEduFormId"></param>
        /// <returns></returns>
        Task<UchPosobieEduForm> GetUchPosobieEduFormByIdAsync(int uchPosobieEduFormId);

        /// <summary>
        /// Удаляет привязку "Учебное пособие - Форма обучения"
        /// </summary>
        /// <param name="uchPosobieEduForm"></param>
        /// <returns></returns>
        Task RemoveUchPosobieEduFormAsync(UchPosobieEduForm uchPosobieEduForm);

        /// <summary>
        /// Возвращает количество учебных пособий
        /// </summary>
        /// <returns></returns>
        Task<int> GetUchPosobiyaCount();
    }
}