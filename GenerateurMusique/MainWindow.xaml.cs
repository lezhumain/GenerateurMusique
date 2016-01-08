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
        MidiComposer _composer = new MidiComposer();        
        public ObservableCollection<string> Songs = new ObservableCollection<string>(); 

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
    }
}
