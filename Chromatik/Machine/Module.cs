using System;
using System.Collections.Generic;
using System.Text;

namespace Chromatik.Machine
{
    /// <summary>
    /// Represents a combination of a <see cref="Machine.Rotor"/> and a <see cref="Machine.PlugBoard"/>
    /// </summary>
    public class Module : ICloneable
    {
        /// <summary>
        /// Create a module for a <see cref="Machine.PlugBoard"/>
        /// </summary>
        /// <param name="plugBoard"></param>
        public Module(PlugBoard plugBoard) : this(plugBoard, null)
        { }
        /// <summary>
        /// Create a module for a <see cref="Machine.Rotor"/>
        /// </summary>
        /// <param name="rotor"></param>
        public Module(Rotor rotor) : this(null, rotor)
        { }
        /// <summary>
        /// Create a module for a <see cref="Machine.Rotor"/> and a <see cref="Machine.PlugBoard"/>
        /// </summary>
        /// <param name="plugBoard"></param>
        /// <param name="rotor"></param>
        public Module(PlugBoard plugBoard, Rotor rotor)
        {
            if (plugBoard == null && rotor == null)
                throw new ArgumentNullException(null, "The " + nameof(rotor) + " and the " + nameof(plugBoard) + " may not be null.");

            PlugBoard = plugBoard;
            Rotor = rotor;
        }

        /// <summary>
        /// <see cref="Machine.PlugBoard"/> of this module
        /// </summary>
        public PlugBoard PlugBoard { get; }
        /// <summary>
        /// <see cref="Machine.Rotor"/> of this module
        /// </summary>
        public Rotor Rotor { get; }

        /// <summary>
        /// Rotate the rotor to the specified position.
        /// </summary>
        /// <returns></returns>
        public void RotateToPosition(char position)
        {
            if (Rotor != null)
                Rotor.RotateToPosition(position);
        }
        /// <summary>
        /// Rotate the rotor to the next position.
        /// </summary>  
        /// <returns>true if the next should be rotate</returns>
        public bool Rotate()
        {
            if (Rotor != null)
                return Rotor.Rotate();
            return true;
        }

        /// <summary>
        /// Process the input signal to left output signal (Rotor then Plugs).
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public char ProcessLeft(char input)
        {
            if (Rotor != null)
                input = Rotor.ProcessLeft(input);
            if (PlugBoard != null)
                input = PlugBoard.Process(input);
            return input;
        }
        /// <summary>
        /// Process the input signal to right output signal (Rotor then Plugs).
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
		public char ProcessRight(char input)
        {
            if (PlugBoard != null)
                input = PlugBoard.Process(input);
            if (Rotor != null)
                input = Rotor.ProcessRight(input);
            return input;
        }
        /// <summary>
        /// Reset the rotor to the initial position.
        /// </summary>
        /// <returns></returns>
        public void Reset()
        {
            if (Rotor != null)
                Rotor.Reset();
        }

        /// <summary></summary>
        public override string ToString()
        {
            if (Rotor != null)
                return PlugBoard.ToString();
            else if (PlugBoard != null)
                return Rotor.ToString();
            else
                return Rotor.ToString() + " / " + PlugBoard.ToString();
        }

        /// <summary>
        /// Creates a duplicate of this Module.
        /// </summary>
        /// <returns></returns>
        public Module Clone()
        {
            return CloneModule(false);
        }
        /// <summary>
        /// Creates a duplicate of this Module and reset then.
        /// </summary>
        /// <returns></returns>
        public Module Clone(bool andReset)
        {
            return CloneModule(andReset);
        }
        /// <summary>
        /// Creates a duplicate of this Module.
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return CloneModule(false);
        }
        /// <summary>
        /// Creates a duplicate of this Module and reset then.
        /// </summary>
        /// <returns></returns>
        public Module CloneModule(bool andReset)
        {
            return new Module(PlugBoard.Clone(), Rotor.Clone(andReset));
        }
    }
}
