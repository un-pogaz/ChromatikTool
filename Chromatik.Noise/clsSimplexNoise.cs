﻿// Simplex Noise for C#
// Copyright © Benjamin Ward 2019
// See LICENSE
// Simplex Noise implementation offering 1D, 2D, and 3D forms w/ values in the range of 0 to 255.
// Based on work by Heikki Törmälä (2012) and Stefan Gustavson (2006).

using System;

namespace Chromatik.Noise
{
    /// <summary>
    /// Implementation of the Perlin simplex noise, an improved Perlin noise algorithm.
    /// Based loosely on SimplexNoise1234 by Stefan Gustavson: http://staffwww.itn.liu.se/~stegu/aqsis/aqsis-newnoise/
    /// </summary>
    public static class _clsSimplexNoise
    {
        /// <summary>
        /// Start the instance with the Seed 0
        /// </summary>
        static _clsSimplexNoise()
        {
            Seed = 0;
        }
        
        /// <summary>
        /// Seed of the noise
        /// </summary>
        public static int Seed
        {
            get { return _seed; }
            set
            {
                perm = new byte[512];
                var random = new Random(value);
                random.NextBytes(perm);

                _seed = value;
            }
        }
        private static int _seed;

        private static byte[] perm;

        /// <summary>
        /// Get a line of noise
        /// </summary>
        /// <param name="length">Lenght of the line</param>
        /// <param name="scale">Scale factor</param>
        /// <returns>The result values are between 0 and 255 (<see cref="byte"/> range).</returns>
        public static float[] Calc1D(int length, float scale)
        {
            var values = new float[length];
            for (var i = 0; i < length; i++)
                values[i] = CalcPixel1D(i, scale);
            return values;
        }
        /// <summary>
        /// Get a plane of noise
        /// </summary>
        /// <param name="width">Width of the plane</param>
        /// <param name="height">Height of the plane</param>
        /// <param name="scale">Scale factor</param>
        /// <returns>The result values are between 0 and 255 (<see cref="byte"/> range).</returns>
        public static float[,] Calc2D(int width, int height, float scale)
        {
            var values = new float[width, height];
            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                    values[i, j] = CalcPixel2D(i, j, scale);
            return values;
        }
        /// <summary>
        /// Get a cube of noise
        /// </summary>
        /// <param name="width">Width of the cube</param>
        /// <param name="height">Height of the cube</param>
        /// <param name="length">Length of the cube</param>
        /// <param name="scale">Scale factor</param>
        /// <returns>The result values are between 0 and 255 (<see cref="byte"/> range).</returns>
        public static float[,,] Calc3D(int width, int height, int length, float scale)
        {
            var values = new float[width, height, length];
            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                    for (var k = 0; k < length; k++)
                        values[i, j, k] = CalcPixel3D(i, j , k , scale);
            return values;
        }

        /// <summary>
        /// Calculate the noise value for a X coordinate
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="scale">Scale factor</param>
        /// <returns>The result values are between 0 and 255 (<see cref="byte"/> range).</returns>
        public static float CalcPixel1D(int x, float scale)
        {
            return ToRange(Generate(x * scale));
        }
        /// <summary>
        /// Calculate the noise value for a X;Y coordinate
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="scale">Scale factor</param>
        /// <returns>The result values are between 0 and 255 (<see cref="byte"/> range).</returns>
        public static float CalcPixel2D(int x, int y, float scale)
        {
            return ToRange(Generate(x * scale, y * scale));
        }
        /// <summary>
        /// Calculate the noise value for a X;Y;Z coordinate
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        /// <param name="scale">Scale factor</param>
        /// <returns>The result values are between 0 and 255 (<see cref="byte"/> range).</returns>
        public static float CalcPixel3D(int x, int y, int z, float scale)
        {
            return ToRange(Generate(x * scale, y * scale, z * scale)) ;
        }


        private static float ToRange(float input)
        {
            return ToRange(input, SimplexNoiseRange.Byte);
        }
        private static float ToRange(float input, SimplexNoiseRange range)
        {
            switch (range)
            {
                case SimplexNoiseRange.Byte:
                    return input * 128 + 128;
                case SimplexNoiseRange.ZeroToOne:
                    return input * 0.5f + 1;
                case SimplexNoiseRange.ZeroToHundred:
                    return input * 50 + 100;
                default: // SimplexNoiseRange.nOneToOne
                    return input;
            }
        }


        /// <summary>
        /// 1D simplex noise
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <returns>The result values are between -1 and 1 [-1,1].</returns>
        private static float Generate(float x)
        {
            var i0 = FastFloor(x);
            var i1 = i0 + 1;
            var x0 = x - i0;
            var x1 = x0 - 1.0f;

            var t0 = 1.0f - x0 * x0;
            t0 *= t0;
            var n0 = t0 * t0 * Grad(perm[i0 & 0xff], x0);

            var t1 = 1.0f - x1 * x1;
            t1 *= t1;
            var n1 = t1 * t1 * Grad(perm[i1 & 0xff], x1);
            // The maximum value of this noise is 8*(3/4)^4 = 2.53125
            // A factor of 0.395 scales to fit exactly within [-1,1]
            return 0.395f * (n0 + n1);
        }

        /// <summary>
        /// 2D simplex noise
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>The result values are between -1 and 1 [-1,1].</returns>
        private static float Generate(float x, float y)
        {
            const float F2 = 0.366025403f; // F2 = 0.5*(sqrt(3.0)-1.0)
            const float G2 = 0.211324865f; // G2 = (3.0-Math.sqrt(3.0))/6.0

            float n0, n1, n2; // Noise contributions from the three corners

            // Skew the input space to determine which simplex cell we're in
            var s = (x + y) * F2; // Hairy factor for 2D
            var xs = x + s;
            var ys = y + s;
            var i = FastFloor(xs);
            var j = FastFloor(ys);

            var t = (i + j) * G2;
            var X0 = i - t; // Unskew the cell origin back to (x,y) space
            var Y0 = j - t;
            var x0 = x - X0; // The x,y distances from the cell origin
            var y0 = y - Y0;

            // For the 2D case, the simplex shape is an equilateral triangle.
            // Determine which simplex we are in.
            int i1, j1; // Offsets for second (middle) corner of simplex in (i,j) coords
            if (x0 > y0) { i1 = 1; j1 = 0; } // lower triangle, XY order: (0,0)->(1,0)->(1,1)
            else { i1 = 0; j1 = 1; }      // upper triangle, YX order: (0,0)->(0,1)->(1,1)

            // A step of (1,0) in (i,j) means a step of (1-c,-c) in (x,y), and
            // a step of (0,1) in (i,j) means a step of (-c,1-c) in (x,y), where
            // c = (3-sqrt(3))/6

            var x1 = x0 - i1 + G2; // Offsets for middle corner in (x,y) unskewed coords
            var y1 = y0 - j1 + G2;
            var x2 = x0 - 1.0f + 2.0f * G2; // Offsets for last corner in (x,y) unskewed coords
            var y2 = y0 - 1.0f + 2.0f * G2;

            // Wrap the integer indices at 256, to avoid indexing perm[] out of bounds
            var ii = Mod(i, 256);
            var jj = Mod(j, 256);

            // Calculate the contribution from the three corners
            var t0 = 0.5f - x0 * x0 - y0 * y0;
            if (t0 < 0.0f) n0 = 0.0f;
            else
            {
                t0 *= t0;
                n0 = t0 * t0 * Grad(perm[ii + perm[jj]], x0, y0);
            }

            var t1 = 0.5f - x1 * x1 - y1 * y1;
            if (t1 < 0.0f) n1 = 0.0f;
            else
            {
                t1 *= t1;
                n1 = t1 * t1 * Grad(perm[ii + i1 + perm[jj + j1]], x1, y1);
            }

            var t2 = 0.5f - x2 * x2 - y2 * y2;
            if (t2 < 0.0f) n2 = 0.0f;
            else
            {
                t2 *= t2;
                n2 = t2 * t2 * Grad(perm[ii + 1 + perm[jj + 1]], x2, y2);
            }

            // Add contributions from each corner to get the final noise value.
            // The result is scaled to return values in the interval [-1,1].
            return 40.0f * (n0 + n1 + n2); // TODO: The scale factor is preliminary!
        }

        /// <summary>
        /// 3D simplex noise
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        /// <returns>The result values are between -1 and 1 [-1,1].</returns>
        private static float Generate(float x, float y, float z)
        {
            // Simple skewing factors for the 3D case
            const float F3 = 0.333333333f;
            const float G3 = 0.166666667f;

            float n0, n1, n2, n3; // Noise contributions from the four corners

            // Skew the input space to determine which simplex cell we're in
            var s = (x + y + z) * F3; // Very nice and simple skew factor for 3D
            var xs = x + s;
            var ys = y + s;
            var zs = z + s;
            var i = FastFloor(xs);
            var j = FastFloor(ys);
            var k = FastFloor(zs);

            var t = (i + j + k) * G3;
            var X0 = i - t; // Unskew the cell origin back to (x,y,z) space
            var Y0 = j - t;
            var Z0 = k - t;
            var x0 = x - X0; // The x,y,z distances from the cell origin
            var y0 = y - Y0;
            var z0 = z - Z0;

            // For the 3D case, the simplex shape is a slightly irregular tetrahedron.
            // Determine which simplex we are in.
            int i1, j1, k1; // Offsets for second corner of simplex in (i,j,k) coords
            int i2, j2, k2; // Offsets for third corner of simplex in (i,j,k) coords

            /* This code would benefit from a backport from the GLSL version! */
            if (x0 >= y0)
            {
                if (y0 >= z0)
                { i1 = 1; j1 = 0; k1 = 0; i2 = 1; j2 = 1; k2 = 0; } // X Y Z order
                else if (x0 >= z0) { i1 = 1; j1 = 0; k1 = 0; i2 = 1; j2 = 0; k2 = 1; } // X Z Y order
                else { i1 = 0; j1 = 0; k1 = 1; i2 = 1; j2 = 0; k2 = 1; } // Z X Y order
            }
            else
            { // x0<y0
                if (y0 < z0) { i1 = 0; j1 = 0; k1 = 1; i2 = 0; j2 = 1; k2 = 1; } // Z Y X order
                else if (x0 < z0) { i1 = 0; j1 = 1; k1 = 0; i2 = 0; j2 = 1; k2 = 1; } // Y Z X order
                else { i1 = 0; j1 = 1; k1 = 0; i2 = 1; j2 = 1; k2 = 0; } // Y X Z order
            }

            // A step of (1,0,0) in (i,j,k) means a step of (1-c,-c,-c) in (x,y,z),
            // a step of (0,1,0) in (i,j,k) means a step of (-c,1-c,-c) in (x,y,z), and
            // a step of (0,0,1) in (i,j,k) means a step of (-c,-c,1-c) in (x,y,z), where
            // c = 1/6.

            var x1 = x0 - i1 + G3; // Offsets for second corner in (x,y,z) coords
            var y1 = y0 - j1 + G3;
            var z1 = z0 - k1 + G3;
            var x2 = x0 - i2 + 2.0f * G3; // Offsets for third corner in (x,y,z) coords
            var y2 = y0 - j2 + 2.0f * G3;
            var z2 = z0 - k2 + 2.0f * G3;
            var x3 = x0 - 1.0f + 3.0f * G3; // Offsets for last corner in (x,y,z) coords
            var y3 = y0 - 1.0f + 3.0f * G3;
            var z3 = z0 - 1.0f + 3.0f * G3;

            // Wrap the integer indices at 256, to avoid indexing perm[] out of bounds
            var ii = Mod(i, 256);
            var jj = Mod(j, 256);
            var kk = Mod(k, 256);

            // Calculate the contribution from the four corners
            var t0 = 0.6f - x0 * x0 - y0 * y0 - z0 * z0;
            if (t0 < 0.0f) n0 = 0.0f;
            else
            {
                t0 *= t0;
                n0 = t0 * t0 * Grad(perm[ii + perm[jj + perm[kk]]], x0, y0, z0);
            }

            var t1 = 0.6f - x1 * x1 - y1 * y1 - z1 * z1;
            if (t1 < 0.0f) n1 = 0.0f;
            else
            {
                t1 *= t1;
                n1 = t1 * t1 * Grad(perm[ii + i1 + perm[jj + j1 + perm[kk + k1]]], x1, y1, z1);
            }

            var t2 = 0.6f - x2 * x2 - y2 * y2 - z2 * z2;
            if (t2 < 0.0f) n2 = 0.0f;
            else
            {
                t2 *= t2;
                n2 = t2 * t2 * Grad(perm[ii + i2 + perm[jj + j2 + perm[kk + k2]]], x2, y2, z2);
            }

            var t3 = 0.6f - x3 * x3 - y3 * y3 - z3 * z3;
            if (t3 < 0.0f) n3 = 0.0f;
            else
            {
                t3 *= t3;
                n3 = t3 * t3 * Grad(perm[ii + 1 + perm[jj + 1 + perm[kk + 1]]], x3, y3, z3);
            }

            // Add contributions from each corner to get the final noise value.
            // The result is scaled to stay just inside [-1,1]
            return 32.0f * (n0 + n1 + n2 + n3); // TODO: The scale factor is preliminary!
        }

        

        private static int FastFloor(float x)
        {
            return (x > 0) ? ((int)x) : (((int)x) - 1);
        }

        private static int Mod(int x, int m)
        {
            var a = x % m;
            return a < 0 ? a + m : a;
        }

        private static float Grad(int hash, float x)
        {
            var h = hash & 15;
            var grad = 1.0f + (h & 7);   // Gradient value 1.0, 2.0, ..., 8.0
            if ((h & 8) != 0) grad = -grad;         // Set a random sign for the gradient
            return (grad * x);           // Multiply the gradient with the distance
        }

        private static float Grad(int hash, float x, float y)
        {
            var h = hash & 7;      // Convert low 3 bits of hash code
            var u = h < 4 ? x : y;  // into 8 simple gradient directions,
            var v = h < 4 ? y : x;  // and compute the dot product with (x,y).
            return ((h & 1) != 0 ? -u : u) + ((h & 2) != 0 ? -2.0f * v : 2.0f * v);
        }

        private static float Grad(int hash, float x, float y, float z)
        {
            var h = hash & 15;     // Convert low 4 bits of hash code into 12 simple
            var u = h < 8 ? x : y; // gradient directions, and compute dot product.
            var v = h < 4 ? y : h == 12 || h == 14 ? x : z; // Fix repeats at h = 12 to 15
            return ((h & 1) != 0 ? -u : u) + ((h & 2) != 0 ? -v : v);
        }

        private static float Grad(int hash, float x, float y, float z, float t)
        {
            var h = hash & 31;      // Convert low 5 bits of hash code into 32 simple
            var u = h < 24 ? x : y; // gradient directions, and compute dot product.
            var v = h < 16 ? y : z;
            var w = h < 8 ? z : t;
            return ((h & 1) != 0 ? -u : u) + ((h & 2) != 0 ? -v : v) + ((h & 4) != 0 ? -w : w);
        }
    }

    enum SimplexNoiseRange
    {
        Byte = 256,
        ZeroToOne = 1,
        nOneToOne = 2,
        ZeroToHundred = 100,
    }
}
