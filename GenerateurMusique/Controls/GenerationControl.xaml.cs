using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using GenerateurMusique.Annotations;

namespace GenerateurMusique
{
    /// <summary>
    /// Logique d'interaction pour GenerationControl.xaml
    /// </summary>
    public partial class GenerationControl : INotifyPropertyChanged
    {
        private GenerationTest _gen;

        public GenerationTest Gen
        {
            get { return _gen; }
            set { _gen = value;
                Panel.DataContext = _gen;
                Liste.ItemsSource = _gen.Indivs;
            }
        }

        public GenerationControl(ObservableCollection<GenerationTest> geners)
        {
            InitializeComponent();
            DataContext = this;

            //foreach (GenerationTest gener in geners)
            //    Geners.Add(gener);
        }

        public GenerationControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
