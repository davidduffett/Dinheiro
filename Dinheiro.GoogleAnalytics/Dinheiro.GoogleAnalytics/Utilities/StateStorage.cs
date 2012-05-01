using System.Collections;
using System.Web;

namespace Dinheiro.GoogleAnalytics.Utilities
{
    public interface IStateStorage
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
        void Remove(string key);
    }

    public class HttpContextStateStorage : IStateStorage
    {
        public T Get<T>(string key)
        {
            return (T)HttpContext.Current.Items[key];
        }

        public void Set<T>(string key, T value)
        {
            HttpContext.Current.Items[key] = value;
        }

        public void Remove(string key)
        {
            HttpContext.Current.Items.Remove(key);
        }
    }

    public class InMemoryStateStorage : IStateStorage
    {
        private readonly IDictionary _items = new Hashtable();

        public T Get<T>(string key)
        {
            return (T)_items[key];
        }

        public void Set<T>(string key, T value)
        {
            _items[key] = value;
        }

        public void Remove(string key)
        {
            _items.Remove(key);
        }
    }
}