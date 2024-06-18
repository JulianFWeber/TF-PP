using TF_PP.Services;
using TF_PP.Services.DTOs;
using TF_PP.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TF_PP.DB.Models;


namespace TF_PP.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProdutoController : ControllerBase
    {
        public readonly ProdutosService _productService;
        public readonly ILogger _logger;

        public ProdutoController(ProdutosService productService, ILogger<ProdutoController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        /// <summary>
        /// Insere novo produto.
        /// </summary>
        /// <param name="desc">A descrição do produto.</param>
        /// <returns>Os detalhes do produto.</returns>
        /// <response code="200">Retorna o JSON com os detalhes do produto.</response>
        /// <response code="500">Erro   interno do servidor.</response>
        [HttpPost]
        public IActionResult Post(ProdutoDTO dto)
        {
            try
            {
                var product = _productService.Post(dto);
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var product = _productService.GetById(id);
                return Ok(product);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtém um produto pela descrição.
        /// </summary>
        /// <param name="desc">A descrição do produto.</param>
        /// <returns>Os detalhes do produto.</returns>
        /// <response code="200">Retorna o JSON com os detalhes do produto.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet("description/{desc}")]
        [ProducesResponseType(typeof(IEnumerable<ProdutoDTO>), 200)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<ProdutoDTO>> GetByDesc(string desc)
        {
            try
            {
                var entity = _productService.GetByDesc(desc);
                return Ok(entity);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, new { error = e.Message });
            }
        }


        /// <summary>
        /// Atualiza um produto.
        /// </summary>
        /// <param name="id">O ID do produto a ser atualizado.</param>
        /// <param name="product">Os dados atualizados do produto.</param>
        /// <returns>Os detalhes do produto atualizado.</returns>
        /// <response code="200">Retorna o JSON com os detalhes do produto atualizado.</response>
        /// <response code="400">Os dados enviados não são válidos.</response>
        /// <response code="404">Produto não encontrado.</response>
        /// <response code="422">Campos obrigatórios não enviados para a atualização.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TbProduct), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Update(int id, ProdutoDTO dto)
        {
            try
            {
                var product = _productService.Put(dto, id);
                return Ok(product);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*[HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _productService.Delete(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
    }
}