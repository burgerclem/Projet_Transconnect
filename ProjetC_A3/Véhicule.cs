using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetC_A3
{
    abstract public class Véhicule
    {
        protected string modele;
        protected int annee;
        protected string plaque;
        protected int type;
        protected DateTime date = DateTime.Parse("2024-01-01");

        public Véhicule(string modele, int annee, string plaque,int type,DateTime date )
        {
            this.modele = modele;
            this.annee = annee;
            this.plaque = plaque;
            this.type = type;
            this.date = date;
        }
        public string Modele
            { get { return modele; } }
        public int Annee
            { get { return annee; } }
        public string Plaque
            { get { return plaque; } }
    }
}
