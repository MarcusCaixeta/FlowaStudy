using AutoMapper;
using FlowaStudy.Application.AssetTransactions.CreateAssetTransaction;
using FlowaStudy.WebApi.Commom;
using FlowaStudy.WebApi.Features.AssetTransactions.CreateAssetTransaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlowaStudy.WebApi.Features.AssetTransactionTransactions
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetTransactionController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AssetTransactionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateAssetTransactionResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAssetTransaction([FromBody] CreateAssetTransactionRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateAssetTransactionRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateAssetTransactionCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateAssetTransactionResponse>
            {
                Success = true,
                Message = "AssetTransaction created successfully",
                Data = _mapper.Map<CreateAssetTransactionResponse>(response)
            });
        }
    }
}
