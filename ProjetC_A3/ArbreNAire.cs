using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetC_A3
{
    public
        .0class ArbreNAire
    {
        private NoeudN root;
        public NoeudN Root { get=> root; set { this.root = value; } }

        public ArbreNAire(Salarié value)
        {
            this.root = new NoeudN(value);
        }
        public ArbreNAire()
        {
            this.root = null;
        }

        public void AddChild(Salarié parentValue, Salarié childValue)
        {
            NoeudN parentNode = FindNode(root, parentValue);
            if (parentNode != null)
            {
                NoeudN childNode = new NoeudN(childValue);
                parentNode.AddChild(childNode);
            }
        }

        public NoeudN FindNode(NoeudN node, Salarié value)
        {
            if (node == null) return null; // Nouvelle ligne ajoutée

            if (node.Value.Equals(value))
            {
                return node;
            }
            else
            {
                foreach (NoeudN child in node.Children)
                {
                    NoeudN result = FindNode(child, value);
                    if (result != null)
                    {
                        return result;
                    }
                }
                return null;
            }
        }
        public NoeudN FindNodeByNss(int nss)
        {
            if (root != null && root.Value.Nss == nss)
            {
                return root;
            }

            return FindNodeByNssRecursive(root, nss);
        }

        private NoeudN FindNodeByNssRecursive(NoeudN node, int nss)
        {
            if (node == null)
            {
                return null;
            }

            foreach (NoeudN child in node.Children)
            {
                if (child.Value.Nss == nss)
                {
                    return child;
                }

                NoeudN foundNode = FindNodeByNssRecursive(child, nss);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }

            return null;
        }
        public void ConstruireArbreDepuisCSV(string chemin)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", chemin);
            string[] lignes = File.ReadAllLines(path);
            Dictionary<int, Salarié> salaries = new Dictionary<int, Salarié>(); //Salarié avec son nss pour clé
            Dictionary<int, int?> relations = new Dictionary<int, int?>(); //Clé nss, valeur nss du parent

            foreach (string ligne in lignes)
            {
                string[] champs = ligne.Split(',');
                int nss = Convert.ToInt32(champs[1]);
                int? nssParent = string.IsNullOrEmpty(champs[0]) ? (int?)nss : Convert.ToInt32(champs[0]); 
                Salarié salarie = new Salarié(
                    nss,
                    champs[2],
                    champs[3],
                    DateTime.Parse(champs[4]),
                    champs[5],
                    champs[6],
                    champs[7],
                    DateTime.Parse(champs[8]),
                    champs[9],
                    Convert.ToInt32(champs[10])
                );

                salaries[nss] = salarie;
                relations[nss] = nssParent;
            }

            // Identifier la racine (le salarié sans parent)
            foreach (var relation in relations)
            {
                if (relation.Value == relation.Key)
                {
                    root = new NoeudN(salaries[relation.Key]);
                    break;
                }
            }

            // Construire l'arbre
            foreach (var relation in relations)
            {
                if (relation.Value.HasValue && relation.Value != relation.Key)
                {
                    Salarié parent = salaries[relation.Value.Value];
                    Salarié child = salaries[relation.Key];
                    AddChild(parent, child);
                }

            }
        }
    }
}