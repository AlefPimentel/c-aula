using System;

namespace NewImobiliaria.Observer
{
    public class LogObserver : IObserver
    {
        public void Atualizar(string mensagem)
        {
            Console.WriteLine("LOG: " + mensagem);
        }
    }
}