using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetC_A3
{
    public class Client : Personne
    {
        List<Commande> commandes;
        public Client(int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, string tel, List<Commande> commandes = null)
            : base(nss, nom, prenom, naissance, adresse, mail, tel)
        {
            this.nss = nss;
            this.nom = nom;
            this.prenom = prenom;
            this.naissance = naissance;
            this.adresse = adresse;
            this.mail = mail;
            this.tel = tel;
            this.commandes = commandes;
        }

        public List<Commande> Commandes
        {
            get { return this.commandes; }
            set { this.commandes = value; }
        }
        public override string ToString()
        {
            string res = base.ToString();
            if(this.commandes != null)
            {
                foreach (Commande commande in commandes)
                {
                    res += "\n" + commande.ToString();
                }
            }
            return res;
        }
        public override int Nss
        {
            get { return this.nss; }
        }
        public override string Nom
        {
            get { return this.nom; }
            set { this.nom = value; }
        }
        public override string Prenom
        {
            get { return this.prenom; }
            set
            {
                this.prenom = value;
            }
        }
        public override DateTime Naissance
        {
            get { return this.naissance; }
        }
        public override string Adresse
        {
            get { return this.adresse; }
            set { this.adresse = value; }
        }
        public override string Mail
        {
            get { return this.mail; }
            set { this.mail = value; }
        }
        public override string Tel
        {
            get { return this.tel; }
            set { this.tel = value; }
        }
        static public Dictionary<int,int> MoyenneCommande()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", "Client.csv");
            string[] file = File.ReadAllLines(path);
            Dictionary<int, int> dictionnaire = new Dictionary<int, int>();

            foreach (var lines in file)
            {
                string[] info_temp = lines.Split(',');
                string nss_client = info_temp[0];

                string path_commande = Path.Combine(Environment.CurrentDirectory, @"Data\", "Commande.csv");
                string[] file_commande = File.ReadAllLines(path_commande);
                int temp = 0;
                foreach (var lines_commande in file_commande)
                {
                    string[] info_temp2 = lines_commande.Split(',');
                    if (info_temp2[1] == nss_client)
                    {
                        temp+= Convert.ToInt32(info_temp2[4]);
                    }
                }
                temp /= file_commande.Length;
                int nss_client_int = Convert.ToInt32(nss_client);
                dictionnaire[nss_client_int] = temp;
            }
            return dictionnaire;
        }
    }
}
