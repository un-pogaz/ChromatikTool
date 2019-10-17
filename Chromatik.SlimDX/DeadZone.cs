using System;
using System.Collections.Generic;
using System.Text;

namespace Chromatik.SlimDX
{
    /// <summary>
    /// Class to define the dead zone for different axes.
    /// </summary>
    public class DeadZone
    {
        /// <summary>
        /// Create a clas with no dead zone
        /// </summary>
        public DeadZone() : this(0)
        { }
        /// <summary>
        /// Create a clas with the specified value.
        /// </summary>
        /// <param name="value"></param>
        public DeadZone(int value)
        {
            SetAll(value);
        }
        /// <summary>
        /// Set the dead zone of all axe.
        /// </summary>
        /// <param name="value"></param>
        public void SetAll(int value)
        {
            if (value < 0)
                value = 0;
            else if (value >= JoystickDevice.Range)
                value = JoystickDevice.Range - 1;

            AxeX = value;
            AxeY = value;
            AxeZ = value;
            AccelerationX = value;
            AccelerationY = value;
            AccelerationZ = value;
            AngularAccelerationX = value;
            AngularAccelerationY = value;
            AngularAccelerationZ = value;
            AngularAccelerationX = value;
            AngularAccelerationY = value;
            AngularAccelerationZ = value;
            AngularVelocityX = value;
            AngularVelocityY = value;
            AngularVelocityZ = value;
            ForceX = value;
            ForceY = value;
            ForceZ = value;
            RotationX = value;
            RotationY = value;
            RotationZ = value;
            TorqueX = value;
            TorqueY = value;
            TorqueZ = value;
            VelocityX = value;
            VelocityY = value;
            VelocityZ = value;
        }

        /// <summary>
        /// Calculation of a new value proportional to the dead zone.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dead"></param>
        /// <returns></returns>
        static public int Calc(int value, int dead)
        {
            bool nega = false;
            if (value < 0)
            {
                nega = true;
                value = value * -1;
            }

            if (value <= dead)
                value = 0;
            else
            {
                double perc = (value - dead) / (double)(JoystickDevice.Range - dead);
                value = (int)Math.Round(JoystickDevice.Range * perc);
            }

            if (nega)
                value = value * -1;
            return value;
        }

        public int AxeX { get; set; }
        public int AxeY { get; set; }
        public int AxeZ { get; set; }

        public int AccelerationX { get; set; }
        public int AccelerationY { get; set; }
        public int AccelerationZ { get; set; }

        public int AngularAccelerationX { get; set; }
        public int AngularAccelerationY { get; set; }
        public int AngularAccelerationZ { get; set; }

        public int AngularVelocityX { get; set; }
        public int AngularVelocityY { get; set; }
        public int AngularVelocityZ { get; set; }

        public int ForceX { get; set; }
        public int ForceY { get; set; }
        public int ForceZ { get; set; }

        public int RotationX { get; set; }
        public int RotationY { get; set; }
        public int RotationZ { get; set; }

        public int TorqueX { get; set; }
        public int TorqueY { get; set; }
        public int TorqueZ { get; set; }

        public int VelocityX { get; set; }
        public int VelocityY { get; set; }
        public int VelocityZ { get; set; }
    }
}
