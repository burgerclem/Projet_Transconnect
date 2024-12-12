using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using DocumentFormat.OpenXml.ExtendedProperties;
using Irony.Parsing;
using System.Threading;

namespace ProjetC_A3
{
    public class Program
    {
        static void AfficherArbre(NoeudN node, int niveau)
        {
           Console.WriteLine(new string(' ', niveau * 2) + node.Value.Nom + " (" + node.Value.Poste + ")");
            foreach (NoeudN child in node.Children)
            {
                AfficherArbre(child, niveau + 1);
            }
        }
        static void Main(string[] args)
        {             
            Console.Title = "Transconnect App"; //donne une nom à la fenetre de la console
            Titre();
            Titre_Camion();
            int ex = -1;
            string stop = "";
            do
            {
                if(ex != -1)
                {
                    Console.Clear();
                }
                Console.WriteLine("Saisir le numéro du module :\n1-Module Client\n2-Module Salarié\n3-Module Commande\n4-Module Stats\n5-Module Autre");
                ex = Convert.ToInt32(Console.ReadLine());
                if(ex == 1 || ex == 2 || ex==3 || ex==4 || ex==5)
                {
                    switch (ex)
                    {
                        case 1:
                            ModuleClient();
                            break;
                        case 2:
                            ModuleSalarie();
                            break;
                        case 3:
                            ModuleCommande();
                            break;
                        case 4:
                            ModuleStatistique();
                            break;
                        case 5:
                            ModuleAutre();
                            break;
                        default:
                            ex = -1;
                            break;
                    }
                }
                Console.WriteLine("Voulez-vous continuer ? (OUI/NON)");
                stop = Console.ReadLine();
            } while (stop.ToUpper() == "OUI");
        }
        static public void ModuleClient()
        {
            Console.Clear();
            Console.WriteLine("   ____   _   _                  _    ");
            Console.WriteLine("  / ___| | | (_)   ___   _ __   | |_  ");
            Console.WriteLine(" | |     | | | |  / _ \\ | '_ \\  | __|");
            Console.WriteLine(" | |___  | | | | |  __/ | | | | | |_ ");
            Console.WriteLine("  \\____| |_| |_|  \\___| |_| |_|  \\__|");
            Console.WriteLine();
            Console.WriteLine("Que souhaitez-vous faire ?\n1-Entrer un nouveau client\n2-Supprimer un client\n3-Modifier un client\n4-Afficher les clients");
            string fileName = "Client.csv";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            string[] file = File.ReadAllLines(path);
            int ex = -1;
            int stop = 0;
            do
            {
                ex = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (ex)
                {
                    case 1:
                        Console.WriteLine("Saisir sous cette forme :\"nss nom prenom date_naissance adresse mail tel\"");
                        string[] temp = Console.ReadLine().Split(' ');
                        File.AppendAllText(path, temp[0] + "," + temp[1] + "," + temp[2] + "," + temp[3] + "," + temp[4] + "," + temp[5] + "," + temp[6]);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Saisir un nss, nom et prenom de cette forme : \"nss nom prenom\"");
                        string[] temp3 = Console.ReadLine().Split(' ');
                        string suppr = temp3[0] + "," + temp3[1] + "," + temp3[2];
                        file = file.Where(l => !l.Contains(suppr)).ToArray();
                        File.WriteAllLines(path, file);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Saisir un nss, nom et prenom de cette forme à modifier : \"nss nom prenom\"");
                        string[] a = Console.ReadLine().Split(' ');
                        for (int i = 0; i < file.Length; i++)
                        {
                            string[] info_temp = file[i].Split(',');
                            Client client_temp = new Client(Convert.ToInt32(info_temp[0]), info_temp[1], info_temp[2], DateTime.Parse(info_temp[3]), info_temp[4], info_temp[5], info_temp[6]);
                            if (client_temp.Nss == Convert.ToInt32(a[0]) && client_temp.Nom == a[1] && client_temp.Prenom == a[2])
                            {
                                Console.WriteLine("Que voulez-vous modifier ?\n1-Nom\n2-Prenom\n3-Adresse\n4-Mail\n5-Tel");
                                int temp_num = -1;
                                while (temp_num == -1)
                                {
                                    temp_num = Convert.ToInt32(Console.ReadLine());
                                }
                                switch (temp_num)
                                {
                                    case 1:
                                        Console.WriteLine("Donnez un nouveau nom");
                                        string new_nom = Console.ReadLine();
                                        file[i].Replace(client_temp.Nom, new_nom);
                                        File.WriteAllLines(path, file);
                                        break;
                                    case 2:
                                        Console.WriteLine("Donnez un nouveau prenom");
                                        string new_prenom = Console.ReadLine();
                                        file[i].Replace(client_temp.Prenom, new_prenom);
                                        File.WriteAllLines(path, file);
                                        break;
                                    case 3:
                                        Console.WriteLine("Donnez une nouvelle adresse");
                                        string new_adresse = Console.ReadLine();
                                        file[i].Replace(client_temp.Adresse, new_adresse);
                                        File.WriteAllLines(path, file);
                                        break;
                                    case 4:
                                        Console.WriteLine("Donnez un nouveau nom");
                                        string new_mail = Console.ReadLine();
                                        file[i].Replace(client_temp.Mail, new_mail);
                                        File.WriteAllLines(path, file);
                                        break;
                                    case 5:
                                        Console.WriteLine("Donnez un nouveau nom");
                                        string new_tel = Console.ReadLine();
                                        file[i].Replace(client_temp.Tel, new_tel);
                                        File.WriteAllLines(path, file);
                                        break;
                                    default:
                                        temp_num = -1;
                                        break;
                                }
                            }
                        }
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Tri par :\n1-Ordre Alphabétique\n2-Par ville\n3-Par achat");
                        int temp_num2 = -1;
                        while (temp_num2 < 0)
                        {

                            temp_num2 = Convert.ToInt32(Console.ReadLine());
                        }
                        List<Client> liste_client = new List<Client>(); 
                        foreach(string line in file)
                        {
                            string[] info_temp = line.Split(',');
                            liste_client.Add(new Client(Convert.ToInt32(info_temp[0]), info_temp[1], info_temp[2], DateTime.Parse(info_temp[3]), info_temp[4], info_temp[5], info_temp[6]));
                        }
                        switch (temp_num2)
                        {
                            case 1:
                                List<Client> sortedClient = liste_client.OrderBy(o => o.Prenom).OrderBy(o => o.Nom).ToList();
                                sortedClient.ForEach(x => Console.WriteLine(x.ToString()));
                                break;
                            case 2:
                                List<Client> sortedClient2 = liste_client.OrderBy(o => o.Adresse.Split(" ")[4]).ToList();
                                sortedClient2.ForEach(x => Console.WriteLine(x.ToString()));
                                break;
                            case 3:
                                List<Client> sortedClient3 = liste_client.OrderBy(o => o.Commandes.Select(c => c.Prix).Average()).ToList();
                                sortedClient3.ForEach(x => Console.WriteLine(x.ToString()));
                                break;
                            default:
                                temp_num2 = -1;
                                break;
                        }
                        break;
                    default:
                        ex = -1;
                        break;
                }
            } while (ex != 1 && ex != 2 && ex != 3 && ex != 4);
        }

        static public void ModuleSalarie()
        {
            Console.Clear();
            Console.WriteLine("   ____            _                  _     __         ");
            Console.WriteLine("  / ___|    __ _  | |   __ _   _ __  (_)   /_/     ");
            Console.WriteLine("  \\___ \\   / _` | | |  / _` | | '__| | |  / _ \\    ");
            Console.WriteLine("   ___) | | (_| | | | | (_| | | |    | | |  __/    ");
            Console.WriteLine("  |____/   \\__,_| |_|  \\__,_| |_|    |_|  \\___|    ");
            Console.WriteLine();
            Console.WriteLine(("Que souhaitez-vous faire ?\n1-Embaucher un salarié\n2-Licencier un salarié\n3-Afficher les salariés\n4-Afficher l'organigramme"));
            string fileName = "Salarie.csv";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            int ex = -1;
            while(ex<=0 || ex > 4)
            {
                ex=Convert.ToInt32(Console.ReadLine());
            }
            switch (ex)
            {
                case 1:
                    Console.WriteLine("Saisir le nom ddu salarié à embaucher sous cette forme :\"nss nom prenom date_naissance adresse mail tel poste salaire\"");
                    string nouveau = Console.ReadLine();
                    string[] temp = nouveau.Split(' ');
                    Salarié test=new Salarié(Convert.ToInt32(temp[0]), temp[1], temp[2], Convert.ToDateTime(temp[3]), temp[4], temp[5], temp[6], DateTime.Now, temp[7], Convert.ToInt32(temp[8]));
                    string[] lines = File.ReadAllLines(path);
                    bool testi = true;
                    int compteur = 0;
                    foreach(string line in lines)
                    {
                        if (lines[compteur] != "")
                        {
                            string[] fortnite = line.Split(',');
                            if (Convert.ToInt32(fortnite[0]) == Convert.ToInt32(temp[0]))
                            {
                                Console.WriteLine("Ce salarié est déja dans la base de donnée");
                                testi = false;
                                break;

                            }
                        }
                        compteur += 1;
                    }
                    if (testi == false) { break; }
                    nouveau = Convert.ToInt32(temp[0])+","+ temp[1] + "," + temp[2] + "," + temp[3] + "," + temp[4] + "," + temp[5] + "," + temp[6] + "," + DateTime.Now.ToShortDateString() + "," + temp[7] + "," + Convert.ToInt32(temp[8]);
                    int indexPremiereLigneVide = Array.IndexOf(lines, "");

                    if (indexPremiereLigneVide != -1)
                    {
                        lines[indexPremiereLigneVide] = nouveau;
                    }
                    else
                    {
                        Array.Resize(ref lines, lines.Length + 1);
                        lines[lines.Length - 1] = nouveau;
                    }
                    Console.WriteLine("Salarié ajouté à la base de donnée");
                    File.WriteAllLines(path, lines);

                    break;

                case 2:
                    Console.WriteLine("Saisir le nss, nom et prenom du salarié à licencier, de cette forme : \"nss nom prenom\"");
                    string[] temp2 = Console.ReadLine().Split(' ');
                    string[] lines2 = File.ReadAllLines(path);
                    int compte = 0;
                    bool fait = false;
                    foreach (string line in lines2)
                    {
                        if (lines2[compte] != "")
                        {
                            string[] fortnite = line.Split(',');
                            if (Convert.ToInt32(temp2[0]) == Convert.ToInt32(fortnite[0]) && temp2[1] == fortnite[1] && temp2[2] == fortnite[2])
                            {
                                lines2[compte] = "";
                                fait = true;
                            }
                        }
                        compte += 1;
                    }
                    File.WriteAllLines(path, lines2);
                    if (fait) { Console.WriteLine("Le salarié à été retiré de la base de donnée"); }
                    else { Console.WriteLine("Aucun salarié trouvé avec ses critères"); }
                    break;

                case 3:
                    string[] lines3 = File.ReadAllLines(path);
                    int compter = 0;
                    foreach (string line in lines3)
                    {
                        if (lines3[compter] != "")
                        {
                            Console.WriteLine(line + "\n");
                        }
                        compter++;
                    }
                    break;
                case 4:
                    ArbreNAire arbre = new ArbreNAire();
                    arbre.ConstruireArbreDepuisCSV("Salarie.csv");

                    if (arbre.Root != null)
                    {
                        AfficherArbre(arbre.Root, 0);
                    }
                    else
                    {
                        Console.WriteLine("L'arbre est vide.");
                    }
                    break;
                default:
                    break;
            }
            

        }

        static public void ModuleCommande()
        {
            Console.Clear();
            Console.WriteLine("   ____                                                       _         ");
            Console.WriteLine("  / ___|   ___    _ __ ___    _ __ ___     __ _   _ __     __| |   ___  ");
            Console.WriteLine(" | |      / _ \\  | '_ ` _ \\  | '_ ` _ \\   / _` | | '_ \\   / _` |  / _ \\");
            Console.WriteLine(" | |___  | (_) | | | | | | | | | | | | | | (_| | | | | | | (_| | |  __/");
            Console.WriteLine("  \\____|  \\___/  |_| |_| |_| |_| |_| |_|  \\__,_| |_| |_|  \\__,_|  \\___|");
            Console.WriteLine();
            Console.WriteLine("Que souhaitez-vous faire ?\n1-Entrer une nouvelle commande\n2-Supprimer une commande\n3-Modifier une commande\n4-Afficher les commandes");
            int a = Convert.ToInt32(Console.ReadLine());
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", "Commande.csv");
            string path2 = Path.Combine(Environment.CurrentDirectory, @"Data\", "Client.csv");
            string path3 = Path.Combine(Environment.CurrentDirectory, @"Data\", "Vehicule.csv");
            string path4 = Path.Combine(Environment.CurrentDirectory, @"Data\", "Chauffeur.csv");
            string[] file = File.ReadAllLines(path);
            string[] file2 = File.ReadAllLines(path2);
            string[] file3 = File.ReadAllLines(path3);
            string[] file4 = File.ReadAllLines(path4);
            switch (a)
            {
                case 1:
                    Console.WriteLine("Saisir votre nss, nom et prenom afin de passer commande, de cette forme : \"nss nom prenom\"");
                    string[] temp_clientcom= Console.ReadLine().Split(' ');
                    string[] lines_clientcom = File.ReadAllLines(path2);
                    int compte = 0;
                    bool fait = true;
                    int nss_client = Convert.ToInt32(temp_clientcom[0]);
                    foreach (string line in lines_clientcom)
                    {
                        if (lines_clientcom[compte] != "")
                        {
                            string[] fortnite = line.Split(',');
                            if (Convert.ToInt32(temp_clientcom[0]) == Convert.ToInt32(fortnite[0]) && temp_clientcom[1] == fortnite[1] && temp_clientcom[2] == fortnite[2])
                            {
                                fait = false;
                            }
                        }
                        compte += 1;
                    }
                    if (fait) { Console.WriteLine("Client introuvable, veuillez vous créer votre compte client"); }
                    else
                    {
                        Console.WriteLine("Saisir les paramètres de la commande");
                        Console.WriteLine("Saisir une ville de départ");
                        string ville_de_depart = Console.ReadLine();
                        Console.WriteLine("Saisir une ville d'arrivée");
                        string ville_de_arrivee = Console.ReadLine();
                        Console.WriteLine("Saisir le numéro du véhicule \n1-Voiture\n2-Camionette\n3-Camion Frigorifique\n4-Camion Citerne\n5-Camion Benne");
                        int numero_vehicule = Convert.ToInt32(Console.ReadLine());
                        Véhicule vehicom = null;
                        string[] lines_vehicom = File.ReadAllLines(path3);
                        compte = 0;
                        foreach (string line in lines_vehicom)
                        {
                            if (lines_vehicom[compte] != "")
                            {
                                string[] fortnite = line.Split(',');
                                if (fortnite.Length==6)
                                {
                                    if (numero_vehicule == Convert.ToInt32(fortnite[4]) && DateTime.Parse(fortnite[5]) != DateTime.Today)
                                    {
                                        switch (numero_vehicule)
                                        {
                                            case 1:
                                                lines_vehicom[compte] = fortnite[0] + "," + fortnite[1] + "," + fortnite[2] + "," + fortnite[3] + "," + fortnite[4] + "," + DateTime.Today;
                                                vehicom = new Voiture(fortnite[0], Convert.ToInt32(fortnite[1]), fortnite[2], Convert.ToInt32(fortnite[3]), Convert.ToInt32(fortnite[4]), DateTime.Today);
                                                break;
                                            case 2:
                                                lines_vehicom[compte] = fortnite[0] + "," + fortnite[1] + "," + fortnite[2] + "," + fortnite[3] + "," + fortnite[4] + "," + DateTime.Today;
                                                vehicom = new Camionnette(fortnite[0], Convert.ToInt32(fortnite[1]), fortnite[2], fortnite[3], Convert.ToInt32(fortnite[4]), DateTime.Today);
                                                break;
                                            case 3:
                                                lines_vehicom[compte] = fortnite[0] + "," + fortnite[1] + "," + fortnite[2] + "," + fortnite[3] + "," + fortnite[4] + "," + DateTime.Today;
                                                vehicom = new Camion_Frigorifique(fortnite[0], Convert.ToInt32(fortnite[1]), fortnite[2], Convert.ToInt32(fortnite[3]), Convert.ToInt32(fortnite[4]), DateTime.Today);
                                                break;
                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    if (numero_vehicule == Convert.ToInt32(fortnite[5]) && DateTime.Parse(fortnite[6]) != DateTime.Today)
                                    {
                                        switch (numero_vehicule)
                                        {
                                            case 4:
                                                lines_vehicom[compte] = fortnite[0] + "," + fortnite[1] + "," + fortnite[2] + "," + fortnite[3] + "," + fortnite[4] + "," + fortnite[5] + "," + DateTime.Today;
                                                vehicom = new Camion_Citerne(fortnite[0], Convert.ToInt32(fortnite[1]), fortnite[2], Convert.ToInt32(fortnite[3]),fortnite[4], Convert.ToInt32(fortnite[5]), DateTime.Today);
                                                break;
                                            case 5:
                                                lines_vehicom[compte] = fortnite[0] + "," + fortnite[1] + "," + fortnite[2] + "," + fortnite[3] + "," + fortnite[4] + "," + fortnite[5] + "," + DateTime.Today;
                                                vehicom = new Camion_Benne(fortnite[0], Convert.ToInt32(fortnite[1]), fortnite[2], Convert.ToInt32(fortnite[3]), Convert.ToBoolean(fortnite[4]), Convert.ToInt32(fortnite[5]), DateTime.Today);
                                                break;
                                        }
                                        break;
                                    }
                                }
                            }
                            compte += 1;
                        }
                        File.WriteAllLines(path3, lines_vehicom);
                        Chauffeur chauffcom = null;
                        string[] lines_chauffcom = File.ReadAllLines(path4);
                        compte = 0;
                        foreach (string line2 in lines_chauffcom)
                        {
                            if (lines_chauffcom[compte] != "")
                            {
                                string[] fortnite = line2.Split(',');
                                if (DateTime.Parse(fortnite[8]) != DateTime.Today)
                                {
                                    lines_chauffcom[compte] = fortnite[0] + "," + fortnite[1] + "," + fortnite[2] + "," + fortnite[3] + "," + fortnite[4] + "," + fortnite[5] + "," + fortnite[6] + "," + fortnite[7] + "," + DateTime.Today;
                                    chauffcom = new Chauffeur(Convert.ToInt32(fortnite[0]), fortnite[1], fortnite[2], DateTime.Parse(fortnite[3]), fortnite[4], fortnite[5], fortnite[6], DateTime.Parse(fortnite[7]), DateTime.Today);
                                    break;
                                }
                            }
                            compte += 1;
                        }
                        File.WriteAllLines(path4, lines_chauffcom);
                        string[] lines_com = File.ReadAllLines(path);
                        Commande com = new Commande(null,DateTime.Now, ville_de_depart, ville_de_arrivee);
                        string nouveau = com.Id + "," + nss_client + "," + ville_de_depart + "," + ville_de_arrivee + "," + (com.Prix+5*(-chauffcom.Embauche.Year+DateTime.Now.Year))+ "," + chauffcom.Nss + "," + DateTime.Today + "," + vehicom.Plaque;
                        int indexPremiereLigneVide = Array.IndexOf(lines_com, "");
                        if (indexPremiereLigneVide != -1)
                        {
                            lines_com[indexPremiereLigneVide] = nouveau;
                        }
                        else
                        {
                            Array.Resize(ref lines_com, lines_com.Length + 1);
                            lines_com[lines_com.Length - 1] = nouveau;
                        }
                        Console.WriteLine("Commande prise en charge");
                        File.WriteAllLines(path, lines_com);
                    }
                    
                    break;
                case 2:
                    Console.WriteLine("Saisir un numéro de commande:");
                    int id2 = Convert.ToInt32(Console.ReadLine());
                    List<string> file_List2 = file.ToList();
                    int index2 = file_List2.FindIndex(l => {
                        string[] columns = l.Split(',');
                        return int.Parse(columns[0]) == id2;
                    });
                    if(index2 == -1)
                    {
                        file_List2.RemoveAt(index2);
                        File.WriteAllLines(path, file_List2);
                    }
                    break;
                case 3:           
                    Console.WriteLine("Saisir un numéro de commande:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    List<string> file_List = file.ToList();
                    int index = file_List.FindIndex(l => {
                        string[] columns = l.Split(',');
                        return int.Parse(columns[0]) == id;
                    });
                    if(index != -1)
                    {
                            Console.WriteLine("Que voulez-vous modifier ?\n1-Ville de départ\n2-Ville d'arrivée");
                            int temp_num = -1;
                            while (temp_num == -1)
                            {
                                temp_num = Convert.ToInt32(Console.ReadLine());
                            }
                            switch (temp_num)
                            {
                                case 1:
                                    Console.WriteLine("Donnez une nouvelle ville de départ");
                                    string new_nom = Console.ReadLine();
                                    string[] columns = file_List[index].Split(',');
                                    columns[2] = new_nom;
                                    file_List[index] = string.Join(",", columns);
                                    File.WriteAllLines(path, file_List);
                                    break;
                                case 2:
                                    Console.WriteLine("Donnez une nouvelle ville d'arrivée");
                                    string new_nom2 = Console.ReadLine();
                                    string[] columns2 = file_List[index].Split(',');
                                    columns2[3] = new_nom2;
                                    file_List[index] = string.Join(",", columns2);
                                    File.WriteAllLines(path, file_List);
                                    break;
                                default:
                                    temp_num = -1;
                                    break;
                            }
                    }
                    break;
                case 4:
                    List<Commande> liste_commande = new List<Commande>();
                    Client client_temp = null;
                    foreach (string line in file)
                    {
                        string[] info_temp = line.Split(',');
                        foreach (string line2 in file2)
                        {
                            string[] client_decoupe = line2.Split(',');
                            if (client_decoupe[0] == info_temp[0])
                            {
                                client_temp = new Client(Convert.ToInt32(info_temp[0]), client_decoupe[1], client_decoupe[2], DateTime.Parse(client_decoupe[3]), client_decoupe[4], client_decoupe[5], client_decoupe[6]);
                            }
                        }
                        liste_commande.Add(new Commande(client_temp, DateTime.Parse(info_temp[1]), info_temp[2], info_temp[3]));
                    }
                    liste_commande.ForEach(x => x.ToString());
                    break;
                default:
                    break;
            }
        }

        static public void ModuleStatistique()
        {
            Console.Clear();
            Console.WriteLine("   ____    _             _     _         _     _                                ");
            Console.WriteLine("  / ___|  | |_    __ _  | |_  (_)  ___  | |_  (_)   __ _   _   _    ___   ___   ");
            Console.WriteLine("  \\___ \\  | __|  / _` | | __| | | / __| | __| | |  / _` | | | | |  / _ \\ / __|  ");
            Console.WriteLine("   ___) | | |_  | (_| | | |_  | | \\__ \\ | |_  | | | (_| | | |_| | |  __/ \\__ \\  ");
            Console.WriteLine("  |____/   \\__|  \\__,_|  \\__| |_| |___/  \\__| |_|  \\__, |  \\__,_|  \\___| |___/  ");
            Console.WriteLine("                                                      |_|                       ");
            Console.WriteLine();

            Console.WriteLine("Module statistique :");
            Console.WriteLine("1-Afficher par chauffeur le nombre de livraisons effectuées");
            Console.WriteLine("2-Afficher les commandes selon une période de temps");
            Console.WriteLine("3-Afficher la moyenne des prix des commandes");
            Console.WriteLine("4-Afficher la moyenne des comptes clients");
            Console.WriteLine("5-Afficher la liste des commandes pour un client");

            Console.WriteLine("\nSaisir votre choix : ");
            string a = Console.ReadLine();

            if(int.TryParse(a, out int value))
            {
                switch(value)
                {
                    case 1:
                        Dictionary<int, int> dico_temp = Chauffeur.NombreOp();
                        foreach(var dico in dico_temp)
                        {
                            Console.WriteLine(dico.Key + " " + dico.Value);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Veuillez indiquez une période de temps de la forme : YYYY-MM-DD");
                        Console.Write("Choisissez une date de début: ");
                        DateTime debut = DateTime.Parse(Console.ReadLine());
                        Console.Write("\nChoisissez une date de fin: ");
                        DateTime fin = DateTime.Parse(Console.ReadLine());
                        string pathx = Path.Combine(Environment.CurrentDirectory, @"Data\", "Commande.csv");
                        string[] lines_comtemps = File.ReadAllLines(pathx);
                        string pathy = Path.Combine(Environment.CurrentDirectory, @"Data\", "Client.csv");
                        string[] lines_clitemps = File.ReadAllLines(pathy);
                        int compte = 0;
                        string t="Voici les commandes ayant été passé entre le "+debut+" et le "+fin+" sont:";
                        bool test = false;
                        foreach (string line in lines_comtemps)
                        {
                            if (lines_comtemps[compte] != "")
                            {
                                string[] fortnite = line.Split(',');
                                if (DateTime.Parse(fortnite[6])>=debut && DateTime.Parse(fortnite[6]) <= fin)
                                {
                                    int nss = Convert.ToInt32(fortnite[1]);
                                    string nom_client = "";
                                    string prenom_client = "";
                                    foreach (string line2 in lines_clitemps)
                                    {
                                        string[] fortnite2= line2.Split(",");
                                        if (nss == Convert.ToInt32(fortnite2[0]))
                                        {
                                            nom_client = fortnite2[1];
                                            prenom_client = fortnite2[2];
                                            break;
                                        }
                                    }
                                    test = true;
                                    t += "\nN° Commande: " + fortnite[0] + " Nss Client: " + nss + " Nom Client: " + nom_client + " Prenom Client: " + prenom_client + " Prix Commande: " + fortnite[4] + " Ville départ: " + fortnite[2] + " Ville Arrivee: " + fortnite[3]+" Date Commade"+fortnite[6];
                                }
                            }
                            compte += 1;
                        }
                        if (test) { Console.WriteLine(t); }
                        else { Console.WriteLine("Aucune commande n'a été passé durent cette période."); }
                        break;
                    case 3:
                        Commande commande_temp = new Commande();
                        int commande_moy = commande_temp.Moyenne();
                        Console.WriteLine(commande_moy);
                        break;
                    case 4:
                        Dictionary<int,int> dico_moyenne_client = Client.MoyenneCommande();
                        foreach (var dico in dico_moyenne_client)
                        {
                            Console.WriteLine(dico.Key + " " + dico.Value);
                        }
                        break;
                    case 5:
                        Console.WriteLine("Choisir le nss du client");
                        int nss_client = Convert.ToInt32(Console.ReadLine());
                        string path = Path.Combine(Environment.CurrentDirectory, @"Data\", "Client.csv");
                        string[] file = File.ReadAllLines(path);
                        string nom = ""; string prenom = "";
                        foreach(var line in file)
                        {
                            string[] info_temp = line.Split(',');
                            if (Convert.ToInt32(info_temp[0]) == nss_client)
                            {
                                nom = info_temp[1]; prenom = info_temp[2];
                            }
                        }
                        string path_commande = Path.Combine(Environment.CurrentDirectory, @"Data\", "Commande.csv");
                        string[] file_commande = File.ReadAllLines(path_commande);
                        Console.WriteLine(nss_client + " " + nom + " " + prenom + " :");
                        foreach (var line in file_commande)
                        {
                            string[] info_temp = line.Split(',');
                            if (Convert.ToInt32(info_temp[1]) == nss_client)
                            {
                                Console.WriteLine(info_temp[2] + " " + info_temp[3] + " " + info_temp[4] + " " + info_temp[5] + " " + info_temp[6] + " " + info_temp[7]);
                            } 
                        }
                        break;
                    default:
                        break;
                }
            }
        }



        static public void ModuleAutre()
        {
            Console.WriteLine("     _              _                  ");
            Console.WriteLine("    / \\     _   _  | |_   _ __    ___  ");
            Console.WriteLine("   / _ \\   | | | | | __| | '__|  / _ \\");
            Console.WriteLine("  / ___ \\  | |_| | | |_  | |    |  __/");
            Console.WriteLine(" /_/   \\_\\  \\__,_|  \\__| |_|     \\___|");
            Console.WriteLine();

            Console.WriteLine("Que souhaitez-vous faire ? : \n1-Ecrire un commentaire\n2-Lire les commentaires\n3-Ajouter un véhicule à la base de donnée");
            int ex2 = -1;
            while (0 > ex2 || ex2 > 4)
            {
                ex2 = Convert.ToInt32(Console.ReadLine());
            }

            switch (ex2)
            {
                case 1:
                    Console.WriteLine("Saisir un nss");
                    string nss_saisie = Console.ReadLine();
                    if(int.TryParse(nss_saisie,out int result))
                    {
                        Console.WriteLine("Saisir le commentaire :");
                        string com = Console.ReadLine();

                        Console.WriteLine("Saisir une note (possiblement décimale) entre 1 et 10");
                        int note = -1;
                        do
                        {
                            note = Convert.ToInt32(Console.ReadLine());
                        } while (note < 1 || note > 10);

                        string path_com = Path.Combine(Environment.CurrentDirectory, @"Data\", "Commentaire.csv");
                        File.AppendAllText(path_com, nss_saisie + "," + com + "," + note);
                    }
                    break;
                case 2:
                    string path_com2 = Path.Combine(Environment.CurrentDirectory, @"Data\", "Commentaire.csv");
                    string[] file = File.ReadAllLines(path_com2);
                    foreach(var ligne in file)
                    {
                        string[] info_temp = ligne.Split(',');
                        Console.WriteLine("Nss client : " + info_temp[0] + " Commentaire : " + info_temp[1] + " Note : " + info_temp[2]);
                    }
                    break;
                case 3:
                    string fileName = "Vehicule.csv";
                    string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
                            Console.WriteLine("Saisir le numéro du véhicule \n1-Voiture\n2-Camionette\n3-Camion Frigorifique\n4-Camion Citerne\n5-Camion Benne");
                            int num_vehi = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Saisir les données du véhicules de la forme :marque année plaque:");
                            string nouveau = Console.ReadLine()+" "+num_vehi;
                            bool six_element = true;
                            switch (num_vehi)
                            {
                                case 1:
                                    Console.WriteLine("Saisir le nombre de passager que la voiture peu prendre");
                                    nouveau += " "+Console.ReadLine();
                                    break;
                                case 2:
                                    Console.WriteLine("Saisir l'usage de la camionette");
                                    nouveau += " " + Console.ReadLine();
                                    break;
                                case 3:
                                    Console.WriteLine("Saisir le nombre de groupe éléctrogène sur le camion frigorifique");
                                    nouveau += " " + Console.ReadLine();
                                    break;
                                case 4:
                                    Console.WriteLine("Saisir la taille de la cuve en m^3 du camion citerne");
                                    nouveau += " " + Console.ReadLine();
                                    Console.WriteLine("Saisir le type de produit trandporté dans le camion citerne");
                                    nouveau += " " + Console.ReadLine();
                                    six_element = false;
                                    break;
                                case 5:
                                    Console.WriteLine("Saisir le nombre de benne sur le camion benne");
                                    nouveau += " " + Console.ReadLine();
                                    Console.WriteLine("Saisir si le camion benne à une grue ou non (true ou false)");
                                    nouveau += " " + Console.ReadLine();
                                    six_element = false;
                                    break;
                            }
                            nouveau += " " + "2024-01-01";
                            string[] temp = nouveau.Split(' ');                            
                            string[] lines_newvehi = File.ReadAllLines(path);
                            bool testi = true;
                            int compteur = 0;
                            foreach (string line in lines_newvehi)
                            {
                                if (lines_newvehi[compteur] != "")
                                {
                                    string[] fortnite = line.Split(',');
                                    Console.WriteLine(fortnite[2]);
                                    Console.WriteLine(nouveau[2]);
                                    if (fortnite[2].Equals(temp[2]))
                                    {
                                        Console.WriteLine("Ce vehicule est déja dans la base de donnée");
                                        testi = false;
                                        break;

                                    }
                                }
                                compteur += 1;
                            }
                            if (testi == false) { break; }
                            if (six_element)
                            {
                                nouveau = temp[0] + "," + temp[1] + "," + temp[2] + "," + temp[3] + "," + temp[4] + "," + temp[5];
                            }
                            else
                            {
                            nouveau = temp[0] + "," + temp[1] + "," + temp[2] + "," + temp[3] + "," + temp[4] + "," + temp[5]+","+temp[6];
                            }
                            
                            int indexPremiereLigneVide = Array.IndexOf(lines_newvehi, "");

                            if (indexPremiereLigneVide != -1)
                            {
                                lines_newvehi[indexPremiereLigneVide] = nouveau;
                            }
                            else
                            {
                                Array.Resize(ref lines_newvehi, lines_newvehi.Length + 1);
                                lines_newvehi[lines_newvehi.Length - 1] = nouveau;
                            }
                            Console.WriteLine("Vehicule ajouté à la base de donnée");
                            File.WriteAllLines(path, lines_newvehi);
                            break;
                case 4:
                    break;
                default:
                    break;
            }
        }




            static public void Titre()
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("        _______ _______ ______ _   _ _______ _____ ____  _   _ ");
            Console.WriteLine("     /\\|__   __|__   __|  ____| \\ | |__   __|_   _/ __ \\| \\ | |");
            Console.WriteLine("    /  \\  | |     | |  | |__  |  \\| |  | |    | || |  | |  \\| |");
            Console.WriteLine("   / /\\ \\ | |     | |  |  __| | . ` |  | |    | || |  | | . ` |");
            Console.WriteLine("  / ____ \\| |     | |  | |____| |\\  |  | |   _| || |__| | |\\  |");
            Console.WriteLine(" /_/    \\_\\_|     |_|  |______|_| \\_|  |_|  |_____\\____/|_| \\_|");
            Console.WriteLine("                                                               ");
            Console.WriteLine("                                                               ");




            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n Pour une expérience optimale, veillez passer la console en pleine écran.\n Appuyer sur une touche un fois cela fait");
            Console.ReadKey();


            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.WriteLine(); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine(); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine();
            Console.WriteLine("████████╗");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.WriteLine(); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine(); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("████████╗");
            Console.WriteLine("╚══██╔══╝██████╗ ");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.WriteLine(); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine("████████╗"); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("╚══██╔══╝██████╗ ");
            Console.WriteLine("   ██║   ██╔══██╗ █████╗ ");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.WriteLine(); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("████████╗");
            Console.WriteLine("╚══██╔══╝██████╗ "); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██╔══██╗ █████╗ ");
            Console.WriteLine("   ██║   ██████╔╝██╔══██╗███╗   ██╗");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.WriteLine("████████╗"); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╚══██╔══╝██████╗ ");
            Console.WriteLine("   ██║   ██╔══██╗ █████╗ "); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██████╔╝██╔══██╗███╗   ██╗");
            Console.WriteLine("   ██║   ██╔══██╗███████║████╗  ██║███████╗");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("████████╗");
            Console.WriteLine("╚══██╔══╝██████╗ "); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ██║   ██╔══██╗ █████╗ ");
            Console.WriteLine("   ██║   ██████╔╝██╔══██╗███╗   ██╗"); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██╔══██╗███████║████╗  ██║███████╗");
            Console.WriteLine("   ╚═╝   ██║  ██║██╔══██║██╔██╗ ██║██╔════╝ ██████╗");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("████████╗██████╗ ");
            Console.WriteLine("╚══██╔══╝██╔══██╗ █████╗ "); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ██║   ██████╔╝██╔══██╗███╗   ██╗");
            Console.WriteLine("   ██║   ██╔══██╗███████║████╗  ██║███████╗"); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██║  ██║██╔══██║██╔██╗ ██║██╔════╝ ██████╗");
            Console.WriteLine("   ╚═╝   ╚═╝  ╚═╝██║  ██║██║╚██╗██║███████╗██╔════╝ ██████╗ ");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("████████╗██████╗  █████╗ ");
            Console.WriteLine("╚══██╔══╝██╔══██╗██╔══██╗███╗   ██╗"); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ██║   ██████╔╝███████║████╗  ██║███████╗");
            Console.WriteLine("   ██║   ██╔══██╗██╔══██║██╔██╗ ██║██╔════╝ ██████╗"); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██║  ██║██║  ██║██║╚██╗██║███████╗██╔════╝ ██████╗ ");
            Console.WriteLine("   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝██║ ╚████║╚════██║██║     ██╔═══██╗███╗   ██╗");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("████████╗██████╗  █████╗ ███╗   ██╗");
            Console.WriteLine("╚══██╔══╝██╔══██╗██╔══██╗████╗  ██║███████╗"); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ██║   ██████╔╝███████║██╔██╗ ██║██╔════╝ ██████╗");
            Console.WriteLine("   ██║   ██╔══██╗██╔══██║██║╚██╗██║███████╗██╔════╝ ██████╗ "); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██║  ██║██║  ██║██║ ╚████║╚════██║██║     ██╔═══██╗███╗   ██╗");
            Console.WriteLine("   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝███████║██║     ██║   ██║████╗  ██║███╗   ██╗");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("████████╗██████╗  █████╗ ███╗   ██╗███████╗");
            Console.WriteLine("╚══██╔══╝██╔══██╗██╔══██╗████╗  ██║██╔════╝ ██████╗"); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ██║   ██████╔╝███████║██╔██╗ ██║███████╗██╔════╝ ██████╗ ");
            Console.WriteLine("   ██║   ██╔══██╗██╔══██║██║╚██╗██║╚════██║██║     ██╔═══██╗███╗   ██╗"); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██║  ██║██║  ██║██║ ╚████║███████║██║     ██║   ██║████╗  ██║███╗   ██╗");
            Console.WriteLine("   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝╚██████╗██║   ██║██╔██╗ ██║████╗  ██║███████╗");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("████████╗██████╗  █████╗ ███╗   ██╗███████╗ ██████╗");
            Console.WriteLine("╚══██╔══╝██╔══██╗██╔══██╗████╗  ██║██╔════╝██╔════╝ ██████╗ "); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ██║   ██████╔╝███████║██╔██╗ ██║███████╗██║     ██╔═══██╗███╗   ██╗");
            Console.WriteLine("   ██║   ██╔══██╗██╔══██║██║╚██╗██║╚════██║██║     ██║   ██║████╗  ██║███╗   ██╗"); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██║  ██║██║  ██║██║ ╚████║███████║╚██████╗██║   ██║██╔██╗ ██║████╗  ██║███████╗");
            Console.WriteLine("   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝ ╚═════╝╚██████╔╝██║╚██╗██║██╔██╗ ██║██╔════╝ ██████╗");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("████████╗██████╗  █████╗ ███╗   ██╗███████╗ ██████╗ ██████╗ ");
            Console.WriteLine("╚══██╔══╝██╔══██╗██╔══██╗████╗  ██║██╔════╝██╔════╝██╔═══██╗███╗   ██╗"); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ██║   ██████╔╝███████║██╔██╗ ██║███████╗██║     ██║   ██║████╗  ██║███╗   ██╗");
            Console.WriteLine("   ██║   ██╔══██╗██╔══██║██║╚██╗██║╚════██║██║     ██║   ██║██╔██╗ ██║████╗  ██║███████╗"); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██║  ██║██║  ██║██║ ╚████║███████║╚██████╗╚██████╔╝██║╚██╗██║██╔██╗ ██║██╔════╝ ██████╗");
            Console.WriteLine("   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚═════╝ ██║ ╚████║██║╚██╗██║█████╗  ██╔════╝████████╗");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("████████╗██████╗  █████╗ ███╗   ██╗███████╗ ██████╗ ██████╗ ███╗   ██╗");
            Console.WriteLine("╚══██╔══╝██╔══██╗██╔══██╗████╗  ██║██╔════╝██╔════╝██╔═══██╗████╗  ██║███╗   ██╗"); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ██║   ██████╔╝███████║██╔██╗ ██║███████╗██║     ██║   ██║██╔██╗ ██║████╗  ██║███████╗");
            Console.WriteLine("   ██║   ██╔══██╗██╔══██║██║╚██╗██║╚════██║██║     ██║   ██║██║╚██╗██║██╔██╗ ██║██╔════╝ ██████╗"); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██║  ██║██║  ██║██║ ╚████║███████║╚██████╗╚██████╔╝██║ ╚████║██║╚██╗██║█████╗  ██╔════╝████████╗");
            Console.WriteLine("   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝██║ ╚████║██╔══╝  ██║     ╚══██╔══╝");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("████████╗██████╗  █████╗ ███╗   ██╗███████╗ ██████╗ ██████╗ ███╗   ██╗███╗   ██╗");
            Console.WriteLine("╚══██╔══╝██╔══██╗██╔══██╗████╗  ██║██╔════╝██╔════╝██╔═══██╗████╗  ██║████╗  ██║███████╗"); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ██║   ██████╔╝███████║██╔██╗ ██║███████╗██║     ██║   ██║██╔██╗ ██║██╔██╗ ██║██╔════╝ ██████╗");
            Console.WriteLine("   ██║   ██╔══██╗██╔══██║██║╚██╗██║╚════██║██║     ██║   ██║██║╚██╗██║██║╚██╗██║█████╗  ██╔════╝████████╗"); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██║  ██║██║  ██║██║ ╚████║███████║╚██████╗╚██████╔╝██║ ╚████║██║ ╚████║██╔══╝  ██║     ╚══██╔══╝");
            Console.WriteLine("   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═══╝███████╗██║        ██║   ");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("████████╗██████╗  █████╗ ███╗   ██╗███████╗ ██████╗ ██████╗ ███╗   ██╗███╗   ██╗███████╗");
            Console.WriteLine("╚══██╔══╝██╔══██╗██╔══██╗████╗  ██║██╔════╝██╔════╝██╔═══██╗████╗  ██║████╗  ██║██╔════╝ ██████╗"); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ██║   ██████╔╝███████║██╔██╗ ██║███████╗██║     ██║   ██║██╔██╗ ██║██╔██╗ ██║█████╗  ██╔════╝████████╗");
            Console.WriteLine("   ██║   ██╔══██╗██╔══██║██║╚██╗██║╚════██║██║     ██║   ██║██║╚██╗██║██║╚██╗██║██╔══╝  ██║     ╚══██╔══╝"); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██║  ██║██║  ██║██║ ╚████║███████║╚██████╗╚██████╔╝██║ ╚████║██║ ╚████║███████╗██║        ██║   ");
            Console.WriteLine("   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═══╝╚══════╝╚██████╗   ██║   ");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("████████╗██████╗  █████╗ ███╗   ██╗███████╗ ██████╗ ██████╗ ███╗   ██╗███╗   ██╗███████╗ ██████╗");
            Console.WriteLine("╚══██╔══╝██╔══██╗██╔══██╗████╗  ██║██╔════╝██╔════╝██╔═══██╗████╗  ██║████╗  ██║██╔════╝██╔════╝████████╗"); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ██║   ██████╔╝███████║██╔██╗ ██║███████╗██║     ██║   ██║██╔██╗ ██║██╔██╗ ██║█████╗  ██║     ╚══██╔══╝");
            Console.WriteLine("   ██║   ██╔══██╗██╔══██║██║╚██╗██║╚════██║██║     ██║   ██║██║╚██╗██║██║╚██╗██║██╔══╝  ██║        ██║   "); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██║  ██║██║  ██║██║ ╚████║███████║╚██████╗╚██████╔╝██║ ╚████║██║ ╚████║███████╗╚██████╗   ██║   ");
            Console.WriteLine("   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═══╝╚══════╝ ╚═════╝   ██║   ");

            Thread.Sleep(100);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("████████╗██████╗  █████╗ ███╗   ██╗███████╗ ██████╗ ██████╗ ███╗   ██╗███╗   ██╗███████╗ ██████╗████████╗");
            Console.WriteLine("╚══██╔══╝██╔══██╗██╔══██╗████╗  ██║██╔════╝██╔════╝██╔═══██╗████╗  ██║████╗  ██║██╔════╝██╔════╝╚══██╔══╝"); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ██║   ██████╔╝███████║██╔██╗ ██║███████╗██║     ██║   ██║██╔██╗ ██║██╔██╗ ██║█████╗  ██║        ██║   ");
            Console.WriteLine("   ██║   ██╔══██╗██╔══██║██║╚██╗██║╚════██║██║     ██║   ██║██║╚██╗██║██║╚██╗██║██╔══╝  ██║        ██║   "); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██║  ██║██║  ██║██║ ╚████║███████║╚██████╗╚██████╔╝██║ ╚████║██║ ╚████║███████╗╚██████╗   ██║   ");
            Console.WriteLine("   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═══╝╚══════╝ ╚═════╝   ╚═╝   ");

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static public void Titre2()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("████████╗██████╗  █████╗ ███╗   ██╗███████╗ ██████╗ ██████╗ ███╗   ██╗███╗   ██╗███████╗ ██████╗████████╗");
            Console.WriteLine("╚══██╔══╝██╔══██╗██╔══██╗████╗  ██║██╔════╝██╔════╝██╔═══██╗████╗  ██║████╗  ██║██╔════╝██╔════╝╚══██╔══╝"); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ██║   ██████╔╝███████║██╔██╗ ██║███████╗██║     ██║   ██║██╔██╗ ██║██╔██╗ ██║█████╗  ██║        ██║   ");
            Console.WriteLine("   ██║   ██╔══██╗██╔══██║██║╚██╗██║╚════██║██║     ██║   ██║██║╚██╗██║██║╚██╗██║██╔══╝  ██║        ██║   "); Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ██║   ██║  ██║██║  ██║██║ ╚████║███████║╚██████╗╚██████╔╝██║ ╚████║██║ ╚████║███████╗╚██████╗   ██║   ");
            Console.WriteLine("   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═══╝╚══════╝ ╚═════╝   ╚═╝   ");
        }

        static public void Titre_Camion()
        {
            string a = "                                                                                                           __________________________________________________";
            string b = "                                                                                                   /|     |                                                 |";
            string c = "                                                                                                   ||     |     ___             __                          |";
            string d = "                                                                                              .----|-----,|      |  __ _ __  _ /   _ __ __  _  _ _|_        |";
            string e = "                                                                                              ||  ||   ==||      |  | (_|| |_> \\__(_)| || |(/_(_  |_       |";
            string f = "                                                                                         .-----'--'|   ==||                                                 |";
            string g = "                                                                                         |)-      ~|     ||_________________________________________________|";
            string h = "                                                                                         | ___     |     |____...==..._  >\\______________________________|";
            string i = "                                                                                       [_/.-.\\\"--\"-------- //.-.  .-.\\/   |/            \\   .-.  .-. //";
            string j = "                                                                                           ( o )`=========\"\"\"`( o )( o )     o              `( o )( o )`";
            string k = "                                                                                            '-'                '-'  '-'                       '-'  '-'";
             
            for(int p = 1; p < 80; p++)
            {
                Thread.Sleep(30);
                Console.Clear();
                a = a.Substring(1, a.Length - 1);
                b = b.Substring(1, b.Length - 1);
                c = c.Substring(1, c.Length - 1);
                d = d.Substring(1, d.Length - 1);
                e = e.Substring(1, e.Length - 1);
                f = f.Substring(1, f.Length - 1);
                g = g.Substring(1, g.Length - 1);
                h = h.Substring(1, h.Length - 1);
                i = i.Substring(1, i.Length - 1);
                j = j.Substring(1, j.Length - 1);
                k = k.Substring(1, k.Length - 1);
                Titre2();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(a + "\n" + b + "\n" + c + "\n" + d + "\n" + e + "\n" + f + "\n" + g + "\n" + h + "\n" + i + "\n" + j + "\n" + k);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}