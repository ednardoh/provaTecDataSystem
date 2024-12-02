using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebCadTarefa.Domain;
using WebCadTarefa.Dto;
using WebCadTarefa.Interfaces;
using WebCadTarefa.ValidationDate;

namespace WebCadTarefa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefas _tarefas;

        public TarefasController(ITarefas tarefas)
        {
            _tarefas = tarefas;
        }

        [HttpGet("Busca/{id}")]
        [SwaggerOperation(Summary = "Busca de Tarefas por id ")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var retorno = await _tarefas.GetAsync(id);
            return retorno != null ? Ok(retorno) : BadRequest();
        }

        [HttpGet("descricaoTarefa/{descricaoTarefa}")]
        [SwaggerOperation(Summary = "Busca de Tarefas pela Descrição da Tarefa ")]
        public async Task<IActionResult> GetByEmailSenhaAsync(string descricaoTarefa)
        {
            var retorno = await _tarefas.GetByDescricaotarefaAsync(descricaoTarefa);
            return retorno != null ? Ok(retorno) : BadRequest();
        }

        [HttpGet("statusTarefa/{statusTarefa}")]
        [SwaggerOperation(Summary = "Busca de Tarefas pelo Status da Tarefa ")]
        public async Task<IActionResult> GetByStatusTarefaAsync(string statusTarefa)
        {
            var retorno = await _tarefas.GetByStatusTarefaAsync(statusTarefa);
            return retorno != null ? Ok(retorno) : BadRequest();
        }

        [HttpGet("Consulta/{id}")]
        [SwaggerOperation(Summary = "Consulta de tarefas para Edição ")]
        public async Task<IActionResult> GetAsyncEdit(int id)
        {
            var retorno = await _tarefas.GetAsyncEdit(id);
            return retorno != null ? Ok(retorno) : BadRequest();
        }

        [HttpGet("Lista")]
        [SwaggerOperation(Summary = "Busca todos as tarefas ")]
        public async Task<IActionResult> GetAllAsync()
        {
            var retorno = await _tarefas.GetAllAsync();
            return retorno?.Count > 0 ? Ok(retorno) : BadRequest();
        }

        [HttpDelete("Excluir/{id}")]
        [SwaggerOperation(Summary = "Exclui Tarefas ")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var retorno = await _tarefas.DeleteAsync(id);

            if (retorno)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut("Editar/{id}")]
        [SwaggerOperation(Summary = "Altera Tarefas ")]
        public async Task<IActionResult> UpdateAsync(int id, TarefasDTO tarefasRequest)
        {

            Tarefas tarefas = new Tarefas()
            {
                ID              = id,
                Titulotarefa    = tarefasRequest.Titulotarefa,
                Descricaotarefa = tarefasRequest.Descricaotarefa,
                Datacriacao     = tarefasRequest.Datacriacao,
                Dataconclusao   = tarefasRequest.Dataconclusao,
                Status          = tarefasRequest.Status
            };

            //Validando data de Conclusão
            if (ValidaData.Validar(tarefas.Datacriacao, tarefas.Dataconclusao))
            {
                return BadRequest();
            } else
            {
                var retorno = await _tarefas.UpdateAsync(tarefas);

                if (retorno)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }

        }

        [HttpPost("Nova")]
        [SwaggerOperation(Summary = "Incluir Tarefas ")]
        public async Task<IActionResult> PostAsync(TarefasDTO tarefasRequest)
        {
            Tarefas tarefas = new Tarefas()
            {
                Titulotarefa    = tarefasRequest.Titulotarefa,
                Descricaotarefa = tarefasRequest.Descricaotarefa,
                Datacriacao     = tarefasRequest.Datacriacao,
                Dataconclusao   = tarefasRequest.Dataconclusao,
                Status          = tarefasRequest.Status
            };
            var retorno = await _tarefas.CreateAsync(tarefas);
            if (retorno)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
