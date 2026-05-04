using System.Collections.Generic;

namespace NewImobiliaria.Observer
{
    public class EventBus
    {
        private readonly Dictionary<string, List<IObserver>> _observers = new();

        public void Subscribe(string evento, IObserver observer)
        {
            if (!_observers.ContainsKey(evento))
            {
                _observers[evento] = new List<IObserver>();
            }

            _observers[evento].Add(observer);
        }

        public void Publish(string evento, object data)
        {
            if (!_observers.ContainsKey(evento))
            {
                return;
            }

            foreach (var observer in _observers[evento])
            {
                observer.Update(evento, data);
            }
        }
    }
}
