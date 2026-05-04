using Microsoft.AspNetCore.Mvc;
using NewImobiliaria.Models;
using NewImobiliaria.Services;
using NewImobiliaria.Builder;
using NewImobiliaria.Observer;

namespace NewImobiliaria.Controllers
{
    [ApiController]
    [Route("api/imovel")]
    public class ImovelController : ControllerBase
    {
        // Serviço responsável pelo CRUD dos imóveis
        private static ImovelService _service;

        /*
         ============================================================
          OBSERVER - CONFIGURAÇÃO INICIAL
         ============================================================

         Aqui configuramos o Subject e registramos os Observers.

         
         "Sempre que algo acontecer, alguém será avisado"
        */
        static ImovelController()
        {
            var eventBus = new EventBus();

            eventBus.Subscribe("novo_lead", new LogObserver());
            eventBus.Subscribe("novo_lead", new EmailObserver());
            eventBus.Subscribe("novo_lead", new AnalyticsObserver());

            _service = new ImovelService(eventBus);
        }

        // =========================
        // CREATE (POST)
        // =========================
        [HttpPost]
        public IActionResult Criar([FromBody] Imovel imovel)
        {
            /*
             ============================================================
              PADRÃO BUILDER
             ============================================================

             Construímos o objeto Imovel passo a passo.

              Vantagens:
             - Código limpo
             - Evita construtores complexos
             - Fácil manutenção

             
             "Eu controlo como o objeto é criado"
            */
            var novo = new ImovelBuilder()
                .SetId(imovel.Id)
                .SetTitulo(imovel.Titulo)
                .SetTipo(imovel.Tipo)
                .SetPreco(imovel.Preco)
                .SetArea(imovel.Area)
                .SetCidade(imovel.Cidade)
                .SetQuartos(imovel.Quartos)
                .Build();

            return Ok(_service.Criar(novo));
        }

        // =========================
        // READ (GET)
        // =========================
        [HttpGet]
        public IActionResult Listar()
        {
            
            return Ok(_service.ListarComComposite());
        }

        // =========================
        // UPDATE (PUT)
        // =========================
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Imovel imovel)
        {
            var atualizado = _service.Atualizar(id, imovel);

            if (atualizado == null)
                return NotFound();

            return Ok(atualizado);
        }

        // =========================
        // DELETE (DELETE)
        // =========================
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var ok = _service.Deletar(id);

            if (!ok)
                return NotFound();

            return Ok("Removido");
        }
    }
}
