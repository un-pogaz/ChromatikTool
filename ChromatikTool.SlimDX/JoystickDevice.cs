using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX.DirectInput;

namespace Chromatik.SlimDX
{
    /// <summary>
    /// Static class for use and load the connected joysticks 
    /// </summary>
    static public class JoystickDevice
    {
        /// <summary>
        /// Maximum value for analog inputs (Axes...)
        /// </summary>
        static public ushort Range { get; } = 1000;
        /// <summary>
        /// List of all device connected
        /// </summary>
        static public DirectInput Inputs { get; } = new DirectInput();
        /// <summary>
        /// List of joysticks connected
        /// </summary>
        static public Joystick[] Sticks { get; } = GetJoysticks();

        /// <summary>
        /// 
        /// </summary>
        static JoystickDevice()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static Joystick[] GetJoysticks()
        {
            IList<Joystick> rslt = new List<Joystick>();

            // Parcours les péripherique de type Joystick
            foreach (var deviceInstance in Inputs.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly))
            {
                try
                {
                    Joystick stick = new Joystick(Inputs, deviceInstance.InstanceGuid);
                    stick.Acquire();

                    foreach (var deviceObject in stick.GetObjects())
                    {
                        // Definit un axe dans l'instance Joystick
                        if ((deviceObject.ObjectType & ObjectDeviceType.AbsoluteAxis) != 0)
                            stick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-Range, Range);

                        // Definit un axe dans l'instance Joystick
                        if ((deviceObject.ObjectType & ObjectDeviceType.RelativeAxis) != 0)
                            stick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-Range, Range);

                        // Definit un button dans l'instance Joystick
                        if ((deviceObject.ObjectType & ObjectDeviceType.PushButton) != 0)
                            stick.GetObjectPropertiesById((int)deviceObject.ObjectType);

                        // Definit un button dans l'instance Joystick
                        if ((deviceObject.ObjectType & ObjectDeviceType.ToggleButton) != 0)
                            stick.GetObjectPropertiesById((int)deviceObject.ObjectType);
                    }

                    rslt.Add(stick);
                }
                catch (DirectInputException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return rslt.ToArray();
        }
    }

}
