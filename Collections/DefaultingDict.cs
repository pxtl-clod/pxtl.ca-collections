using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Pxtl.Collections
{
    public class DefaultingDict<K, V> : VirtualDict<K, V>
    {
        // Underlying dictionary - everything delegates
        // to this.

        protected virtual V defaultValueConstructorImplementation()
        {
            return default(V);
        }
        
        //ValueConstructor system to construct defaults.
        public delegate V ValueConstructor();
        private ValueConstructor valueConstructionHandler;
        public ValueConstructor ValueConstructionHandler
        {
            get
            {
                return valueConstructionHandler;
            }
            set
            {
                valueConstructionHandler = value;
            }
        }


        #region Constructors
        //custom constructor to use a custom valueconstructor
        public DefaultingDict(ValueConstructor con)
            : this()
        {
            this.ValueConstructionHandler = con;
        }

        public DefaultingDict(ValueConstructor con, IEqualityComparer<K> comparer)
            : this(comparer)
        {
            this.ValueConstructionHandler = con;
        }

        // We provide all the same constructors that the generic
        // Dictionary provides.

        public DefaultingDict() : base() { }

        public DefaultingDict(IEqualityComparer <K> comparer) : base(new Dictionary<K, V>(comparer)) { }

        public DefaultingDict(int capacity) : base(capacity) { }

        public DefaultingDict(int capacity, IEqualityComparer<K> comparer) : base(capacity, comparer) { }

        public DefaultingDict(IDictionary<K, V> originalDictionary) : this(originalDictionary, false) {}

        public DefaultingDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer) : base(originalDictionary, comparer) { }
        #endregion

        // This methods allow an existing dictionary to be wrapped.
        // (The public constructors that take an IDictionary all
        // copy the data.)
        public static new DefaultingDict<K, V> Wrap(IDictionary<K, V> originalDictionary)
        {
            return new DefaultingDict<K, V>(originalDictionary, true);
        }

        // Private constructor used to enable a dictionary either to be
        // copied, or used as the underying storage.
        protected DefaultingDict(IDictionary<K, V> originalDictionary, bool wrap) : base(originalDictionary, wrap) {}

        // This is the raison d'etre of this whole class - an indexer
        // which adds an empty V when K does not exist.
        public override V this[K key]
        {
            get
            {
                V val;
                if(base.TryGetValue(key, out val))
                {
                    return base[key];
                }
                else
                {
                    V temp = (ValueConstructionHandler == null) ? defaultValueConstructorImplementation() : ValueConstructionHandler();
                    this[key] = temp;
                    return temp;
                }
            }
            set
            {
                base[key] = value;
            }
        }
    }

    public class AutoConstructingDict<K, V> : DefaultingDict<K, V> where V : new()
    {
        #region Constructors
        //custom constructor to use a custom valueconstructor
        public AutoConstructingDict(ValueConstructor con)
            : base(con)
        { }

        public AutoConstructingDict(ValueConstructor con, IEqualityComparer<K> comparer)
            : base(con, comparer)
        { }

        public AutoConstructingDict() : base()
        { }

        public AutoConstructingDict(IEqualityComparer<K> comparer)
            : base(comparer)
        { }

        public AutoConstructingDict(int capacity)
            : base(capacity)
        { }

        public AutoConstructingDict(int capacity, IEqualityComparer<K> comparer)
            : base(capacity, comparer)
        { }

        public AutoConstructingDict(IDictionary<K, V> originalDictionary)
            : base(originalDictionary)
        { }

        protected AutoConstructingDict(IDictionary<K, V> originalDictionary, bool wrap)
            : base(originalDictionary, wrap)
        { }

        public AutoConstructingDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer)
            : base(originalDictionary, comparer)
        { }
        #endregion

        protected override V defaultValueConstructorImplementation()
        {
            return new V();
        }
    }

    public class ChangeNotingDict<K, V> : VirtualDict<K, V> where V: ICloneable
    {
        // Underlying dictionary - everything delegates
        // to this.

        private IDictionary<K, ChangeNote<V>> changes;
        public IDictionary<K, ChangeNote<V>> Changes
        {
            get
            {
                return changes;
            }
        }

        public delegate void ChangeHandler(K key, ChangeNote<V> change);
        public ChangeHandler Changed;

        #region New Methods and Members

        public bool NoteChanges = false;

        protected void NoteChange(K key, ChangeType type, V val)
        {
            if (NoteChanges)
            {
                ChangeNote<V> change = new ChangeNote<V>(type, val);
                changes[key] = change;
                if (Changed != null) Changed(key, change);
            }
        }

        #endregion

        #region Constructors
        // We provide all the same constructors that the generic
        // Dictionary provides.

        public ChangeNotingDict() : base()
        {
            changes = new Dictionary<K, ChangeNote<V>>();
        }

        public ChangeNotingDict(ChangeHandler onChanged)
            : this()
        {
            Changed = onChanged;
        }

        public ChangeNotingDict(ChangeHandler onChanged, IEqualityComparer<K> comparer)
            : this(comparer)
        {
            Changed = onChanged;
        }

        public ChangeNotingDict(IEqualityComparer<K> comparer) 
            : base(comparer)
        {
            changes = new Dictionary<K, ChangeNote<V>>(comparer);
        }

        public ChangeNotingDict(int capacity)
            : base(capacity)
        {
            changes = new Dictionary<K, ChangeNote<V>>();
        }

        public ChangeNotingDict(int capacity, IEqualityComparer<K> comparer)
            : base(capacity, comparer)
        {
            changes = new Dictionary<K, ChangeNote<V>>(comparer);
        }

        public ChangeNotingDict(IDictionary<K, V> originalDictionary)
            : base(originalDictionary)
        {
            changes = new Dictionary<K, ChangeNote<V>>();
        }

        public ChangeNotingDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer) 
            : base(originalDictionary, comparer)
        {
            changes = new Dictionary<K, ChangeNote<V>>(comparer);
        }
        #endregion

        // This methods allow an existing dictionary to be wrapped.
        // (The public constructors that take an IDictionary all
        // copy the data.)
        public static new ChangeNotingDict<K, V> Wrap(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer)
        {
            return new ChangeNotingDict<K, V>(originalDictionary, comparer, true);
        }

        // Private constructor used to enable a dictionary either to be
        // copied, or used as the underying storage.
        protected ChangeNotingDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer, bool wrap)
            : base(originalDictionary, wrap) 
        {
            changes = new Dictionary<K, ChangeNote<V>>(comparer);
        }

        // This is the raison d'etre of this whole class - an indexer
        // which notes any changes
        public override V this[K key]
        {
            get
            {
                 return base[key];
            }
            set
            {
                base[key] = value;
                NoteChange(key, ChangeType.Set, value);
            }
        }

        // Delegating implementations of all other methods.
        public override bool Remove(K key)
        {
            NoteChange(key, ChangeType.Remove, default(V));
            return base.Remove(key);          
        }
        public override void Add(K key, V value)
        {
            base.Add(key, value);
            NoteChange(key, ChangeType.Add, value);
        }
        public override bool Remove(KeyValuePair<K, V> item)
        {
            NoteChange(item.Key, ChangeType.Remove, default(V));
            return base.Remove(item);
        }

        public override void Clear()
        {
            foreach (K key in Keys)
            {
                NoteChange(key, ChangeType.Remove, default(V));
            }
            base.Clear();
        }

        public override void Add(KeyValuePair<K, V> item)
        {
            Changes[item.Key] = new ChangeNote<V>(ChangeType.Add, item.Value);
            base.Add(item);
        }
    }   
}
