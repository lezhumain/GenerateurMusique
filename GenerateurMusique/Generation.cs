using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateurMusique
{
    public class Generation
    {
        public ObservableCollection<Individu> Individus = new ObservableCollection<Individu>();

        private static int generationCpt = 0;
        private int _numGeneration;

        public string Name => "Génération" + _numGeneration;


        public Generation(Individu[] indivs)
        {
            _numGeneration = generationCpt;

            foreach (Individu individu in indivs)
                Individus.Add(individu);

            generationCpt++;
        }
    }
}
