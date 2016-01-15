using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using GenerateurMusique.Model;
using GenerateurMusique.ViewModels;

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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.PlayClick(sender, e);
        }

        private void SongClick(object sender, SelectionChangedEventArgs e)
        {
            //bool check = SongList.SelectedItems.Count == 1;

            //if (SongList.SelectedItems.Count != 1)
            //    return;

            //Individu ind = SongList.SelectedItem as Individu;
            Individu ind = e.AddedItems[0] as Individu;

            ind?.Play();

            SaveButton.IsEnabled = true;
            //Debug.WriteLine("ok");
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



        private void Save(object sender, RoutedEventArgs e)
        {
            //SaveButton.IsEnabled = false;


            //if (SongList.SelectedItems.Count != 1)
            //    return;


            //Individu selected = SongList.SelectedItem as Individu;
            //if (selected == null)
            //    return;

            //FolderBrowserDialog dialog = new FolderBrowserDialog();
            //DialogResult result = dialog.ShowDialog();
            //var path = dialog.SelectedPath;
            //var filename = selected.MidiFileName;

            //try
            //{
            //    MidiComposer mc = new MidiComposer();
            //    if (!File.Exists(filename))
            //        mc.CreateAndPlayMusic(selected.Notes, selected.MidiFileName, false);

            //    File.Copy(filename, path + "\\" + filename);
            //    SaveButton.IsEnabled = true;
            //}
            //catch (Exception exception)
            //{
            //    Debug.WriteLine("Erreur: " + exception.Message);
            //}
        }
    }
}
