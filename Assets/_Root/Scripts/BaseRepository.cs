using System.Collections.Generic;
using System;

internal interface IRepository : IDisposable
{

}

internal abstract class BaseRepository<TKey, TValue, TConfig> : IRepository
{
    private readonly Dictionary<TKey, TValue> _items;

    public IReadOnlyDictionary<TKey, TValue> Items => _items;

    protected BaseRepository(IEnumerable<TConfig> configs) =>
        _items = CreateItems(configs);

    public void Dispose() =>
        _items.Clear();

    private Dictionary<TKey, TValue> CreateItems(IEnumerable<TConfig> configs)
    {
        var items = new Dictionary<TKey, TValue>();

        foreach (TConfig config in configs)
        {
            items[GetKey(config)] = CreateItems(config);
        }
        return items;
    }

    protected abstract TKey GetKey(TConfig config);

    protected abstract TValue CreateItems(TConfig config);
}
