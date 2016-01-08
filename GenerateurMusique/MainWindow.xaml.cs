using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GenerateurMusique
{
    public partial class MainWindow : Window
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
        public ObservableCollection<string> Songs = new ObservableCollection<string>();
        private Population[] populations = new Population[Population.MAXINDIVIDUS];

        public MainWindow()
        {
            InitializeComponent();

            SongList.ItemsSource = Songs;

            // On s'abonne à la fermeture du programme pour pouvoir nettoyer le répertoire et les fichiers midi
            Closed += MainWindow_Closed;
        }
        
        // On efface les fichiers .mid que l'on avait créé à la fin du programme
        void MainWindow_Closed(object sender, EventArgs e)
        {
            _composer.Close();
        }

        // Clic sur le bouton : on lance la création d'un fichier et on le joue
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _composer.CreateAndPlayMusic();
        }

        private void SongClick(object sender, SelectionChangedEventArgs e)
        {
            string item = e.AddedItems[0] as string;

            if (item == null)
                return;

            _composer.PlayMIDI(item);
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            Population pop = new Population(1);
            populations[0] = pop;

            Individu[] individus = pop.GetIndividus();

            foreach (Individu individu in individus)
                Songs.Add(individu.MidiFileName);
        }

        private void NextGenClick(object sender, RoutedEventArgs e)
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
                Songs.Add(newInd.MidiFileName);
                newPop[i] = newInd;
            }

            populations[nbPopulation] = new Population(nbPopulation + 1, newPop);
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

            Population p = populations[nbPopulation - 1];
            Individu i1 = p.GetIndividuAt(rnd1);
            Individu i2 = p.GetIndividuAt(rnd2);

            if (i1.Fitness > i2.Fitness)
                return i1;

            if (i1.Fitness < i2.Fitness)
                return i2;

            return rnd3 == 0 ? i1 : i2;
        }
    }
}
