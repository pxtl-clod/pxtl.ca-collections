using PxtlCa.Collections.Polyfills;

namespace PxtlCa.ObjectProxies;

public class DictionaryMemberProxy<TKey, TValue> : MemberProxy<IDictionary<TKey, TValue>, TValue> {
    public readonly TKey Key;

    public DictionaryMemberProxy(TKey key) {
        ArgumentGuard.ThrowIfNull(key, nameof(key));
        Key = key;
    }

    public override TValue Get(IDictionary<TKey, TValue> parent) {
        ArgumentGuard.ThrowIfNull(parent, nameof(parent));
        return parent[Key];
    }

    public override void Set(IObjectProxy<IDictionary<TKey, TValue>> parent, TValue memberValue) {
        ArgumentGuard.ThrowIfNull(parent, nameof(parent));
        parent.ThrowIfValNull();
        parent!.Val![Key] = memberValue;
    }

    public override ICollection<string> AsPathStrings {
        get {
            return [Key?.ToString() ?? ""];
        }
    }
}