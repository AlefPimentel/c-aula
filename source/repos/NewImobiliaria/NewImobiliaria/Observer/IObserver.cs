namespace NewImobiliaria.Observer
{
    public interface IObserver
    {
        void Update(string evento, object data);
    }
}
