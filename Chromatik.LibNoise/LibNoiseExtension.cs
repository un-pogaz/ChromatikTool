using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNoise
{
    /// <summary>
    /// Enumerates the noise values range
    /// </summary>
    public enum NoiseRange : int
    {
        /// <summary>
        /// Default range [-1;1]
        /// </summary>
        NegativeOneToPositiveOne = 0,

        /// <summary>
        /// Byte range [0;255]
        /// </summary>
        Byte = 255,
        /// <summary>
        /// Zero to One range [0;1]
        /// </summary>
        ZeroToOne = 1,
        /// <summary>
        /// Zero to hundred range [0;100]
        /// </summary>
        ZeroToHundred = 100,
        /// <summary>
        /// Zero to thousand range [0;1000]
        /// </summary>
        ZeroToThousand = 1000
    }

    /// <summary>
    /// class for extend <see cref="LibNoise"/> modules
    /// </summary>
    public static class LibNoiseExtension
    {
        private const NoiseRange _default = NoiseRange.NegativeOneToPositiveOne;

        /// <summary></summary>
        /// <param name="input"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        static public float ToRange(float input, NoiseRange range)
        {
            float factor = 1;

            switch (range)
            {
                case NoiseRange.Byte:
                    factor = (int)range;
                    break;
                case NoiseRange.ZeroToOne:
                    factor = (int)range;
                    break;
                case NoiseRange.ZeroToHundred:
                    factor = (int)range;
                    break;
                case NoiseRange.ZeroToThousand:
                    factor = (int)range;
                    break;

                default: // SimplexNoiseRange.nOneToOne
                    return input;
            }

            return input * (factor / 2) + (factor / 2);
        }

        #region GetValue(NoiseRange)
        /// <summary>
        /// Generates an output value within a specified range based on the coordinates of the specified input value.
        /// </summary>
        /// <param name="module"><see cref="IModule1D"/> represent the noise generator.</param>
        /// <param name="x">The input coordinate on the x-axis.</param>
        /// <param name="range">Target range to convert the output value.</param>
        /// <returns>The resulting output value in target range.</returns>
        static public float GetValue(this IModule1D module, float x, NoiseRange range)
        {
            return ToRange(module.GetValue(x), range);
        }
        /// <summary>
        /// Generates an output value within a specified range based on the coordinates of the specified input value.
        /// </summary>
        /// <param name="module"><see cref="IModule2D"/> represent the noise generator.</param>
        /// <param name="x">The input coordinate on the x-axis.</param>
        /// <param name="y">The input coordinate on the y-axis.</param>
        /// <param name="range">Target range to convert the output value.</param>
        /// <returns>The resulting output value in target range.</returns>
        static public float GetValue(this IModule2D module, float x, float y, NoiseRange range)
        {
            return ToRange(module.GetValue(x, y), range);
        }
        /// <summary>
        /// Generates an output value within a specified range based on the coordinates of the specified inputs values.
        /// </summary>
        /// <param name="module"><see cref="IModule3D"/> represent the noise generator.</param>
        /// <param name="x">The input coordinate on the x-axis.</param>
        /// <param name="y">The input coordinate on the y-axis.</param>
        /// <param name="z">The input coordinate on the z-axis.</param>
        /// <param name="range">Target range to convert the output value.</param>
        /// <returns>The resulting output value in target range.</returns>
        static public float GetValue(this IModule3D module, float x, float y, float z, NoiseRange range)
        {
            return ToRange(module.GetValue(x, y, z), range);
        }
        /// <summary>
        /// Generates an output value within a specified range based on the coordinates of the specified inputs values.
        /// </summary>
        /// <param name="module"><see cref="IModule4D"/> represent the noise generator.</param>
        /// <param name="x">The input coordinate on the x-axis.</param>
        /// <param name="y">The input coordinate on the y-axis.</param>
        /// <param name="z">The input coordinate on the z-axis.</param>
        /// <param name="w">The input coordinate on the w-axis.</param>
        /// <param name="range">Target range to convert the output value.</param>
        /// <returns>The resulting output value in target range.</returns>
        static public float GetValue(this IModule4D module, float x, float y, float z, float w, NoiseRange range)
        {
            return ToRange(module.GetValue(x, y, z, w), range);
        }
        #endregion

        #region GetLine
        /// <summary>
        /// Generates an line of values.
        /// </summary>
        /// <param name="module"><see cref="IModule1D"/> represent a noise generator.</param>
        /// <param name="lenght">The lenght of the line (x-axis).</param>
        /// <returns>The resulting line.</returns>
        static public float[] GetLine(this IModule1D module, int lenght)
        {
            return module.GetLine(lenght, 1, _default);
        }

        /// <summary>
        /// Generates an line of values.
        /// </summary>
        /// <param name="module"><see cref="IModule1D"/> represent a noise generator.</param>
        /// <param name="lenght">The lenght of the line (x-axis).</param>
        /// <param name="scaleFactor">The scale factor of the line.</param>
        /// <returns>The resulting line in target range.</returns>
        static public float[] GetLine(this IModule1D module, int lenght, float scaleFactor)
        {
            return module.GetLine(lenght, scaleFactor, _default);
        }
        /// <summary>
        /// Generates an line of values within a specified range.
        /// </summary>
        /// <param name="module"><see cref="IModule1D"/> represent a noise generator.</param>
        /// <param name="lenght">The lenght of the line (x-axis).</param>
        /// <param name="range">Target range to convert the line.</param>
        /// <returns>The resulting line in target range.</returns>
        static public float[] GetLine(this IModule1D module, int lenght, NoiseRange range)
        {
            return module.GetLine(lenght, 1, range);
        }
        /// <summary>
        /// Generates an line of values within a specified range value.
        /// </summary>
        /// <param name="module"><see cref="IModule1D"/> represent a noise generator.</param>
        /// <param name="lenght">The lenght of the line (x-axis).</param>
        /// <param name="scaleFactor">The scale factor of the line.</param>
        /// <param name="range">Target range to convert the line.</param>
        /// <returns>The resulting line in target range.</returns>
        static public float[] GetLine(this IModule1D module, int lenght, float scaleFactor, NoiseRange range)
        {
            return module.GetLine(lenght, scaleFactor, range, 0);
        }

        /// <summary>
        /// Generates an line of values within a specified coordinates of the specified start value.
        /// </summary>
        /// <param name="module"><see cref="IModule1D"/> represent a noise generator.</param>
        /// <param name="lenght">The lenght of the line (x-axis).</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <returns>The resulting line in target range.</returns>
        static public float[] GetLine(this IModule1D module, int lenght, int start_x)
        {
            return module.GetLine(lenght, 1, _default, start_x);
        }
        /// <summary>
        /// Generates an line of values within a specified coordinates of the specified start value.
        /// </summary>
        /// <param name="module"><see cref="IModule1D"/> represent a noise generator.</param>
        /// <param name="lenght">The lenght of the line (x-axis).</param>
        /// <param name="scaleFactor">The scale factor of the line.</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <returns>The resulting line in target range.</returns>
        static public float[] GetLine(this IModule1D module, int lenght, float scaleFactor, int start_x)
        {
            return module.GetLine(lenght, scaleFactor, _default, start_x);
        }
        /// <summary>
        /// Generates an line of values within a specified range based on the coordinates of the specified start value.
        /// </summary>
        /// <param name="module"><see cref="IModule1D"/> represent a noise generator.</param>
        /// <param name="lenght">The lenght of the line (x-axis).</param>
        /// <param name="range">Target range to convert the line.</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <returns>The resulting line in target range.</returns>
        static public float[] GetLine(this IModule1D module, int lenght, NoiseRange range, int start_x)
        {
            return module.GetLine(lenght, 1, range, start_x);
        }

        /// <summary>
        /// Generates an line of values within a specified range based on the coordinates of the specified start value.
        /// </summary>
        /// <param name="module"><see cref="IModule1D"/> represent a noise generator.</param>
        /// <param name="lenght">The lenght of the line (x-axis).</param>
        /// <param name="scaleFactor">The scale factor of the line.</param>
        /// <param name="range">Target range to convert the line.</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <returns>The resulting line in target range.</returns>
        static public float[] GetLine(this IModule1D module, int lenght, float scaleFactor, NoiseRange range, int start_x)
        {
            float[] rslt = new float[lenght];
            for (int l = 0; l < lenght; l++)
                rslt[l] = module.GetValue((start_x + l) * scaleFactor, range);

            return rslt;
        }
        #endregion

        #region GetPlane
        /// <summary>
        /// Generates an plan of values.
        /// </summary>
        /// <param name="module"><see cref="IModule2D"/> represent a noise generator.</param>
        /// <param name="width">The width of the plan (x-axis).</param>
        /// <param name="height">The height of the plan (y-axis).</param>
        /// <returns>The resulting plan.</returns>
        static public float[,] GetPlane(this IModule2D module, int width, int height)
        {
            return module.GetPlane(width, height, 1, _default);
        }

        /// <summary>
        /// Generates an plan of values.
        /// </summary>
        /// <param name="module"><see cref="IModule2D"/> represent a noise generator.</param>
        /// <param name="width">The width of the plan (x-axis).</param>
        /// <param name="height">The height of the plan (y-axis).</param>
        /// <param name="scaleFactor">The scale factor of the plan.</param>
        /// <returns>The resulting plan.</returns>
        static public float[,] GetPlane(this IModule2D module, int width, int height, float scaleFactor)
        {
            return module.GetPlane(width, height, scaleFactor, _default);
        }
        /// <summary>
        /// Generates an plan of values within a specified range.
        /// </summary>
        /// <param name="module"><see cref="IModule2D"/> represent a noise generator.</param>
        /// <param name="width">The width of the plan (x-axis).</param>
        /// <param name="height">The height of the plan (y-axis).</param>
        /// <param name="range">Target range to convert the plan.</param>
        /// <returns>The resulting plan in target range.</returns>
        static public float[,] GetPlane(this IModule2D module, int width, int height, NoiseRange range)
        {
            return module.GetPlane(width, height, 1, range);
        }
        /// <summary>
        /// Generates an plan of values within a specified range.
        /// </summary>
        /// <param name="module"><see cref="IModule2D"/> represent a noise generator.</param>
        /// <param name="width">The width of the plan (x-axis).</param>
        /// <param name="height">The height of the plan (y-axis).</param>
        /// <param name="scaleFactor">The scale factor of the plan.</param>
        /// <param name="range">Target range to convert the plan.</param>
        /// <returns>The resulting plan in target range.</returns>
        static public float[,] GetPlane(this IModule2D module, int width, int height, float scaleFactor, NoiseRange range)
        {
            return module.GetPlane(width, height, scaleFactor, range, 0, 0);
        }

        /// <summary>
        /// Generates an plan of values within a specified coordinates of the specified start values.
        /// </summary>
        /// <param name="module"><see cref="IModule2D"/> represent a noise generator.</param>
        /// <param name="width">The width of the plan (x-axis).</param>
        /// <param name="height">The height of the plan (y-axis).</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <param name="start_y">The start coordinate on the y-axis.</param>
        /// <returns>The resulting plan.</returns>
        static public float[,] GetPlane(this IModule2D module, int width, int height, int start_x, int start_y)
        {
            return module.GetPlane(width, height, 1, _default, start_x, start_y);
        }
        /// <summary>
        /// Generates an plan of values within a specified coordinates of the specified start values.
        /// </summary>
        /// <param name="module"><see cref="IModule2D"/> represent a noise generator.</param>
        /// <param name="width">The width of the plan (x-axis).</param>
        /// <param name="height">The height of the plan (y-axis).</param>
        /// <param name="scaleFactor">The scale factor of the plan.</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <param name="start_y">The start coordinate on the y-axis.</param>
        /// <returns>The resulting plan.</returns>
        static public float[,] GetPlane(this IModule2D module, int width, int height, float scaleFactor, int start_x, int start_y)
        {
            return module.GetPlane(width, height, scaleFactor, _default, start_x, start_y);
        }
        /// <summary>
        /// Generates an plan of values within a specified range based on the coordinates of the specified start values.
        /// </summary>
        /// <param name="module"><see cref="IModule2D"/> represent a noise generator.</param>
        /// <param name="width">The width of the plan (x-axis).</param>
        /// <param name="height">The height of the plan (y-axis).</param>
        /// <param name="range">Target range to convert the plan.</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <param name="start_y">The start coordinate on the y-axis.</param>
        /// <returns>The resulting plan in target range.</returns>
        static public float[,] GetPlane(this IModule2D module, int width, int height, NoiseRange range, int start_x, int start_y)
        {
            return module.GetPlane(width, height, 1, range, start_x, start_y);
        }

        /// <summary>
        /// Generates an plan of values within a specified range based on the coordinates of the specified start values.
        /// </summary>
        /// <param name="module"><see cref="IModule2D"/> represent a noise generator.</param>
        /// <param name="width">The width of the plan (x-axis).</param>
        /// <param name="height">The height of the plan (y-axis).</param>
        /// <param name="scaleFactor">The scale factor of the plan.</param>
        /// <param name="range">Target range to convert the plan.</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <param name="start_y">The start coordinate on the y-axis.</param>
        /// <returns>The resulting plan in target range.</returns>
        static public float[,] GetPlane(this IModule2D module, int width, int height, float scaleFactor, NoiseRange range, int start_x, int start_y)
        {
            float[,] rslt = new float[width, height];
            for (int w = 0; w < width; w++)
                for (int h = 0; h < height; h++)
                    rslt[w, h] = module.GetValue((start_x + w) * scaleFactor, (start_y + h) * scaleFactor, range);

            return rslt;
        }
        #endregion

        #region GetCube
        /// <summary>
        /// Generates an cube of values.
        /// <para>Caution: the <see cref="OutOfMemoryException"/> arrives quickly</para>
        /// </summary>
        /// <param name="module"><see cref="IModule3D"/> represent a noise generator.</param>
        /// <param name="width">The width of the cube (x-axis).</param>
        /// <param name="height">The height of the cube (y-axis).</param>
        /// <param name="depth">The depth" of the cube (z-axis).</param>
        /// <returns>The resulting cube.</returns>
        static public float[,,] GetCube(this IModule3D module, int width, int height, int depth)
        {
            return module.GetCube(width, height, depth, 1, _default);
        }

        /// <summary>
        /// Generates an cube of values.
        /// <para>Caution: the <see cref="OutOfMemoryException"/> arrives quickly</para>
        /// </summary>
        /// <param name="module"><see cref="IModule3D"/> represent a noise generator.</param>
        /// <param name="width">The width of the cube (x-axis).</param>
        /// <param name="height">The height of the cube (y-axis).</param>
        /// <param name="depth">The depth" of the cube (z-axis).</param>
        /// <param name="scaleFactor">The scale factor of the cube.</param>
        /// <returns>The resulting cube.</returns>
        static public float[,,] GetCube(this IModule3D module, int width, int height, int depth, float scaleFactor)
        {
            return module.GetCube(width, height, depth, scaleFactor, _default);
        }
        /// <summary>
        /// Generates an cube of values within a specified range.
        /// <para>Caution: the <see cref="OutOfMemoryException"/> arrives quickly</para>
        /// </summary>
        /// <param name="module"><see cref="IModule3D"/> represent a noise generator.</param>
        /// <param name="width">The width of the cube (x-axis).</param>
        /// <param name="height">The height of the cube (y-axis).</param>
        /// <param name="depth">The depth" of the cube (z-axis).</param>
        /// <param name="range">Target range to convert the cube.</param>
        /// <returns>The resulting cube in target range.</returns>
        static public float[,,] GetCube(this IModule3D module, int width, int height, int depth, NoiseRange range)
        {
            return module.GetCube(width, height, depth, 1, range);
        }
        /// <summary>
        /// Generates an cube of values within a specified range.
        /// <para>Caution: the <see cref="OutOfMemoryException"/> arrives quickly</para>
        /// </summary>
        /// <param name="module"><see cref="IModule3D"/> represent a noise generator.</param>
        /// <param name="width">The width of the cube (x-axis).</param>
        /// <param name="height">The height of the cube (y-axis).</param>
        /// <param name="depth">The depth" of the cube (z-axis).</param>
        /// <param name="scaleFactor">The scale factor of the cube.</param>
        /// <param name="range">Target range to convert the cube.</param>
        /// <returns>The resulting cube in target range.</returns>
        static public float[,,] GetCube(this IModule3D module, int width, int height, int depth, float scaleFactor, NoiseRange range)
        {
            return module.GetCube(width, height, depth, scaleFactor, range, 0, 0, 0);
        }

        /// <summary>
        /// Generates an cube of values within a specified coordinates of the specified start values.
        /// <para>Caution: the <see cref="OutOfMemoryException"/> arrives quickly</para>
        /// </summary>
        /// <param name="module"><see cref="IModule3D"/> represent a noise generator.</param>
        /// <param name="width">The width of the cube (x-axis).</param>
        /// <param name="height">The height of the cube (y-axis).</param>
        /// <param name="depth">The depth" of the cube (z-axis).</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <param name="start_y">The start coordinate on the y-axis.</param>
        /// <param name="start_z">The start coordinate on the z-axis.</param>
        /// <returns>The resulting cube.</returns>
        static public float[,,] GetCube(this IModule3D module, int width, int height, int depth, int start_x, int start_y, int start_z)
        {
            return module.GetCube(width, height, depth, 1, _default, start_x, start_y, start_z);
        }
        /// <summary>
        /// Generates an cube of values within a specified coordinates of the specified start values.
        /// <para>Caution: the <see cref="OutOfMemoryException"/> arrives quickly</para>
        /// </summary>
        /// <param name="module"><see cref="IModule3D"/> represent a noise generator.</param>
        /// <param name="width">The width of the cube (x-axis).</param>
        /// <param name="height">The height of the cube (y-axis).</param>
        /// <param name="depth">The depth" of the cube (z-axis).</param>
        /// <param name="scaleFactor">The scale factor of the cube.</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <param name="start_y">The start coordinate on the y-axis.</param>
        /// <param name="start_z">The start coordinate on the z-axis.</param>
        /// <returns>The resulting cube.</returns>
        static public float[,,] GetCube(this IModule3D module, int width, int height, int depth, float scaleFactor, int start_x, int start_y, int start_z)
        {
            return module.GetCube(width, height, depth, scaleFactor, _default, start_x, start_y, start_z);
        }
        /// <summary>
        /// Generates an cube of values within a specified range based on the coordinates of the specified start values.
        /// <para>Caution: the <see cref="OutOfMemoryException"/> arrives quickly</para>
        /// </summary>
        /// <param name="module"><see cref="IModule3D"/> represent a noise generator.</param>
        /// <param name="width">The width of the cube (x-axis).</param>
        /// <param name="height">The height of the cube (y-axis).</param>
        /// <param name="depth">The depth" of the cube (z-axis).</param>
        /// <param name="range">Target range to convert the cube.</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <param name="start_y">The start coordinate on the y-axis.</param>
        /// <param name="start_z">The start coordinate on the z-axis.</param>
        /// <returns>The resulting cube in target range.</returns>
        static public float[,,] GetCube(this IModule3D module, int width, int height, int depth, NoiseRange range, int start_x, int start_y, int start_z)
        {
            return module.GetCube(width, height, depth, 1, range, start_x, start_y, start_z);
        }

        /// <summary>
        /// Generates an cube of values within a specified range based on the coordinates of the specified start values.
        /// <para>Caution: the <see cref="OutOfMemoryException"/> arrives quickly</para>
        /// </summary>
        /// <param name="module"><see cref="IModule3D"/> represent a noise generator.</param>
        /// <param name="width">The width of the cube (x-axis).</param>
        /// <param name="height">The height of the cube (y-axis).</param>
        /// <param name="depth">The depth" of the cube (z-axis).</param>
        /// <param name="scaleFactor">The scale factor of the cube.</param>
        /// <param name="range">Target range to convert the cube.</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <param name="start_y">The start coordinate on the y-axis.</param>
        /// <param name="start_z">The start coordinate on the z-axis.</param>
        /// <returns>The resulting cube in target range.</returns>
        static public float[,,] GetCube(this IModule3D module, int width, int height, int depth, float scaleFactor, NoiseRange range, int start_x, int start_y, int start_z)
        {
            float[,,] rslt = new float[width, height, depth];
            for (int w = 0; w < width; w++)
                for (int h = 0; h < height; h++)
                    for (int d = 0; d < depth; d++)
                            rslt[w, h, d] = module.GetValue((start_x + w) * scaleFactor, (start_y + h) * scaleFactor, (start_z + d) * scaleFactor, range);

            return rslt;
        }
        #endregion

        #region GetHypercube
        /// <summary>
        /// <para>Warning: easy way to get a <see cref="OutOfMemoryException"/></para>
        /// Generates an hypercube of values.
        /// </summary>
        /// <param name="module"><see cref="IModule4D"/> represent the noise generator.</param>
        /// <param name="width">The width of the hypercube (x-axis).</param>
        /// <param name="height">The height of the hypercube (y-axis).</param>
        /// <param name="depth">The depth" of the hypercube (z-axis).</param>
        /// <param name="time">The time of the hypercube (w-axis).</param>
        /// <returns>The resulting hypercube.</returns>
        static public float[,,,] GetHypercube(this IModule4D module, int width, int height, int depth, int time)
        {
            return module.GetHypercube(width, height, depth, time, 1, _default);
        }

        /// <summary>
        /// <para>Warning: easy way to get a <see cref="OutOfMemoryException"/></para>
        /// Generates an hypercube of values.
        /// </summary>
        /// <param name="module"><see cref="IModule4D"/> represent the noise generator.</param>
        /// <param name="width">The width of the hypercube (x-axis).</param>
        /// <param name="height">The height of the hypercube (y-axis).</param>
        /// <param name="depth">The depth" of the hypercube (z-axis).</param>
        /// <param name="time">The time of the hypercube (w-axis).</param>
        /// <param name="scaleFactor">The scale factor of the hypercube.</param>
        /// <returns>The resulting hypercube.</returns>
        static public float[,,,] GetHypercube(this IModule4D module, int width, int height, int depth, int time, float scaleFactor)
        {
            return module.GetHypercube(width, height, depth, time, scaleFactor, _default);
        }
        /// <summary>
        /// <para>Warning: easy way to get a <see cref="OutOfMemoryException"/></para>
        /// Generates an hypercube of values within a specified range.
        /// </summary>
        /// <param name="module"><see cref="IModule4D"/> represent the noise generator.</param>
        /// <param name="width">The width of the hypercube (x-axis).</param>
        /// <param name="height">The height of the hypercube (y-axis).</param>
        /// <param name="depth">The depth" of the hypercube (z-axis).</param>
        /// <param name="time">The time of the hypercube (w-axis).</param>
        /// <param name="range">Target range to convert the hypercube.</param>
        /// <returns>The resulting hypercube in target range.</returns>
        static public float[,,,] GetHypercube(this IModule4D module, int width, int height, int depth, int time, NoiseRange range)
        {
            return module.GetHypercube(width, height, depth, time, 1, range);
        }
        /// <summary>
        /// <para>Warning: easy way to get a <see cref="OutOfMemoryException"/></para>
        /// Generates an hypercube of values within a specified range.
        /// </summary>
        /// <param name="module"><see cref="IModule4D"/> represent the noise generator.</param>
        /// <param name="width">The width of the hypercube (x-axis).</param>
        /// <param name="height">The height of the hypercube (y-axis).</param>
        /// <param name="depth">The depth" of the hypercube (z-axis).</param>
        /// <param name="time">The time of the hypercube (w-axis).</param>
        /// <param name="scaleFactor">The scale factor of the hypercube.</param>
        /// <param name="range">Target range to convert the hypercube.</param>
        /// <returns>The resulting hypercube in target range.</returns>
        static public float[,,,] GetHypercube(this IModule4D module, int width, int height, int depth, int time, float scaleFactor, NoiseRange range)
        {
            return module.GetHypercube(width, height, depth, time, scaleFactor, range, 0, 0, 0, 0);
        }

        /// <summary>
        /// <para>Warning: easy way to get a <see cref="OutOfMemoryException"/></para>
        /// Generates an hypercube of values within a specified coordinates of the specified start values.
        /// </summary>
        /// <param name="module"><see cref="IModule4D"/> represent the noise generator.</param>
        /// <param name="width">The width of the hypercube (x-axis).</param>
        /// <param name="height">The height of the hypercube (y-axis).</param>
        /// <param name="depth">The depth" of the hypercube (z-axis).</param>
        /// <param name="time">The time of the hypercube (w-axis).</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <param name="start_y">The start coordinate on the y-axis.</param>
        /// <param name="start_z">The start coordinate on the z-axis.</param>
        /// <param name="start_w">The start coordinate on the w-axis.</param>
        /// <returns>The resulting hypercube.</returns>
        static public float[,,,] GetHypercube(this IModule4D module, int width, int height, int depth, int time, int start_x, int start_y, int start_z, int start_w)
        {
            return module.GetHypercube(width, height, depth, time, 1, _default, start_x, start_y, start_z, start_w);
        }
        /// <summary>
        /// <para>Warning: easy way to get a <see cref="OutOfMemoryException"/></para>
        /// Generates an hypercube of values within a specified coordinates of the specified start values.
        /// </summary>
        /// <param name="module"><see cref="IModule4D"/> represent the noise generator.</param>
        /// <param name="width">The width of the hypercube (x-axis).</param>
        /// <param name="height">The height of the hypercube (y-axis).</param>
        /// <param name="depth">The depth" of the hypercube (z-axis).</param>
        /// <param name="time">The time of the hypercube (w-axis).</param>
        /// <param name="scaleFactor">The scale factor of the hypercube.</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <param name="start_y">The start coordinate on the y-axis.</param>
        /// <param name="start_z">The start coordinate on the z-axis.</param>
        /// <param name="start_w">The start coordinate on the w-axis.</param>
        /// <returns>The resulting hypercube.</returns>
        static public float[,,,] GetHypercube(this IModule4D module, int width, int height, int depth, int time, float scaleFactor, int start_x, int start_y, int start_z, int start_w)
        {
            return module.GetHypercube(width, height, depth, time, scaleFactor, _default, start_x, start_y, start_z, start_w);
        }
        /// <summary>
        /// <para>Warning: easy way to get a <see cref="OutOfMemoryException"/></para>
        /// Generates an hypercube of values within a specified range based on the coordinates of the specified start values.
        /// </summary>
        /// <param name="module"><see cref="IModule4D"/> represent the noise generator.</param>
        /// <param name="width">The width of the hypercube (x-axis).</param>
        /// <param name="height">The height of the hypercube (y-axis).</param>
        /// <param name="depth">The depth" of the hypercube (z-axis).</param>
        /// <param name="time">The time of the hypercube (w-axis).</param>
        /// <param name="range">Target range to convert the hypercube.</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <param name="start_y">The start coordinate on the y-axis.</param>
        /// <param name="start_z">The start coordinate on the z-axis.</param>
        /// <param name="start_w">The start coordinate on the w-axis.</param>
        /// <returns>The resulting hypercube in target range.</returns>
        static public float[,,,] GetHypercube(this IModule4D module, int width, int height, int depth, int time, NoiseRange range, int start_x, int start_y, int start_z, int start_w)
        {
            return module.GetHypercube(width, height, depth, time, 1, range, start_x, start_y, start_z, start_w);
        }
        /// <summary>
        /// <para>Warning: easy way to get a <see cref="OutOfMemoryException"/></para>
        /// Generates an hypercube of values within a specified range based on the coordinates of the specified start values.
        /// </summary>
        /// <param name="module"><see cref="IModule4D"/> represent the noise generator.</param>
        /// <param name="width">The width of the hypercube (x-axis).</param>
        /// <param name="height">The height of the hypercube (y-axis).</param>
        /// <param name="depth">The depth" of the hypercube (z-axis).</param>
        /// <param name="time">The time of the hypercube (w-axis).</param>
        /// <param name="scaleFactor">The scale factor of the hypercube.</param>
        /// <param name="range">Target range to convert the hypercube.</param>
        /// <param name="start_x">The start coordinate on the x-axis.</param>
        /// <param name="start_y">The start coordinate on the y-axis.</param>
        /// <param name="start_z">The start coordinate on the z-axis.</param>
        /// <param name="start_w">The start coordinate on the w-axis.</param>
        /// <returns>The resulting hypercube in target range.</returns>
        static public float[,,,] GetHypercube(this IModule4D module, int width, int height, int depth, int time, float scaleFactor, NoiseRange range, int start_x, int start_y, int start_z, int start_w)
        {
            float[,,,] rslt = new float[width, height, depth, time];
            for (int w = 0; w < width; w++)
                for (int h = 0; h < height; h++)
                    for (int d = 0; d < depth; d++)
                        for (int t = 0; t < time; t++)
                            rslt[w,h,d,t] = module.GetValue((start_x + w) * scaleFactor, (start_y + h) * scaleFactor, (start_z + d) * scaleFactor, (start_w + t) * scaleFactor, range);
            
            return rslt;
        }
        #endregion
    }
}
