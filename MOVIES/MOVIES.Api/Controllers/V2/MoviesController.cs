using Microsoft.AspNetCore.Mvc;
using MOVIES.Api.Auth;
using MOVIES.Api.Mapping;
using MOVIES.Application.Services;

namespace MOVIES.Api.Controllers.V2;

[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet(ApiEndpoints.V2.Movies.Get)]
    public async Task<IActionResult> Get([FromRoute] string idOrSlug,
        //[FromServices] LinkGenerator linkGenerator,
        CancellationToken token)
    {
        var userId = HttpContext.GetUserId();

        var movie = Guid.TryParse(idOrSlug, out var id)
            ? await _movieService.GetByIdAsync(id, userId, token)
            : await _movieService.GetBySlugAsync(idOrSlug, userId, token);

        if (movie is null)
            return NotFound();

        var response = movie.MapToResponse();

        //var movieObject = new { id = movie.Id };
        //response.Links.Add(new Link
        //{
        //    Href = linkGenerator.GetPathByAction(HttpContext, nameof(Get), values: new { idOrSlug = movie.Id }),
        //    Rel = "self",
        //    Type = "GET"
        //});

        //response.Links.Add(new Link
        //{
        //    Href = linkGenerator.GetPathByAction(HttpContext, nameof(Update), values: movieObject ),
        //    Rel = "self",
        //    Type = "PUT"
        //});

        //response.Links.Add(new Link
        //{
        //    Href = linkGenerator.GetPathByAction(HttpContext, nameof(Delete), values: movieObject),
        //    Rel = "self",
        //    Type = "DELETE"
        //});

        return Ok(response);
    }
}
