using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProjetC_A3
{
    public class Chauffeur : Personne
    {
        //Une seule livraison par jour, libre
        DateTime embauche;
        DateTime date = DateTime.Parse("2024-01-01");
        public Chauffeur(int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, string tel, DateTime embauche,DateTime date)
            : base(nss, nom, prenom, naissance, adresse, mail, tel)
        {
            this.nss = nss;
            this.nom = nom;
            this.prenom = prenom;
            this.naissance = naissance;
            this.adresse = adresse;
            this.mail = mail;
            this.tel = tel;
            this.date = date;
            this.embauche = embauche;
        }

        public bool Vehicule_Dispo()
        {
            string fileName = "Chauffeur.csv";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            string[] lines = File.ReadAllLines(path);
            bool test = false;
            foreach (string line in lines)
            {
                string[] fortnite = line.Split(',');
                if (date != DateTime.Today)
                {
                    test = true;
                }
            }
            return test;
        }


        public override string Nom { get { return nom; } set { nom = value; } }
        public override string Prenom { get { return prenom; } set { prenom = value; } }
        public override string Adresse { get { return adresse; } set { adresse = value; } }
        public override string Mail { get { return mail; } set { mail = value; } }
        public override string Tel { get { return tel; } set { tel = value; } }
        public override int Nss { get { return nss; } }
        public override DateTime Naissance { get { return naissance; } }
        public DateTime Embauche { get { return embauche; } }
        public DateTime Date { get { return date; } }


        static public Dictionary<int,int> NombreOp()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", "Chauffeur.csv");
            string[] file = File.ReadAllLines(path);
            Dictionary<int, int> dictionnaire = new Dictionary<int, int>();

            foreach(var lines in file)
            {
                string[] info_temp = lines.Split(',');
                string nss_chauffeur = info_temp[0];

                string path_commande = Path.Combine(Environment.CurrentDirectory, @"Data\", "Commande.csv");
                string[] file_commande = File.ReadAllLines(path_commande);
                int temp = 0;
                foreach(var lines_commande in file_commande)
                {
                    string[] info_temp2 = lines_commande.Split(',');
                    if (info_temp2[5] == nss_chauffeur)
                    {
                        temp++;
                    }
                }
                int nss_chauffeur_int = Convert.ToInt32(nss_chauffeur);
                dictionnaire[nss_chauffeur_int] = temp;
            }
            return dictionnaire;
        }
    }
}
