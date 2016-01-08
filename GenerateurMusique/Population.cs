using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateurMusique
{
    public class Population
    {
        public static readonly short MAXINDIVIDUS = 10;
        public static readonly short MUTARATE = 10;
        public static readonly short CROSSOVER = 5;

        private Individu[] _individus = new Individu[MAXINDIVIDUS];

        public int NbGenerations => 0; // TODO

        public Population()
        {
            //TODO
            for (int i = 0; i < MAXINDIVIDUS; i++)
            {
                //_individus[i] = new Individu();
            }
        }

        public void SelectParents()
        {
            //TODO
            throw new NotImplementedException();
        }

        public void Survival()
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
