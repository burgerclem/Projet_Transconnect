using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjetC_A3
{
    public class Commande : IMoyenne
    {
        int id;
        Client client;
        string ville_depart;
        string ville_arrivee;
        int prix;
        Chauffeur chauffeur;
        DateTime date;
        Véhicule véhicule;

        public Commande(Client client, DateTime date, string ville_depart, string ville_arrivee)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", "Commande.csv");
            string[] file = File.ReadAllLines(path).Last().Split(',');
            int int_temp = Convert.ToInt32(file[0]);
            this.id = int_temp + 1;
            this.client = client;
            this.date = date;
            this.ville_depart = ville_depart;
            this.ville_arrivee = ville_arrivee;
            string fileName = "Distances.csv";
            string path2 = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            (this.prix, var timeInMinutes, var pathList) = Kilometrage(path2);
            Console.WriteLine($"Distance: {this.prix} km, Time: {timeInMinutes} minutes, Path: {string.Join(" -> ", pathList)}");
        }

        public Commande()
        {
            this.id = -1;
            this.client = null;
            this.date = DateTime.Parse("1900-01-01");
            this.ville_depart = "";
            this.ville_arrivee = "";
            this.prix = -1;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Client Client
        {
            get { return client; }
            set { client = value; }
        }

        public string Ville_depart
        {
            get { return ville_depart; }
            set { ville_depart = value; }
        }

        public string Ville_arrivee
        {
            get { return ville_arrivee; }
            set { ville_arrivee = value; }
        }

        public int Prix
        {
            get { return prix; }
            set { prix = value; }
        }

        public Chauffeur Chauffeur
        {
            get { return chauffeur; }
            set { chauffeur = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public Véhicule Véhicule
        {
            get { return véhicule; }
            set { véhicule = value; }
        }

        public override string ToString()
        {
            return client.ToString() + "\n" + ville_depart + " " + ville_arrivee + " " + prix * 2 + " " + date;
        }

        public (int, int, List<string>) Kilometrage(string fileName)
        {
            var distances = new Dictionary<string, Dictionary<string, (int distance, int time)>>();
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                string depart = parts[0];
                string arrivee = parts[1];
                int distance = Convert.ToInt32(parts[2]);
                int time = ConvertTimeToMinutes(parts[3]);

                if (!distances.ContainsKey(depart))
                {
                    distances[depart] = new Dictionary<string, (int, int)>();
                }
                if (!distances.ContainsKey(arrivee))
                {
                    distances[arrivee] = new Dictionary<string, (int, int)>();
                }

                distances[depart][arrivee] = (distance, time);
                distances[arrivee][depart] = (distance, time); // Si le graphe est non-directionnel
            }

            var distancesCourtes = new Dictionary<string, int>();
            var tempsCourts = new Dictionary<string, int>();
            var chemin = new Dictionary<string, string>();
            var noeudsNonVisites = new List<string>();

            foreach (var noeud in distances)
            {
                distancesCourtes[noeud.Key] = int.MaxValue;
                tempsCourts[noeud.Key] = int.MaxValue;
                noeudsNonVisites.Add(noeud.Key);
            }

            distancesCourtes[ville_depart] = 0;
            tempsCourts[ville_depart] = 0;

            while (noeudsNonVisites.Any())
            {
                noeudsNonVisites.Sort((x, y) => distancesCourtes[x] - distancesCourtes[y]);
                var noeudActuel = noeudsNonVisites.First();
                noeudsNonVisites.Remove(noeudActuel);

                if (noeudActuel == ville_arrivee)
                {
                    break;
                }

                foreach (var voisin in distances[noeudActuel])
                {
                    var altDistance = distancesCourtes[noeudActuel] + voisin.Value.distance;
                    var altTime = tempsCourts[noeudActuel] + voisin.Value.time;
                    if (altDistance < distancesCourtes[voisin.Key])
                    {
                        distancesCourtes[voisin.Key] = altDistance;
                        tempsCourts[voisin.Key] = altTime;
                        chemin[voisin.Key] = noeudActuel;
                    }
                }
            }

            var pathList = new List<string>();
            var currentNode = ville_arrivee;
            while (currentNode != null)
            {
                pathList.Insert(0, currentNode);
                chemin.TryGetValue(currentNode, out currentNode);
            }

            return (distancesCourtes[ville_arrivee], tempsCourts[ville_arrivee], pathList);
        }

        public int ConvertTimeToMinutes(string time)
        {
            if (time.Contains('h'))
            {
                var parts = time.Split('h');
                int hours = int.Parse(parts[0]);
                int minutes = int.Parse(parts[1]);
                return hours * 60 + minutes;
            }
            else
            {
                return int.Parse(time.Replace("mn", ""));
            }
        }

        public int Moyenne()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", "Commande.csv");
            string[] file = File.ReadAllLines(path);
            int total = 0;
            foreach (var line in file)
            {
                string[] temp = line.Split(',');
                total += Convert.ToInt32(temp[4]);
            }
            total /= file.Length;
            return total;
        }
    }
}
