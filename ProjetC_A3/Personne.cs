using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetC_A3
{
    abstract public class Personne
    {
        protected int nss;
        protected string nom;
        protected string prenom;
        protected DateTime naissance;
        protected string adresse;
        protected string mail;
        protected string tel;

        public Personne(int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, string tel)
        {
            this.nss = nss;
            this.nom = nom;
            this.prenom = prenom;
            this.naissance = naissance;
            this.adresse = adresse;
            this.mail = mail;
            this.tel = tel;
        }

        abstract public string Nom { get; set; }
        abstract public string Prenom { get; set; }
        abstract public string Adresse { get; set; }
        abstract public string Mail { get; set; }
        abstract public string Tel { get; set; }
        abstract public int Nss { get; }
        abstract public DateTime Naissance { get; }
        public override string ToString()
        {
            return nss + " " + nom + " " + prenom + " " + naissance.ToShortDateString() + " " + adresse + " " + mail + " " + tel;
        }
    }
}
