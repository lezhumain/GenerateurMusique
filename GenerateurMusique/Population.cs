using System.Collections.Generic;

namespace GenerateurMusique
{
    public class Population
    {
        public static readonly short MAXINDIVIDUS = 10;
        public static readonly short MUTARATE = 10;
        public static readonly short CROSSOVER = 30;

        private Individu[] _individus = new Individu[MAXINDIVIDUS];

        //public int NbGenerations => 0; // TODO

        public Population(int nbGeneration, Individu[] individus = null)
        {
            
            for (int i = 0; i < MAXINDIVIDUS; i++)
                _individus[i] = individus == null ? new Individu() : individus[i];
        }

        public Population(IReadOnlyList<Individu> individus)
        {
            for (int i = 0; i < MAXINDIVIDUS; i++)
            {
                _individus[i] = individus[i];
            }
        }

        public Individu GetIndividuAt(int index)
        {
            return _individus[index];
        }

        public Individu[] GetIndividus()
        {
            return _individus;
        }

        //public void SelectParents()
        //{
        //    //TODO
        //    throw new NotImplementedException();
        //}

        //public void Survival()
        //{
        //    //TODO
        //    throw new NotImplementedException();
        //}

        public void NextGen()
        {
            
        }
    }
}
