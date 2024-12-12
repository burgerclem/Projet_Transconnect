using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetC_A3
{
    public class Noeud
    {
        int valeur;
        Noeud filsg;
        Noeud filsd;
        #region Propriétés
        public int Valeur
        {
            get { return this.valeur; }
            set { this.valeur = value; }
        }
        public Noeud FilsDroit
        {
            get { return this.filsd; }
            set { this.filsd = value; }
        }
        public Noeud FilsGauche
        {
            get { return this.filsg; }
            set { this.filsg = value; }
        }

        #endregion
        #region Constructeurs
        public Noeud( int val, Noeud ng, Noeud nd)
        {
            this.valeur = val;
            this.filsg = ng;
            this.filsd = nd;

        }
        public Noeud (int valeur)
        {
            this.valeur = valeur;
            this.filsg = null;
            this.filsd = null;
        }
        public Noeud()
        {
            this.valeur = 0;
            this.filsg = null;
            this.filsd = null;
        }
        #endregion
        public bool AssocierNoeudFilsGauche(Noeud enfant)
        {
            bool ok = false;
           
                if (this.filsg == null && enfant != null)
                {
                    this.filsg = enfant;
                    ok = true;
                }

            
            return ok;
        }
        public bool AssocierNoeudFilsDroite(Noeud enfant)
        {
            bool ok = false;
           
                if (this.filsd == null && enfant != null)
                {
                    this.filsd = enfant;
                    ok = true;
                }
            
            return ok;
        }
    }
}
