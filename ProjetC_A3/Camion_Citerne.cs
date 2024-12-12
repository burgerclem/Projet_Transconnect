using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetC_A3
{
    public class Camion_Citerne : Véhicule
    {
        int taille_cuve;
        string produit;
        public Camion_Citerne(string modele, int annee, string plaque, int taille_cuve, string produit,int type, DateTime date)
            : base(modele, annee, plaque, type, date)
        {
            this.modele = modele;
            this.annee = annee;
            this.plaque = plaque;
            this.type = type;
            this.date = date;
            this.taille_cuve = taille_cuve;
            this.produit = produit;
        }
        public int Taille_cuve
            { get { return this.taille_cuve; } }
        public string Produit
            { get { return this.produit; } }
    }
}
