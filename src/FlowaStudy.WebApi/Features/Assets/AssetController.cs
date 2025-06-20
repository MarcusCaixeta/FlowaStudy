using AutoMapper;
using FlowaStudy.Application.Assets.CreateAsset;
using FlowaStudy.WebApi.Commom;
using FlowaStudy.WebApi.Features.Assets.CreateAsset;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlowaStudy.WebApi.Features.Assets
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AssetController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateAssetResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsset([FromBody] CreateAssetRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateAssetRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateAssetCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateAssetResponse>
            {
                Success = true,
                Message = "Asset created successfully",
                Data = _mapper.Map<CreateAssetResponse>(response)
            });
        }
    }
}
