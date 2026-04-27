using NewImobiliaria.Models;

namespace NewImobiliaria.Builder
{
    public class ImovelBuilder
    {
        private Imovel _imovel = new Imovel();

        public ImovelBuilder SetId(int id)
        {
            _imovel.Id = id;
            return this;
        }

        public ImovelBuilder SetTitulo(string titulo)
        {
            _imovel.Titulo = titulo;
            return this;
        }

        public ImovelBuilder SetTipo(string tipo)
        {
            _imovel.Tipo = tipo;
            return this;
        }

        public ImovelBuilder SetPreco(double preco)
        {
            _imovel.Preco = preco;
            return this;
        }

        public Imovel Build()
        {
            return _imovel;
        }
    }
}