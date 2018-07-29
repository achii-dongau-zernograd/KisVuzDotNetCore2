using KisVuzDotNetCore2.Models.Struct;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.InitDatabase
{
    /// <summary>
    /// Инициализация таблиц о структуре образовательной организации
    /// </summary>
    public static class InitDatabaseStructData
    {
        /// <summary>
        /// Инициализация структуры образовательной организации
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateStructData(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Адреса"
                if (!await context.Addresses.AnyAsync())
                {
                    Address addressPers = new Address
                    {
                        AddressId = 1,
                        PostCode = "346493",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "Октябрьский район, п. Персиановский",
                        Street = "ул. Кривошлыкова",
                        HouseNumber = "24"
                    };          //1
                    Address addressLenina19 = new Address
                    {
                        AddressId = 2,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Ленина",
                        HouseNumber = "19"
                    };      //2
                    Address addressLenina19_a = new Address
                    {
                        AddressId = 3,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Ленина",
                        HouseNumber = "19a"
                    };    //3
                    Address addressLenina21 = new Address
                    {
                        AddressId = 4,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Ленина",
                        HouseNumber = "21"
                    };      //4
                    Address addressLenina25_29 = new Address
                    {
                        AddressId = 5,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Ленина",
                        HouseNumber = "25/29"
                    };   //5
                    Address addressSovetskaya15_4 = new Address
                    {
                        AddressId = 6,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "15/4"
                    };//6
                    Address addressSovetskaya17 = new Address
                    {
                        AddressId = 7,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "17"
                    };  //7
                    Address addressSovetskaya17_21 = new Address
                    {
                        AddressId = 8,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "17/21"
                    };//8
                    Address addressSovetskaya19 = new Address
                    {
                        AddressId = 9,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "19"
                    };  //9
                    Address addressSovetskaya21_a = new Address
                    {
                        AddressId = 10,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "21a"
                    };//10
                    Address addressSovetskaya23 = new Address
                    {
                        AddressId = 11,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "23"
                    };  //11                    
                    Address addressSovetskaya28_30 = new Address
                    {
                        AddressId = 12,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "28/30"
                    };//12
                    Address addressSelercionniy34 = new Address
                    {
                        AddressId = 13,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "пер. Селекционный",
                        HouseNumber = "34"
                    };  //13
                    Address addressTelmana36 = new Address
                    {
                        AddressId = 14,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Тельмана",
                        HouseNumber = "36"
                    };      //14
                    Address addressTelmana61_2a = new Address
                    {
                        AddressId = 15,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Тельмана",
                        HouseNumber = "61/2a"
                    };  //15
                    Address addressSalsk = new Address
                    {
                        AddressId = 16,
                        PostCode = "347603",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "Сальский район, п. Конный завод им.Буденного",
                        Street = "База отдыха, строение",
                        HouseNumber = "2"
                    };          //16
                    await context.Addresses.AddRangeAsync(
                        addressPers,
                        addressLenina19,
                        addressLenina19_a,
                        addressLenina21,
                        addressLenina25_29,
                        addressSovetskaya15_4,
                        addressSovetskaya17,
                        addressSovetskaya17_21,
                        addressSovetskaya19,
                        addressSovetskaya21_a,
                        addressSovetskaya23,
                        addressSovetskaya28_30,
                        addressSelercionniy34,
                        addressTelmana36,
                        addressTelmana61_2a,
                        addressSalsk
                        );
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Университеты"
                if (!await context.StructUniversities.AnyAsync())
                {
                    StructUniversity university = new StructUniversity
                    {
                        StructUniversityId = 1,
                        StructUniversityName = "ФГБОУ ВО Донской ГАУ",
                        DateOfCreation = new DateTime(1993, 07, 05),
                        ExistenceOfFilials = true,
                        WorkingRegime = "Режим работы: вход в учебные корпуса и общежития — по пропускам и студенческим билетам",
                        WorkingSchedule = "График работы: понедельник-пятница с 8.00 до 17.00. Выходной — суббота, воскресенье",
                        AddressId = 1
                    };//ФГБОУ ВО Донской ГАУ

                    await context.StructUniversities.AddAsync(university);
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Институты"
                if (!await context.StructInstitutes.AnyAsync())
                {
                    StructInstitute institute = new StructInstitute
                    {
                        StructInstituteId = 1,
                        StructInstituteName = "Азово-Черноморский инженерный институт ФГБОУ ВО Донской ГАУ",
                        DateOfCreation = new DateTime(2014, 09, 15),
                        ExistenceOfFilials = false,
                        WorkingRegime = "Режим работы: вход в учебные корпуса и общежития — по пропускам и студенческим билетам",
                        WorkingSchedule = "График работы: понедельник-пятница с 8.00 до 17.00. Выходной — суббота, воскресенье",
                        UniversityId = 1,
                        AddressId = 4,
                        Faxes = new List<Fax> { new Fax { FaxValue = "(886359) 43-3-80", FaxComment = "Азово-Черноморский инженерный институт ФГБОУ ВО Донской ГАУ" } },
                        Emailes = new List<Email> { new Email { EmailValue = "achgaa@achgaa.ru", EmailComment = "Азово-Черноморский инженерный институт ФГБОУ ВО Донской ГАУ" } },
                        Telephones = new List<Telephone>
                        {
                            new Telephone{TelephoneNumber="(886359) 41-8-31", TelephoneComment="Приемная комиссия"},
                            new Telephone{TelephoneNumber="(886359) 43-3-80", TelephoneComment="Канцелярия"},
                            new Telephone{TelephoneNumber="(886359) 42-6-78", TelephoneComment="Факультет  «Инженерно-технологический» "},
                            new Telephone{TelephoneNumber="(886359) 41-6-56", TelephoneComment="Факультет «Энергетический»"},
                            new Telephone{TelephoneNumber="(886359) 42-1-98", TelephoneComment="Факультет «Экономика и управление территориями»"},
                            new Telephone{TelephoneNumber="(886359) 35-9-96", TelephoneComment="Факультет «Среднее профессиональное образование»"},
                        }
                    };// Азово-Черноморский инженерный институт ФГБОУ ВО Донской ГАУ

                    await context.StructInstitutes.AddAsync(institute);
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Типы структурных подразделений"
                if (!await context.StructSubvisionTypes.AnyAsync())
                {
                    StructSubvisionType structSubvisionType1 = new StructSubvisionType
                    {
                        StructSubvisionTypeId = 1,
                        StructSubvisionTypeName = "Органы управления образовательной организации"
                    };// Органы управления образовательной организации

                    StructSubvisionType structSubvisionType2 = new StructSubvisionType
                    {
                        StructSubvisionTypeId = 2,
                        StructSubvisionTypeName = "Структурные подразделения образовательной организации"
                    };// Структурные подразделения образовательной организации

                    StructSubvisionType structSubvisionType3 = new StructSubvisionType
                    {
                        StructSubvisionTypeId = 3,
                        StructSubvisionTypeName = "Факультеты"
                    };// Факультеты

                    StructSubvisionType structSubvisionType4 = new StructSubvisionType
                    {
                        StructSubvisionTypeId = 4,
                        StructSubvisionTypeName = "Кафедры"
                    };// Кафедры

                    await context.StructSubvisionTypes.AddRangeAsync(structSubvisionType1, structSubvisionType2, structSubvisionType3, structSubvisionType4);
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Должности"
                if (!await context.Posts.AnyAsync())
                {
                    Post post1 = new Post
                    {
                        PostId = 1,
                        PostName = "Директор"
                    };//Директор
                    Post post2 = new Post
                    {
                        PostId = 2,
                        PostName = "Исполняющий обязанности заместителя директора по учебной работе"
                    };//Исполняющий обязанности заместителя директора по учебной работе
                    Post post3 = new Post
                    {
                        PostId = 3,
                        PostName = "Заместитель директора по научной работе"
                    };//Заместитель директора по научной работе               
                    Post post4 = new Post
                    {
                        PostId = 4,
                        PostName = "Исполняющий обязанности заместителя директора по социальной работе"
                    };//Исполняющий обязанности заместителя директора по социальной работе
                    Post post5 = new Post
                    {
                        PostId = 5,
                        PostName = "Исполняющий обязанности заместителя директора по воспитательной работе"
                    };//Исполняющий обязанности заместителя директора по воспитательной работе
                    Post post6 = new Post
                    {
                        PostId = 6,
                        PostName = "Заместитель директора по административно-хозяйственной работе"
                    };//Заместитель директора по административно-хозяйственной работе
                    Post post7 = new Post
                    {
                        PostId = 7,
                        PostName = "Исполняющий обязанности заместителя директора по связям с общественностью"
                    };//Исполняющий обязанности заместителя директора по связям с общественностью
                    Post post8 = new Post
                    {
                        PostId = 8,
                        PostName = "Главный бухгалтер"
                    };//Главный бухгалтер
                    Post post9 = new Post
                    {
                        PostId = 9,
                        PostName = "Начальник"
                    };//Начальник
                    Post post10 = new Post
                    {
                        PostId = 10,
                        PostName = "Заведующий архивом"
                    };//Заведующий архивом
                    Post post11 = new Post
                    {
                        PostId = 11,
                        PostName = "Ответственный секретарь приемной комиссии"
                    };//Ответственный секретарь приемной комиссии
                    Post post12 = new Post
                    {
                        PostId = 12,
                        PostName = "Инструктор ГО"
                    };//Инструктор ГО
                    Post post13 = new Post
                    {
                        PostId = 13,
                        PostName = "Декан"
                    };//Декан
                    Post post14 = new Post
                    {
                        PostId = 14,
                        PostName = "Исполняющий обязанности декана"
                    };//Исполняющий обязанности декана
                    Post post15 = new Post
                    {
                        PostId = 15,
                        PostName = "Заведующий кафедрой"
                    };//Заведующий кафедрой
                    Post post16 = new Post
                    {
                        PostId = 16,
                        PostName = "Исполняющий обязанности заведующего кафедрой"
                    };//Исполняющий обязанности заведующего кафедрой
                    Post post17 = new Post
                    {
                        PostId = 17,
                        PostName = "Заведующий научно-технической библиотекой"
                    };//Заведующий научно-технической библиотекой
                    Post post18 = new Post
                    {
                        PostId = 18,
                        PostName = "Заведующий"
                    };//Заведующий
                    Post post19 = new Post
                    {
                        PostId = 19,
                        PostName = "Заведующий студенческим общежитием №1"
                    };//Заведующий студенческим общежитием №1
                    Post post20 = new Post
                    {
                        PostId = 20,
                        PostName = "Заведующий студенческим общежитием №2"
                    };//Заведующий студенческим общежитием №2
                    Post post21 = new Post
                    {
                        PostId = 21,
                        PostName = "Заведующий студенческим общежитием №3"
                    };//Заведующий студенческим общежитием №3
                    Post post22 = new Post
                    {
                        PostId = 22,
                        PostName = "Заведующий студенческим общежитием №4"
                    };//Заведующий студенческим общежитием №4
                    Post post23 = new Post
                    {
                        PostId = 23,
                        PostName = "Заведующий студенческим общежитием №5"
                    };//Заведующий студенческим общежитием №5
                    Post post24 = new Post
                    {
                        PostId = 24,
                        PostName = "Главный инженер"
                    };//Главный инженер
                    Post post25 = new Post
                    {
                        PostId = 25,
                        PostName = "Комендант учебного корпуса №1"
                    };//Комендант учебного корпуса №1
                    Post post26 = new Post
                    {
                        PostId = 26,
                        PostName = "Комендант учебного корпуса №2"
                    };//Комендант учебного корпуса №2
                    Post post27 = new Post
                    {
                        PostId = 27,
                        PostName = "Комендант учебного корпуса №3"
                    };//Комендант учебного корпуса №3
                    Post post28 = new Post
                    {
                        PostId = 28,
                        PostName = "Комендант административного корпуса"
                    };//Комендант административного корпуса
                    Post post29 = new Post
                    {
                        PostId = 29,
                        PostName = "Комендант учебного корпуса №5"
                    };//Комендант учебного корпуса №5
                    Post post30 = new Post
                    {
                        PostId = 30,
                        PostName = "Комендант учебного корпуса №6"
                    };//Комендант учебного корпуса №6
                    Post post31 = new Post
                    {
                        PostId = 31,
                        PostName = "Комендант учебного корпуса №7"
                    };//Комендант учебного корпуса №7                    
                    Post post32 = new Post
                    {
                        PostId = 32,
                        PostName = "Ассистент"
                    };//Ассистент
                    Post post33 = new Post
                    {
                        PostId = 33,
                        PostName = "Старший преподаватель"
                    };//Старший преподаватель
                    Post post34 = new Post
                    {
                        PostId = 34,
                        PostName = "Доцент"
                    };//Доцент
                    Post post35 = new Post
                    {
                        PostId = 35,
                        PostName = "Профессор"
                    };//Профессор
                    await context.Posts.AddRangeAsync(
                        post1, post2, post3, post4, post5, post6, post7, post8, post9, post10,
                        post11, post12, post13, post14, post15, post16, post17, post18, post19, post20,
                        post21, post22, post23, post24, post25, post26, post27, post28, post29, post30,
                        post31, post32, post33, post34, post35);
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Структурные подразделения"
                if (!await context.StructSubvisions.AnyAsync())
                {
                    StructSubvision structSubvision1 = new StructSubvision
                    {
                        StructSubvisionId = 1,
                        StructSubvisionName = "Дирекция",
                        StructSubvisionFioChief = "Серёгин Александр Анатольевич",
                        StructSubvisionPostChiefId = 1,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "achgaa@achgaa.ru", EmailComment = "Дирекция" },
                        StructSubvisionTypeId = 1
                    };//Директор
                    StructSubvision structSubvision2 = new StructSubvision
                    {
                        StructSubvisionId = 2,
                        StructSubvisionName = "Дирекция",
                        StructSubvisionFioChief = "Глечикова Наталья Александровна",
                        StructSubvisionPostChiefId = 2,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Исполняющий обязанности заместителя директора по учебной работе" },
                        StructSubvisionTypeId = 1
                    };//Заместитель директора по учебной работе
                    StructSubvision structSubvision3 = new StructSubvision
                    {
                        StructSubvisionId = 3,
                        StructSubvisionName = "Дирекция",
                        StructSubvisionFioChief = "Юдаев Игорь Викторович",
                        StructSubvisionPostChiefId = 3,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Заместитель директора по научной работе" },
                        StructSubvisionTypeId = 1
                    };//Заместитель директора по научной работе
                    StructSubvision structSubvision4 = new StructSubvision
                    {
                        StructSubvisionId = 4,
                        StructSubvisionName = "Дирекция",
                        StructSubvisionFioChief = "Кабанов Александр Николаевич",
                        StructSubvisionPostChiefId = 4,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Исполняющий обязанности заместителя директора по социальной работе" },
                        StructSubvisionTypeId = 1
                    };//Заместитель директора по социальной работе
                    StructSubvision structSubvision5 = new StructSubvision
                    {
                        StructSubvisionId = 5,
                        StructSubvisionName = "Дирекция",
                        StructSubvisionFioChief = "Асатурян Сергей Вартанович",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Исполняющий обязанности заместителя директора по воспитательной работе" },
                        StructSubvisionTypeId = 1
                    };//Заместитель директора по воспитательной работе
                    StructSubvision structSubvision6 = new StructSubvision
                    {
                        StructSubvisionId = 6,
                        StructSubvisionName = "Дирекция",
                        StructSubvisionFioChief = "Джанибеков Казбек Азрет-Алиевич",
                        StructSubvisionPostChiefId = 6,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Заместитель директора по административно-хозяйственной работе" },
                        StructSubvisionTypeId = 1
                    };//Заместитель директора по административно-хозяйственной работе
                    StructSubvision structSubvision7 = new StructSubvision
                    {
                        StructSubvisionId = 7,
                        StructSubvisionName = "Дирекция",
                        StructSubvisionFioChief = "Бондаренко Анатолий Михайлович",
                        StructSubvisionPostChiefId = 7,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Исполняющий обязанности заместителя директора по связям с общественностью" },
                        StructSubvisionTypeId = 1
                    };//Заместитель директора по связям с общественностью
                    StructSubvision structSubvision8 = new StructSubvision
                    {
                        StructSubvisionId = 8,
                        StructSubvisionName = "Отдел финансового планирования и бухгалтерского учета",
                        StructSubvisionFioChief = "Поваляева Елена Петровна",
                        StructSubvisionPostChiefId = 8,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "achgaa@itog.biz", EmailComment = "Отдел финансового планирования и бухгалтерского учета" },
                        StructSubvisionTypeId = 2
                    };//Отдел финансового планирования и бухгалтерского учета
                    StructSubvision structSubvision9 = new StructSubvision
                    {
                        StructSubvisionId = 9,
                        StructSubvisionName = "Отдел кадрового и документационного обеспечения",
                        StructSubvisionFioChief = "Головина Наталья Юрьевна",
                        StructSubvisionPostChiefId = 9,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "kadr@achgaa.ru", EmailComment = "Отдел кадрового и документационного обеспечения" },
                        StructSubvisionTypeId = 2
                    };//Отдел кадрового и документационного обеспечения
                    StructSubvision structSubvision10 = new StructSubvision
                    {
                        StructSubvisionId = 10,
                        StructSubvisionName = "Архив",
                        StructSubvisionFioChief = "Голодная Татьяна Николаевна",
                        StructSubvisionPostChiefId = 10,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Архив" },
                        StructSubvisionTypeId = 2
                    };//Архив
                    StructSubvision structSubvision11 = new StructSubvision
                    {
                        StructSubvisionId = 11,
                        StructSubvisionName = "Приемная комиссия",
                        StructSubvisionFioChief = "Должиков Валерий Викторович",
                        StructSubvisionPostChiefId = 11,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "priem@achgaa.ru", EmailComment = "Приемная комиссия" },
                        StructSubvisionTypeId = 2
                    };//Приемная комиссия
                    StructSubvision structSubvision12 = new StructSubvision
                    {
                        StructSubvisionId = 12,
                        StructSubvisionName = "Отдел информационных технологий и издательской деятельности",
                        StructSubvisionFioChief = "Лопатин Андрей Дмитриевич",
                        StructSubvisionPostChiefId = 9,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Отдел информационных технологий и издательской деятельности" },
                        StructSubvisionTypeId = 2
                    };//Отдел информационных технологий и издательской деятельности
                    StructSubvision structSubvision13 = new StructSubvision
                    {
                        StructSubvisionId = 13,
                        StructSubvisionName = "Штаб гражданской обороны и чрезвычайных ситуаций",
                        StructSubvisionFioChief = "Мангуш Антон Петрович",
                        StructSubvisionPostChiefId = 12,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Штаб гражданской обороны и чрезвычайных ситуаций" },
                        StructSubvisionTypeId = 2
                    };//Штаб гражданской обороны и чрезвычайных ситуаций
                    StructSubvision structSubvision14 = new StructSubvision
                    {
                        StructSubvisionId = 14,
                        StructSubvisionName = "Учебный отдел",
                        StructSubvisionFioChief = "Лашина Тамара Ашотовна",
                        StructSubvisionPostChiefId = 9,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "uotdel@achgaa.ru", EmailComment = "Учебный отдел" },
                        StructSubvisionTypeId = 2
                    };//Учебный отдел
                    StructSubvision structSubvisionSpo = new StructSubvision
                    {
                        StructSubvisionId = 15,
                        StructSubvisionName = "Факультет среднего профессионального образования",
                        StructSubvisionFioChief = "Черемисин Юрий Михайлович",
                        StructSubvisionPostChiefId = 13,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "dekspo@mail.ru", EmailComment = "Факультет среднего профессионального образования" },
                        StructSubvisionTypeId = 3
                    };//Факультет среднего профессионального образования
                    StructSubvision structSubvisionInjTech = new StructSubvision
                    {
                        StructSubvisionId = 16,
                        StructSubvisionName = "Инженерно-технологический факультет",
                        StructSubvisionFioChief = "Глобин Андрей Николаевич",
                        StructSubvisionPostChiefId = 13,
                        StructSubvisionAdressId = 11,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "atfachgaa@mail.ru", EmailComment = "Инженерно-технологический факультет" },
                        StructSubvisionTypeId = 3
                    };//Инженерно-технологический факультет
                    StructSubvision structSubvisionEnerg = new StructSubvision
                    {
                        StructSubvisionId = 17,
                        StructSubvisionName = "Энергетический факультет",
                        StructSubvisionFioChief = "Степанчук Геннадий Владимирович",
                        StructSubvisionPostChiefId = 13,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Энергетический факультет" },
                        StructSubvisionTypeId = 3
                    };//Энергетический факультет
                    StructSubvision structSubvisionEconom = new StructSubvision
                    {
                        StructSubvisionId = 18,
                        StructSubvisionName = "Факультет \"Экономика и управление территориями\"",
                        StructSubvisionFioChief = "Рева Алла Федоровна",
                        StructSubvisionPostChiefId = 13,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "achgaa@itog.biz", EmailComment = "Факультет \"Экономика и управление территориями\"" },
                        StructSubvisionTypeId = 3
                    };//Факультет \"Экономика и управление территориями\"
                    StructSubvision structSubvisionKafTiIuS = new StructSubvision
                    {
                        StructSubvisionId = 19,
                        StructSubvisionName = "Кафедра \"Теплоэнергетика и информационно-управляющие системы\"",
                        StructSubvisionFioChief = "Литвинов Владимир Николаевич",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "itius@achgaa.ru", EmailComment = "Кафедра \"Теплоэнергетика и информационно-управляющие системы\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Теплоэнергетика и информационно-управляющие системы\"
                    StructSubvision structSubvisionKafTbiF = new StructSubvision
                    {
                        StructSubvisionId = 20,
                        StructSubvisionName = "Кафедра \"Техносферная безопасность и физика\"",
                        StructSubvisionFioChief = "Шабанов Николай Иванович",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Кафедра \"Техносферная безопасность и физика\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Техносферная безопасность и физика\"
                    StructSubvision structSubvisionKafFiS = new StructSubvision
                    {
                        StructSubvisionId = 21,
                        StructSubvisionName = "Кафедра \"Физвоспитание и спорт\"",
                        StructSubvisionFioChief = "Пятикопов Сергей Михайлович",
                        StructSubvisionPostChiefId = 16,
                        StructSubvisionAdressId = 3,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "sport@achgaa.ru", EmailComment = "Кафедра \"Физвоспитание и спорт\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Физвоспитание и спорт\"
                    StructSubvision structSubvisionKafEEOiEM = new StructSubvision
                    {
                        StructSubvisionId = 22,
                        StructSubvisionName = "Кафедра \"Эксплуатация энергетического оборудования и электрических машин\"",
                        StructSubvisionFioChief = "Таранов Михаил Алексеевич",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "expl_el_mach@achgaa.ru", EmailComment = "Кафедра \"Эксплуатация энергетического оборудования и электрических машин\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Эксплуатация энергетического оборудования и электрических машин\"
                    StructSubvision structSubvisionKafEEiET = new StructSubvision
                    {
                        StructSubvisionId = 23,
                        StructSubvisionName = "Кафедра \"Электроэнергетика и электротехника\"",
                        StructSubvisionFioChief = "Забродина Ольга Борисовна",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "toeеs@achgaa.ru", EmailComment = "Кафедра \"Электроэнергетика и электротехника\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Электроэнергетика и электротехника\"
                    StructSubvision structSubvisionKafBUAiA = new StructSubvision
                    {
                        StructSubvisionId = 24,
                        StructSubvisionName = "Кафедра \"Бухгалтерский учет, анализ и аудит\"",
                        StructSubvisionFioChief = "Чумакова Наталья Валерьевна",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "account@achgaa.ru", EmailComment = "Кафедра \"Бухгалтерский учет, анализ и аудит\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Бухгалтерский учет, анализ и аудит\"
                    StructSubvision structSubvisionKafGDiInYaz = new StructSubvision
                    {
                        StructSubvisionId = 25,
                        StructSubvisionName = "Кафедра \"Гуманитарные дисциплины и иностранные языки\"",
                        StructSubvisionFioChief = "Глушко Ирина Васильевна",
                        StructSubvisionPostChiefId = 16,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "kppiinjz@achgaa.ru", EmailComment = "Кафедра \"Гуманитарные дисциплины и иностранные языки\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Гуманитарные дисциплины и иностранные языки\"
                    StructSubvision structSubvisionKafZUiK = new StructSubvision
                    {
                        StructSubvisionId = 26,
                        StructSubvisionName = "Кафедра \"Землеустройство и кадастры\"",
                        StructSubvisionFioChief = "Бондаренко Анатолий Михайлович",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Кафедра \"Землеустройство и кадастры\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Землеустройство и кадастры\"
                    StructSubvision structSubvisionEiU = new StructSubvision
                    {
                        StructSubvisionId = 27,
                        StructSubvisionName = "Кафедра \"Экономика и управление\"",
                        StructSubvisionFioChief = "Рева Алла Федоровна",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "ekonom_i_upravlenie@achgaa.ru", EmailComment = "Кафедра \"Экономика и управление\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Экономика и управление\"
                    StructSubvision structSubvisionAiSsk = new StructSubvision
                    {
                        StructSubvisionId = 28,
                        StructSubvisionName = "Кафедра \"Агрономия и селекция с/х культур\"",
                        StructSubvisionFioChief = "Хронюк Василий Борисович",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "tr@achgaa.ru", EmailComment = "Кафедра \"Агрономия и селекция с/х культур\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Агрономия и селекция с/х культур\"
                    StructSubvision structSubvisionVMiM = new StructSubvision
                    {
                        StructSubvisionId = 29,
                        StructSubvisionName = "Кафедра \"Высшая математика и механика\"",
                        StructSubvisionFioChief = "Степовой Дмитрий Владимирович",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Кафедра \"Высшая математика и механика\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Высшая математика и механика\"
                    StructSubvision structSubvisionTSinAPK = new StructSubvision
                    {
                        StructSubvisionId = 30,
                        StructSubvisionName = "Кафедра \"Технический сервис в агропромышленном комплексе\"",
                        StructSubvisionFioChief = "Никитченко Сергей Леонидович",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 8,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "kaf-ts@achgaa.ru", EmailComment = "Кафедра \"Технический сервис в агропромышленном комплексе\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Технический сервис в агропромышленном комплексе\"
                    StructSubvision structSubvisionTiSmAPK = new StructSubvision
                    {
                        StructSubvisionId = 31,
                        StructSubvisionName = "Кафедра \"Технологии и средства механизации агропромышленного комплекса\"",
                        StructSubvisionFioChief = "Толстоухова Татьяна Николаевна",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "mtppshp@achgaa.ru", EmailComment = "Кафедра \"Технологии и средства механизации агропромышленного комплекса\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Технологии и средства механизации агропромышленного комплекса\"
                    StructSubvision structSubvisionTiA = new StructSubvision
                    {
                        StructSubvisionId = 32,
                        StructSubvisionName = "Кафедра \"Тракторы и автомобили\"",
                        StructSubvisionFioChief = "Нагорский Леонид Алексеевич",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 8,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Кафедра \"Тракторы и автомобили\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Тракторы и автомобили\"
                    StructSubvision structSubvisionEAiTTP = new StructSubvision
                    {
                        StructSubvisionId = 33,
                        StructSubvisionName = "Кафедра \"Эксплуатация автомобилей и технологии транспортных процессов\"",
                        StructSubvisionFioChief = "Щиров Владимир Николаевич",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Кафедра \"Эксплуатация автомобилей и технологии транспортных процессов\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Эксплуатация автомобилей и технологии транспортных процессов\"
                    StructSubvision structSubvision15 = new StructSubvision
                    {
                        StructSubvisionId = 34,
                        StructSubvisionName = "Библиотека",
                        StructSubvisionFioChief = "Мирошникова Людмила Федоровна",
                        StructSubvisionPostChiefId = 17,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "biblioteka@achgaa.ru", EmailComment = "Библиотека" },
                        StructSubvisionTypeId = 2
                    };//Библиотека
                    StructSubvision structSubvision16 = new StructSubvision
                    {
                        StructSubvisionId = 35,
                        StructSubvisionName = "Учебный центр прикладных квалификаций",
                        StructSubvisionFioChief = "Иванов Павел Александрович",
                        StructSubvisionPostChiefId = 18,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебный центр прикладных квалификаций" },
                        StructSubvisionTypeId = 2
                    };//Учебный центр прикладных квалификаций
                    StructSubvision structSubvision17 = new StructSubvision
                    {
                        StructSubvisionId = 36,
                        StructSubvisionName = "Агротехнологический центр",
                        StructSubvisionFioChief = "Меркулов Александр Филиппович",
                        StructSubvisionPostChiefId = 1,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Агротехнологический центр" },
                        StructSubvisionTypeId = 2
                    };//Агротехнологический центр
                    StructSubvision structSubvision18 = new StructSubvision
                    {
                        StructSubvisionId = 37,
                        StructSubvisionName = "Центр инжиниринга и трансфера",
                        StructSubvisionFioChief = "Хижняк Владимир Иванович",
                        StructSubvisionPostChiefId = 1,
                        StructSubvisionAdressId = 7,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Центр инжиниринга и трансфера" },
                        StructSubvisionTypeId = 2
                    };//Центр инжиниринга и трансфера
                    StructSubvision structSubvision19 = new StructSubvision
                    {
                        StructSubvisionId = 38,
                        StructSubvisionName = "Учебно-научно-производственная агротехнологическая лаборатория",
                        StructSubvisionFioChief = "Кувшинова Елена Константиновна",
                        StructSubvisionPostChiefId = 18,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебно-научно-производственная агротехнологическая лаборатория" },
                        StructSubvisionTypeId = 2
                    };//Учебно-научно-производственная агротехнологическая лаборатория
                    StructSubvision structSubvision20 = new StructSubvision
                    {
                        StructSubvisionId = 39,
                        StructSubvisionName = "Центр профессиональной ориентации и работы с молодежью",
                        StructSubvisionFioChief = "Черноусов Иван Николаевич",
                        StructSubvisionPostChiefId = 18,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Центрпрофессиональ ной ориентации и работы с молодежью" },
                        StructSubvisionTypeId = 2
                    };//Центр профессиональной ориентации и работы с молодежью
                    StructSubvision structSubvision21 = new StructSubvision
                    {
                        StructSubvisionId = 40,
                        StructSubvisionName = "Комбинат студенческого питания",
                        StructSubvisionFioChief = "Айнушов Адиль Искендер оглы",
                        StructSubvisionPostChiefId = 9,
                        StructSubvisionAdressId = 13,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Комбинат студенческого питания" },
                        StructSubvisionTypeId = 2
                    };//Комбинат студенческого питания
                    StructSubvision structSubvision22 = new StructSubvision
                    {
                        StructSubvisionId = 41,
                        StructSubvisionName = "База отдыха",
                        StructSubvisionFioChief = "Рогозин Сергей Федорович",
                        StructSubvisionPostChiefId = 18,
                        StructSubvisionAdressId = 16,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "База отдыха" },
                        StructSubvisionTypeId = 2
                    };//База отдыха
                    StructSubvision structSubvision23 = new StructSubvision
                    {
                        StructSubvisionId = 42,
                        StructSubvisionName = "Студенческий городок. Общежитие №1",
                        StructSubvisionFioChief = "Шевченко Светлана Георгиевна",
                        StructSubvisionPostChiefId = 19,
                        StructSubvisionAdressId = 9,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Студенческий городок. Общежитие №1" },
                        StructSubvisionTypeId = 2
                    };//Студенческий городок. Общежитие №1
                    StructSubvision structSubvision24 = new StructSubvision
                    {
                        StructSubvisionId = 43,
                        StructSubvisionName = "Студенческий городок. Общежитие №2",
                        StructSubvisionFioChief = "Мирошникова лариса Алексеевна",
                        StructSubvisionPostChiefId = 20,
                        StructSubvisionAdressId = 11,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Студенческий городок. Общежитие №2" },
                        StructSubvisionTypeId = 2
                    };//Студенческий городок. Общежитие №2
                    StructSubvision structSubvision25 = new StructSubvision
                    {
                        StructSubvisionId = 44,
                        StructSubvisionName = "Студенческий городок. Общежитие №3",
                        StructSubvisionFioChief = "Ляхова Татьяна Михайловна",
                        StructSubvisionPostChiefId = 21,
                        StructSubvisionAdressId = 5,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Студенческий городок. Общежитие №3" },
                        StructSubvisionTypeId = 2
                    };//Студенческий городок. Общежитие №3
                    StructSubvision structSubvision26 = new StructSubvision
                    {
                        StructSubvisionId = 45,
                        StructSubvisionName = "Студенческий городок. Общежитие №4",
                        StructSubvisionFioChief = "Кунец Светлана Сергеевна",
                        StructSubvisionPostChiefId = 22,
                        StructSubvisionAdressId = 14,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Студенческий городок. Общежитие №4" },
                        StructSubvisionTypeId = 2
                    };//Студенческий городок. Общежитие №4
                    StructSubvision structSubvision27 = new StructSubvision
                    {
                        StructSubvisionId = 46,
                        StructSubvisionName = "Студенческий городок. Общежитие №5",
                        StructSubvisionFioChief = "Корсунова Вера Николаевна",
                        StructSubvisionPostChiefId = 23,
                        StructSubvisionAdressId = 15,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Студенческий городок. Общежитие №5" },
                        StructSubvisionTypeId = 2
                    };//Студенческий городок. Общежитие №5
                    StructSubvision structSubvision28 = new StructSubvision
                    {
                        StructSubvisionId = 47,
                        StructSubvisionName = "Инженерная служба",
                        StructSubvisionFioChief = "Коршунов Александр Иванович",
                        StructSubvisionPostChiefId = 24,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Инженерная служба" },
                        StructSubvisionTypeId = 2
                    };//Инженерная служба
                    StructSubvision structSubvision29 = new StructSubvision
                    {
                        StructSubvisionId = 48,
                        StructSubvisionName = "Ремонтностроительный участок",
                        StructSubvisionFioChief = "Дубограй Татьяна Гавриловна",
                        StructSubvisionPostChiefId = 9,
                        StructSubvisionAdressId = 7,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Ремонтностроительный участок" },
                        StructSubvisionTypeId = 2
                    };//Ремонтностроительный участок
                    StructSubvision structSubvision30 = new StructSubvision
                    {
                        StructSubvisionId = 49,
                        StructSubvisionName = "Хозяйственный отдел",
                        StructSubvisionFioChief = "Чичиланов Илья Иванович",
                        StructSubvisionPostChiefId = 9,
                        StructSubvisionAdressId = 7,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Хозяйственный отдел" },
                        StructSubvisionTypeId = 2
                    };//Хозяйственный отдел
                    StructSubvision structSubvision31 = new StructSubvision
                    {
                        StructSubvisionId = 50,
                        StructSubvisionName = "Учебный корпус №1",
                        StructSubvisionFioChief = "Бондаренко Татьяна Анатольевна",
                        StructSubvisionPostChiefId = 25,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебный корпус №1" },
                        StructSubvisionTypeId = 2
                    };//Учебный корпус №1
                    StructSubvision structSubvision32 = new StructSubvision
                    {
                        StructSubvisionId = 51,
                        StructSubvisionName = "Учебный корпус №2",
                        StructSubvisionFioChief = "Бондаренко Татьяна Анатольевна",
                        StructSubvisionPostChiefId = 26,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебный корпус №2" },
                        StructSubvisionTypeId = 2
                    };//Учебный корпус №2
                    StructSubvision structSubvision33 = new StructSubvision
                    {
                        StructSubvisionId = 52,
                        StructSubvisionName = "Учебный корпус №3",
                        StructSubvisionFioChief = "Левченко Наталья Петровна",
                        StructSubvisionPostChiefId = 27,
                        StructSubvisionAdressId = 8,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебный корпус №3" },
                        StructSubvisionTypeId = 2
                    };//Учебный корпус №3
                    StructSubvision structSubvision34 = new StructSubvision
                    {
                        StructSubvisionId = 53,
                        StructSubvisionName = "Административный корпус",
                        StructSubvisionFioChief = "Левченко Наталья Петровна",
                        StructSubvisionPostChiefId = 28,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Административный корпус" },
                        StructSubvisionTypeId = 2
                    };//Административный корпус
                    StructSubvision structSubvision35 = new StructSubvision
                    {
                        StructSubvisionId = 54,
                        StructSubvisionName = "Учебный корпус №5",
                        StructSubvisionFioChief = "Щербакова Павлина Григорьевна",
                        StructSubvisionPostChiefId = 29,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебный корпус №5" },
                        StructSubvisionTypeId = 2
                    };//Учебный корпус №5
                    StructSubvision structSubvision36 = new StructSubvision
                    {
                        StructSubvisionId = 55,
                        StructSubvisionName = "Учебный корпус №6",
                        StructSubvisionFioChief = "Щербакова Павлина Григорьевна",
                        StructSubvisionPostChiefId = 30,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебный корпус №6" },
                        StructSubvisionTypeId = 2
                    };//Учебный корпус №6
                    StructSubvision structSubvision37 = new StructSubvision
                    {
                        StructSubvisionId = 56,
                        StructSubvisionName = "Учебный корпус №7",
                        StructSubvisionFioChief = "Щербакова Павлина Григорьевна",
                        StructSubvisionPostChiefId = 31,
                        StructSubvisionAdressId = 7,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебный корпус №7" },
                        StructSubvisionTypeId = 2
                    };//Учебный корпус №7
                    StructSubvision structSubvision38 = new StructSubvision
                    {
                        StructSubvisionId = 57,
                        StructSubvisionName = "Автогараж",
                        StructSubvisionFioChief = "Мусенко Владимир Александрович",
                        StructSubvisionPostChiefId = 9,
                        StructSubvisionAdressId = 10,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Автогараж" },
                        StructSubvisionTypeId = 2
                    };//Автогараж
                    await context.StructSubvisions.AddRangeAsync(structSubvision1, structSubvision2,
                        structSubvision3, structSubvision4, structSubvision5, structSubvision6,
                        structSubvision7, structSubvision8, structSubvision9, structSubvision10,
                        structSubvision11, structSubvision12, structSubvision13, structSubvision14,
                        structSubvision15, structSubvision16, structSubvision17, structSubvision18,
                        structSubvision19, structSubvision20, structSubvision21, structSubvision22,
                        structSubvision23, structSubvision24, structSubvision25, structSubvision26,
                        structSubvision27, structSubvision28, structSubvision29, structSubvision30,
                        structSubvision31, structSubvision32, structSubvision33, structSubvision34,
                        structSubvision35, structSubvision36, structSubvision37, structSubvision38,

                        structSubvisionSpo, structSubvisionInjTech, structSubvisionEnerg, structSubvisionEconom,

                        structSubvisionKafTiIuS, structSubvisionKafTbiF, structSubvisionKafFiS,
                        structSubvisionKafEEOiEM, structSubvisionKafEEiET, structSubvisionKafBUAiA,
                        structSubvisionKafGDiInYaz, structSubvisionKafZUiK, structSubvisionEiU,
                        structSubvisionAiSsk, structSubvisionVMiM, structSubvisionTSinAPK,
                        structSubvisionTiSmAPK, structSubvisionTiA, structSubvisionEAiTTP);

                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Факультеты"
                if (!await context.StructFacultets.AnyAsync())
                {
                    StructFacultet facultetSpo = new StructFacultet
                    {
                        StructFacultetId = (int)StructFacultetEnum.Spo,
                        StructInstituteId = 1,
                        StructSubvisionId = 15
                    };

                    StructFacultet facultetInjTech = new StructFacultet
                    {
                        StructFacultetId = (int)StructFacultetEnum.InjTech,
                        StructInstituteId = 1,
                        StructSubvisionId = 16
                    };

                    StructFacultet facultetEnerg = new StructFacultet
                    {
                        StructFacultetId = (int)StructFacultetEnum.Energ,
                        StructInstituteId = 1,
                        StructSubvisionId = 17
                    };

                    StructFacultet facultetEconom = new StructFacultet
                    {
                        StructFacultetId = (int)StructFacultetEnum.Econom,
                        StructInstituteId = 1,
                        StructSubvisionId = 18
                    };

                    await context.StructFacultets.AddRangeAsync(new StructFacultet[] { facultetSpo,
                        facultetInjTech, facultetEnerg, facultetEconom
                    });
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Кафедры"
                if (!await context.StructKafs.AnyAsync())
                {
                    StructKaf kaf1 = new StructKaf
                    {
                        StructKafId = 1,
                        StructFacultetId = 3,
                        StructSubvisionId = 19
                    };

                    StructKaf kaf2 = new StructKaf
                    {
                        StructKafId = 2,
                        StructFacultetId = 3,
                        StructSubvisionId = 20
                    };

                    StructKaf kaf3 = new StructKaf
                    {
                        StructKafId = 3,
                        StructFacultetId = 3,
                        StructSubvisionId = 21
                    };

                    StructKaf kaf4 = new StructKaf
                    {
                        StructKafId = 4,
                        StructFacultetId = 3,
                        StructSubvisionId = 22
                    };

                    StructKaf kaf5 = new StructKaf
                    {
                        StructKafId = 5,
                        StructFacultetId = 3,
                        StructSubvisionId = 23
                    };

                    StructKaf kaf6 = new StructKaf
                    {
                        StructKafId = 6,
                        StructFacultetId = 4,
                        StructSubvisionId = 24
                    };

                    StructKaf kaf7 = new StructKaf
                    {
                        StructKafId = 7,
                        StructFacultetId = 4,
                        StructSubvisionId = 25
                    };

                    StructKaf kaf8 = new StructKaf
                    {
                        StructKafId = 8,
                        StructFacultetId = 4,
                        StructSubvisionId = 26
                    };

                    StructKaf kaf9 = new StructKaf
                    {
                        StructKafId = 9,
                        StructFacultetId = 4,
                        StructSubvisionId = 27
                    };

                    StructKaf kaf10 = new StructKaf
                    {
                        StructKafId = 10,
                        StructFacultetId = 4,
                        StructSubvisionId = 28
                    };

                    StructKaf kaf11 = new StructKaf
                    {
                        StructKafId = 11,
                        StructFacultetId = 2,
                        StructSubvisionId = 29
                    };

                    StructKaf kaf12 = new StructKaf
                    {
                        StructKafId = 12,
                        StructFacultetId = 2,
                        StructSubvisionId = 30
                    };

                    StructKaf kaf13 = new StructKaf
                    {
                        StructKafId = 13,
                        StructFacultetId = 2,
                        StructSubvisionId = 31
                    };

                    StructKaf kaf14 = new StructKaf
                    {
                        StructKafId = 14,
                        StructFacultetId = 2,
                        StructSubvisionId = 32
                    };

                    StructKaf kaf15 = new StructKaf
                    {
                        StructKafId = 15,
                        StructFacultetId = 2,
                        StructSubvisionId = 33
                    };


                    await context.StructKafs.AddRangeAsync(
                        kaf1,
                        kaf2,
                        kaf3,
                        kaf4,
                        kaf5,
                        kaf6,
                        kaf7,
                        kaf8,
                        kaf9,
                        kaf10,
                        kaf11,
                        kaf12,
                        kaf13,
                        kaf14,
                        kaf15);
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }

    }
}
