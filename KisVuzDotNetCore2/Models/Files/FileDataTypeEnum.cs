﻿namespace KisVuzDotNetCore2.Models.Files
{
    /// <summary>
    /// Перечисление типов данных файлов
    /// </summary>
    public enum FileDataTypeEnum
    {
        /// <summary>
        /// Положение о структурном подразделении
        /// </summary>
        PolojenOStructPodrazd = 1,
        /// <summary>
        /// Положения об образовательной деятельности
        /// </summary>
        PolojenObObrazovatDeyat = 2,
        /// <summary>
        /// Устав образовательной организации
        /// </summary>
        UstavObrazovatelnoyOrganizatsii = 3,
        /// <summary>
        /// Лицензия на осуществление образовательной деятельности
        /// </summary>
        LicenziyaNaOsushestvlenieObrazovatelnoyDeyatelnosti = 4,
        /// <summary>
        /// Свидетельство о государственной аккредитации (с приложениями)
        /// </summary>
        SvidetelstvoOGosudarstvennoyAccreditatsii = 5,
        /// <summary>
        /// План финансово-хозяйственной деятельности
        /// </summary>
        PlanFinansovoHozyaystvennoyDeyatelnosti = 6,
        /// <summary>
        /// Локальные нормативные акты, регламентирующие правила приема обучающихся
        /// </summary>
        LocalnieNormativnieActiReglamentiruyushiePravilaPriemaObuchaushihsya = 7,
        /// <summary>
        /// Локальные нормативные акты, регламентирующие режим занятий обучающихся
        /// </summary>
        LocalnieNormativnieActiReglamentiruyushieRejimZanyatiyObuchaushihsya = 8,
        /// <summary>
        /// Локальные нормативные акты, регламентирующие формы, периодичность и порядок текущего контроля успеваемости и промежуточной аттестации обучающихся
        /// </summary>
        LocalnieNormativnieActiReglamentiruyushieTekushiyKontrolIPromAttestObuchaushihsya = 9,
        /// <summary>
        /// Локальные нормативные акты, регламентирующие порядок и основания перевода, отчисления и восстановления обучающихся
        /// </summary>
        LocalnieNormativnieActiReglamentiruyushiePerevodOtchislenieIVosstanovlenieObuchaushihsya = 10,
        /// <summary>
        /// Локальные нормативные акты, регламентирующие порядок оформления возникновения, приостановления и прекращения отношений между образовательной организацией, обучающимися и (или) родителями (законными представителями) несовершеннолетних обучающихся
        /// </summary>
        LocalnieNormativnieActiReglamentiruyushieOtnosheniyaSNesovershennoletnimiObuchayushimisya = 11,
        /// <summary>
        /// Правила внутреннего распорядка обучающихся
        /// </summary>
        PravilaVnutrennegoRasporyadkaObuchaushihsya = 12,
        /// <summary>
        /// Правила внутреннего трудового распорядка
        /// </summary>
        PravilaVnutrennegoTrudovogoRasporyadka = 13,
        /// <summary>
        /// Коллективный договор
        /// </summary>
        KollektivniyDogovor = 14,
        /// <summary>
        /// Отчет о результатах самообследования
        /// </summary>
        OtchetORezultatahSamoobsledovaniya = 15,
        /// <summary>
        /// Документ о порядке оказания платных образовательных услуг
        /// </summary>
        PoryadokOkazaniyaPlatnihObrazovatelnihUslug = 16,
        /// <summary>
        /// Образец договора об оказании платных образовательных услуг
        /// </summary>
        ObrazecDogovoraObOkazaniiPlatnihObrazovatelnihUslug = 17,
        /// <summary>
        /// Документ об утверждении стоимости обучения по каждой образовательной программе
        /// </summary>
        DocumentObUtverjdeniiStoimostiObucheniya = 18,
        /// <summary>
        /// Предписания органов, осуществляющих государственный контроль (надзор) в сфере образования
        /// </summary>
        PredpisaniyaOrganovNadzora = 19,
        /// <summary>
        /// Отчеты об исполнении предписаний органов, осуществляющих государственный контроль (надзор) в сфере образования
        /// </summary>
        OtchetiObIspolneniiPredpisaniyOrganovNadzora = 20,
        /// <summary>
        /// Удостоверение о повышении квалификации
        /// </summary>
        UdostoverenieOPovisheniiKvalifikacii = 21,
        /// <summary>
        /// Диплом о профессиональной переподготовке
        /// </summary>
        DiplomOProfessionalnoyPerepodgotovke = 24,
        /// <summary>
        /// Диплом о среднем профессиональном образовании
        /// </summary>
        DiplomSPO = 25,
        /// <summary>
        /// Диплом о высшем образовании
        /// </summary>
        DiplomVO = 26,
        /// <summary>
        /// Учебный план
        /// </summary>
        UchebniyPlan = 31,
        /// <summary>
        /// Основная профессиональная образовательная программа (ОПОП)
        /// </summary>
        OPOP = 32,
        /// <summary>
        /// Календарный учебный график
        /// </summary>
        KalendarniyUchebniyGraphik=33,
        /// <summary>
        /// Рабочая программа дисциплины
        /// </summary>
        RabProgrammaDisciplini = 34,
        /// <summary>
        /// Аннотация к рабочей программе
        /// </summary>
        Annotation = 35,
        /// Учебное пособие
        /// </summary>
        UchebnoePosobie = 36,
        /// <summary>
        /// Курс лекций
        /// </summary>
        KursLekcii = 37,
        /// <summary>
        /// Лабораторный практикум
        /// </summary>
        LaboratorniiPraktikum = 38,
        /// <summary>
        /// Методические указания
        /// </summary>
        MetodicheskieUkazaniya = 39,
        /// <summary>
        /// Информация о поступлении и расходовании финансовых и материальных средств
        /// </summary>
        InfOPostupleniiIRashodovaniiFinIMaterialnihSredstv = 40,
        /// <summary>
        /// Федеральный нормативный акт, регламентирующий наличие и условия предоставления стипендии
        /// </summary>
        StipendiiFederalGrant = 41,
        /// <summary>
        /// Локальный нормативный акт, регламентирующий наличие и условия предоставления стипендии
        /// </summary>
        StipendiiLocalGrant = 42,
        /// <summary>
        /// Фонд оценочных средств
        /// </summary>
        FondOcenochnihSredstv = 43,
        /// <summary>
        /// Работа пользователя
        /// </summary>
        UserWork = 44,
        /// <summary>
        ///  Рецензия на работу пользователя
        /// </summary>
        UserWorkRecenziya = 45,
        /// <summary>
        /// Договор с электронной библ. системой
        /// </summary>
        DogovorElBiblSystem = 46,
        /// <summary>
        /// Научная статья
        /// </summary>
        Article = 47,
        /// <summary>
        /// Патент на изобретение
        /// </summary>
        PatentNaIzobretenie = 48,
        /// <summary>
        /// Патент на полезную модель
        /// </summary>
        PatentNaPoleznuyuModel = 49,
        /// <summary>
        /// Свидетельство о регистрации программы для ЭВМ
        /// </summary>
        SvidetelstvoNaProgrammu = 50,
        /// <summary>
        /// Свидетельство о регистрации базы данных
        /// </summary>
        SvidetelstvoNaBazuDannih = 51,
        /// <summary>
        /// Монография
        /// </summary>
        Monografiya = 52,
        /// <summary>
        /// Патент на селекционное достижение
        /// </summary>
        PatentNaSelekcionnoeDostigenie = 53,
        /// <summary>
        /// Патент на промышленный образец
        /// </summary>
        PatentNaPromyshlenniyObrazets = 54,
        /// <summary>
        /// Положение в соответствии со статьёй 30 ФЗ № 273 от 29.12.12 
        /// </summary>
        PolojenSt30Fz273 = 56,
        /// <summary>
        /// Результаты освоения образовательной программы
        /// </summary>
        RezultOsvoenObrazovatProgr = 57,
        /// <summary>
        /// Согласие на обработку персональных данных
        /// </summary>
        SoglasieNaObrabotkuPersonalnihDannih = 58,
        /// <summary>
        /// Аттестат о среднем общем образовании
        /// </summary>
        AttestatOSrednemObshemObrazovanii = 59,
        /// <summary>
        /// Паспорт
        /// </summary>
        Passport = 60,
        /// <summary>
        /// Файл, подтверждающий индивидуальное достижение абитуриента
        /// </summary>
        IndividualnoeDostijenieAbiturienta = 61
    }
}
