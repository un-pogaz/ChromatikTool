using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Chromatik.Sequence
{

    public class AudioactiveLine
    {
        public AudioactiveElement[] Elements;

        public AudioactiveElement[] TransuranicTable;

        public bool IsExotic;

        public bool IsTransuranic;

        public BigInteger Line;

        public AudioactiveLine(BigInteger line)
        {

        }
    }

    public class AudioactiveElement
    {
        public BigInteger AtomicNumber { get; }
        public string Name { get; }
        public string ChemicalSymbol { get; }

        public bool IsExotic;

        public bool IsTransuranic;

        public AudioactiveElement[] Decay { get; internal set; }
        
        internal AudioactiveElement(BigInteger atomicNumber, string name, string chemicalSymbol)
        {
            AtomicNumber = atomicNumber;
            if (string.IsNullOrWhiteSpace(name))
            {

            }
            else
                Name = name;

            if (string.IsNullOrWhiteSpace(chemicalSymbol))
            {

            }
            else
                ChemicalSymbol = chemicalSymbol;


            ChemicalSymbol = chemicalSymbol;
        }

        internal AudioactiveElement(BigInteger atomicNumber) : this (atomicNumber, null, null)
        {
        }
    }

    /// <summary>
    /// Audioactive Table for Commons Elements
    /// </summary>
    public struct AudioactiveTable
    {
        static AudioactiveTable()
        {
            Hydro­gen.Decay = new AudioactiveElement[] { Hydro­gen };
            He­lium.Decay = new AudioactiveElement[] { Lith­ium };

            Lith­ium.Decay = new AudioactiveElement[] { He­lium };
        }

        
        static private BigInteger GetBigInteger(string s) { return BigInteger.Parse(s.Replace(" ", "")); }

        //1
        static public AudioactiveElement Hydrogen { get; } = new AudioactiveElement(GetBigInteger("22"), "Hydrogen", "H");
        static public AudioactiveElement Helium { get; } = new AudioactiveElement(GetBigInteger("131122211332 113221122112 13322112"), "Helium", "He");

        //2
        static public AudioactiveElement Lithium { get; } = new AudioactiveElement(GetBigInteger("3122113222 1222112112 3222112"), "Lithium", "Li");
        static public AudioactiveElement Beryllium { get; } = new AudioactiveElement(GetBigInteger("11131221131211 32211332113221 12211213322112"), "Beryllium", "Be");
        static public AudioactiveElement Boron { get; } = new AudioactiveElement(GetBigInteger("132113212221 132221222112 1123222112"), "Boron", "B");
        static public AudioactiveElement Carbon { get; } = new AudioactiveElement(GetBigInteger("3113112211 3221122112 13322112"), "Carbon", "C");
        static public AudioactiveElement Azote { get; } = new AudioactiveElement(GetBigInteger("111312212221 121123222112"), "Azote", "N");
        static public AudioactiveElement Oxygen { get; } = new AudioactiveElement(GetBigInteger("1321122112 13322112"), "Oxygen", "O");
        static public AudioactiveElement Fluorine { get; } = new AudioactiveElement(GetBigInteger("22"), "Fluorine", "F");
        static public AudioactiveElement Neon { get; } = new AudioactiveElement(GetBigInteger("22"), "Neon", "Ne");

        //3
        static public AudioactiveElement Sodium { get; } = new AudioactiveElement(GetBigInteger("22"), "Sodium", "Na");
        static public AudioactiveElement Magnesium { get; } = new AudioactiveElement(GetBigInteger("22"), "Magnesium", "Mg");
        static public AudioactiveElement Aluminium { get; } = new AudioactiveElement(GetBigInteger("22"), "Aluminium", "Al");
        static public AudioactiveElement Silicon { get; } = new AudioactiveElement(GetBigInteger("22"), "Silicon", "Si");
        static public AudioactiveElement Phosphorus { get; } = new AudioactiveElement(GetBigInteger("22"), "Phosphorus", "P");
        static public AudioactiveElement Sulfur { get; } = new AudioactiveElement(GetBigInteger("22"), "Sulfur", "S");
        static public AudioactiveElement Chlorine { get; } = new AudioactiveElement(GetBigInteger("22"), "Chlorine", "Cl");
        static public AudioactiveElement Argon { get; } = new AudioactiveElement(GetBigInteger("22"), "Argon", "Ar");

        //4
        static public AudioactiveElement Potassium { get; } = new AudioactiveElement(GetBigInteger("22"), "Potassium", "K");
        static public AudioactiveElement Calcium { get; } = new AudioactiveElement(GetBigInteger("22"), "Calcium", "Ca");
        static public AudioactiveElement Scandium { get; } = new AudioactiveElement(GetBigInteger("22"), "Scandium", "Sc");
        static public AudioactiveElement Titanium { get; } = new AudioactiveElement(GetBigInteger("22"), "Titanium", "Ti");
        static public AudioactiveElement Vanadium { get; } = new AudioactiveElement(GetBigInteger("22"), "Vanadium", "V");
        static public AudioactiveElement Chromium { get; } = new AudioactiveElement(GetBigInteger("22"), "Chromium", "Cr");
        static public AudioactiveElement Manganese { get; } = new AudioactiveElement(GetBigInteger("22"), "Manganese", "Mn");
        static public AudioactiveElement Iron { get; } = new AudioactiveElement(GetBigInteger("22"), "Iron", "Fe");
        static public AudioactiveElement Cobalt { get; } = new AudioactiveElement(GetBigInteger("22"), "Cobalt", "Co");
        static public AudioactiveElement Nickel { get; } = new AudioactiveElement(GetBigInteger("22"), "Nickel", "Ni");
        static public AudioactiveElement Copper { get; } = new AudioactiveElement(GetBigInteger("22"), "Copper", "Cu");
        static public AudioactiveElement Zinc { get; } = new AudioactiveElement(GetBigInteger("22"), "Zinc", "Zn");
        static public AudioactiveElement Gallium { get; } = new AudioactiveElement(GetBigInteger("22"), "Gallium", "Ga");
        static public AudioactiveElement Germanium { get; } = new AudioactiveElement(GetBigInteger("22"), "Germanium", "Ge");
        static public AudioactiveElement Arsenic { get; } = new AudioactiveElement(GetBigInteger("22"), "Arsenic", "As");
        static public AudioactiveElement Selenium { get; } = new AudioactiveElement(GetBigInteger("22"), "Selenium", "Se");
        static public AudioactiveElement Bromine { get; } = new AudioactiveElement(GetBigInteger("22"), "Bromine", "Br");
        static public AudioactiveElement Krypton { get; } = new AudioactiveElement(GetBigInteger("22"), "Krypton", "Kr");

        //5

        //6

        //7
        
        public struct Commons
        {
            static Commons()
            {
                foreach (var item in ElementTable)
                {
                    ElementNames.Add(item.Name);
                    ElementSymbols.Add(item.ChemicalSymbol);
                    ElementNumber.Add(item.AtomicNumber);
                }
            }

            public AudioactiveElement this[BigInteger index]
            {
                get {


                    throw new IndexOutOfRangeException();
                }
            }

            static private List<string> ElementNames;

            static private List<string> ElementSymbols;

            static private List<BigInteger> ElementNumber;

            static private AudioactiveElement[] ElementTable = new AudioactiveElement[]
            {
            Hydrogen,
            Helium,
            Lithium,
            Beryllium,
            Boron,
            Carbon,
            Azote,
            Oxygen,
            Fluorine,
            Neon,
            Sodium,
            Magnesium,
            Aluminium,
            Silicon,
            Phosphorus,
            Sulfur,
            Chlorine,
            Argon,
            Potassium,
            Calcium,
            Scandium,
            Titanium,
            Vanadium,
            Chromium,
            Manganese,
            Iron,
            Cobalt,
            Nickel,
            Copper,
            Zinc,
            Gallium,
            Germanium,
            Arsenic,
            Selenium,
            Bromine,
            Krypton,
            };
        }

        //Transuranic Elements
        static public AudioactiveTable[] GetTransuranicTables(ulong Number)
        {
            List<AudioactiveTable> rslt = new List<AudioactiveTable>();

            string stu = Number.ToString().Replace("1", "").Replace("2", "").Replace("3", "");

            if (stu.Contains("4"))
                rslt.Add(Transuranic.n4);
            if (stu.Contains("5"))
                rslt.Add(Transuranic.n5);
            if (stu.Contains("6"))
                rslt.Add(Transuranic.n6);
            if (stu.Contains("7"))
                rslt.Add(Transuranic.n7);
            if (stu.Contains("8"))
                rslt.Add(Transuranic.n8);
            if (stu.Contains("9"))
                rslt.Add(Transuranic.n9);

            return rslt.ToArray();
        }
        public struct Transuranic
        {
            static public AudioactiveTable n4 = new AudioactiveTable(4);
            static public AudioactiveTable n5 = new AudioactiveTable(5);
            static public AudioactiveTable n6 = new AudioactiveTable(6);
            static public AudioactiveTable n7 = new AudioactiveTable(7);
            static public AudioactiveTable n8 = new AudioactiveTable(8);
            static public AudioactiveTable n9 = new AudioactiveTable(9);
        }

        private AudioactiveTable(short transuranicNumber)
        {
            if (transuranicNumber > 9 || transuranicNumber < 4)
                throw new ArgumentOutOfRangeException();

            TransuranicNumber = transuranicNumber;
            Neptu­nium = new AudioactiveElement(GetBigInteger("131122211332 113221122112 1332211" + TransuranicNumber), "Neptunium", "Np");
            Plutonium = new AudioactiveElement(GetBigInteger("3122113222 1222112112 322211" + TransuranicNumber), "Plutonium­", "Pu");

            Neptu­nium.Decay = new AudioactiveElement[] { Hydrogen, Plutonium };
            Plutonium.Decay = new AudioactiveElement[] { Neptu­nium };
        }
        public short TransuranicNumber { get; }
        public AudioactiveElement Neptu­nium { get; }
        public AudioactiveElement Pluto­nium { get; }

    }
}
