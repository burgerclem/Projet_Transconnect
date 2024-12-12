using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetC_A3
{
    public class Camionnette : Véhicule
    {
        string usage;
        public Camionnette(string modele, int annee, string plaque, string usage,int type, DateTime date)
            : base(modele, annee, plaque,type,date)
        {
            this.modele = modele;
            this.annee = annee;
            this.plaque = plaque;
            this.type = type;
            this.date = date;
            this.usage = usage;
        }
        public string Usage
            { get { return usage; } }
    }
}
