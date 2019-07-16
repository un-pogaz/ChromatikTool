using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Linq;

namespace Chromatik.Sequence
{
    // http://eljjdx.canalblog.com/archives/2015/06/29/32278826.html
    public sealed class Conway
    {
        /// <summary>
        /// The seed of the Sequence (first member)
        /// </summary>
        public ulong Seed { get; set; }

        /// <summary>
        /// Count of members from the Sequence
        /// </summary>
        public ulong Lenght
        {
            get { return (ulong)(Members.LongLength); }
            set
            {
                if (value > Lenght)
                    AddMember(value - Lenght);
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
            get {
                if (Members.Length == 0)
                    throw new NullReferenceException();

                return Members[Lenght - 1];
            }
        }

        public bool Transuranic {
            get {
                if (TransuranicTable == null || TransuranicTable.Length == 0)
                    return false;
                else
                    return true;
            }
        }
        public AudioactiveTable[] TransuranicTable { get; private set; }

        public AudioactiveTable.Commons CommonsTable { get; } = new AudioactiveTable.Commons();

        /// <summary>
        /// Creat a Conway Sequence with a length of 5 members for the seed 1
        /// </summary>
        public Conway() : this(1, 5) { }
        /// <summary>
        /// Creat a Conway Sequence with a specificed length of members for the seed 1
        /// </summary>
        public Conway(ulong lenght) : this(1, lenght) { }
        /// <summary>
        /// Creat a Conway Sequence with a length of members for a seed specificed
        /// </summary>
        public Conway(ulong seed, ulong lenght)
        {
            Seed = seed;
            
            //TransuranicTable = AudioactiveTable.GetTransuranicTables(Seed);
            
            //AudioactiveElement n = CommonsTable[1];

            _members = new BigInteger[] { Seed };

            AddMember(lenght - 1);
        }

        /// <summary>
        /// Add a member to the Sequence
        /// </summary>
        public BigInteger AddMember()
        {
            _members = Members.Concat(new BigInteger[] { GetNextMember() });
            return LastMember;
        }

        /// <summary>
        /// Add x members to the Sequence
        /// </summary>
        public BigInteger AddMember(ulong newMember)
        {
            for (ulong i = 1; i <= newMember; i++)
                AddMember();
            return LastMember;
        }

        private BigInteger GetNextMember()
        {
            string s = LastMember.ToString();

            string rslt = string.Empty;
            uint a = 0;
            char n = s[0];

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == n)
                    a++;
                else
                {
                    rslt += a.ToString() + n.ToString();
                    n = s[i];
                    a = 1;
                }
            }

            rslt += a.ToString() + n.ToString();

            return BigInteger.Parse(rslt.Replace(" ", ""));
        }
    }
}
