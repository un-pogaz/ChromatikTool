using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromatik.Noise
{
    public enum NoiseRange
    {
        Byte = 256,
        ZeroToOne = 1,
        nOneToOne = 2,
        ZeroToHundred = 100,
        ZeroToThousand = 1000,
    }

    internal static class InternalNoise
    {
        internal static float ToRange(float input, NoiseRange range)
        {
            switch (range)
            {
                case NoiseRange.Byte:
                    return input * 128 + 128;
                case NoiseRange.ZeroToOne:
                    return input * 0.5f + 1;
                case NoiseRange.ZeroToHundred:
                    return input * 50 + 100;
                case NoiseRange.ZeroToThousand:
                    return input * 500 + 1000;
                default: // SimplexNoiseRange.nOneToOne
                    return input;
            }
        }
    }
}
