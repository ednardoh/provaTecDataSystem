using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebCadTarefa.Interfaces;
using WebCadTarefa.Dto;
using WebCadTarefa.Domain;

namespace WebCadTarefa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarios _usuarios;
        public UsuariosController(IUsuarios usuarios)
        {
            _usuarios = usuarios;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca de usuários por id ")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var retorno = await _usuarios.GetAsync(id);
            return retorno != null ? Ok(retorno) : BadRequest();
        }

        [HttpGet("username/{username}")]
        [SwaggerOperation(Summary = "Busca de usuários pelo nome ")]
        public async Task<IActionResult> GetByEmailSenhaAsync(string username) 
        {
            var retorno = await _usuarios.GetByUsernameAsync(username);
            return retorno != null ? Ok(retorno) : BadRequest();
        }

        [HttpGet("")]
        [SwaggerOperation(Summary = "Busca todos os usuários ")]
        public async Task<IActionResult> GetAllAsync()
        {
            var retorno = await _usuarios.GetAllAsync();
            return retorno?.Count > 0 ? Ok(retorno) : BadRequest();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Exclui usuários ")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var retorno = await _usuarios.DeleteAsync(id);

            if (retorno)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Altera usuários ")]
        public async Task<IActionResult> UpdateAsync(int id, UsuariosDTO usuariosRequest)
        {
            Usuarios usuarios = new Usuarios() { ID = id,
                                                 Username = usuariosRequest.Username,
                                                 Senha    = usuariosRequest.Senha,
                                                 Bloquear = usuariosRequest.Bloquear,                                              
            };
            var retorno = await _usuarios.UpdateAsync(usuarios);

            if (retorno)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost()]
        [SwaggerOperation(Summary = "Incluir usuários ")]
        public async Task<IActionResult> PostAsync(UsuariosDTO usuariosRequest)
        {
            Usuarios usuarios = new Usuarios() { 
                     Username = usuariosRequest.Username,
                     Senha    = usuariosRequest.Senha,
                     Bloquear = usuariosRequest.Bloquear,
                                              };
            var retorno = await _usuarios.CreateAsync(usuarios);
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
