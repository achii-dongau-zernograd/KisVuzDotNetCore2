using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Перечисление внешних ресурсов
    /// </summary>
    public enum ExternalResourceEnum
    {        
        SocialNetworks_VKontakte = 1,
        SocialNetworks_ORCID = 2,
        SocialNetworks_Mendeley = 3,               
        Messengers_Skype = 4,        
        CitationBases_Elibrary = 5,
        CitationBases_Scopus = 6        
    }
}
