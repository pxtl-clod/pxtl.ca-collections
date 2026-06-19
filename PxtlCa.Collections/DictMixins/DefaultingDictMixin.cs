namespace PxtlCa.Collections.DictMixins
{
    public class DefaultingDictMixin<K, V> : DictMixin<K, V>
    {
        #region Constructors

        public DefaultingDictMixin() : base()
        {

        }

        public DefaultingDictMixin(ValueConstructor con)
            : this()
        {
            this.ValueConstructionHandler = con;
        }

        #endregion

        protected virtual V _DefaultValueConstructorImplementation(K key)
        {
            return default(V);
        }

        //ValueConstructor system to construct defaults.
        public delegate V ValueConstructor(K key);
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

        // This is the raison d'etre of this whole class - an indexer
        // which adds an empty V when K does not exist.
        public override V  GetVal(IDictionary<K,V> thisDict, MixableDict<K,V>.DictMixinHolder NextMixin, K key)
        {
 	         V val;
             if (thisDict.TryGetValue(key, out val))
                {
                    return NextMixin[key];
                }
                else
                {
                    V temp = (ValueConstructionHandler == null) ? _DefaultValueConstructorImplementation(key) : ValueConstructionHandler(key);
                    thisDict[key] = temp;
                    return temp;
                }
        }
    }
}
