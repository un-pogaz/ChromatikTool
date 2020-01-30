using System;
using System.Collections.Generic;
using System.Text;

namespace Chromatik
{
    public class CSSproperties
    {

    }
    

    public class CSSpropertiesPack : ICloneable
    {
        public CSSpropertiesPack()
        {

        }


        public CSSpropertiesPack Clone()
        {
            return new CSSpropertiesPack();
        }
        object ICloneable.Clone() { return Clone(); }
    }
}
