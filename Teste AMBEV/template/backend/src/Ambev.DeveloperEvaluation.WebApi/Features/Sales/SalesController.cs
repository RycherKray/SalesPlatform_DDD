using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{    

    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateSaleCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
            {
                Success = true,
                Message = "Venda criada com sucesso",
                Data = _mapper.Map<CreateSaleResponse>(result)
            });
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<List<GetAllSalesResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSales(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllSalesQuery(), cancellationToken);

            return Ok(new ApiResponseWithData<List<GetAllSalesResponse>>
            {
                Success = true,
                Message = "Vendas recuperadas com sucesso",
                Data = _mapper.Map<List<GetAllSalesResponse>>(result)
            });
        }

        [HttpPatch("{id}/cancel")]
        public async Task<IActionResult> CancelSale(Guid id, CancellationToken cancellationToken)
        {
            var command = new CancelSaleCommand { SaleId = id };
            var result = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<CancelSaleResponse>(result);

            return Ok(new ApiResponseWithData<CancelSaleResponse>
            {
                Success = true,
                Message = "Venda cancelada com sucesso",
                Data = _mapper.Map<CancelSaleResponse>(result)
            });
            
        }
    }
}
