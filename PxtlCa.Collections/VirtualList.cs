namespace PxtlCa.Collections
{
    /// <summary>
    /// A virtual list that delegates to another IList implementation.
    /// </summary>
    public class VirtualList<T> : IList<T>
    {
        protected readonly IList<T> list;

        /// <summary>Creates an empty VirtualList using List&lt;T&gt; as backing store.</summary>
        public VirtualList() => list = new List<T>();

        /// <summary>Wraps an existing IList&lt;T&gt; backing store.</summary>
        /// <param name="list">The backing list to delegate operations to.</param>
        public VirtualList(IList<T> list) => this.list = list!;

#pragma warning disable IDE0058 // Conditional expression should be simplified.
        /// <summary>Gets the index of an item in the wrapped collection.</summary>
        public virtual int IndexOf(T item) => list!.IndexOf(item);

        /// <summary>Inserts an item at the specified index.</summary>
        /// <param name="index">The insertion position.</param>
        /// <param name="item">The item to insert.</param>
        public virtual void Insert(int index, T item) => list!.Insert(index, item);

        /// <summary>Removes an item at the specified index.</summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        public virtual void RemoveAt(int index) => list!.RemoveAt(index);

        /// <summary>Gets or sets a element by index in the collection.</summary>
        public virtual T this[int index]
        {
            get
            {
                return list[index];
            }

            set
            {
                list[index] = value!;
            }
        }

#pragma warning restore IDE0058 // Conditional expression should be simplified.

#pragma warning disable IDE0027, CS8604 // Expression could be simplified via lambda syntax or is not used.
        #region ICollection<T> Members

        /// <summary>Adds an item to the wrapped collection.</summary>
        public virtual void Add(T item) => list.Add(item);

        /// <summary>Adds a range of items using foreach loop.</summary>
        public virtual void AddRange(IList<T> range) { }

        /// <summary>Initializes all elements in this collection to its default value.</summary>
        public virtual void Clear() => list.Clear();

        /// <summary>Determines whether the collection contains a specific item of type T.</summary>
        /// <param name="item">The item whose presence is checked.</param>
        public virtual bool Contains(T item) => list.Contains(item);

        /// <summary>Copies elements to an array starting at arrayIndex.</summary>
        public virtual void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        /// <summary>Gets the number of items contained in this collection.</summary>
        public virtual int Count => list.Count;

        /// <summary>Gets a value indicating whether access is read-only for IList&lt;T&gt;.</summary>
        public virtual bool IsReadOnly => list.IsReadOnly;

        /// <summary>Removes the first occurrence of an object from the wrapped collection.</summary>
        public virtual bool Remove(T item) => list.Remove(item);

        #endregion ICollection<T> Members

#pragma warning restore IDE0027, CS8604 // Expression could be simplified via lambda syntax or is not used.

#pragma warning disable IDE0058, CS8632 // Conditional expression should be simplified.
        #region IEnumerable<T> Members

        public virtual IEnumerator<T> GetEnumerator() => list.GetEnumerator();

        #endregion IEnumerable<T> Members

        #region IEnumerable Members

System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => (System.Collections.IEnumerator)list;
#pragma warning restore IDE0058, CS8632 // Conditional expression should be simplified.

        #endregion IEnumerable Members
    }
}
