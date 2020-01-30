using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chromatik
{
    public enum CSSunitEnum
    {
        //Relative
        /// <summary>Font size</summary>
        [EnumStringValue("em")]
        EM,
        /// <summary>Font size of root element</summary>
        [EnumStringValue("rem")]
        REM,
        /// <summary>Percent of the parent element</summary>
        [EnumStringValue("%")]
        Percent,

        //Absolute
        /// <summary>Pixel</summary>
        [EnumStringValue("px")]
        Pixel,
        /// <summary>Centimeter</summary>
        [EnumStringValue("cm")]
        Centimeter,
        /// <summary>Millimeter</summary>
        [EnumStringValue("mm")]
        Millimeter,
        /// <summary>Inch</summary>
        [EnumStringValue("in")]
        Inch,
        /// <summary>Point</summary>
        [EnumStringValue("pt")]
        Point,
        /// <summary>Pica</summary>
        [EnumStringValue("pc")]
        Pica,
    }
    
    public struct CSSvalue : ICloneable
    {
        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                if (Unit == CSSunitEnum.Pixel)
                    _value = Math.Round(value, 0, MidpointRounding.AwayFromZero);
                else
                    _value = Math.Round(value, 3, MidpointRounding.AwayFromZero);
            }
        }
        public CSSunitEnum Unit { get; }

        static CSSvalue() { }

        public CSSvalue(double value, CSSunitEnum unit)
        {
            _value = 0;

            Unit = unit;
            Value = value;
        }
        public CSSvalue(CSSvalue value) : this(value.Value, value.Unit) { }

        public CSSvalue Clone() { return new CSSvalue(this); }
        object ICloneable.Clone() { return Clone(); }

        /// <summary></summary>
        public override string ToString()
        {
            string rslt;
            if (Value == 0)
                rslt = 0.ToString(System.Globalization.CultureInfo.InvariantCulture);
            else
                rslt = Value.ToString(System.Globalization.CultureInfo.InvariantCulture).TrimEnd('0', '.', ',') + EnumStringValueAttribute.GetFrom(Unit);

            return rslt;
        }
    }
}
