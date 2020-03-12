using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Reflection
{
    /// <summary>
    /// The AssemblyHelper obtains the information of Assemblies.
    /// </summary>
    public static class AssemblyExtension
    {
        /// <summary>
        /// Gets the title of the assembly.
        /// </summary>
        /// <param name="assembly">The length of the new array.</param>
        /// <returns>The assembly title.</returns>
        public static string GetTitle(this Assembly assembly)
        { // Get all Title attributes on this assembly
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            // If there is at least one Title attribute
            if (attributes.Length > 0)
            {
                // Select the first one
                
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                // If it is not an empty string, return it
                if (titleAttribute.Title != "")
                    return titleAttribute.Title;
            }
            // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
            return System.IO.Path.GetFileNameWithoutExtension(assembly.CodeBase);
        }

        /// <summary>
        /// Get the Stream of the specified Resource in the assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public IO.Stream GetManifestResourceStream(this Assembly assembly, params string[] name)
        {
            return assembly.GetManifestResourceStream(name.Join("."));
        }
        /// <summary>
        /// Get the string of the specified Resource in the assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public string GetManifestResourceString(this Assembly assembly, params string[] name)
        {
            return assembly.GetManifestResourceString(Encoding.UTF8, name);
        }
        /// <summary>
        /// Get the string of the specified Resource in the assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="name"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        static public string GetManifestResourceString(this Assembly assembly, Encoding encoding, params string[] name)
        {
            IO.Stream stream = assembly.GetManifestResourceStream(name);
            if (stream != null)
                using (IO.StreamReader reader = new IO.StreamReader(stream, encoding, true, 2048))
                    return reader.ReadToEnd();

            return null;
        }

        /// <summary>
        /// Get the Info of the specified Resource in the assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public ManifestResourceInfo GetManifestResourceInfo(this Assembly assembly, params string[] name)
        {
            return assembly.GetManifestResourceInfo(name.Join("."));
        }
        
        /// <summary>
        /// Get the name of the Assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        static public string GetAssemblyName(this Assembly assembly)
        {
            return assembly.GetName().Name;
        }

    }
}
