using System;
using System.Collections.Generic;
using System.Text;

namespace Chromatik.SlimDX
{

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
}
