using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System.Collections.ObjectModel
{
    /// <summary>
    /// Represents a read-only collection where the content is based on another collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    public class AutoRefreshReadOnlyCollection<T> : Collections.ObjectModel.ReadOnlyCollection<T>
    {
        /// <summary>
        /// Source collection of this <see cref="AutoRefreshReadOnlyCollection{T}"/>
        /// </summary>
        protected ICollection<T> SourceCollection { get; }

        /// <summary>
        /// Create a empty <see cref="AutoRefreshReadOnlyCollection{T}"/>
        /// </summary>
        protected AutoRefreshReadOnlyCollection() : this(new List<T>())
        { }
        /// <summary>
        /// Create a <see cref="AutoRefreshReadOnlyCollection{T}"/> where the content is based on another collection.
        /// </summary>
        public AutoRefreshReadOnlyCollection(ICollection<T> sourceCollection) : base(new List<T>())
        {
            if (sourceCollection == null)
                throw new ArgumentNullException(null, "The source " + nameof(ICollection<T>) + " cannot be null.");

            SourceCollection = sourceCollection;
        }

        /// <summary>
        /// Refresh the content of the <see cref="AutoRefreshReadOnlyCollection{T}"/>
        /// </summary>
        virtual public void Refresh()
        {
            Clear();
            SourceCollection.ForEach(Add);
        }

        /// <summary>
        /// Clear <see cref="Items"/>
        /// </summary>
        protected void Clear()
        {
            base.Items.Clear();
        }
        /// <summary>
        /// Add a entry to the <see cref="Items"/>
        /// </summary>
        protected void Add(T item)
        {
            base.Items.Add(item);
        }

        /// <summary>
        /// Returns the <see cref="IList{T}"/> as <see cref="AutoRefreshReadOnlyCollection{T}"/> included in a wrapper.
        /// </summary>
        new protected IList<T> Items
        {
            get
            {
                Refresh();
                return base.Items;
            }
        }

        /// <summary>
        /// Gets the number of items contained in the <see cref="AutoRefreshReadOnlyCollection{T}"/>
        /// </summary>
        new public int Count { get { return Items.Count; } }
        /// <summary>
        /// Returns the element to the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        new public T this[int index] { get { return Items[index]; } }
        /// <summary>
        /// Determines whether <see cref="AutoRefreshReadOnlyCollection{T}"/> contains a specific value.
        /// </summary>
        /// <param name="item">Object to be found in <see cref="AutoRefreshReadOnlyCollection{T}"/>.</param>
        /// <returns>true if item exists in <see cref="AutoRefreshReadOnlyCollection{T}"/>; otherwise, false.</returns>
        new public bool Contains(T item) { return Items.Contains(item); }
        /// <summary>
        /// Copies elements from <see cref="AutoRefreshReadOnlyCollection{T}"/> to <see cref="Array"/>, starting at a particular System.Array index.
        /// </summary>
        /// <param name="array">One-dimensional System.Array which is the destination of the elements copied from <see cref="AutoRefreshReadOnlyCollection{T}"/>. <see cref="Array"/> must have zero base indexing.</param>
        /// <param name="arrayIndex">Zero base index in array from which copying starts.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> has the value null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="ArgumentException">The number of elements in the source <see cref="AutoRefreshReadOnlyCollection{T}"/> is greater than the amount of space available between arrayIndex and the end of the destination array.</exception>
        new public void CopyTo(T[] array, int arrayIndex) { Items.CopyTo(array, arrayIndex); }
        /// <summary>
        ///  Returns an enumerator that iterates within <see cref="AutoRefreshReadOnlyCollection{T}"/>.
        /// </summary>
        /// <returns><see cref="IEnumerator{T}"/> for <see cref="AutoRefreshReadOnlyCollection{T}"/>.</returns>
        new public IEnumerator<T> GetEnumerator() { return Items.GetEnumerator(); }
        /// <summary>
        /// Finds the specified object and returns the base index zero of the first occurrence throughout the <see cref="AutoRefreshReadOnlyCollection{T}"/>.
        /// </summary>
        /// <param name="value">Object to be found in <see cref="AutoRefreshReadOnlyCollection{T}"/>.The value can be null for reference types.</param>
        /// <returns>Zero base index of the first occurrence of an item in the entire <see cref="AutoRefreshReadOnlyCollection{T}"/>, if it exists; if not, -1.</returns> 
        new public int IndexOf(T value) { return Items.IndexOf(value); }
    }

}
