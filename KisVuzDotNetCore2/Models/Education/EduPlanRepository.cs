using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    public class EduPlanRepository : IEduPlanRepository
    {
        private readonly AppIdentityDBContext _context;

        public EduPlanRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Создаёт структуру учебного плана
        /// </summary>
        /// <param name="eduPlan"></param>
        /// <returns></returns>
        public async Task CreateEduPlanStructureAsync(EduPlan eduPlan)
        {
            // Создаём список, содержащий блоки дисциплин
            if (eduPlan.BlokDiscipl == null)
            {
                eduPlan.BlokDiscipl = new List<BlokDiscipl>();
                await _context.SaveChangesAsync();
            }

            // Заполняем блоки дисциплин
            if (eduPlan.BlokDiscipl.Count == 0)
            {
                foreach (var blokDisciplName in _context.BlokDisciplName.OrderBy(b=>b.BlokDisciplNameName))
                {
                    BlokDiscipl blokDiscipl = new BlokDiscipl();
                    blokDiscipl.BlokDisciplNameId = blokDisciplName.BlokDisciplNameId;
                    eduPlan.BlokDiscipl.Add(blokDiscipl);
                }

                await _context.SaveChangesAsync();
            }

            foreach (var blokDiscipl in eduPlan.BlokDiscipl)
            {
                if(blokDiscipl.BlokDisciplChast==null)
                {
                    blokDiscipl.BlokDisciplChast = new List<BlokDisciplChast>();
                    await _context.SaveChangesAsync();
                }

                if(blokDiscipl.BlokDisciplChast.Count==0)
                {
                    foreach (var blokDisciplChastName in _context.BlokDisciplChastName)
                    {
                        BlokDisciplChast blokDisciplChast = new BlokDisciplChast();
                        blokDisciplChast.BlokDisciplId = blokDiscipl.BlokDisciplId;
                        blokDisciplChast.BlokDisciplChastNameId = blokDisciplChastName.BlokDisciplChastNameId;

                        blokDiscipl.BlokDisciplChast.Add(blokDisciplChast);
                    }
                    
                }
            }
            await _context.SaveChangesAsync();



            return;
        }

        /// <summary>
        /// Возвращает объект дисциплины из учебного плана
        /// </summary>
        /// <param name="eduPlan"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public Discipline GetDiscipline(EduPlan eduPlan, int disciplineId)
        {
            Discipline discipline=null;
            eduPlan.BlokDiscipl
                .ForEach(b => b.BlokDisciplChast
                    .ForEach(c => c.Disciplines
                        .ForEach(d => { if (d.DisciplineId == disciplineId) { discipline = d; } })));
            return discipline;
        }

        /// <summary>
        /// Возвращает учебный план со всеми связанными данными
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <returns></returns>
        public async Task<EduPlan> GetEduPlanAsync(int eduPlanId)
        {
            var eduPlan = await _context.EduPlans
                .Include(e => e.EduForm)
                .Include(e => e.EduPlanPdf)
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduProgramPodg)
                .Include(e => e.EduSrok)
                .Include(e => e.StructKaf.StructSubvision)
                .Include(e => e.EduVidDeyatList)
                    .ThenInclude(v => v.EduVidDeyat)
                .Include(e => e.EduPlanEduYearBeginningTrainings)
                    .ThenInclude(y => y.EduYearBeginningTraining)
                .Include(e => e.EduPlanEduYears)
                    .ThenInclude(y => y.EduYear)
                .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplName)
                .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.BlokDisciplChastName)
                .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.Kurses)
                                .ThenInclude(k => k.Semestres)
                                    .ThenInclude(s => s.SemestrName)
                .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.Kurses)
                                .ThenInclude(k => k.Semestres)
                                    .ThenInclude(s => s.SemestrVidUchebRaboti)
                                        .ThenInclude(u => u.VidUchebRabotiName)
                .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.Kurses)
                                .ThenInclude(k => k.EduKurs)
                 .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.Kurses)
                                .ThenInclude(k => k.Semestres)
                                    .ThenInclude(s => s.SemestrFormKontrolName)
                                        .ThenInclude(f => f.FormKontrolName)
                 .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.DisciplineName)
                 .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.EduAnnotations)
                                .ThenInclude(a => a.FileModel)
                 .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.RabPrograms)
                                .ThenInclude(a => a.FileModel)
                 .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.FondOcenochnihSredstvList)
                                .ThenInclude(a => a.FileModel)
                 .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.TeacherDisciplines)
                                .ThenInclude(td => td.Teacher)
                                    .ThenInclude(t => t.AppUser)
                 .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.DisciplinePomeshenies)
                                .ThenInclude(dp => dp.Pomeshenie)
                                    .ThenInclude(p => p.Korpus)
                 .SingleOrDefaultAsync(e => e.EduPlanId == eduPlanId);

            return eduPlan;
        }

        /// <summary>
        /// Удаляет объект дисциплины
        /// </summary>
        /// <param name="discipline"></param>
        /// <returns></returns>
        public async Task RemoveDiscipline(Discipline discipline)
        {
            _context.Disciplines.Remove(discipline);
            await _context.SaveChangesAsync();
        }
    }
}