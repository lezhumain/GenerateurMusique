﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using GenerateurMusique.MidiHelper;
using GenerateurMusique.Model;
using GenerateurMusique.ViewModels;
using Application = System.Windows.Forms.Application;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.MessageBox;

namespace GenerateurMusique.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowVM _vm = new MainWindowVM();
        public Population _params = new Population();
        private Parametres fenetre;

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

            System.Windows.MessageBox.Show("Vous avez bien sauvegardé.");
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
            //Create.IsEnabled = false;

            _vm.CreateClick(sender, e);

            //NextGen.IsEnabled = true;
        }

        private void NextGenClick(object sender, RoutedEventArgs e)
        {
            //NextGen.IsEnabled = false;

            _vm.NextGenClick(sender, e);

            //NextGen.IsEnabled = true;
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


            System.Windows.MessageBox.Show("Bien chargé la famille ;)");
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
            Help fenetre = new Help();
            fenetre.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            fenetre = new Parametres(_params) {DataContext = _params};
            fenetre.ValiderButton.Click += ParamValiderClick;
            fenetre.AnnulerButton.Click += ParamAnnulerClick;


            fenetre.Show();
        }

        private void ParamValiderClick(object sender, RoutedEventArgs e)
        {
            ShowConfirm();
        }

        private void ShowConfirm()
        {
            string sMessageBoxText = "Si vous avez changé le nombre de notes ou le nombre d'individus, la population sera remise à 0.\n Voulez-vous continuer?";
            string sCaption = "Confirmation";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    if (_params.MaxNotes != fenetre.MaxNotes ||
                        _params.MaxIndividus != fenetre.MaxIndividus)
                    {
                        _vm.Gens.Clear();
                    }
                    fenetre.Close();
                    break;
            }
        }

        private void ParamAnnulerClick(object sender, RoutedEventArgs e)
        {
            _params = fenetre.OldParam;
            fenetre.Close();
        }
    }
}
