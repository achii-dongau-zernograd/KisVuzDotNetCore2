using Microsoft.AspNetCore.Mvc;

namespace KisVuzDotNetCore2.Controllers
{
    /// <summary>
    /// Контроллер переключения между обычной версией
    /// и версией для слабовидящих
    /// </summary>
    public class SlabovidController : Controller
    {
        public IActionResult Change()
        {
            byte[] slabovid;
            if(HttpContext.Session.TryGetValue("slabovid",out slabovid))
            {
                if (slabovid[0] == 1)
                    HttpContext.Session.Set("slabovid", new byte[] { 0 });
                else
                    HttpContext.Session.Set("slabovid", new byte[] { 1 });                
            }
            else
            {
                HttpContext.Session.Set("slabovid", new byte[] { 1 });
            }

            return RedirectToAction("Index","Home");
        }
    }
}
