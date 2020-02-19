using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Cryptography.Machine
{
    /// <summary>
    /// Represente a Rotor for the <see cref="Enigma"/>
    /// </summary>
	sealed public partial class RotorEnigma : Rotor, ICloneable
    {
        /// <summary>
        /// Initialize a rotor with a set of given wires.
        /// </summary>
        public RotorEnigma(string id, IEnumerable<char> wires) : this(id, wires, null)
        { }
        /// <summary>
        /// Initialize a rotor with a set of given wires.
        /// </summary>
        public RotorEnigma(string id, IEnumerable<char> wires, char? initialPosition) : this(id, wires, initialPosition, null)
		{ }
        /// <summary>
        /// Initialize a rotor with a set of given wires and the specified rotate.
        /// </summary>
        public RotorEnigma(string id, IEnumerable<char> wires, char? initialPosition, char? rotateAt) : this(id, wires, initialPosition, rotateAt, null)
        { }
        /// <summary>
        /// Initialize a rotor with a set of given wires and the specifieds rotates.
        /// </summary>
		public RotorEnigma(string id, IEnumerable<char> wires, char? initialPosition, char? rotateAt, char? rotateAtSecondary) : base(id, wires, initialPosition)
        {
            if (rotateAt == null)
                RotateAt = OperatingAlphabet[0];
            else
            {
                if (!AlphabetContains(rotateAt))
                    throw ExceptionInvalidChar(rotateAt.GetValueOrDefault(), nameof(rotateAt));
                else
                    RotateAt = rotateAt.GetValueOrDefault();
            }

            if (rotateAtSecondary == null && !AlphabetContains(rotateAt))
                throw ExceptionInvalidChar(rotateAtSecondary.GetValueOrDefault(), nameof(rotateAtSecondary));
            else
                RotateAtSecondary = rotateAtSecondary;
        }

        /// <summary>
        /// First char to rotate adjacent rotor
        /// </summary>
        public new char RotateAt
        {
            set
            {
                if (!AlphabetContains(value))
                    throw ExceptionInvalidChar(value, nameof(value));
                else
                {
                    if (RotateAtSecondary == null)
                        base.RotateAt = new char[] { value };
                    else
                        base.RotateAt = new char[] { value, RotateAtSecondary.GetValueOrDefault() };

                }
            }
            get
            {
                return base.RotateAt[0];
            }
        }
        /// <summary>
        /// Secondary char to rotate adjacent rotor
        /// </summary>
        public char? RotateAtSecondary
        {
            set
            {
                if (value == null)
                    base.RotateAt = new char[] { RotateAt };
                else
                {
                    if (!AlphabetContains(value))
                        throw ExceptionInvalidChar(value.GetValueOrDefault(), nameof(value));
                    else
                        base.RotateAt = new char[] { RotateAt, value.GetValueOrDefault() };
                }
            }

            get
            {
                if (base.RotateAt.Length >= 2)
                    return base.RotateAt[1];
                else
                    return null;
            }
        }

        /// <summary>
        /// Creates a duplicate of this rotor
        /// </summary>
        /// <returns></returns>
        new public RotorEnigma Clone()
        {
            return CloneRotor(false);
        }
        /// <summary>
        /// Creates a duplicate of this rotor and reset then.
        /// </summary>
        /// <returns></returns>
        new public RotorEnigma Clone(bool andReset)
        {
            return CloneRotor(andReset);
        }
        /// <summary>
        /// Creates a duplicate of this rotor
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return CloneRotor(false);
        }
        /// <summary>
        /// Creates a duplicate of this rotor and reset then.
        /// </summary>
        /// <returns></returns>
        new public RotorEnigma CloneRotor(bool andReset)
        {
            RotorEnigma rslt = new RotorEnigma(Id, source_alphabet, InitialPosition, RotateAt, RotateAtSecondary);
            if (!andReset)
                rslt.RotateToPosition(OffsetPosition);
            return rslt;
        }
    }
}