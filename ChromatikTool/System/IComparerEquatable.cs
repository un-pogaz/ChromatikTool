using System;

namespace System
{
    /// <summary>
    /// Shortened <see langword="interface"/> for <see cref="IEquatable{T}"/> like and <see cref="Collections.Generic.IComparer{T}"/> like
    /// </summary>
    public interface IComparerEquatable<T> : IEquatable<T>, Collections.Generic.IEqualityComparer<T>, Collections.IEqualityComparer, IComparable<T>, IComparable, Collections.Generic.IComparer<T>, Collections.IComparer
    {

    }
}
