using Microsoft.AspNetCore.Mvc;
using MOVIES.Application.Models;
using MOVIES.Application.Repositories;
using MOVIES.Contracts.Requests;

namespace MOVIES.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;

    public MoviesController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateMovieRequest createMovieRequest)
    {
        Movie movie = new Movie 
        {
            Id = Guid.NewGuid(),
            Title = createMovieRequest.Title,
            YearOfRelease = createMovieRequest.YearOfRelease,
            Genres = createMovieRequest.Genres.ToList(),
        };

        await _movieRepository.CreateAsync(movie);

        return Created($"/api/movies/{movie.Id}", movie);
    }
}
