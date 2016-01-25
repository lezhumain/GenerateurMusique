using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using GenerateurMusique.Model;

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

        public void SavePopulation(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(this._individus.GetType());
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, this._individus);
            }

        }
        public void LoadPopulation(string filename)
        {
            XmlTextReader xr = new XmlTextReader(filename);
            XmlSerializer xs = new XmlSerializer(typeof(Individu[]));
            this._individus = (Individu[])xs.Deserialize(xr);
        }
    }
}
