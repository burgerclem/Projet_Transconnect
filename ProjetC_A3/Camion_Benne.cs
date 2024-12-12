using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetC_A3
{
    public class Camion_Benne : Véhicule
    {
        int nombre_benne;
        bool grue;
        public Camion_Benne(string modele, int annee, string plaque,int type, bool grue, int nombre_benne, DateTime date)
            : base(modele, annee, plaque, type, date)
        {
            this.modele = modele;
            this.annee = annee;
            this.plaque = plaque;
            this.type = type;
            this.date = date;
            this.nombre_benne = nombre_benne;
            this.grue = grue;
        }

        public int Nombre_benne
            { get { return this.nombre_benne; } }
        public bool Grue
            { get { return this.grue; } }
    }
}
