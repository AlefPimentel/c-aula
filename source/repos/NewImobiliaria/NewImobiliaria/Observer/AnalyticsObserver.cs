using System;

namespace NewImobiliaria.Observer
{
    public class AnalyticsObserver : IObserver
    {
        public void Update(string evento, object data)
        {
            Console.WriteLine($"Evento trackeado '{evento}' com dados: {data}");
        }
    }
}
