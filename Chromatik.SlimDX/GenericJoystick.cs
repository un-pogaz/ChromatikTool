using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX.DirectInput;

namespace Chromatik.SlimDX
{
    /// <summary>
    /// Represents a generic Joystick
    /// </summary>
    public class GenericJoystick
    {
        /// <summary>
        /// Device of this instance
        /// </summary>
        public Joystick device { get; }

        /// <summary>
        /// Create a representation of a joystick
        /// </summary>
        /// <param name="joystick"></param>
        public GenericJoystick(Joystick joystick) : this(joystick, 0, false)
        { }
        /// <summary>
        /// Create a representation of a joystick and set a dead zone for all axe
        /// </summary>
        /// <param name="joystick"></param>
        /// <param name="deadZone"></param>
        public GenericJoystick(Joystick joystick, int deadZone) : this(joystick, deadZone, false)
        { }
        /// <summary>
        /// Create a representation of a joystick and enable UseDirectStat
        /// </summary>
        /// <param name="joystick"></param>
        /// <param name="directStat"></param>
        public GenericJoystick(Joystick joystick, bool directStat) : this(joystick, 0, directStat)
        { }
        /// <summary>
        /// Create a representation of a joystick, set a dead zone for all axes and enable UseDirectStat
        /// </summary>
        /// <param name="joystick"></param>
        /// <param name="deadZone"></param>
        /// <param name="directStat"></param>
        public GenericJoystick(Joystick joystick, int deadZone, bool directStat)
        {
            device = joystick;
            UseDirectStat = directStat;
            DeadZone.SetAll(deadZone);
            ReadInputsValues();
        }

        /// <summary>
        /// Enables the updating of values when they are called.
        /// </summary>
        /// <remarks>If enabled, the values (Axes, Buttons...) will be updated when they are calle
        /// Otherwise, use <see cref="UpdateOutput()"/> to update the values.</remarks>
        public bool UseDirectStat { get; set; }

        /// <summary>
        /// Dead zone value for the different axes
        /// </summary>
        public DeadZone DeadZone { get; } = new JoystickDeadZone(0);

        /// <summary>
        /// Inversed axes of the joystick
        /// </summary>
        public InversedAxe InversedAxe { get; } = new JoystickInversedAxe();
        
        protected JoystickState JoystickState
        {
            get {
                if (UseDirectStat)
                    ReadInputsValues();

                return _JoystickState;
            }
            set { _JoystickState = value; }
        }
        protected JoystickState _JoystickState;


        /// <summary>
        /// Update input value of the device
        /// </summary>
        public void UpdateOutput()
        {
            ReadInputsValues();
        }
        /// <summary>
        /// Read the input of the device and update <see cref="JoystickState"/>
        /// </summary>
        protected void ReadInputsValues()
        {
            JoystickState = device.GetCurrentState();
        }

        public bool[] Buttons
        {
            get { return JoystickState.GetButtons(); }
        }

        public int[] AccelerationSliders
        {
            get { return JoystickState.GetAccelerationSliders(); }
        }
        public int[] ForceSliders
        {
            get { return JoystickState.GetForceSliders(); }
        }
        public int[] PointOfViewControllers
        {
            get { return JoystickState.GetPointOfViewControllers(); }
        }
        public int[] Sliders
        {
            get { return JoystickState.GetSliders(); }
        }
        public int[] VelocitySliders
        {
            get { return JoystickState.GetVelocitySliders(); }
        }

        public int AxeX
        {
            get
            {
                if (InversedAxe.AxeX)
                    return JoystickDeadZone.Calc(-JoystickState.X, DeadZone.AxeX);
                else
                    return JoystickDeadZone.Calc(JoystickState.X, DeadZone.AxeX);
            }
        }
        public int AxeY
        {
            get
            {
                if (InversedAxe.AxeY)
                    return JoystickDeadZone.Calc(-JoystickState.Y, DeadZone.AxeY);
                else
                    return JoystickDeadZone.Calc(JoystickState.Y, DeadZone.AxeY);
            }
        }
        public int AxeZ
        {
            get
            {
                if (InversedAxe.AxeZ)
                    return JoystickDeadZone.Calc(-JoystickState.Z, DeadZone.AxeZ);
                else
                    return JoystickDeadZone.Calc(JoystickState.Z, DeadZone.AxeZ);
            }
        }

        public int AccelerationX
        {
            get
            {
                if (InversedAxe.AccelerationX)
                    return JoystickDeadZone.Calc(-JoystickState.AccelerationX, DeadZone.AccelerationX);
                else
                    return JoystickDeadZone.Calc(JoystickState.AccelerationX, DeadZone.AccelerationX);
            }
        }
        public int AccelerationY
        {
            get
            {
                if (InversedAxe.AccelerationY)
                    return JoystickDeadZone.Calc(-JoystickState.AccelerationY, DeadZone.AccelerationY);
                else
                    return JoystickDeadZone.Calc(JoystickState.AccelerationY, DeadZone.AccelerationY);
            }
        }
        public int AccelerationZ
        {
            get
            {
                if (InversedAxe.AccelerationZ)
                    return JoystickDeadZone.Calc(-JoystickState.AccelerationZ, DeadZone.AccelerationZ);
                else
                    return JoystickDeadZone.Calc(JoystickState.AccelerationZ, DeadZone.AccelerationZ);
            }
        }

        public int AngularAccelerationX
        {
            get
            {
                if (InversedAxe.AngularAccelerationX)
                    return JoystickDeadZone.Calc(-JoystickState.AngularAccelerationX, DeadZone.AngularAccelerationX);
                else
                    return JoystickDeadZone.Calc(JoystickState.AngularAccelerationX, DeadZone.AngularAccelerationX);
            }
        }
        public int AngularAccelerationY
        {
            get
            {
                if (InversedAxe.AngularAccelerationY)
                    return JoystickDeadZone.Calc(-JoystickState.AngularAccelerationY, DeadZone.AngularAccelerationY);
                else
                    return JoystickDeadZone.Calc(JoystickState.AngularAccelerationY, DeadZone.AngularAccelerationY);
            }
        }
        public int AngularAccelerationZ
        {
            get
            {
                if (InversedAxe.AngularAccelerationZ)
                    return JoystickDeadZone.Calc(-JoystickState.AngularAccelerationZ, DeadZone.AngularAccelerationZ);
                else
                    return JoystickDeadZone.Calc(JoystickState.AngularAccelerationZ, DeadZone.AngularAccelerationZ);
            }
        }

        public int AngularVelocityX
        {
            get
            {
                if (InversedAxe.AngularVelocityX)
                    return JoystickDeadZone.Calc(-JoystickState.AngularVelocityX, DeadZone.AngularVelocityX);
                else
                    return JoystickDeadZone.Calc(JoystickState.AngularVelocityX, DeadZone.AngularVelocityX);
            }
        }
        public int AngularVelocityY
        {
            get
            {
                if (InversedAxe.AngularVelocityY)
                    return JoystickDeadZone.Calc(-JoystickState.AngularVelocityY, DeadZone.AngularVelocityY);
                else
                    return JoystickDeadZone.Calc(JoystickState.AngularVelocityY, DeadZone.AngularVelocityY);
            }
        }
        public int AngularVelocityZ
        {
            get
            {
                if (InversedAxe.AngularVelocityZ)
                    return JoystickDeadZone.Calc(-JoystickState.AngularVelocityZ, DeadZone.AngularVelocityZ);
                else
                    return JoystickDeadZone.Calc(JoystickState.AngularVelocityZ, DeadZone.AngularVelocityZ);
            }
        }

        public int ForceX
        {
            get
            {
                if (InversedAxe.ForceX)
                    return JoystickDeadZone.Calc(-JoystickState.ForceX, DeadZone.ForceX);
                else
                    return JoystickDeadZone.Calc(JoystickState.ForceX, DeadZone.ForceX);
            }
        }
        public int ForceY
        {
            get
            {
                if (InversedAxe.ForceY)
                    return JoystickDeadZone.Calc(-JoystickState.ForceY, DeadZone.ForceY);
                else
                    return JoystickDeadZone.Calc(JoystickState.ForceY, DeadZone.ForceY);
            }
        }
        public int ForceZ
        {
            get
            {
                if (InversedAxe.ForceZ)
                    return JoystickDeadZone.Calc(-JoystickState.ForceZ, DeadZone.ForceZ);
                else
                    return JoystickDeadZone.Calc(JoystickState.ForceZ, DeadZone.ForceZ);
            }
        }

        public int RotationX
        {
            get
            {
                if (InversedAxe.RotationX)
                    return JoystickDeadZone.Calc(-JoystickState.RotationX, DeadZone.RotationX);
                else
                    return JoystickDeadZone.Calc(JoystickState.RotationX, DeadZone.RotationX);
            }
        }
        public int RotationY
        {
            get
            {
                if (InversedAxe.RotationY)
                    return JoystickDeadZone.Calc(-JoystickState.RotationY, DeadZone.RotationY);
                else
                    return JoystickDeadZone.Calc(JoystickState.RotationY, DeadZone.RotationY);
            }
        }
        public int RotationZ
        {
            get
            {
                if (InversedAxe.RotationZ)
                    return JoystickDeadZone.Calc(-JoystickState.RotationZ, DeadZone.RotationZ);
                else
                    return JoystickDeadZone.Calc(JoystickState.RotationZ, DeadZone.RotationZ);
            }
        }

        public int TorqueX
        {
            get
            {
                if (InversedAxe.TorqueX)
                    return JoystickDeadZone.Calc(-JoystickState.TorqueX, DeadZone.TorqueX);
                else
                    return JoystickDeadZone.Calc(JoystickState.TorqueX, DeadZone.TorqueX);
            }
        }
        public int TorqueY
        {
            get
            {
                if (InversedAxe.TorqueY)
                    return JoystickDeadZone.Calc(-JoystickState.TorqueY, DeadZone.TorqueY);
                else
                    return JoystickDeadZone.Calc(JoystickState.TorqueY, DeadZone.TorqueY);
            }
        }
        public int TorqueZ
        {
            get
            {
                if (InversedAxe.TorqueZ)
                    return JoystickDeadZone.Calc(-JoystickState.TorqueZ, DeadZone.TorqueZ);
                else
                    return JoystickDeadZone.Calc(JoystickState.TorqueZ, DeadZone.TorqueZ);
            }
        }

        public int VelocityX
        {
            get
            {
                if (InversedAxe.VelocityX)
                    return JoystickDeadZone.Calc(-JoystickState.VelocityX, DeadZone.VelocityX);
                else
                    return JoystickDeadZone.Calc(JoystickState.VelocityX, DeadZone.VelocityX);
            }
        }
        public int VelocityY
        {
            get
            {
                if (InversedAxe.VelocityY)
                    return JoystickDeadZone.Calc(-JoystickState.VelocityY, DeadZone.VelocityY);
                else
                    return JoystickDeadZone.Calc(JoystickState.VelocityY, DeadZone.VelocityY);
            }
        }
        public int VelocityZ
        {
            get
            {
                if (InversedAxe.VelocityZ)
                    return JoystickDeadZone.Calc(-JoystickState.VelocityZ, DeadZone.VelocityZ);
                else
                    return JoystickDeadZone.Calc(JoystickState.VelocityZ, DeadZone.VelocityZ);
            }
        }
    }

    /// <summary>
    /// Class to define the inversed axes.
    /// </summary>
    public class InversedAxe
    {
        public bool AxeX { get; set; }
        public bool AxeY { get; set; }
        public bool AxeZ { get; set; }

        public bool AccelerationX { get; set; }
        public bool AccelerationY { get; set; }
        public bool AccelerationZ { get; set; }

        public bool AngularAccelerationX { get; set; }
        public bool AngularAccelerationY { get; set; }
        public bool AngularAccelerationZ { get; set; }

        public bool AngularVelocityX { get; set; }
        public bool AngularVelocityY { get; set; }
        public bool AngularVelocityZ { get; set; }

        public bool ForceX { get; set; }
        public bool ForceY { get; set; }
        public bool ForceZ { get; set; }

        public bool RotationX { get; set; }
        public bool RotationY { get; set; }
        public bool RotationZ { get; set; }

        public bool TorqueX { get; set; }
        public bool TorqueY { get; set; }
        public bool TorqueZ { get; set; }

        public bool VelocityX { get; set; }
        public bool VelocityY { get; set; }
        public bool VelocityZ { get; set; }
    }

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
