using NewImobiliaria.Models;

namespace NewImobiliaria.Builder
{
    public class ImovelBuilder
    {
        private readonly Imovel _imovel = new Imovel();

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

        public ImovelBuilder SetPreco(decimal preco)
        {
            _imovel.Preco = preco;
            return this;
        }

        public ImovelBuilder SetArea(double? area)
        {
            _imovel.Area = area;
            return this;
        }

        public ImovelBuilder SetCidade(string cidade)
        {
            _imovel.Cidade = cidade;
            return this;
        }

        public ImovelBuilder SetQuartos(int? quartos)
        {
            _imovel.Quartos = quartos;
            return this;
        }

        public Imovel Build()
        {
            if (string.IsNullOrWhiteSpace(_imovel.Titulo) || _imovel.Preco <= 0)
            {
                throw new Exception("Imóvel inválido");
            }

            return _imovel;
        }
    }
}
