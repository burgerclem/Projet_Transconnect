using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetC_A3
{
    public class Voiture : Véhicule
    {
        int nombre_passagers;
        public Voiture(string modele, int annee, string plaque, int type, int nombre_passagers, DateTime date)
            :base(modele,annee, plaque,type, date)
        {
            this.modele = modele;
            this.annee = annee;
            this.plaque = plaque;
            this.type = type;
            this.date = date;
            this.nombre_passagers = nombre_passagers;
        }
        public int Nombre_passagers
            { get { return nombre_passagers; } }
    }
}
