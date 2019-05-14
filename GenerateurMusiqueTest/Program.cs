using GenerateurMusique.MidiHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateurMusiqueTest
{
    class Program
    {
        static void TestFrequenciesToValues()
        {
            int[] expectedValues = new[] { NoteHelper.minValue, NoteHelper.maxValue, (NoteHelper.minValue + NoteHelper.maxValue) / 2 };

            int[] freqs = new[] { NoteHelper.minFrequency, NoteHelper.maxFrequency, (NoteHelper.minFrequency + NoteHelper.maxFrequency) / 2};
            int[] values = NoteHelper.FrequenciesToValues(freqs);

            if(values.Length != freqs.Length)
            {
                Console.WriteLine(string.Format("values.Length != freqs.Length: {0} != {1}", values.Length, freqs.Length));
                return;
            }

            for(int i = 0; i < values.Length; ++i)
            {
                if (values[i] != expectedValues[i])
                {
                    Console.WriteLine(string.Format("values[i] != expectedValues[i]: {0} != {1}", values[i], expectedValues[i]));
                    return;
                }
            }

            Console.WriteLine("TestFrequenciesToValues: No error.");
        }

        // TODO fix this
        static void TestValuesToFrequencies()
        {
            int[] expectedFrequencys = new[] { NoteHelper.minFrequency, NoteHelper.maxFrequency, (NoteHelper.minFrequency + NoteHelper.maxFrequency) / 2 };

            int[] values = new[] { NoteHelper.minValue, NoteHelper.maxValue, (NoteHelper.minValue + NoteHelper.maxValue) / 2 };
            int[] freqs = NoteHelper.ValuesToFrequencies(values);

            if (values.Length != freqs.Length)
            {
                Console.WriteLine(string.Format("values.Length != freqs.Length: {0} != {1}", values.Length, freqs.Length));
                return;
            }

            for (int i = 0; i < freqs.Length; ++i)
            {
                if (freqs[i] != expectedFrequencys[i])
                {
                    Console.WriteLine(string.Format("values[i] != expectedFrequencys[i]: {0} != {1}", freqs[i], expectedFrequencys[i]));
                    return;
                }
            }

            Console.WriteLine("TestValuesToFrequencies: No error.");
        }

        static void TestGetImgBytes()
        {
            string fileName = @"C:\Users\Dju\Pictures\tortueGenial.jpg";
            byte[] imgBytes = ImageHelper.GetBytes(fileName);

            if (imgBytes.Length == 0)
            {
                Console.WriteLine("Didn't get any byte.");
                return;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(imgBytes.Length);
            }
        }

        static void TestSaveImgToMidi()
        {
            try
            {
                string fileName = @"C:\Users\Dju\Pictures\tortueGenial.jpg";
                string outFileName = @"C:\Users\Dju\Pictures\tortueGenial.midi";
                byte[] imgBytes = ImageHelper.GetBytes(fileName);
                int[] notes = NoteHelper.NotesFromBytes(imgBytes);

                MidiComposer composer = new MidiComposer();
                composer.CreateAndPlayMusic(notes, outFileName, true);

                Console.WriteLine("Wrote " + outFileName);
            }
            catch(Exception e)
            {
                Console.WriteLine("Couldn't write MIDI");
            }
        }

        static void TestSaveIconToMidi()
        {
            try
            {
                string fileName = @"C:\Users\Dju\Pictures\fuck.ico";
                string outFileName = @"C:\Users\Dju\Pictures\fuck.midi";
                Icon ic = new Icon(fileName);
                byte[] imgBytes = ImageHelper.IconToBytes(ic);
                int[] notes = NoteHelper.NotesFromBytes(imgBytes);

                MidiComposer composer = new MidiComposer();
                composer.CreateAndPlayMusic(notes, outFileName, true);

                Console.WriteLine("Wrote " + outFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't write MIDI");
            }
        }

        static void Main(string[] args)
        {
            //TestFrequenciesToValues();
            //TestValuesToFrequencies();
            //TestGetImgBytes();
            //TestSaveImgToMidi();
            TestSaveIconToMidi();
            Console.WriteLine("Done\nPress a key to exist.");
            Console.ReadKey();
        }
    }
}
