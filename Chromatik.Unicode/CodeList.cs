using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromatik;

namespace Chromatik.Unicode
{
    /// <summary>
    /// Represents a list of Block
    /// </summary>
    public class CodeBlockList : Dictionary<string, CodeBlock>
    {
        public CodeBlock this[int index]
        {
            get {
                if (index < 0 || index >= this.Count)
                    throw new IndexOutOfRangeException();

                int i = 0;
                foreach (string item in this.Keys)
                {
                    if (i == index)
                        return this[item];

                    i++;
                }
                return null;
            }
            set {
                if (index < 0 || index >= this.Count)
                    throw new IndexOutOfRangeException();
                
                int i = 0;
                foreach (string item in this.Keys)
                {
                    if (i == index)
                    {
                        this[item] = value;
                        break;
                    }
                    i++;
                }
            }
        }
        new public IEnumerator<CodeBlock> GetEnumerator()
        {
            foreach (CodeBlock item in this.Values)
                yield return item;
        }

        public void Add(CodeBlock codeBlock) { this.Add(codeBlock.Name, codeBlock); }

    }
    
    /// <summary>
    /// Represents a liste of Plane
    /// </summary>
    public class CodePlaneList : Dictionary<string, CodePlane>
    {
        public CodePlane this[int index]
        {
            get {
                if (index < 0 || index >= this.Count)
                    throw new IndexOutOfRangeException();

                int i = 0;
                foreach (CodePlane item in this.Values)
                {
                    if (i == index)
                        return item;
                    i++;
                }
                return null;
            }
            set {
                if (index < 0 || index >= this.Count)
                    throw new IndexOutOfRangeException();

                int i = 0;
                foreach (string item in this.Keys)
                {
                    if (i == index)
                    {
                        this[item] = value;
                        break;
                    }
                    i++;
                }
            }
        }
        new public IEnumerator<CodePlane> GetEnumerator()
        {
            foreach (CodePlane item in this.Values)
                yield return item;
        }

        public void Add(CodePlane codePlane) { this.Add(codePlane.Name, codePlane); }
        
    }


}
