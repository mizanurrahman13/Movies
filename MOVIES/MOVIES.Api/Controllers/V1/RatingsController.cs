using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOVIES.Api.Auth;
using MOVIES.Api.Mapping;
using MOVIES.Application.Services;
using MOVIES.Contracts.Requests.V1;
using MOVIES.Contracts.Responses;

namespace MOVIES.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion(1.0)]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [Authorize]
        [HttpPut(ApiEndpoints.V1.Movies.Rate)]
        [ProducesResponseType(typeof(MovieResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RateMovie([FromRoute] Guid id, [FromBody] RateMovieRequest request, CancellationToken token)
        {
            var userId = HttpContext.GetUserId();

            var result = await _ratingService.RateMovieAsync(id, request.Rating, userId!.Value, token);

            return result ? Ok() : NotFound();
        }

        [Authorize]
        [HttpDelete(ApiEndpoints.V1.Movies.DeleteRating)]
        [ProducesResponseType(typeof(MovieResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRating([FromRoute] Guid id, CancellationToken token)
        {
            var userId = HttpContext.GetUserId();
            var result = await _ratingService.DeleteRatingAsync(id, userId!.Value, token);

            return result ? Ok() : NotFound();
        }

        [Authorize]
        [HttpGet(ApiEndpoints.V1.Ratings.GetUserRatings)]
        [ProducesResponseType(typeof(IEnumerable<MovieResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserRatings(CancellationToken token = default)
        {
            var userId = HttpContext.GetUserId();
            var ratings = await _ratingService.GetRatingsForUserAsync(userId!.Value, token);
            var ratingsResponse = ratings.MapToResponse();

            return Ok(ratingsResponse);
        }
    }
}
