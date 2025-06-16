using AutoMapper;
using FlowaStudy.Application.FinancialAssets.CreateFinancialAsset;
using FlowaStudy.Application.FinancialAssets.GetAllFinancialAsset;
using FlowaStudy.WebApi.Commom;
using FlowaStudy.WebApi.Features.FinancialAssets.CreateFinancialAsset;
using FlowaStudy.WebApi.Features.FinancialAssets.GetAllFinancialAsset;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlowaStudy.WebApi.Features.FinancialAssets
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialAssetsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FinancialAssetsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateFinancialAssetResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFinancialAsset([FromBody] CreateFinancialAssetRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateFinancialAssetRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateFinancialAssetCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateFinancialAssetResponse>
            {
                Success = true,
                Message = "Financial Asset created successfully",
                Data = _mapper.Map<CreateFinancialAssetResponse>(response)
            });
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetAllFinancialAssetResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllFinancialAsset(CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetAllFinancialAssetCommand(), cancellationToken);
            var response = _mapper.Map<GetAllFinancialAssetResponse>(results);

            return Ok(new ApiResponseWithData<GetAllFinancialAssetResponse>
            {
                Success = true,
                Message = "query successfully",
                Data = _mapper.Map<GetAllFinancialAssetResponse>(response)
            });
        }
    }
}
