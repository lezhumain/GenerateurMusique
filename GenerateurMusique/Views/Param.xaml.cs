using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GenerateurMusique.Views
{
    /// <summary>
    /// Logique d'interaction pour Help.xaml
    /// </summary>
    public partial class Parametres : Window
    {
        public readonly Population OldParam;
        public short MaxIndividus { get; set; }
        public short Mutarate { get; set; }
        public short Crossover { get; set; }
        public short MaxNotes { get; set; }


        public Parametres(Population oldParam)
        {
            InitializeComponent();

            MaxNotes = oldParam.MaxNotes;
            MaxIndividus = oldParam.MaxIndividus;
            Crossover = oldParam.Crossover;
            Mutarate = oldParam.Mutarate;
        }

        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

    }
}
