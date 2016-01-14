using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;

namespace GenerateurMusique
{
    public class MidiComposer
    {
        static Random _rand = new Random();
        MediaPlayer _mplayer;
        bool _isPlaying;
        string _strFileName;
        public static int NbFile;
        private static readonly int MAXFILES = 2;

        //List<OldIndividu> _songs = new List<OldIndividu>();

        public MidiComposer()
        {
            // Initialisation du lecteur
            _mplayer = new MediaPlayer();
            _mplayer.MediaEnded += mplayer_MediaEnded;
            _isPlaying = false;
        }

        void StopPlayer()
        {
            _mplayer.Stop();
            _mplayer.Close();
            _isPlaying = false;
        }

        // Lancé lorsque le fichier a fini sa lecture, pour le fermer proprement
        void mplayer_MediaEnded(object sender, EventArgs e)
        {
            StopPlayer();
            _isPlaying = false;
        }

        public void PlayMIDI(string strFileName)
        {
            if(_isPlaying)
                StopPlayer();

            // TODO get nbFile ok
            _mplayer.Open(new Uri(strFileName, UriKind.Relative));
            NbFile++;
            _isPlaying = true;
            _mplayer.Play();
        }

        // Méthode principale
        public void CreateAndPlayMusic(int[] notes = null, string filename = null, bool doPlay = true)
        {
            // s'il y a un fichier en cours de lecture on l'arrête 
            if (_isPlaying)
            {
                StopPlayer();
            }

            // Générateur aléatoire
            //Random rand = new Random();

            // 1) Créer le fichier MIDI
            // a. Créer un fichier et une piste audio ainsi que les informations de tempo
            MIDISong song = CreateSong("Piste1");

            // b. Choisir un instrument entre 1 et 128 
            // Liste complète ici : http://fr.wikipedia.org/wiki/General_MIDI
            int instrument = GetRandom(1, 129);
            song.SetChannelInstrument(instrument);

            // c. Ajouter des notes
            // Chaque note est comprise entre 0 et 127 (12 correspond au type de note, fixe ici à des 1/4)
            // L'équivalence avec les notes / octaves est disponible ici : https://andymurkin.files.wordpress.com/2012/01/midi-int-midi-note-no-chart.jpg
            // Ici 16 notes aléatoire entre 16 et 96 (pour éviter certaines notes trop aigues ou trop graves)
            for (int i = 0; i < 16; i++)
            {
                int note;
                if (notes == null)
                {
                    note = GetRandom(24, 96);
                }
                else
                {
                    note = notes[i];
                }
                song.AddNote(note);

            }

            // on écrit le fichier
            _strFileName = WriteMIDI(song, filename);

            //_songs.Add(new OldIndividu(song));

            //((MainWindow)Application.Current.MainWindow).Songs.Add(_strFileName);

            if (doPlay)
            {
                // 2) Jouer un fichier MIDI
                PlayMIDI(_strFileName);
            }
        }

        public void Close()
        {
            // s'il y a un fichier en cours de lecture on l'arrête 
            if (_isPlaying)
            {
                StopPlayer();
                _isPlaying = false;
            }
            DeleteFiles();
        }
        public static void DeleteFiles(string path= "./", string pattern= "Fichier*.mid")
        { 
            IEnumerable<string> files = Directory.EnumerateFiles(path, pattern);
            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception)
                {

                    Debug.WriteLine("Error lors de la suppression du fichier " + file);
                }
            }
        }

        /// <summary>
        /// Creates a single tracked song
        /// </summary>
        /// <param name="trackName">Explicit as hell</param>
        /// <param name="bpm">Bits per minute (tempo)</param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public static MIDISong CreateSong(string trackName, int bpm = 150, string signature = null)
        {
            MIDISong song = new MIDISong();
            song.AddTrack(trackName);

            //string[] splitSignature = signature.Split('/');
            //if(splitSignature.Length != 2)
            //    throw new Exception("Error: Wrong song signature");

            song.SetTimeSignature(0, 4, 4);
            song.SetTempo(0, bpm);

            return song;
        }

        public static int GetRandom(int incMin, int exMax)
        {
            return _rand.Next(incMin, exMax);
        }

        public static string WriteMIDI(MIDISong song, string filename = null, int nbFile = -1)
        {
            if (nbFile == -1)
                nbFile = NbFile;

            // d. Enregistrer le fichier .mid (lisible dans un lecteur externe par exemple)
            // on prépare le flux de sortie
            MemoryStream ms = new MemoryStream();
            song.Save(ms);
            ms.Seek(0, SeekOrigin.Begin);
            byte[] src = ms.GetBuffer();
            byte[] dst = new byte[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                dst[i] = src[i];
            }
            ms.Close();

            if (nbFile >= MAXFILES)
            {
                DeleteFiles();
                nbFile = 0;
            }

            // et on écrit le fichier
            string strFileName = filename ?? "Fichier" + nbFile + ".mid";
            FileStream objWriter = File.Create(strFileName);
            objWriter.Write(dst, 0, dst.Length);
            objWriter.Close();
            objWriter.Dispose();
            objWriter = null;

            return strFileName;
        }
    }
}
