using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetC_A3
{
    public class Salarié : Personne
    {
        DateTime entree;
        string poste;
        int salaire;

        public Salarié(int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, string tel, DateTime entree, string poste, int salaire)
            :base(nss,nom,prenom,naissance,adresse,mail,tel)
        {
            this.nss = nss;
            this.nom = nom;
            this.prenom = prenom;
            this.naissance = naissance;
            this.adresse = adresse;
            this.mail = mail;
            this.tel = tel;
            this.entree = entree;
            this.poste = poste;
            this.salaire = salaire;
        }
        static public Salarié findSalarié(int nss)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", "Salarie.csv");
            string[] file = File.ReadAllLines(path);
            Salarié res = null;
            if(file != null && file.Length != 0)
            {
                for (int i = 0; i < file.Length; i++)
                {
                    string[] info_temp = file[i].Split(',');
                    if (Convert.ToInt32(info_temp[1]) == nss)
                    {
                        res = new Salarié(Convert.ToInt32(info_temp[1]), info_temp[2], info_temp[3], DateTime.Parse(info_temp[4]), info_temp[5], info_temp[6], info_temp[7], DateTime.Parse(info_temp[8]), info_temp[9], Convert.ToInt32(info_temp[10]));
                    }
                }
            }
            return res;
        }
        public DateTime Entree { get { return entree; } }
        public string Poste { get { return poste; } set { poste = value; } }
        public int Salaire { get { return salaire; } set { salaire = value; } }
        public override string Nom { get { return nom; } set { nom = value; } }
        public override string Prenom { get { return prenom; } set { prenom = value; } }
        public override DateTime Naissance { get { return entree; } }
        public override string Adresse { get { return adresse; } set { adresse = value; } }
        public override string Mail { get { return mail; } set { mail = value; } }
        public override string Tel { get { return tel; } set { tel = value; } }
        public override int Nss { get { return nss; } } 
    }
}
