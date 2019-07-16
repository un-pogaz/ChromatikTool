using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Linq;

namespace Chromatik.Sequence
{
    public sealed class Robinson
    {
        /// <summary>
        /// The seed of the Sequence (first member)
        /// </summary>
        public ulong Seed { get; }

        /// <summary>
        /// Count of members from the Sequence
        /// </summary>
        public uint Lenght {
            get { return (uint)(Members.Length); }
            set {
                if (value > Members.Length)
                    AddMember(value - (uint)(Members.Length));
            }
        }

        /// <summary>
        /// Member of the Sequence
        /// </summary>
        public BigInteger[] Members { get { return _members; } }

        /// <summary>
        /// Member of the Sequence.
        /// Protected for heritance
        /// </summary>
        private BigInteger[] _members = new BigInteger[0];

        /// <summary>
        /// The last member of the Sequence
        /// </summary>
        public BigInteger LastMember
        {
            get
            {
                if (Members.Length == 0)
                    throw new NullReferenceException();

                return Members[Lenght - 1];
            }
        }
        /// <summary>
        /// Creat a Robinson Sequence for a specificed seed and stabilizes it
        /// </summary>
        public Robinson(ulong seed) : this(seed, 1, true) { }
        /// <summary>
        /// Creat a Robinson Sequence with a length of members and a seed specificed
        /// </summary>
        public Robinson(ulong seed, uint lenght) : this(seed, lenght, false) { }


        private Robinson(ulong seed, uint lenght, bool stable)
        {
            Seed = seed;
            _members = new BigInteger[] { Seed };
            Lenght = lenght;
            if (stable)
                GoToStable();
        }

        /// <summary>
        /// Add a member to the Sequence
        /// </summary>
        public BigInteger AddMember()
        {
            BigInteger n = GetNextMember();
            if (!Stable)
                Stable = Members.Contains(n);
            _members = Members.Concat(new BigInteger[] { n });
            return LastMember;
        }

        /// <summary>
        /// Add x members to the Sequence
        /// </summary>
        public BigInteger AddMember(uint newMember)
        {
            for (int i = 1; i <= newMember; i++)
                AddMember();
            return LastMember;
        }
        private BigInteger GetNextMember()
        {
            int n9 = 0;
            int n8 = 0;
            int n7 = 0;
            int n6 = 0;
            int n5 = 0;
            int n4 = 0;
            int n3 = 0;
            int n2 = 0;
            int n1 = 0;
            int n0 = 0;

            foreach (char c in LastMember.ToString())
            {
                if (c == '9')
                    n9++;
                else if (c == '8')
                    n8++;
                else if (c == '7')
                    n7++;
                else if (c == '6')
                    n6++;
                else if (c == '5')
                    n5++;
                else if (c == '4')
                    n4++;
                else if (c == '3')
                    n3++;
                else if (c == '2')
                    n2++;
                else if (c == '1')
                    n1++;
                else if (c == '0')
                    n0++;
            }

            string n = string.Empty;
            if (n9 > 0)
                n += n9 + "9";
            if (n8 > 0)
                n += n8 + "8";
            if (n7 > 0)
                n += n7 + "7";
            if (n6 > 0)
                n += n6 + "6";
            if (n5 > 0)
                n += n5 + "5";
            if (n4 > 0)
                n += n4 + "4";
            if (n3 > 0)
                n += n3 + "3";
            if (n2 > 0)
                n += n2 + "2";
            if (n1 > 0)
                n += n1 + "1";
            if (n0 > 0)
                n += n0 + "0";

            return BigInteger.Parse(n);
        }

        /// <summary>
        /// If the sequence as stabilised
        /// </summary>
        public bool Stable { get; private set; } = false;

        /// <summary>
        /// Lenght of periode if the sequence as stabilised
        /// </summary>
        public int StablePeriode
        {
            get {
                if (Stable)
                    return (int)(Lenght - Members.ToList().IndexOf(LastMember)) - 1;
                else
                    return -1;
            }
        }

        /// <summary>
        /// Will add members until the "stable" period of the sequence
        /// </summary>
        /// <returns>true if the Sequence as "stabilised" under 100000 addition</returns>
        public bool GoToStable()
        {
            int t = 0;
            while (!Stable && t < 100000)
            {
                AddMember();
                t++;
            }

            if (Stable)
                AddMember((uint)(Members.Length - Members.ToList().IndexOf(LastMember) - 2));

            return Stable;
        }
    }
}
