using PxtLCa.Collections.Polyfills;

namespace PxtlCa.Collections
{
    public interface IObjectProxy<T>
    {
        T Val { get; set;}
    }

    public static class IObjectProxyExtensions {
        public static void ThrowIfValNull<T>(this IObjectProxy<T>? proxy, string? parameterName = null, string? exceptionMessage = null) {
            ArgumentGuard.ThrowIfNull(proxy, parameterName ?? nameof(proxy));
            throw new NullProxyValueException(exceptionMessage ?? $"Cannot complete operation. Proxy member '{proxy!.Val}' is null.");
        }
    }

    public abstract class ObjectProxy<T> : IObjectProxy<T>
    {
        abstract public T Val { get; set; }
        public override bool Equals(object obj) {
            return Equals(Val, obj);
        }
        public override int GetHashCode() {
            return Val?.GetHashCode() ?? -1;
        }
        public override string? ToString() {
            return Val?.ToString();
        }
    }

    public class DelegatingObjectProxy<T> : ObjectProxy<T>
    {
        public DelegatingObjectProxy(GetHandler getter, SetHandler setter)
        {
            Getter += getter;
            Setter += setter;
        }
        public delegate T GetHandler();
        public delegate void SetHandler(T value);
        public GetHandler Getter;
        public SetHandler Setter;

        public override T Val
        {
            get
            {
                return Getter();
            }
            set
            {
                Setter(value);
            }
        }
    }

    public interface IObjectMemberProxy<TParent, TMember> : IObjectProxy<TMember>
    {
        IMemberProxy<TParent, TMember> MemberProxy { get; }
        IObjectProxy<TParent> Parent { get; }
    }

    public class ObjectMemberProxy<TParent, TMember> : IObjectMemberProxy<TParent, TMember>
    {
        private IMemberProxy<TParent, TMember> _MemberProxy;
        public IMemberProxy<TParent, TMember> MemberProxy
        {
            get
            {
                return _MemberProxy;
            }
        }

        private IObjectProxy<TParent> _Parent;
        public IObjectProxy<TParent> Parent
        {
            get
            {
                return _Parent;
            }
        }

        public ObjectMemberProxy(IMemberProxy<TParent, TMember> memberProxy, IObjectProxy<TParent> parent)
        {
            ArgumentGuard.ThrowIfNull(memberProxy, nameof(memberProxy));
            parent.ThrowIfValNull(nameof(parent));

            _MemberProxy = memberProxy;
            _Parent = parent;
        }

        public TMember Val
        {
	        get 
	        { 
		        return MemberProxy.Get(Parent!.Val!);
	        }
	        set 
	        { 
		        MemberProxy.Set(Parent, value);
	        }
        }
    }

    public class IndexerProxy<TKey, TValue> : ObjectMemberProxy<IDictionary<TKey, TValue>, TValue>
    {
        public IndexerProxy(IDictionary<TKey, TValue> dict, TKey key)
            : base(new IndexerMemberProxy<TKey, TValue>(key), new RefProxy<IDictionary<TKey, TValue>>(dict)) 
        {}
    }

    public class TreePathProxy<TDict, TKey> : ObjectMemberProxy<TDict, TDict> where TDict : IDictionary<TKey, TDict>
    {
        public TreePathProxy(IObjectProxy<TDict> nodeProxy, IList<TKey> keyPath) : base(new TreePathMemberProxy<TDict, TKey>(keyPath), nodeProxy) {}
    }

    //public class TreePathProxy<D, K> : ObjectProxy<D> where D : IDictionary<K, D>
    //{
    //    public IObjectProxy<D> dict;
    //    public TreePathProxy(IObjectProxy<D> dict, IList<K> keyPath)
    //    {
    //        this.dict = dict;
    //        this.keyPath = keyPath;
    //    }
    //    public override D Val
    //    {
    //        get
    //        {
    //            if (keyPath == null)
    //                return dict.Val;

    //            D curNode = dict.Val;
    //            foreach (K name in keyPath)
    //            {
    //                curNode = curNode[name];
    //            }
    //            return curNode;
    //        }
    //        set
    //        {
    //            if (keyPath == null)
    //            {
    //                dict.Val = value;
    //                return;
    //            }

    //            IObjectProxy<D> curNode = dict;
    //            foreach (K name in keyPath)
    //            {
    //                curNode = new IndexerProxy<K, D>(curNode.Val, name);
    //            }
    //            curNode.Val = value; 
    //        }
    //    }
    //}

    public class DelegatingProxy<T> : ObjectProxy<T>
    {
        public delegate T Getter();
        public delegate void Setter(T value);

        protected Getter GetHandler;
        protected Setter SetHandler;
        public DelegatingProxy(Getter getHandler, Setter setHandler)
        {
            this.GetHandler = getHandler;
            this.SetHandler = setHandler;
        }

        public override T Val {
            get { return GetHandler(); }
            set { SetHandler(value); }
        }
    }

    public class RefProxy<T> : ObjectProxy<T>
    {
    	protected T _val;
        public RefProxy(T val) { _val = val; }
        public override T Val
        {
            get { return _val; }
            set { _val = value!; }
        }

        public delegate void UsingDelegate(RefProxy<T> proxy);
        //TODO: IDisposable object wrapper?
        public static void UsingVal(ref T value, UsingDelegate procedure)
        {
        	RefProxy<T> proxy = new RefProxy<T>(value);
            procedure(proxy);
            value = proxy.Val;
        }
    }
    
    public interface IKeyedProxyFactory<D, K, V>
    {
    	IObjectProxy<V> Create(IObjectProxy<D> dict, K key);
    }
    
    public class IndexerProxyFactory<D, K, V> : IKeyedProxyFactory<D, K, V> where D : IDictionary<K,V>
    {
    	private IndexerProxyFactory(){}
    	public static readonly IndexerProxyFactory<D,K,V> Instance = new IndexerProxyFactory<D,K,V>();
        public IObjectProxy<V> Create(IObjectProxy<D> dict, K key)
    	{
            return Create(dict.Val, key);
    	}

        public IObjectProxy<V> Create(D dict, K key)
    	{
    		return new IndexerProxy<K,V>(dict, key);
    	}
    }
    
    public class TreePathProxyFactory<D, K> : IKeyedProxyFactory<D, IList<K>, D> where D:IDictionary<K,D>
    {
    	private TreePathProxyFactory(){}
    	public static readonly TreePathProxyFactory<D,K> Instance = new TreePathProxyFactory<D,K>();
        public IObjectProxy<D> Create(IObjectProxy<D> dict, IList<K> keyList)
    	{
    		return new TreePathProxy<D,K>(dict, keyList);
    	}
    }
}
