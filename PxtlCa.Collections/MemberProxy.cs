namespace PxtlCa.Collections
{
    public interface IMemberProxy<TParent, TMember>
    {
        TMember Get(TParent parent);
        void Set(IObjectProxy<TParent> parent, TMember memberValue);
        ICollection<string> AsPath { get;}
    }

    public abstract class MemberProxy<TParent, TMember> : IMemberProxy<TParent, TMember>
    {
        public abstract TMember Get(TParent parent);
        public abstract void Set(IObjectProxy<TParent> parent, TMember memberValue);
        public abstract ICollection<string> AsPath { get;}
    }

    public class SelfMember<T> : MemberProxy<T, T>
    {
        public static readonly SelfMember<T> Instance = new SelfMember<T>();

        public override T Get(T parent)
        {
            return parent;
        }

        public override void Set(IObjectProxy<T> parent, T memberValue)
        {
            parent.Val = memberValue;
        }

        public override ICollection<string> AsPath
        {
            get
            {
                return new List<string>();
            }
        }
    }

    public class IndexerMemberProxy<TKey, TValue> : MemberProxy<IDictionary<TKey, TValue>, TValue>
    {
        public readonly TKey Key;

        public IndexerMemberProxy(TKey key)
        {
            Key = key;
        }

        public override TValue Get(IDictionary<TKey, TValue> parent)
        {
            return parent[Key];
        }

        public override void Set(IObjectProxy<IDictionary<TKey, TValue>> parent, TValue memberValue)
        {
            parent.Val[Key] = memberValue;
        }

        public override ICollection<string> AsPath
        {
            get { return new string[] { Key.ToString() }; }
        } 
    }

    public class ConcatenatedMemberProxy<TRoot, TJoin, TMember> : MemberProxy<TRoot, TMember>
    {
        public readonly IMemberProxy<TRoot, TJoin> First;
        public readonly IMemberProxy<TJoin, TMember> Second;

        public ConcatenatedMemberProxy(IMemberProxy<TRoot, TJoin> first, IMemberProxy<TJoin, TMember> second)
        {
            First = first;
            Second = second;
        }

        public override TMember Get(TRoot parent)
        {
            return Second.Get(First.Get(parent));
        }

        public override void Set(IObjectProxy<TRoot> parent, TMember memberValue)
        {
            IMemberObjectProxy<TRoot, TJoin> firstObjectProxy = new MemberObjectProxy<TRoot,TJoin>(First, parent);
            Second.Set(firstObjectProxy, memberValue);
        }

        public override ICollection<string> AsPath
        {
            get 
            { 
                List<string> path = new List<string>(First.AsPath);
                path.AddRange(Second.AsPath);
                return path;
            }
        }
    }

    public class TreePathMemberProxy<TDict, TKey> : MemberProxy<TDict, TDict> where TDict : IDictionary<TKey, TDict>
    {
        public readonly IList<TKey> KeyPath;

        public TreePathMemberProxy(IList<TKey> keyPath)
        {
            KeyPath = keyPath;
        }

        public TreePathMemberProxy(TreePathMemberProxy<TDict, TKey> toClone)
        {
            KeyPath = new List<TKey>(toClone.KeyPath);
        }

        public TreePathMemberProxy<TDict, TKey> Clone()
        {
            return new TreePathMemberProxy<TDict, TKey>(this);
        }

        public override TDict Get(TDict parent)
        {
            if (KeyPath == null)
                return parent;

            TDict curNode = parent;
            foreach (TKey name in KeyPath)
            {
                curNode = curNode[name];
            }
            return curNode;
        }

        public override void Set(IObjectProxy<TDict> parent, TDict memberValue)
        {
            if (KeyPath == null)
            {
                parent.Val = memberValue;
                return;
            }

            IObjectProxy<TDict> curNode = parent;
            foreach (TKey name in KeyPath)
            {
                curNode = new IndexerProxy<TKey, TDict>(curNode.Val, name);
            }
            curNode.Val = memberValue; 
        }

        public override ICollection<string> AsPath
        {
            get { 
                List<string> resList = new List<string>(KeyPath.Count);
                foreach (TKey key in KeyPath)
                {
                    resList.Add(key.ToString());
                }
                return resList;
            }
        }
    }
}
