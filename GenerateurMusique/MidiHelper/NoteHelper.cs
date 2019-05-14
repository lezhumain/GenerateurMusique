using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateurMusique.MidiHelper
{
    public class NoteHelper
    {
        static public readonly short maxFrequency = 21000;
        static public readonly short minFrequency = 10;

        // Chaque note est comprise entre 0 et 127 (12?????? correspond au type de note, fixe ici à des 1/4)
        static public readonly short maxValue = 127;
        static public readonly short minValue = 0;

        static public int[] BasicMap(int[] from, int maxTo, int minTo, int maxFrom, int minFrom)
        {
            int[] to = from.Select(f =>
            {
                int toValue = ((f - minFrom) * (maxTo - minTo) / (maxFrom - minFrom)) + minTo;
                return toValue;
            }).ToArray();

            return to;
        }

        /// <summary>
        /// Converti une liste de frequences vers une liste de notes
        /// </summary>
        /// <param name="frequencies">Frequences de 10 a 21000Hz</param>
        /// <returns>Notes de 0 a 127</returns>
        static public int[] FrequenciesToValues(int[] frequencies)
        {
            //int[] values = frequencies.Select(f =>
            //{
            //    int value = ((f - minFrequency) * (maxValue - minValue) / (maxFrequency - minFrequency)) + minValue;
            //    return value;
            //}).ToArray();

            //return values;

            return BasicMap(frequencies, maxValue, minValue, maxFrequency, minFrequency);
        }

        // TODO fix this
        /// <summary>
        /// Converti une liste de notes vers une liste de frequences
        /// </summary>
        /// <param name="values">Notes de 0 a 127</param>
        /// <returns>Frequences de 10 a 21000Hz</returns>
        static public int[] ValuesToFrequencies(int[] values)
        {
            //int[] frequencies = values.Select(f =>
            //{
            //    int value = ((f - minValue) * (maxFrequency - minFrequency) / (maxValue - minValue)) + minFrequency;
            //    return value;
            //}).ToArray();

            //return frequencies;

            return BasicMap(values, maxFrequency, minFrequency, maxValue, minValue);
        }

        static private int[] BytesToInts(byte[] bytes)
        {
            const int critical = 683517;

            // If the system architecture is little-endian (that is, little end first),
            // reverse the byte array.
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            int[] iss = new int[bytes.Length];
            for(int i =0; i < iss.Length; ++i)                
            {
                try
                {
                    if(i >= critical)
                    {
                        iss[i] = 1;
                        int val = bytes[i];
                        Console.WriteLine("val = " + val.ToString());
                    }
                    //iss[i] = BitConverter.ToInt32(bytes, i);
                    iss[i] = bytes[i];
                }
                catch (Exception e)
                {
                    Console.WriteLine("Stopped at index: " + i.ToString());
                    throw e;
                }
            };

            return iss;
        }

        static public int[] NotesFromBytes(byte[] bytes)
        {
            int[] data = BytesToInts(bytes);
            return BasicMap(data, maxValue, minValue, byte.MaxValue, byte.MinValue);
        }
    }
}
