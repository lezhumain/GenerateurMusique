using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace GenerateurMusique.Model
{
    [Serializable]
    public class Generation
    {
        public ObservableCollection<Individu> Individus { get; set; }

        private static int generationCpt = 0;
        public static int GenerationCpt
        {
            get { return generationCpt; }
            set { generationCpt = value; }
        }

        private int _numGeneration;
        [XmlAttribute("NumGeneration")]
        public  int NumGeneration
        {
            get { return _numGeneration;}
            set { _numGeneration = value; }
        }

        public string Name => "Génération" + _numGeneration;


        public Generation(IEnumerable<Individu> indivs)
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

        public static void SetCptGen(int i)
        {
            generationCpt = i;
        }
    }
}
