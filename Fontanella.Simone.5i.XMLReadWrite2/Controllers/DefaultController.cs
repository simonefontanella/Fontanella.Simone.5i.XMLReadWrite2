using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Fontanella.Simone._5i.XMLReadWrite2.Controllers
{
    public class DefaultController : Controller
    {
        string nomeFile = @"~/App_Data/Persone.xml";
        // GET: Default
        public ActionResult Index()
        {
            Persone Persone = new Persone(HostingEnvironment.MapPath(nomeFile));
            return View(Persone);
        }

        public ActionResult Edit(string nome, string cognome, string indirizzo, string emails, string sitiweb,
            string numeriCellulare)
        {
            Persone contatti = new Persone(HostingEnvironment.MapPath(nomeFile))
            {
                new Persona(nome, cognome, indirizzo, emails.Split(',').ToList(),
                    sitiweb.Split(',').ToList(), numeriCellulare.Split(',').ToList())
            };
            contatti.SalvaXml();
            return View("Index", contatti);
        }

        public ActionResult AddContact()
        {
            return View("AddContact");
        }

        public ActionResult ContactDetails(int id)
        {
            Persone persone = new Persone(HostingEnvironment.MapPath(nomeFile));
            return View(persone.FirstOrDefault(x => x.ID == id));
        }

        public ActionResult Modifica(int id)
        {
            Persone persone = new Persone(HostingEnvironment.MapPath(nomeFile));
            return View(persone.FirstOrDefault(x => x.ID == id));
        }
    }
}