using System.Collections.Generic;

namespace NewImobiliaria.Observer
{
    public class ImovelSubject
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void AdicionarObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Notificar(string mensagem)
        {
            foreach (var obs in _observers)
            {
                obs.Atualizar(mensagem);
            }
        }
    }
}