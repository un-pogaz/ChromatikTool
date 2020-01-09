using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// Event class for change or editing of a <see cref="string"/> proprety.
    /// </summary>
    public class StringChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Previous string before the event.
        /// </summary>
        public string OldString { get { return _oldString; } }
        string _oldString;
        /// <summary>
        /// New string calling the event.
        /// </summary>
        public string NewString { get { return _newString; } }
        string _newString;
        /// <summary>
        /// New string to apply at the event.
        /// </summary>
        public string ApplyString { get; set; }

        /// <summary>
        /// Create a <see cref="StringChangedEventArgs"/> for change or editing of a <see cref="string"/> proprety.
        /// </summary>
        /// <param name="oldString"></param>
        /// <param name="newString"></param>
        public StringChangedEventArgs(string oldString, string newString) : this(oldString, newString, newString)
        { }
        /// <summary>
        /// Create a <see cref="StringChangedEventArgs"/> for change or editing of a <see cref="string"/> proprety.
        /// </summary>
        /// <param name="oldString"></param>
        /// <param name="newString"></param>
        /// <param name="applyString"></param>
        public StringChangedEventArgs(string oldString, string newString, string applyString)
        {
            _oldString = oldString;
            _newString = newString;
            ApplyString = applyString;
        }
    }
}
