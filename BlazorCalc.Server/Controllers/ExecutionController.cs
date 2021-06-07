using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorCalc.Business.Contracts;
using BlazorCalc.Repository.Contracts;
using BlazorCalc.Server.Parsers;
using BlazorCalc.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlazorCalc.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExecutionController : ControllerBase
    {
        private readonly IBusinessService _executionService;
        private readonly InputParser _inputParser;
        private readonly ILogger<ExecutionController> _logger;
        private readonly OutputParser _outputParser;
        private readonly IRepository _repository;

        public ExecutionController(IBusinessService executionService,
            IRepository repository,
            OutputParser outputParser,
            InputParser inputParser,
            ILogger<ExecutionController> logger
        )
        {
            _executionService = executionService;
            _repository = repository;
            _outputParser = outputParser;
            _inputParser = inputParser;
            _logger = logger;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] Expression expression)
        {
            try
            {
                var parsedInput = _inputParser.Parse(expression.Value);

                var result = _executionService.Calculate(parsedInput);

                var parsedResult =
                    expression.Round ? _outputParser.Parse(result, null) : _outputParser.Parse(result, 2);

                await _repository.Save(expression.Value,
                    parsedResult);

                return Ok(parsedResult);
            }

            catch (Exception ex)
            {
                if (ex is DivideByZeroException
                    || ex is NotImplementedException)
                    if (expression.FailsToHistory)
                        await _repository.Save(expression.Value,
                            ex.Message);

                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var results = await _repository.GetAll();
            return results;
        }
    }
}