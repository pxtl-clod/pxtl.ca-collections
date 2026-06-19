namespace PxtlCa.ObjectProxies;

public class TreePathMemberProxy<TDictionary, TKey> : MemberProxy<TDictionary, TDictionary>
where TDictionary : IDictionary<TKey, TDictionary> {
    public readonly IList<TKey> KeyPath;

    public TreePathMemberProxy(IList<TKey> keyPath) {
        KeyPath = keyPath;
    }

    public TreePathMemberProxy(TreePathMemberProxy<TDictionary, TKey> toClone) {
        KeyPath = new List<TKey>(toClone.KeyPath);
    }

    public TreePathMemberProxy<TDictionary, TKey> Clone() {
        return new TreePathMemberProxy<TDictionary, TKey>(this);
    }

    public override TDictionary Get(TDictionary parent) {
        if (KeyPath == null) {
            return parent;
        }

        TDictionary curNode = parent;
        foreach (TKey name in KeyPath) {
            curNode = curNode[name];
        }
        return curNode;
    }

    public override void Set(IObjectProxy<TDictionary> parent, TDictionary memberValue) {
        if (KeyPath == null) {
            parent.Val = memberValue;
            return;
        }

        IObjectProxy<TDictionary> curNode = parent;
        foreach (TKey name in KeyPath) {
            curNode = new DictionaryProxy<TKey, TDictionary>(curNode.Val, name);
        }
        curNode.Val = memberValue;
    }

    public override ICollection<string> AsPathStrings {
        get {
            List<string> result = new List<string>(KeyPath.Count);
            foreach (TKey key in KeyPath) {
                result.Add(key?.ToString() ?? "");
            }
            return result;
        }
    }
}