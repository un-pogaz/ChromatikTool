using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Linq;


namespace Chromatik.Sequence
{
    public sealed class Fibonacci
    {
        /// <summary>
        /// The seed of the Sequence (first member)
        /// </summary>
        public ulong Seed { get; }

        /// <summary>
        /// Count of members from the Sequence
        /// </summary>
        public ulong Lenght
        {
            get { return (ulong)(Members.LongLength); }
            set {
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
                if (Lenght == 0)
                    throw new NullReferenceException();

                return Members[Lenght - 1];
            }
        }
        /// <summary>
        /// Creat a Fibonacci sequence with a length of 100 members
        /// </summary>
        public Fibonacci() : this(100) { }
        /// <summary>
        /// Creat a Fibonacci sequence with a specificed length
        /// </summary>
        public Fibonacci(uint lenght) : this(0, lenght) { }
        /// <summary>
        /// Creat a Fibonacci-like sequence with a length of members and a seed specificed
        /// </summary>
        public Fibonacci(ulong seed, uint lenght)
        {
            Seed = seed;
            _members = new BigInteger[] { Seed, Seed + 1 };
            Lenght = lenght;
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
        BigInteger GetNextMember()
        {
            return Members[Lenght - 2] + Members[Lenght - 1];
        }
    }
}
