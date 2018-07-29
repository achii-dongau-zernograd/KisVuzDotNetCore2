namespace KisVuzDotNetCore2.Models.Files
{
    /// <summary>
    /// Перечисление типов данных файлов
    /// </summary>
    public enum FileDataTypeEnum
    {
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
        DiplomOProfessionalnoyPodgotovke = 24,
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
        UserWorkRecenziya = 45
    }
}
