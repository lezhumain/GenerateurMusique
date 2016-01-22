using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using GenerateurMusique.Annotations;
using GenerateurMusique.MidiHelper;
using GenerateurMusique.Model;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace GenerateurMusique.ViewModels
{
    class MainWindowVM : INotifyPropertyChanged
    {
        MidiComposer _composer = new MidiComposer();


        public ObservableCollection<Generation> Gens { get; set; }

        //private Population[] populations = new Population[Population.MAXINDIVIDUS];
        private bool _saveFileExists;
        public bool SaveFileExists { get { return _saveFileExists; } set { _saveFileExists = value; OnPropertyChanged(nameof(SaveFileExists)); } }

        public MainWindowVM()
        {
            Gens = new ObservableCollection<Generation>();
            SaveFileExists = false;
        }

        public void MainWindow_Closed(object sender, EventArgs e)
        {
            _composer.Close();
        }


        public void PlayClick(object sender, RoutedEventArgs routedEventArgs)
        {
            _composer.CreateAndPlayMusic();
        }





        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CreateClick(object sender, RoutedEventArgs routedEventArgs)
        {
            Individu[] individus = new Individu[Population.MAXINDIVIDUS];

            for (int i = 0; i < Population.MAXINDIVIDUS; i++)
                individus[i] = new Individu();


            Generation gen = new Generation(individus);
            Gens.Add(gen);
        }

        public void NextGenClick(object sender, RoutedEventArgs routedEventArgs)
        {
            Individu[] newPop = new Individu[Population.MAXINDIVIDUS];

            for (int i = 0; i < Population.MAXINDIVIDUS; i++)
            {
                int rnd = MidiComposer.GetRandom(0, 100);
                Individu newInd;

                if (rnd < Population.CROSSOVER)
                {
                    Individu parent1 = SelectParent();
                    Individu parent2 = SelectParent();

                    newInd = new Individu(parent1, parent2);
                }
                else
                {
                    Individu parent = SelectParent();

                    newInd = new Individu(parent);
                }

                newInd.Mutate();
                newPop[i] = newInd;

            }

            Generation mg = new Generation(newPop);

            Survival();
            Gens.Add(mg);
            //populations[nbPopulation] = new Population(nbPopulation + 1, newPop);
        }

        private void Survival()
        {
            Gens.RemoveAt(0);
        }

        /// <summary>
        /// Compare le fitness de 2 individus aleatoires.
        /// </summary>
        /// <returns>L'individu ayant le fitness le plus élevé</returns>
        private Individu SelectParent()
        {
            int rnd1 = MidiComposer.GetRandom(0, Population.MAXINDIVIDUS);
            int rnd2 = MidiComposer.GetRandom(0, Population.MAXINDIVIDUS);
            int rnd3 = MidiComposer.GetRandom(0, 2);

            Generation g = Gens.Last();
            Individu i1 = g.Individus[rnd1];
            Individu i2 = g.Individus[rnd2];

            if (i1.Fitness > i2.Fitness)
                return i1;

            if (i1.Fitness < i2.Fitness)
                return i2;

            return rnd3 == 0 ? i1 : i2;
        }

        

        public void SavePopulation(string fileName)
        {
            Generation[] lol = Gens.ToArray();
            XmlSerializer serializer = new XmlSerializer(lol.GetType());

            File.Delete(fileName);

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, lol);
            }
            SaveFileExists = true;
        }
        public void LoadPopulation(string filename)
        {
            XmlTextReader xr = new XmlTextReader(filename);
            XmlSerializer xs = new XmlSerializer(typeof(Generation[]));


            Generation[] lol = ((Generation[])xs.Deserialize(xr));


            Gens.Clear();
            foreach (Generation gen in lol)
                Gens.Add(gen);
        }
    }
}
