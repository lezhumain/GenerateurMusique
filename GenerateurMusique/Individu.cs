using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateurMusique
{
    public class Individu : MIDISong
    {
        //public void Save(MemoryStream objWriter)
        //{
        //    base.Save(objWriter);
        //}

        /// <summary>
        /// Creates an 'Individu' from a 'MIDISong'
        /// </summary>
        /// <param name="song">The MIDISong we create from</param>
        /// <param name="notes">The array containing the actual songNotes</param>
        public Individu(MIDISong song) : base(song)
        {
            int[] notes = song.Notes;

            int minNotes = notes.Length;

            for (int i = 0; i < minNotes; i++)
                _notes[i] = notes[i];

        }

        public Individu(Individu papa, Individu maman)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void Mutate()
        {
            //TODO
            throw new NotImplementedException();
        }

        public void Play()
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
