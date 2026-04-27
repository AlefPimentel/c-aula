using System.Collections.Generic;

namespace NewImobiliaria.Composite
{
    public class ImovelGrupo : IImovelComponente
    {
        private List<IImovelComponente> _itens = new List<IImovelComponente>();

        public void Adicionar(IImovelComponente item)
        {
            _itens.Add(item);
        }

        public object Exibir()
        {
            var lista = new List<object>();

            foreach (var item in _itens)
            {
                lista.Add(item.Exibir());
            }

            return lista;
        }
    }
}