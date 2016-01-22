using System.Collections.ObjectModel;

namespace GenerateurMusique.Model
{
    public class Generation
    {
        public ObservableCollection<Individu> Individus { get; set; }

        private static int generationCpt = 0;
        private int _numGeneration;

        public string Name => "Génération" + _numGeneration;


        public Generation(Individu[] indivs)
        {
            _numGeneration = generationCpt;

            Individus = new ObservableCollection<Individu>();
            foreach (Individu individu in indivs)
                Individus.Add(individu);

            generationCpt++;
        }
        public Generation()
        {
            _numGeneration = generationCpt;
            Individus = new ObservableCollection<Individu>();
            generationCpt++;
        }
    }
}
