using Microsoft.AspNetCore.Mvc;
using MOVIES.Api.Mapping;
using MOVIES.Application.Models;
using MOVIES.Application.Repositories;
using MOVIES.Contracts.Requests;

namespace MOVIES.Api.Controllers;

[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;

    public MoviesController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpPost(ApiEndpoints.Movies.Create)]
    public async Task<IActionResult> Create([FromBody]CreateMovieRequest createMovieRequest)
    {
        Movie movie = createMovieRequest.MapToMovie();

        await _movieRepository.CreateAsync(movie);

        return Created($"/{ApiEndpoints.Movies.Create}/{movie.Id}", movie);
    }
}
