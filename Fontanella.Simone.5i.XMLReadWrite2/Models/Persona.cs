using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Fontanella.Simone._5i.XMLReadWrite2
{
    public class Persona
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Indirizzo { get; set; }
        public List<string> Emails { get; }
        public List<string> SitiWeb { get; }
        public List<string> NumeriTelefono { get; }
        public int ID { get; set; }

        public Persona(XElement e)
        {
            Nome = e.Attribute("nome").Value;
            Cognome = e.Attribute("cognome").Value;
            Indirizzo = e.Attribute("indirizzo").Value;
            Emails = e.Element("Emails").Elements("Email").Select(emailElement => emailElement.Value).ToList();
            SitiWeb = e.Element("SitiWeb").Elements("SitoWeb").Select(sitoElement => sitoElement.Value).ToList();
            NumeriTelefono = e.Element("NumeriCellulare").Elements("NumeroCellulare").Select(cellulareElement => cellulareElement.Value).ToList();
        }

        public XElement XML
        {
            get
            {
                return new XElement(
                    "Persona",
                        new XAttribute("nome", Nome),
                        new XAttribute("cognome", Cognome),
                        new XAttribute("indirizzo", Indirizzo),
                        new XElement("NumeriCellulare",from numero in NumeriTelefono select new XElement("NumeroCellulare", numero)),
                        new XElement("Emails", from email in Emails select new XElement("Email", email)),
                        new XElement("SitiWeb", from sito in SitiWeb select new XElement("SitoWeb", sito))
                );
            }
        }

        public Persona(string nome, string cognome, string indirizzo, List<string> emails, List<string> sitiweb,List<string> numeriCellulare)
        {
            this.Nome = nome;
            this.Cognome = cognome;
            this.Indirizzo = indirizzo;
            this.Emails = emails;
            this.SitiWeb = sitiweb;
            this.NumeriTelefono = numeriCellulare;
            ID = ID;
        }
    }
    public class Persone : List<Persona>
    {
        public string FilePath { get; }

        private XElement XElement
        {
            get
            {
                return new XElement(
               "Rubrica",
                   from item in this
                   select item.XML);
            }
        }

        public Persone(string filePath)
        {
            FilePath = filePath;
            AddRange(from personaElement in XElement.Load(FilePath).Elements("Persona")
                     select new Persona(personaElement));
        }

        public new void Add(Persona contatto)
        {
            base.Add(contatto);
            contatto.ID = IndexOf(contatto);
        }

        public new void AddRange(IEnumerable<Persona> contatti)
        {
            foreach (Persona contatto in contatti)
            {
                Add(contatto);
            }
        }

        public void SalvaXml()
        {
            XElement.Save(FilePath);
        }
    }
}
