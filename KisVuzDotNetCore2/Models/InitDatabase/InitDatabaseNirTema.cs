using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.InitDatabase
{
    public class InitDatabaseNirTema
    {
        /// <summary>
        /// Инициализация таблицы "Тема НИР"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateNirTema(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Тема НИР"
                if (!await context.NirTema.AnyAsync())
                {
                    NirTema NirTemaName1 = new NirTema
                    {
                        NirTemaId = 1,
                        NirTemaName = "Совершенствование технологических процессов производства и переработки сельскохозяйственной продукции",
                    };

                    NirTema NirTemaName2 = new NirTema
                    {
                        NirTemaId = 2,
                        NirTemaName = "Энергоресурсосберегающие технологии и технические средства кормообеспечения животноводства",
                    };

                    NirTema NirTemaName3 = new NirTema
                    {
                        NirTemaId = 3,
                        NirTemaName = "Разработать машинные технологии нового поколения, обеспечивающие модернизацию процессов возделывания базовых полевых культур с целью повышения устойчивости производства урожая, валовых сборов зерна и сокращения потребляемых ресурсов",
                    };

                    NirTema NirTemaName4 = new NirTema
                    {
                        NirTemaId = 4,
                        NirTemaName = "Повышение надежности технологического оборудования для стрижки овец",
                    };
                    NirTema NirTemaName5 = new NirTema
                    {
                        NirTemaId = 5,
                        NirTemaName = "Разработать машинные технологии нового поколения,обеспечивающие модернизацию процессов возделывания базовых полевых культур с целью повышения устойчивости производства урожая, валовых сборов зерна и сокращения потребляемых ресурсов",
                    };
                    NirTema NirTemaName6 = new NirTema
                    {
                        NirTemaId = 6,
                        NirTemaName = "Разработать машинные технологии нового поколения, обеспечивающие модернизацию процессов возделывания базовых полевых культур с целью повышения устойчивости производства урожая, валовых сборов зерна и сокращения потребляемых ресурсов",
                    };
                    NirTema NirTemaName7 = new NirTema
                    {
                        NirTemaId = 7,
                        NirTemaName = "Применение гибридных устройств и систем с эластичными рабочими органами в сельскохозяйственных процессах и технологиях",
                    };
                    NirTema NirTemaName8 = new NirTema
                    {
                        NirTemaId = 8,
                        NirTemaName = "Обоснование параметров малогабаритного навесного агрегата для технического обслуживания машин",
                    };
                    NirTema NirTemaName9 = new NirTema
                    {
                        NirTemaId = 9,
                        NirTemaName = "Совершенствование методики и средств диагностирования двигателей тракторов",
                    };
                    NirTema NirTemaName10 = new NirTema
                    {
                        NirTemaId = 10,
                        NirTemaName = "Повышение техникоэкономических, агротехнических и экологических показателей транспортнотехнологических машин и комплексов",
                    };
                    
        await context.NirTema.AddRangeAsync(
                        NirTemaName1,
                        NirTemaName2,
                        NirTemaName3,
                        NirTemaName4,
                        NirTemaName5,
                        NirTemaName6,
                        NirTemaName7,
                        NirTemaName8,
                        NirTemaName9,
                        NirTemaName10                        
                    );

                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
