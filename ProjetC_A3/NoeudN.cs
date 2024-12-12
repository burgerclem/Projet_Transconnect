using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetC_A3
{
    public class NoeudN
    {
        private Salarié value;
        private List<NoeudN> children;
        public Salarié Value { get => value; set { this.value = value; } }
        public List<NoeudN> Children { get => children; set { this.children = value; } }

        public NoeudN(Salarié value)
        {
            this.value = value;
            this.children = new List<NoeudN>();
        }

        public void AddChild(NoeudN child)
        {
            children.Add(child);
        }
    }
}
