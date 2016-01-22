using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GenerateurMusique.Annotations;
using GenerateurMusique.Model;

namespace GenerateurMusique.Controls
{
    /// <summary>
    /// Logique d'interaction pour GenerationControl.xaml
    /// </summary>
    public partial class GenerationControl : INotifyPropertyChanged
    {
        private Generation _gen;

        public Generation Gen
        {
            get { return _gen; }
            set { _gen = value;
                Panel.DataContext = _gen;
                Liste.ItemsSource = _gen.Individus;
            }
        }

        public GenerationControl(ObservableCollection<Generation> geners)
        {
            InitializeComponent();
            DataContext = this;

            //foreach (Generation gener in geners)
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
