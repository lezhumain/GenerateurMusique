using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using GenerateurMusique.Annotations;
using GenerateurMusique.Model;

namespace GenerateurMusique
{
    public class Population : INotifyPropertyChanged
    {
        public static short MAXINDIVIDUS = 10;
        public static short MUTARATE = 10;
        public static short CROSSOVER = 30;
        public static short MAXNOTES = 16;

        public short MaxIndividus
        {
            get { return MAXINDIVIDUS; }
            set { MAXINDIVIDUS = value; OnPropertyChanged(nameof(MaxIndividus));  }
        }
        public short Mutarate
        {
            get { return MUTARATE; }
            set
            {
                MUTARATE = (short) (value > 100 ? 100 : value);

                OnPropertyChanged(nameof(Mutarate));
            }
        }
        public short Crossover
        {
            get { return CROSSOVER; }
            set
            {
                CROSSOVER = (short)(value > 100 ? 100 : value);
                OnPropertyChanged(nameof(Crossover));
            }
        }
        public short MaxNotes
        {
            get { return MAXNOTES; }
            set{ MAXNOTES = value; OnPropertyChanged(nameof(MaxNotes)); }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Population Clone()
        {
            return new Population
            {
                Crossover = this.Crossover,
                MaxIndividus = this.MaxIndividus,
                MaxNotes = this.MaxNotes,
                Mutarate = this.Mutarate
            };
        }
        // TODO: confirm + reset pop
    }
}
