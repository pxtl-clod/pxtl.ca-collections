using System;
using System.Collections.Generic;
using System.Text;

namespace Pxtl.Collections
{
    public class VirtualList<T> : IList<T>
    {
    	protected IList<T> list;
    	
    	public VirtualList()
    	{
    		list = new List<T>();
    	}
    	
    	public VirtualList(IList<T> list)
    	{
    		this.list = list;
    	}
    	
        #region IList<T> Members

        public virtual int IndexOf(T item)
        {
        	return list.IndexOf(item);
        }

        public virtual void Insert(int index, T item)
        {
        	list.Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
        	list.RemoveAt(index);
        }

        public virtual T this[int index]
        {
            get
            {
            	return list[index];
            }
            set
            {
            	list[index] = value;
            }
        }

        #endregion

        #region ICollection<T> Members

        public virtual void Add(T item)
        {
        	list.Add(item);
        }
        
        public virtual void AddRange(IList<T> range)
        {
            foreach (T item in range)
                Add(item);
        }

        public virtual void Clear()
        {
        	list.Clear();
        }

        public virtual bool Contains(T item)
        {
        	return list.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
        	list.CopyTo(array, arrayIndex);
        }

        public virtual int Count
        {
            get { return list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return list.IsReadOnly; }
        }

        public virtual bool Remove(T item)
        {
        	return list.Remove(item);
        }

        #endregion

        #region IEnumerable<T> Members

        public virtual IEnumerator<T> GetEnumerator()
        {
        	return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
        	return (System.Collections.IEnumerator)list.GetEnumerator();
        }

        #endregion
    }
}
