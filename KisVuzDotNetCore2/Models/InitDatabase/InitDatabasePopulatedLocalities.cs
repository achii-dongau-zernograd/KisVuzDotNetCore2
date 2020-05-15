using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Nir;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Инициализация справочников стран, регионов, районов и населённых пунктов
    /// </summary>
    internal class InitDatabasePopulatedLocalities
    {
        /// <summary>
        /// Инициализация таблицы "Страны"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateCountries(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Страны"
                if (!await context.Countries.AnyAsync())
                {
                    var country01 = new Country
                    {
                        CountryId = (int)CountriesEnum.Russia,
                        CountryName = "Россия"
                    };
                    

                    await context.Countries.AddRangeAsync(
                        country01
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }

        /// <summary>
        /// Инициализация таблицы "Регионы"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateRegions(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Регионы"
                if (!await context.Regions.AnyAsync())
                {
                    var row01 = new Region
                    {
                        RegionId = (int)RegionsEnum.RostovskayaOblast,
                        RegionName = "Ростовская область",
                        CountryId = (int)CountriesEnum.Russia
                    };

                    var row02 = new Region
                    {
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray,
                        RegionName = "Краснодарский край",
                        CountryId = (int)CountriesEnum.Russia
                    };

                    var row03 = new Region
                    {
                        RegionId = (int)RegionsEnum.StavropolskiyKray,
                        RegionName = "Ставропольский край",
                        CountryId = (int)CountriesEnum.Russia
                    };


                    await context.Regions.AddRangeAsync(
                        row01,
                        row02,
                        row03
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }

        /// <summary>
        /// Инициализация таблицы "Районы"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateDistricts(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Районы"
                if (!await context.Districts.AnyAsync())
                {
                    var row01 = new District
                    {
                        DistrictId = 01,
                        DistrictName = "Азовский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast,
                        GpsGeometryCenter = new GpsCoordinate { Latitude = 46.90072m, Longitude = 39.181109m }
                    };

                    var row02 = new District
                    {
                        DistrictId = 02,
                        DistrictName = "Аксайский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast,
                        GpsGeometryCenter = new GpsCoordinate { Latitude = 47.288339m, Longitude = 39.97854m }
                    };

                    var row03 = new District
                    {
                        DistrictId = 03,
                        DistrictName = "Багаевский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast,
                        GpsGeometryCenter = new GpsCoordinate { Latitude = 47.250148m, Longitude = 40.379429m }
                    };

                    var row04 = new District
                    {
                        DistrictId = 04,
                        DistrictName = "Белокалитвинский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast,
                        GpsGeometryCenter = new GpsCoordinate { Latitude = 48.237509m, Longitude = 40.900242m }
                    };

                    var row05 = new District
                    {
                        DistrictId = 05,
                        DistrictName = "Боковский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast,
                        GpsGeometryCenter = new GpsCoordinate { Latitude = 49.260432m, Longitude = 41.680222m }
                    };

                    var row06 = new District
                    {
                        DistrictId = 06,
                        DistrictName = "Верхнедонской район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row07 = new District
                    {
                        DistrictId = 07,
                        DistrictName = "Весёловский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row08 = new District
                    {
                        DistrictId = 08,
                        DistrictName = "Волгодонской район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row09 = new District
                    {
                        DistrictId = 09,
                        DistrictName = "Дубовский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row10 = new District
                    {
                        DistrictId = 10,
                        DistrictName = "Егорлыкский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row11 = new District
                    {
                        DistrictId = 11,
                        DistrictName = "Заветинский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row12 = new District
                    {
                        DistrictId = 12,
                        DistrictName = "Зерноградский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row13 = new District
                    {
                        DistrictId = 13,
                        DistrictName = "Зимовниковский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row14 = new District
                    {
                        DistrictId = 14,
                        DistrictName = "Кагальницкий район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row15 = new District
                    {
                        DistrictId = 15,
                        DistrictName = "Каменский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row16 = new District
                    {
                        DistrictId = 16,
                        DistrictName = "Кашарский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row17 = new District
                    {
                        DistrictId = 17,
                        DistrictName = "Константиновский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row18 = new District
                    {
                        DistrictId = 18,
                        DistrictName = "Красносулинский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row19 = new District
                    {
                        DistrictId = 19,
                        DistrictName = "Куйбышевский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row20 = new District
                    {
                        DistrictId = 20,
                        DistrictName = "Мартыновский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row21 = new District
                    {
                        DistrictId = 21,
                        DistrictName = "Матвеево-Курганский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row22 = new District
                    {
                        DistrictId = 22,
                        DistrictName = "Миллеровский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row23 = new District
                    {
                        DistrictId = 23,
                        DistrictName = "Милютинский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row24 = new District
                    {
                        DistrictId = 24,
                        DistrictName = "Морозовский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row25 = new District
                    {
                        DistrictId = 25,
                        DistrictName = "Мясниковский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row26 = new District
                    {
                        DistrictId = 26,
                        DistrictName = "Неклиновский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row27 = new District
                    {
                        DistrictId = 27,
                        DistrictName = "Обливский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row28 = new District
                    {
                        DistrictId = 28,
                        DistrictName = "Октябрьский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row29 = new District
                    {
                        DistrictId = 29,
                        DistrictName = "Орловский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row30 = new District
                    {
                        DistrictId = 30,
                        DistrictName = "Песчанокопский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row31 = new District
                    {
                        DistrictId = 31,
                        DistrictName = "Пролетарский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row32 = new District
                    {
                        DistrictId = 32,
                        DistrictName = "Ремонтненский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row33 = new District
                    {
                        DistrictId = 33,
                        DistrictName = "Родионово-Несветайский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row34 = new District
                    {
                        DistrictId = 34,
                        DistrictName = "Сальский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row35 = new District
                    {
                        DistrictId = 35,
                        DistrictName = "Семикаракорский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row36 = new District
                    {
                        DistrictId = 36,
                        DistrictName = "Советский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row37 = new District
                    {
                        DistrictId = 37,
                        DistrictName = "Тарасовский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row38 = new District
                    {
                        DistrictId = 38,
                        DistrictName = "Тацинский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row39 = new District
                    {
                        DistrictId = 39,
                        DistrictName = "Усть-Донецкий район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row40 = new District
                    {
                        DistrictId = 40,
                        DistrictName = "Целинский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row41 = new District
                    {
                        DistrictId = 41,
                        DistrictName = "Цимлянский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row42 = new District
                    {
                        DistrictId = 42,
                        DistrictName = "Чертковский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    var row43 = new District
                    {
                        DistrictId = 43,
                        DistrictName = "Шолоховский район",
                        RegionId = (int)RegionsEnum.RostovskayaOblast
                    };

                    ///////////// Краснодарский край ////////////////
                    var row44 = new District
                    {
                        DistrictId = 44,
                        DistrictName = "Абинский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row45 = new District
                    {
                        DistrictId = 45,
                        DistrictName = "Анапский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row46 = new District
                    {
                        DistrictId = 46,
                        DistrictName = "Апшеронский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row47 = new District
                    {
                        DistrictId = 47,
                        DistrictName = "Белоглинский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row48 = new District
                    {
                        DistrictId = 48,
                        DistrictName = "Белореченский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row49 = new District
                    {
                        DistrictId = 49,
                        DistrictName = "Брюховецкий район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row50 = new District
                    {
                        DistrictId = 50,
                        DistrictName = "Выселковский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row51 = new District
                    {
                        DistrictId = 51,
                        DistrictName = "Гулькевичский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row52 = new District
                    {
                        DistrictId = 52,
                        DistrictName = "Динской район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row53 = new District
                    {
                        DistrictId = 53,
                        DistrictName = "Ейский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row54 = new District
                    {
                        DistrictId = 54,
                        DistrictName = "Кавказский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row55 = new District
                    {
                        DistrictId = 55,
                        DistrictName = "Калининский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row56 = new District
                    {
                        DistrictId = 56,
                        DistrictName = "Каневской район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row57 = new District
                    {
                        DistrictId = 57,
                        DistrictName = "Кореновский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row58 = new District
                    {
                        DistrictId = 58,
                        DistrictName = "Красноармейский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row59 = new District
                    {
                        DistrictId = 59,
                        DistrictName = "Крыловский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row60 = new District
                    {
                        DistrictId = 60,
                        DistrictName = "Крымский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row61 = new District
                    {
                        DistrictId = 61,
                        DistrictName = "Курганинский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row62 = new District
                    {
                        DistrictId = 62,
                        DistrictName = "Кущёвский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row63 = new District
                    {
                        DistrictId = 63,
                        DistrictName = "Лабинский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row64 = new District
                    {
                        DistrictId = 64,
                        DistrictName = "Ленинградский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row65 = new District
                    {
                        DistrictId = 65,
                        DistrictName = "Мостовский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row66 = new District
                    {
                        DistrictId = 66,
                        DistrictName = "Новокубанский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row67 = new District
                    {
                        DistrictId = 67,
                        DistrictName = "Новопокровский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row68 = new District
                    {
                        DistrictId = 68,
                        DistrictName = "Отрадненский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row69 = new District
                    {
                        DistrictId = 69,
                        DistrictName = "Павловский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row70 = new District
                    {
                        DistrictId = 70,
                        DistrictName = "Приморско-Ахтарский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row71 = new District
                    {
                        DistrictId = 71,
                        DistrictName = "Северский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row72 = new District
                    {
                        DistrictId = 72,
                        DistrictName = "Славянский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row73 = new District
                    {
                        DistrictId = 73,
                        DistrictName = "Староминский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row74 = new District
                    {
                        DistrictId = 74,
                        DistrictName = "Тбилисский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row75 = new District
                    {
                        DistrictId = 75,
                        DistrictName = "Темрюкский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row76 = new District
                    {
                        DistrictId = 76,
                        DistrictName = "Тимашёвский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row77 = new District
                    {
                        DistrictId = 77,
                        DistrictName = "Тихорецкий район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row78 = new District
                    {
                        DistrictId = 78,
                        DistrictName = "Туапсинский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row79 = new District
                    {
                        DistrictId = 79,
                        DistrictName = "Успенский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row80 = new District
                    {
                        DistrictId = 80,
                        DistrictName = "Усть-Лабинский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    var row81 = new District
                    {
                        DistrictId = 81,
                        DistrictName = "Щербиновский район",
                        RegionId = (int)RegionsEnum.KrasnodarskiyKray
                    };

                    //////////////////// Ставропольский край //////////////////
                    var row82 = new District
                    {
                        DistrictId = 82,
                        DistrictName = "Александровский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row83 = new District
                    {
                        DistrictId = 83,
                        DistrictName = "Андроповский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row84 = new District
                    {
                        DistrictId = 84,
                        DistrictName = "Александровский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row85 = new District
                    {
                        DistrictId = 85,
                        DistrictName = "Апанасенковский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row86 = new District
                    {
                        DistrictId = 86,
                        DistrictName = "Арзгирский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row87 = new District
                    {
                        DistrictId = 87,
                        DistrictName = "Благодарненский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row88 = new District
                    {
                        DistrictId = 88,
                        DistrictName = "Будённовский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row89 = new District
                    {
                        DistrictId = 89,
                        DistrictName = "Георгиевский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row90 = new District
                    {
                        DistrictId = 90,
                        DistrictName = "Грачёвский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row91 = new District
                    {
                        DistrictId = 91,
                        DistrictName = "Изобильненский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row92 = new District
                    {
                        DistrictId = 92,
                        DistrictName = "Ипатовский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row93 = new District
                    {
                        DistrictId = 93,
                        DistrictName = "Кировский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row94 = new District
                    {
                        DistrictId = 94,
                        DistrictName = "Кочубеевский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row95 = new District
                    {
                        DistrictId = 95,
                        DistrictName = "Красногвардейский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row96 = new District
                    {
                        DistrictId = 96,
                        DistrictName = "Курский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row97 = new District
                    {
                        DistrictId = 97,
                        DistrictName = "Левокумский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row98 = new District
                    {
                        DistrictId = 98,
                        DistrictName = "Минераловодский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row99 = new District
                    {
                        DistrictId = 99,
                        DistrictName = "Нефтекумский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row100 = new District
                    {
                        DistrictId = 100,
                        DistrictName = "Новоалександровский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row101 = new District
                    {
                        DistrictId = 101,
                        DistrictName = "Новоселицкий район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row102 = new District
                    {
                        DistrictId = 102,
                        DistrictName = "Петровский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row103 = new District
                    {
                        DistrictId = 103,
                        DistrictName = "Предгорный район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row104 = new District
                    {
                        DistrictId = 104,
                        DistrictName = "Советский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row105 = new District
                    {
                        DistrictId = 105,
                        DistrictName = "Степновский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row106 = new District
                    {
                        DistrictId = 106,
                        DistrictName = "Труновский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row107 = new District
                    {
                        DistrictId = 107,
                        DistrictName = "Туркменский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };

                    var row108 = new District
                    {
                        DistrictId = 108,
                        DistrictName = "Шпаковский район",
                        RegionId = (int)RegionsEnum.StavropolskiyKray
                    };




                    await context.Districts.AddRangeAsync(
                               row01, row02, row03, row04, row05, row06, row07, row08, row09,
                        row10, row11, row12, row13, row14, row15, row16, row17, row18, row19,
                        row20, row21, row22, row23, row24, row25, row26, row27, row28, row29,
                        row30, row31, row32, row33, row34, row35, row36, row37, row38, row39,
                        row40, row41, row42, row43, row44, row45, row46, row47, row48, row49,
                        row50, row51, row52, row53, row54, row55, row56, row57, row58, row59,
                        row60, row61, row62, row63, row64, row65, row66, row67, row68, row69,
                        row70, row71, row72, row73, row74, row75, row76, row77, row78, row79,
                        row80, row81, row82, row83, row84, row85, row86, row87, row88, row89,
                        row90, row91, row92, row93, row94, row95, row96, row97, row98, row99,
                        row100, row101, row102, row103, row104, row105, row106, row107, row108
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }

        /// <summary>
        /// Инициализация таблицы "Типы населённых пунктов"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreatePopulatedLocalityTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Типы населённых пунктов"
                if (!await context.PopulatedLocalityTypes.AnyAsync())
                {
                    var row01 = new PopulatedLocalityType
                    {
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Gorod,
                        PopulatedLocalityTypeName = "Город",
                        PopulatedLocalityTypeNameShort = "г."
                    };

                    var row02 = new PopulatedLocalityType
                    {
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Poselok,
                        PopulatedLocalityTypeName = "Посёлок",
                        PopulatedLocalityTypeNameShort = "п."
                    };

                    var row03 = new PopulatedLocalityType
                    {
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Hutor,
                        PopulatedLocalityTypeName = "Хутор",
                        PopulatedLocalityTypeNameShort = "х."
                    };

                    var row04 = new PopulatedLocalityType
                    {
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Stanica,
                        PopulatedLocalityTypeName = "Станица",
                        PopulatedLocalityTypeNameShort = "Ст-ца"
                    };


                    await context.PopulatedLocalityTypes.AddRangeAsync(
                        row01,
                        row02,
                        row03,
                        row04
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }

        /// <summary>
        /// Инициализация таблицы "Населённые пункты"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreatePopulatedLocalities(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Населённые пункты"
                if (!await context.PopulatedLocalities.AnyAsync())
                {
                    var row01 = new PopulatedLocality
                    {
                        PopulatedLocalityId = 1,
                        PopulatedLocalityName = "Зерноград",
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Gorod,
                        DistrictId = 12
                    };

                    var row02 = new PopulatedLocality
                    {
                        PopulatedLocalityId = 2,
                        PopulatedLocalityName = "Дубки",
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Poselok,
                        DistrictId = 12
                    };

                    var row03 = new PopulatedLocality
                    {
                        PopulatedLocalityId = 3,
                        PopulatedLocalityName = "Зерновой",
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Poselok,
                        DistrictId = 12
                    };

                    var row04 = new PopulatedLocality
                    {
                        PopulatedLocalityId = 4,
                        PopulatedLocalityName = "Каменный",
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Hutor,
                        DistrictId = 12
                    };

                    var row05 = new PopulatedLocality
                    {
                        PopulatedLocalityId = 5,
                        PopulatedLocalityName = "Кленовый",
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Poselok,
                        DistrictId = 12
                    };

                    var row06 = new PopulatedLocality
                    {
                        PopulatedLocalityId = 6,
                        PopulatedLocalityName = "Комсомольский",
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Poselok,
                        DistrictId = 12
                    };

                    var row07 = new PopulatedLocality
                    {
                        PopulatedLocalityId = 7,
                        PopulatedLocalityName = "Прудовой",
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Poselok,
                        DistrictId = 12
                    };

                    var row08 = new PopulatedLocality
                    {
                        PopulatedLocalityId = 8,
                        PopulatedLocalityName = "Ракитный",
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Hutor,
                        DistrictId = 12
                    };

                    var row09 = new PopulatedLocality
                    {
                        PopulatedLocalityId = 9,
                        PopulatedLocalityName = "Речной",
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Poselok,
                        DistrictId = 12
                    };

                    var row10 = new PopulatedLocality
                    {
                        PopulatedLocalityId = 10,
                        PopulatedLocalityName = "Шоссейный",
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Poselok,
                        DistrictId = 12
                    };

                    var row11 = new PopulatedLocality
                    {
                        PopulatedLocalityId = 11,
                        PopulatedLocalityName = "Экспериментальный",
                        PopulatedLocalityTypeId = (int)PopulatedLocalityTypesEnum.Poselok,
                        DistrictId = 12
                    };


                    await context.PopulatedLocalities.AddRangeAsync(
                        row01, row02, row03, row04, row05, row06, row07, row08, row09, row10, row11
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
        
    }
}