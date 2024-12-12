using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetC_A3
{
    public class Camion_Frigorifique : Véhicule
    {
        int nombre_electro;
        public Camion_Frigorifique(string modele, int annee, string plaque,int type, int nombre_electro, DateTime date)
            :base(modele,annee,plaque, type, date)
        {
            this.modele = modele;
            this.annee = annee;
            this.plaque = plaque;
            this.type = type;
            this.date = date;
            this.nombre_electro = nombre_electro;
        }
    }
}
