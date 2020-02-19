using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Globalization.Localization
{
    public class XliffIdentified
    {
        public string ID
        {
            get { return _id; }
            set
            {
                StringChangedEventArgs arg = new StringChangedEventArgs(_id, value);
                if (value.IsNullOrWhiteSpace())
                    value = null;
                else
                    value = XliffStatic.ConvertValideID(value);

                arg.ApplyString = value;
                RaiseIDChanged(arg);
                _id = arg.ApplyString;
            }
        }
        string _id;

        // Declare the delegate.
        /// <summary></summary>
        public delegate void IDChangedHandler(object sender, StringChangedEventArgs e);

        // Declare the event.
        /// <summary></summary>
        public event IDChangedHandler IDchanged;
        
        /// <summary>
        /// Raise the <see cref="IDchanged"/> event wiht the specified argument
        /// </summary>
        /// <param name="args"></param>
        protected virtual void RaiseIDChanged(StringChangedEventArgs args)
        {
            // Raise the event in a thread-safe manner using the ?. operator.
            IDchanged?.Invoke(this, args);
        }
    }
}
