using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using TF_PP.Services.Exceptions;
using TF_PP.Services;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace TF_PP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockLogController : ControllerBase
    {
        private readonly StockLogService _service;
        private readonly ILogger<StockLogController> _logger;

        public StockLogController(StockLogService service, ILogger<StockLogController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Obtém o histórico de logs de estoque para um produto específico.
        /// </summary>
        /// <param name="productId">O ID do produto.</param>
        /// <returns>O histórico de logs de estoque para o produto especificado.</returns>
        /// <response code="200">Retorna o JSON com o histórico de logs de estoque.</response>
        /// <response code="404">Nenhum log encontrado para o produto especificado.</response>
        /// <response code="400">Erro ao processar a solicitação.</response>
        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<object>> GetLogByProductId(int productId)
        {
            try
            {
                var logs = _service.GetLogByProductId(productId);
                if (!logs.Any())
                    return NotFound("No logs found for the specified product");

                return Ok(logs);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}