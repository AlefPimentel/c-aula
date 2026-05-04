using NewImobiliaria.Models;
using NewImobiliaria.Observer;
using System.Collections.Generic;
using System.Linq;

namespace NewImobiliaria.Services
{
    public class ImovelService
    {
        
        private List<Imovel> _imoveis = new List<Imovel>();

        /*
         ============================================================
          PADRÃO OBSERVER
         ============================================================

         Responsável por notificar eventos do sistema.

          Ideia:
         "O sistema dispara eventos, sem saber quem vai reagir"
        */
        private readonly EventBus _eventBus;

        public ImovelService(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        // =========================
        // CREATE
        // =========================
        public Imovel Criar(Imovel imovel)
        {
            _imoveis.Add(imovel);

            /*
              Dispara evento de criação
             Todos os observers são notificados automaticamente
            */
            _eventBus.Publish("novo_lead", new
            {
                imovel.Titulo,
                imovel.Tipo,
                imovel.Cidade,
                imovel.Preco
            });

            return imovel;
        }

        // =========================
        // READ SIMPLES
        // =========================
        public List<Imovel> Listar()
        {
            return _imoveis;
        }

        // =========================
        //  COMPOSITE
        // =========================
        public object ListarComComposite()
        {
            /*
             ============================================================
              PADRÃO COMPOSITE
             ============================================================

             Permite tratar:
             - Um único objeto (ImovelItem)
             - Um grupo de objetos (ImovelGrupo)

             da mesma forma.

              Ideia:
             "Objeto individual e coleção são tratados igual"
            */

            var grupo = new Composite.ImovelGrupo();

            foreach (var i in _imoveis)
            {
                // Cada imóvel vira um elemento da estrutura
                grupo.Adicionar(new Composite.ImovelItem(i));
            }

            // Retorna tudo de forma unificada
            return grupo.Exibir();
        }

        // =========================
        // UPDATE
        // =========================
        public Imovel Atualizar(int id, Imovel novo)
        {
            var i = _imoveis.FirstOrDefault(x => x.Id == id);

            if (i == null)
                return null;

            i.Titulo = novo.Titulo;
            i.Tipo = novo.Tipo;
            i.Preco = novo.Preco;
            i.Area = novo.Area;
            i.Cidade = novo.Cidade;
            i.Quartos = novo.Quartos;

            /*
              Dispara evento de atualização
            */
            _eventBus.Publish("imovel_atualizado", i);

            return i;
        }

        // =========================
        // DELETE
        // =========================
        public bool Deletar(int id)
        {
            var i = _imoveis.FirstOrDefault(x => x.Id == id);

            if (i == null)
                return false;

            _imoveis.Remove(i);

            /*
              Dispara evento de remoção
            */
            _eventBus.Publish("imovel_removido", i);

            return true;
        }
    }
}
