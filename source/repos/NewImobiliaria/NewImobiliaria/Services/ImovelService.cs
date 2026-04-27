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
        private ImovelSubject _subject;

        public ImovelService(ImovelSubject subject)
        {
            _subject = subject;
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
            _subject.Notificar("Imóvel cadastrado: " + imovel.Titulo);

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

            /*
              Dispara evento de atualização
            */
            _subject.Notificar("Imóvel atualizado: " + i.Titulo);

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
            _subject.Notificar("Imóvel removido: " + i.Titulo);

            return true;
        }
    }
}