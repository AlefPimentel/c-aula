using NewImobiliaria.Models;

namespace NewImobiliaria.Composite
{
    public class ImovelItem : IImovelComponente
    {
        private Imovel _imovel;

        public ImovelItem(Imovel imovel)
        {
            _imovel = imovel;
        }

        public object Exibir()
        {
            return _imovel;
        }
    }
}