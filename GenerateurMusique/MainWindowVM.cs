using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using GenerateurMusique.Annotations;

namespace GenerateurMusique
{
    class MainWindowVM : INotifyPropertyChanged
    {
        private int nbPopulation
        {
            get
            {
                var max = populations.Count();
                for (int i = 0; i < max; i++)
                {
                    if (populations[i] == null)
                        return i;
                }
                return max;
            }
        }

        MidiComposer _composer = new MidiComposer();


        public ObservableCollection<Generation> Gens { get; set; }

        private Population[] populations = new Population[Population.MAXINDIVIDUS];


        public MainWindowVM()
        {
            Gens = new ObservableCollection<Generation>();
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
            Population pop = new Population(1);
            populations[0] = pop;

            Individu[] individus = pop.GetIndividus();

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
                    var parent1 = SelectParent();
                    var parent2 = SelectParent();

                    newInd = new Individu(parent1, parent2);
                }
                else
                {
                    var parent = SelectParent();

                    newInd = new Individu(parent);
                }

                newInd.Mutate();
                //Gens.Add(newInd);
                newPop[i] = newInd;

            }

            Generation mg = new Generation(newPop);
            Gens.Add(mg);
            //populations[nbPopulation] = new Population(nbPopulation + 1, newPop);
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


    }
}
