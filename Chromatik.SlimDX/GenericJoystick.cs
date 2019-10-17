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
        public DeadZone DeadZone { get; } = new DeadZone(0);

        /// <summary>
        /// Inversed axes of the joystick
        /// </summary>
        public InversedAxe InversedAxe { get; } = new InversedAxe();
        
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
                    return DeadZone.Calc(-JoystickState.X, DeadZone.AxeX);
                else
                    return DeadZone.Calc(JoystickState.X, DeadZone.AxeX);
            }
        }
        public int AxeY
        {
            get
            {
                if (InversedAxe.AxeY)
                    return DeadZone.Calc(-JoystickState.Y, DeadZone.AxeY);
                else
                    return DeadZone.Calc(JoystickState.Y, DeadZone.AxeY);
            }
        }
        public int AxeZ
        {
            get
            {
                if (InversedAxe.AxeZ)
                    return DeadZone.Calc(-JoystickState.Z, DeadZone.AxeZ);
                else
                    return DeadZone.Calc(JoystickState.Z, DeadZone.AxeZ);
            }
        }

        public int AccelerationX
        {
            get
            {
                if (InversedAxe.AccelerationX)
                    return DeadZone.Calc(-JoystickState.AccelerationX, DeadZone.AccelerationX);
                else
                    return DeadZone.Calc(JoystickState.AccelerationX, DeadZone.AccelerationX);
            }
        }
        public int AccelerationY
        {
            get
            {
                if (InversedAxe.AccelerationY)
                    return DeadZone.Calc(-JoystickState.AccelerationY, DeadZone.AccelerationY);
                else
                    return DeadZone.Calc(JoystickState.AccelerationY, DeadZone.AccelerationY);
            }
        }
        public int AccelerationZ
        {
            get
            {
                if (InversedAxe.AccelerationZ)
                    return DeadZone.Calc(-JoystickState.AccelerationZ, DeadZone.AccelerationZ);
                else
                    return DeadZone.Calc(JoystickState.AccelerationZ, DeadZone.AccelerationZ);
            }
        }

        public int AngularAccelerationX
        {
            get
            {
                if (InversedAxe.AngularAccelerationX)
                    return DeadZone.Calc(-JoystickState.AngularAccelerationX, DeadZone.AngularAccelerationX);
                else
                    return DeadZone.Calc(JoystickState.AngularAccelerationX, DeadZone.AngularAccelerationX);
            }
        }
        public int AngularAccelerationY
        {
            get
            {
                if (InversedAxe.AngularAccelerationY)
                    return DeadZone.Calc(-JoystickState.AngularAccelerationY, DeadZone.AngularAccelerationY);
                else
                    return DeadZone.Calc(JoystickState.AngularAccelerationY, DeadZone.AngularAccelerationY);
            }
        }
        public int AngularAccelerationZ
        {
            get
            {
                if (InversedAxe.AngularAccelerationZ)
                    return DeadZone.Calc(-JoystickState.AngularAccelerationZ, DeadZone.AngularAccelerationZ);
                else
                    return DeadZone.Calc(JoystickState.AngularAccelerationZ, DeadZone.AngularAccelerationZ);
            }
        }

        public int AngularVelocityX
        {
            get
            {
                if (InversedAxe.AngularVelocityX)
                    return DeadZone.Calc(-JoystickState.AngularVelocityX, DeadZone.AngularVelocityX);
                else
                    return DeadZone.Calc(JoystickState.AngularVelocityX, DeadZone.AngularVelocityX);
            }
        }
        public int AngularVelocityY
        {
            get
            {
                if (InversedAxe.AngularVelocityY)
                    return DeadZone.Calc(-JoystickState.AngularVelocityY, DeadZone.AngularVelocityY);
                else
                    return DeadZone.Calc(JoystickState.AngularVelocityY, DeadZone.AngularVelocityY);
            }
        }
        public int AngularVelocityZ
        {
            get
            {
                if (InversedAxe.AngularVelocityZ)
                    return DeadZone.Calc(-JoystickState.AngularVelocityZ, DeadZone.AngularVelocityZ);
                else
                    return DeadZone.Calc(JoystickState.AngularVelocityZ, DeadZone.AngularVelocityZ);
            }
        }

        public int ForceX
        {
            get
            {
                if (InversedAxe.ForceX)
                    return DeadZone.Calc(-JoystickState.ForceX, DeadZone.ForceX);
                else
                    return DeadZone.Calc(JoystickState.ForceX, DeadZone.ForceX);
            }
        }
        public int ForceY
        {
            get
            {
                if (InversedAxe.ForceY)
                    return DeadZone.Calc(-JoystickState.ForceY, DeadZone.ForceY);
                else
                    return DeadZone.Calc(JoystickState.ForceY, DeadZone.ForceY);
            }
        }
        public int ForceZ
        {
            get
            {
                if (InversedAxe.ForceZ)
                    return DeadZone.Calc(-JoystickState.ForceZ, DeadZone.ForceZ);
                else
                    return DeadZone.Calc(JoystickState.ForceZ, DeadZone.ForceZ);
            }
        }

        public int RotationX
        {
            get
            {
                if (InversedAxe.RotationX)
                    return DeadZone.Calc(-JoystickState.RotationX, DeadZone.RotationX);
                else
                    return DeadZone.Calc(JoystickState.RotationX, DeadZone.RotationX);
            }
        }
        public int RotationY
        {
            get
            {
                if (InversedAxe.RotationY)
                    return DeadZone.Calc(-JoystickState.RotationY, DeadZone.RotationY);
                else
                    return DeadZone.Calc(JoystickState.RotationY, DeadZone.RotationY);
            }
        }
        public int RotationZ
        {
            get
            {
                if (InversedAxe.RotationZ)
                    return DeadZone.Calc(-JoystickState.RotationZ, DeadZone.RotationZ);
                else
                    return DeadZone.Calc(JoystickState.RotationZ, DeadZone.RotationZ);
            }
        }

        public int TorqueX
        {
            get
            {
                if (InversedAxe.TorqueX)
                    return DeadZone.Calc(-JoystickState.TorqueX, DeadZone.TorqueX);
                else
                    return DeadZone.Calc(JoystickState.TorqueX, DeadZone.TorqueX);
            }
        }
        public int TorqueY
        {
            get
            {
                if (InversedAxe.TorqueY)
                    return DeadZone.Calc(-JoystickState.TorqueY, DeadZone.TorqueY);
                else
                    return DeadZone.Calc(JoystickState.TorqueY, DeadZone.TorqueY);
            }
        }
        public int TorqueZ
        {
            get
            {
                if (InversedAxe.TorqueZ)
                    return DeadZone.Calc(-JoystickState.TorqueZ, DeadZone.TorqueZ);
                else
                    return DeadZone.Calc(JoystickState.TorqueZ, DeadZone.TorqueZ);
            }
        }

        public int VelocityX
        {
            get
            {
                if (InversedAxe.VelocityX)
                    return DeadZone.Calc(-JoystickState.VelocityX, DeadZone.VelocityX);
                else
                    return DeadZone.Calc(JoystickState.VelocityX, DeadZone.VelocityX);
            }
        }
        public int VelocityY
        {
            get
            {
                if (InversedAxe.VelocityY)
                    return DeadZone.Calc(-JoystickState.VelocityY, DeadZone.VelocityY);
                else
                    return DeadZone.Calc(JoystickState.VelocityY, DeadZone.VelocityY);
            }
        }
        public int VelocityZ
        {
            get
            {
                if (InversedAxe.VelocityZ)
                    return DeadZone.Calc(-JoystickState.VelocityZ, DeadZone.VelocityZ);
                else
                    return DeadZone.Calc(JoystickState.VelocityZ, DeadZone.VelocityZ);
            }
        }
    }

}
