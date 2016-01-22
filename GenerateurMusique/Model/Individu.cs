using System.IO;
using GenerateurMusique.MidiHelper;
using System;
using System.Xml.Serialization;

namespace GenerateurMusique.Model
{
    [Serializable]
    public class Individu
    {

        private static int nbIndividus;
        private static short NBNOTES = 16;
        private int[] _notes = new int[NBNOTES];
        [XmlAttribute("Notes")]
        public int[] Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }
        [XmlAttribute("Fitness")]
        public short Fitness;
        public string FitnessStr
        {
            get { return Fitness.ToString(); }
            set
            {
                short val;
                if (short.TryParse(value, out val))
                    Fitness = val;
                else
                    Fitness = 0;
            }
        }

        //private int instrument;

        private string _midiFileName;
        [XmlAttribute("MidiFileName")]
        public string MidiFileName
        {
            get { return _midiFileName; }
            set { _midiFileName = value; }
        }
        public Individu(Individu papa, Individu maman)
        {
            int rnd = MidiComposer.GetRandom(0, NBNOTES);

            for (int i = 0; i < NBNOTES; i++)
            {
                if (i < rnd)
                    _notes[i] = papa._notes[i];
                else
                    _notes[i] = maman._notes[i];
            }

            init();
        }

        public Individu(Individu parent)
        {
            //TODO
            for (int i = 0; i < NBNOTES; i++)
                _notes[i] = parent._notes[i];

            init();
        }

        public Individu()
        {
            // c. Ajouter des notes
            // Chaque note est comprise entre 0 et 127 (12 correspond au type de note, fixe ici à des 1/4)
            // L'équivalence avec les notes / octaves est disponible ici : https://andymurkin.files.wordpress.com/2012/01/midi-int-midi-note-no-chart.jpg
            // Ici 16 notes aléatoire entre 16 et 96 (pour éviter certaines notes trop aigues ou trop graves)
            for (int i = 0; i < 16; i++)
                _notes[i] = MidiComposer.GetRandom(24, 96);

            init();
        }

        private void init()
        {
            nbIndividus++;
            _midiFileName = "Fichier" + nbIndividus + ".mid";
            Fitness = 0;
        }

        public void Mutate()
        {
            for (int i = 0; i < NBNOTES; i++)
            {
                int rnd = MidiComposer.GetRandom(0, 101);

                if (rnd < Population.MUTARATE)
                    _notes[i] = MidiComposer.GetRandom(24, 96);
            }
        }

        public void Play()
        {
            MidiComposer mc = new MidiComposer();
            if (!File.Exists(_midiFileName))
                mc.CreateAndPlayMusic(_notes, _midiFileName, true);
            else
                mc.PlayMIDI(_midiFileName);
        }
    }
}
