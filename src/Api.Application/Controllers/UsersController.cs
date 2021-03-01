using System.Net;
using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Api.Domain.Dtos.User;

namespace Api.Application.Controllers
{
    /// <summary>
    /// Controlador de query e commands dos usuários do sistema
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Método responsável para retornar todos os usuários do sistema.
        /// </summary>
        /// <returns>retorna uma lista de usuários</returns>
        [Authorize("Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.GetAll();
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método responsável por retonar apenas um usuário por meio do Código ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>retorna o usuário consultado</returns>
        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}", Name = "GetByID")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var result = await _service.Get(id);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método responsável por cadastrar novos usuários no sistema
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDtoCreate user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.Post(user);

                if (result != null)
                    return Created(new Uri(Url.Link("GetByID", new { id = result.Id })), result);

                return BadRequest("Falha ao gravar");
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método responsável por alterar um usuário existente no sistema
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserDtoUpdate user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.Put(user);

                if (result != null)
                    return Ok(result);

                return BadRequest("Falha ao gravar");
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método responsável por deletar um usuário existente no sistema
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.Delete(id);

                if (result)
                    return Ok("Usuário removido com sucesso.");

                return BadRequest("Falha ao remover o usuário.");
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
