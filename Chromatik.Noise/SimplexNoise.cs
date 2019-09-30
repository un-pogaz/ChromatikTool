using System;

namespace Chromatik.Noise
{
    public class SimplexNoise
    {
        public SimplexNoise()
        { }

        public LibNoise.Primitive.SimplexPerlin NoiseGenerator { get; } = new LibNoise.Primitive.SimplexPerlin(0, LibNoise.NoiseQuality.Standard);

        public int Seed
        {
            get { return NoiseGenerator.Seed; }
            set { NoiseGenerator.Seed = value; }
        }
        private int _seed = 0;

        public float ScaleFactor
        {
            get { return _ScaleFactor; }
            set {
                if (value == float.NaN || value == float.NegativeInfinity || value == float.PositiveInfinity)
                    value = 0;
                _ScaleFactor = value;
            }
        }
        private float _ScaleFactor = 0.01f;

        public NoiseRange DefaultNoiseRange { get; set; } = NoiseRange.nOneToOne;

        public float GetValue(float x)
        {
            return GetValue(x, DefaultNoiseRange);
        }
        public float GetValue(float x, NoiseRange noiseRange)
        {
            return InternalNoise.ToRange(NoiseGenerator.GetValue(x * ScaleFactor), noiseRange);
        }

        public float GetValue(float x, float y)
        {
            return GetValue(x, y, DefaultNoiseRange);
        }
        public float GetValue(float x, float y, NoiseRange noiseRange)
        {
            return InternalNoise.ToRange(NoiseGenerator.GetValue(x * ScaleFactor, y * ScaleFactor), noiseRange);
        }
        
        public float GetValue(float x, float y, float z)
        {
            return GetValue(x, y, z, DefaultNoiseRange);
        }
        public float GetValue(float x, float y, float z, NoiseRange noiseRange)
        {
            return InternalNoise.ToRange(NoiseGenerator.GetValue(x * ScaleFactor, y * ScaleFactor, z * ScaleFactor), noiseRange);
        }

        public float GetValue(float x, float y, float z, float w)
        {
            return GetValue(x, y, z, w, DefaultNoiseRange);
        }
        public float GetValue(float x, float y, float z, float w, NoiseRange noiseRange)
        {
            return InternalNoise.ToRange(NoiseGenerator.GetValue(x * ScaleFactor, y * ScaleFactor, z * ScaleFactor, w * ScaleFactor), noiseRange);
        }

        public float[] GetLine(int length)
        {
            float[] rslt = new float[length];
            for (int l = 0; l < length; l++)
                rslt[l] = GetValue(l);
            return rslt;
        }
        public float[,] GetPlane(int width, int height)
        {
            float[,] rslt = new float[width, height];
            for (int w = 0; w < width; w++)
                for (int h = 0; h < height; h++)
                    rslt[w,h] = GetValue(w,h);
            return rslt;
        }
        public float[,,] GetCube(int width, int height, int lenght)
        {
            float[,,] rslt = new float[width, height, lenght];
            for (int w = 0; w < width; w++)
                for (int h = 0; h < height; h++)
                    for (int l = 0; l < lenght; l++)
                        rslt[w,h,l] = GetValue(w,h,l);
            return rslt;
        }
        public float[,,,] GetHypercube(int width, int height, int lenght, int time)
        {
            float[,,,] rslt = new float[width, height, lenght, time];
            for (int w = 0; w < width; w++)
                for (int h = 0; h < height; h++)
                    for (int l = 0; l < lenght; l++)
                        for (int t = 0; t < time; t++)
                            rslt[w,h,l,t] = GetValue(w, h, l, t);
            return rslt;
        }

    }

}
