using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Модель "Сведения об учредителях"
    /// </summary>
    public class SvedenCommonUchredLaw
    {
        public string NameUchred { get; set; }        

        public string FullnameUchred { get; set; }

        public string AddressUchred { get; set; }
        
        public string TelUchred { get; set; }
        
        public string MailUchred { get; set; }
        
        public string WebsiteUchred { get; set; }        
    }

    /// <summary>
    /// Хранилище сведений об учредителях
    /// (экземпляров SvedenCommonUchredLaw)
    /// </summary>
    [Serializable]
    public class SvedenCommonUchredLawRepository
    {
        public List<SvedenCommonUchredLaw> SvedenCommonUchredLawList { get; set; } = new List<SvedenCommonUchredLaw>();
        
        public void Add(SvedenCommonUchredLaw _uchredLaw)
        {
            SvedenCommonUchredLawList.Add(_uchredLaw);
        }

        public void Remove(SvedenCommonUchredLaw _uchredLaw)
        {
            SvedenCommonUchredLawList.Remove(_uchredLaw);
        }

        public void ExportToXML(string fileName)
        {
            //передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(SvedenCommonUchredLawRepository));

            //Получаем поток, куда будем записывать сереализованный объект
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(fs, this);
            }
        }

        public void ImportFromXML(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("ошибка чтения файла" + fileName);
                return;
            }
            XmlSerializer formatter = new XmlSerializer(typeof(SvedenCommonUchredLawRepository));

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                try
                {
                    SvedenCommonUchredLawRepository _uchredLawRepository = (SvedenCommonUchredLawRepository)formatter.Deserialize(fs);
                    SvedenCommonUchredLawList = _uchredLawRepository.SvedenCommonUchredLawList;
                }
                catch (Exception exc)
                {
                    Console.WriteLine("ошибка чтения файла" + fileName);
                }
            }
        }


    }
}
