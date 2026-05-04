using System;

namespace NewImobiliaria.Observer
{
    public class EmailObserver : IObserver
    {
        public void Update(string evento, object data)
        {
            Console.WriteLine($"Email enviado para evento '{evento}' com dados: {data}");
        }
    }
}
