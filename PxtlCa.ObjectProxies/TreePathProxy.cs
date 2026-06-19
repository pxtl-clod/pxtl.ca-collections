namespace PxtlCa.ObjectProxies;

public class TreePathProxy<TDictionary, TKey> : ObjectMemberProxy<TDictionary, TDictionary> where TDictionary : IDictionary<TKey, TDictionary> {
    public TreePathProxy(IObjectProxy<TDictionary> nodeProxy, IList<TKey> keyPath) : base(new TreePathMemberProxy<TDictionary, TKey>(keyPath), nodeProxy) { }
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