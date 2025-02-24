using MOVIES.Application.Models;
using MOVIES.Contracts.Requests;
using MOVIES.Contracts.Responses;

namespace MOVIES.Api.Mapping;

public static class ContractMapping
{
    public static Movie MapToMovie(this CreateMovieRequest request)
    {
        return new Movie
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            YearOfRelease = request.YearOfRelease,
            Genres = request.Genres.ToList(),
        };
    }

    public static Movie MapToMovie(this UpdateMovieRequest request, Guid id)
    {
        return new Movie
        {
            Id = id,
            Title = request.Title,
            YearOfRelease = request.YearOfRelease,
            Genres = request.Genres.ToList(),
        };
    }

    public static MovieResponse MapToResponse(this Movie response)
    {
        return new MovieResponse
        {
            Id = Guid.NewGuid(),
            Title = response.Title,
            YearOfRelease = response.YearOfRelease,
            Genres = response.Genres.ToList(),
        };
    }

    public static MoviesResponse MapToResponse(this IEnumerable<Movie> movies)
    {
        return new MoviesResponse
        {
            Items = movies.Select(MapToResponse)
        };
    }
}
