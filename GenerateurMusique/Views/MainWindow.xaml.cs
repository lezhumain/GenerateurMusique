using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using GenerateurMusique.MidiHelper;
using GenerateurMusique.Model;
using GenerateurMusique.ViewModels;
using Button = System.Windows.Controls.Button;

namespace GenerateurMusique.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowVM _vm = new MainWindowVM();

        public MainWindow()
        {
            InitializeComponent();

            //GenList.ItemsSource = Gens;
            DataContext = _vm;


            // On s'abonne à la fermeture du programme pour pouvoir nettoyer le répertoire et les fichiers midi
            Closed += MainWindow_Closed;
        }
        
        // On efface les fichiers .mid que l'on avait créé à la fin du programme
        void MainWindow_Closed(object sender, EventArgs e)
        {
            _vm.MainWindow_Closed(sender, e);
        }

        // Clic sur le bouton : on lance la création d'un fichier et on le joue
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    _vm.PlayClick(sender, e);
        //}
        private void SavePopulation_Click(object sender, RoutedEventArgs e)
        {
            SavePopulation.IsEnabled = false;
            LoadPopulation.IsEnabled = false;

            _vm.SavePopulation(MainWindowVM.XmlFile);

            SavePopulation.IsEnabled = true;
            LoadPopulation.IsEnabled = true;
        }

        private void SongPlayClick(object sender, RoutedEventArgs routedEventArgs)
        {
            ((Button) sender).IsEnabled = false;

            Individu ind = ((Button) sender).DataContext as Individu;

            ind?.Play();

            ((Button) sender).IsEnabled = true;
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            Create.IsEnabled = false;

            _vm.CreateClick(sender, e);

            NextGen.IsEnabled = true;
        }

        private void NextGenClick(object sender, RoutedEventArgs e)
        {
            NextGen.IsEnabled = false;

            _vm.NextGenClick(sender, e);

            NextGen.IsEnabled = true;
        }



        private static void Save(Individu selected)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            string path = dialog.SelectedPath;
            string filename = selected.MidiFileName;

            try
            {
                MidiComposer mc = new MidiComposer();
                if (!File.Exists(filename))
                    mc.CreateAndPlayMusic(selected.Notes, selected.MidiFileName, false);

                File.Copy(filename, path + "\\" + filename);
                //SaveButton.IsEnabled = true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Erreur: " + exception.Message);
            }
        }

        private void LoadPopulation_Click(object sender, RoutedEventArgs e)
        {
            SavePopulation.IsEnabled = false;
            LoadPopulation.IsEnabled = false;

            _vm.LoadPopulation(MainWindowVM.XmlFile);

            SavePopulation.IsEnabled = true;
            LoadPopulation.IsEnabled = true;
        }

        private void SaveDisSong(object sender, RoutedEventArgs e)
        {
            Button bSender = sender as Button;

            Individu cont = bSender?.DataContext as Individu;

            if (cont == null)
                return;

            Save(cont);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            // TODO Henri la famille: afficher une petite fenêtre qui explique comment on se sert de l'app
        }
    }
}
