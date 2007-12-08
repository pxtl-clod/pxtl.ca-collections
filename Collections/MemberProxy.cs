using System;
using System.Collections.Generic;
using System.Text;

namespace Pxtl.Collections
{
    public interface IMemberProxy<TParent, TMember>
    {
        TMember Get(TParent parent);
        void Set(IObjectProxy<TParent> parent, TMember memberValue);
    }

    public abstract class MemberProxy<TParent, TMember> : IMemberProxy<TParent, TMember>
    {
        public abstract TMember Get(TParent parent);
        public abstract void Set(IObjectProxy<TParent> parent, TMember memberValue);
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
            MemberObjectProxy<TRoot, TJoin> firstObjectProxy = new MemberObjectProxy<TRoot,TJoin>(First, parent);
            Second.Set(firstObjectProxy, memberValue);
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
    }
}
