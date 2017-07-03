using System.Collections.ObjectModel;

namespace Turnable.Utilities
{
    public class NamedElementCollection<T> : KeyedCollection<string, T> where T : INamedElement
    {
        protected override string GetKeyForItem(T item)
        {
            return item.Name;
        }
    }
}